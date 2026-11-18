# 專案現況

- 更新日期：2026-11-18。
- 本次任務：Homework 10 — Clone 專案並加入 Roll a Ball 遊戲與 Cinemachine 3 跟隨攝影機。
- 分支：`main`。
- Unity 版本：`6000.3.17f1`（未升級）。

## 本次修改

| 檔案 | 目的 |
| --- | --- |
| `Packages/manifest.json`、`packages-lock.json` | 新增 `com.unity.cinemachine` 3.1.7、`com.unity.inputsystem` 1.19.0（此 Editor 內建版本）、`com.unity.ugui` 2.0.0 |
| `ProjectSettings/ProjectSettings.asset` | `activeInputHandler: 1`（Input System Package (New)） |
| `Assets/_Project/Input/GameInput.inputactions` | Gameplay/Move：WASD、方向鍵、手把左搖桿 |
| `Assets/_Project/Scripts/Runtime/BallController.cs` | 由舊專案移植；改用 InputActionReference，Enable/Disable 與事件訂閱成對；分數與 You Win |
| `Assets/_Project/Scripts/Runtime/Pickup.cs` | 旋轉、碰觸加分後隱藏 |
| `Assets/_Project/Scripts/Editor/RollABallSceneBuilder.cs` | `Tools > Roll a Ball > Build Main Scene`，以 Editor API 產生場景 |
| `Assets/_Project/Scenes/Main.unity` | 由上述工具在 batchmode 產生，已加入 Build Profiles 場景清單 |

## 場景與 Inspector 設定

- Player：Sphere + Rigidbody + BallController（moveAction = Gameplay/Move、speed = 10、scoreText、winText 已指派）。
- Pickups：8 個 Trigger Cube + Pickup；四面牆圍住 20×20 地面。
- Main Camera：Camera + CinemachineBrain。
- CinemachineCamera：Tracking Target = Player；Position Control = CinemachineFollow（Follow Offset (0, 10, -10)、Binding Mode = World Space，避免球滾動時鏡頭跟著旋轉、Damping 1）；Rotation Control = CinemachineRotationComposer。
- UI：Canvas（ScoreText、WinText，使用內建 LegacyRuntime 字型），EventSystem 使用 InputSystemUIInputModule。

## 驗證

- batchmode 執行 `RollABallSceneBuilder.BuildFromCommandLine`：套件解析成功、無編譯錯誤、場景儲存成功。
- 場景檔確認 moveAction、TrackingTarget、BindingMode 已正確序列化。
- Play Mode（WASD 移動、攝影機跟隨、Score / You Win）：**待 Editor 驗證**。

## 下一步

1. Unity Hub 開啟本專案 → 開啟 `Assets/_Project/Scenes/Main.unity` → Play 測試並錄影。
2. Rider Push 到 GitHub，影片上傳 YouTube（公開或不公開列出）。
