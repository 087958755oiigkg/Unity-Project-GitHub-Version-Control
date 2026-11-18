using System.Linq;
using Unity.Cinemachine;
using Unity.Cinemachine.TargetTracking;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RollABall.EditorTools
{
    /// <summary>
    /// 一鍵建立 Roll a Ball 主場景（含 Cinemachine 3 跟隨攝影機）。
    /// 選單：Tools > Roll a Ball > Build Main Scene；或 batchmode 以 -executeMethod 呼叫 BuildFromCommandLine。
    /// 會覆寫 Assets/_Project/Scenes/Main.unity。
    /// </summary>
    public static class RollABallSceneBuilder
    {
        private const string ScenePath = "Assets/_Project/Scenes/Main.unity";
        private const string InputPath = "Assets/_Project/Input/GameInput.inputactions";
        private const string MaterialDir = "Assets/_Project/Art/Materials";

        [MenuItem("Tools/Roll a Ball/Build Main Scene")]
        public static void BuildMenu()
        {
            if (!EditorUtility.DisplayDialog("Build Main Scene", "這會覆寫 " + ScenePath + "，確定？", "建立", "取消")) return;
            Build();
        }

        public static void BuildFromCommandLine()
        {
            Build();
            Debug.Log("[RollABallSceneBuilder] Done: " + ScenePath);
        }

        private static void Build()
        {
            // 先建立新場景：NewScene 會卸載未使用資產，之後才載入參照，避免參照失效。
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            AssetDatabase.ImportAsset(InputPath, ImportAssetOptions.ForceUpdate);
            var move = AssetDatabase.LoadAllAssetsAtPath(InputPath)
                .OfType<InputActionReference>()
                .FirstOrDefault(r => r.action != null && r.action.name == "Move");
            if (move == null) throw new System.Exception("找不到 Gameplay/Move InputActionReference：" + InputPath);

            var groundMat = GetMaterial("Ground", new Color(0.35f, 0.55f, 0.35f));
            var wallMat = GetMaterial("Wall", new Color(0.55f, 0.45f, 0.35f));
            var playerMat = GetMaterial("Player", new Color(0.2f, 0.45f, 0.95f));
            var pickupMat = GetMaterial("Pickup", new Color(1f, 0.8f, 0.1f));

            // Light
            var light = new GameObject("Directional Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.shadows = LightShadows.Soft;
            light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            // Ground + walls
            var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.localScale = new Vector3(2f, 1f, 2f);
            ground.GetComponent<Renderer>().sharedMaterial = groundMat;

            var walls = new GameObject("Walls").transform;
            CreateWall(walls, "Wall North", new Vector3(0f, 0.5f, 10.25f), new Vector3(21f, 1f, 0.5f), wallMat);
            CreateWall(walls, "Wall South", new Vector3(0f, 0.5f, -10.25f), new Vector3(21f, 1f, 0.5f), wallMat);
            CreateWall(walls, "Wall East", new Vector3(10.25f, 0.5f, 0f), new Vector3(0.5f, 1f, 21f), wallMat);
            CreateWall(walls, "Wall West", new Vector3(-10.25f, 0.5f, 0f), new Vector3(0.5f, 1f, 21f), wallMat);

            // Player
            var player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            player.name = "Player";
            player.tag = "Player";
            player.transform.position = new Vector3(0f, 0.5f, 0f);
            player.GetComponent<Renderer>().sharedMaterial = playerMat;
            player.AddComponent<Rigidbody>();
            var ball = player.AddComponent<BallController>();

            // Pickups
            var pickups = new GameObject("Pickups").transform;
            for (int i = 0; i < 8; i++)
            {
                float angle = i * Mathf.PI * 2f / 8f;
                var p = GameObject.CreatePrimitive(PrimitiveType.Cube);
                p.name = "Pickup " + (i + 1);
                p.transform.SetParent(pickups);
                p.transform.position = new Vector3(Mathf.Cos(angle) * 6f, 0.5f, Mathf.Sin(angle) * 6f);
                p.transform.localScale = Vector3.one * 0.5f;
                p.transform.rotation = Quaternion.Euler(45f, 45f, 45f);
                p.GetComponent<BoxCollider>().isTrigger = true;
                p.GetComponent<Renderer>().sharedMaterial = pickupMat;
                p.AddComponent<Pickup>();
            }

            // UI
            var canvasGo = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvasGo.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);

            var scoreText = CreateText(canvasGo.transform, "ScoreText", "Score: 0", 48, TextAnchor.UpperLeft,
                new Vector2(0f, 1f), new Vector2(30f, -30f), new Vector2(500f, 80f));
            var winText = CreateText(canvasGo.transform, "WinText", "You Win!", 120, TextAnchor.MiddleCenter,
                new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(900f, 200f));

            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            eventSystem.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();

            var so = new SerializedObject(ball);
            so.FindProperty("moveAction").objectReferenceValue = move;
            so.FindProperty("scoreText").objectReferenceValue = scoreText;
            so.FindProperty("winText").objectReferenceValue = winText.gameObject;
            so.ApplyModifiedPropertiesWithoutUndo();

            // Main Camera + Cinemachine Brain
            var mainCam = new GameObject("Main Camera", typeof(Camera), typeof(AudioListener), typeof(CinemachineBrain));
            mainCam.tag = "MainCamera";
            mainCam.transform.SetPositionAndRotation(new Vector3(0f, 10f, -10f), Quaternion.Euler(45f, 0f, 0f));

            // Cinemachine Camera following the player
            var vcamGo = new GameObject("CinemachineCamera");
            vcamGo.transform.SetPositionAndRotation(new Vector3(0f, 10f, -10f), Quaternion.Euler(45f, 0f, 0f));
            var vcam = vcamGo.AddComponent<CinemachineCamera>();
            vcam.Target.TrackingTarget = player.transform;
            vcam.Lens.FieldOfView = 60f;

            var follow = vcamGo.AddComponent<CinemachineFollow>();
            follow.FollowOffset = new Vector3(0f, 10f, -10f);
            // 球體會滾動旋轉，使用 World Space 綁定，攝影機才不會跟著球一起轉。
            follow.TrackerSettings.BindingMode = BindingMode.WorldSpace;
            follow.TrackerSettings.PositionDamping = new Vector3(1f, 1f, 1f);

            vcamGo.AddComponent<CinemachineRotationComposer>();

            EditorSceneManager.SaveScene(scene, ScenePath);
            AddSceneToBuild(ScenePath);
            AssetDatabase.SaveAssets();
        }

        private static void CreateWall(Transform parent, string name, Vector3 pos, Vector3 scale, Material mat)
        {
            var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.name = name;
            wall.transform.SetParent(parent);
            wall.transform.position = pos;
            wall.transform.localScale = scale;
            wall.GetComponent<Renderer>().sharedMaterial = mat;
        }

        private static Text CreateText(Transform parent, string name, string content, int size, TextAnchor align,
            Vector2 anchor, Vector2 anchoredPos, Vector2 sizeDelta)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(Text));
            go.transform.SetParent(parent, false);
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = rt.anchorMax = rt.pivot = anchor;
            rt.anchoredPosition = anchoredPos;
            rt.sizeDelta = sizeDelta;
            var text = go.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.text = content;
            text.fontSize = size;
            text.alignment = align;
            text.color = Color.white;
            go.AddComponent<Outline>().effectColor = Color.black;
            return text;
        }

        private static Material GetMaterial(string name, Color color)
        {
            string path = MaterialDir + "/" + name + ".mat";
            var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat == null)
            {
                mat = new Material(Shader.Find("Standard"));
                AssetDatabase.CreateAsset(mat, path);
            }
            mat.color = color;
            EditorUtility.SetDirty(mat);
            return mat;
        }

        private static void AddSceneToBuild(string path)
        {
            var scenes = EditorBuildSettings.scenes.ToList();
            if (scenes.Any(s => s.path == path)) return;
            scenes.Insert(0, new EditorBuildSettingsScene(path, true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
