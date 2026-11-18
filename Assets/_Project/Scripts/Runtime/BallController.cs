using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RollABall
{
    /// <summary>
    /// 玩家球體：讀取 Gameplay/Move 輸入施加推力，並管理分數與勝利文字。
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : MonoBehaviour
    {
        [Tooltip("指派 GameInput.inputactions 裡的 Gameplay/Move。")]
        [SerializeField] private InputActionReference moveAction;

        [Tooltip("推力大小，預設 10。")]
        [SerializeField] private float speed = 10f;

        [Tooltip("顯示分數的 UI Text。")]
        [SerializeField] private Text scoreText;

        [Tooltip("收集完所有 Pickup 後顯示的物件。")]
        [SerializeField] private GameObject winText;

        private Rigidbody rb;
        private Vector2 moveInput;
        private int score;
        private int totalPickups;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            if (moveAction == null) return;
            moveAction.action.performed += OnMove;
            moveAction.action.canceled += OnMove;
            moveAction.action.Enable();
        }

        private void OnDisable()
        {
            if (moveAction == null) return;
            moveAction.action.performed -= OnMove;
            moveAction.action.canceled -= OnMove;
            moveAction.action.Disable();
            moveInput = Vector2.zero;
        }

        private void Start()
        {
            totalPickups = FindObjectsByType<Pickup>(FindObjectsSortMode.None).Length;
            UpdateScore();
            if (winText != null) winText.SetActive(false);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            rb.AddForce(new Vector3(moveInput.x, 0f, moveInput.y) * speed);
        }

        public void CollectPickup()
        {
            score++;
            UpdateScore();
            if (score >= totalPickups && winText != null)
            {
                winText.SetActive(true);
            }
        }

        private void UpdateScore()
        {
            if (scoreText != null) scoreText.text = "Score: " + score;
        }
    }
}
