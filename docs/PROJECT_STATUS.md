# 專案現況

- 更新日期：2026-11-01。
- 本次任務：期中考版本控制與自主學習，只上傳空的 Unity 專案及 README，不新增遊戲功能。
- 階段：本機 Git 已建立，已完成首次 Commit；GitHub 上傳與公開存取驗證尚未完成。
- 分支：`main`。
- 首次 Commit：`21c5bcc`（建立 Unity 6000.3.17f1 空白專案版本紀錄）；後續文件提交可用 `git log --oneline` 查閱。
- Unity 版本：`ProjectSettings/ProjectVersion.txt` 為 `6000.3.17f1`，本機也存在對應 Editor 安裝目錄。
- 實際專案：已有 `Assets/`、`Packages/manifest.json`、`Packages/packages-lock.json` 與 Unity 產生的 `ProjectSettings/`，不是只有文件骨架。
- 已有遊戲程式、場景、Prefab：無，維持空專案。
- 套件與輸入現況：manifest 與 lock 均未包含 `com.unity.inputsystem`；`activeInputHandler: 0` 為舊模式，尚未符合共用輸入規範。本次只處理交件文件與 Git，未變更套件或輸入設定；若日後開發遊戲，須先依初始化指南在 Editor 安裝相容 Input System 並改成 New，不可沿用舊輸入 API。
- 本次文件修改：README 加入專案資訊、實際 Commit 紀錄、待完成的 GitHub 操作與概念草稿；保留原始範本說明。
- Inspector 設定：本次未新增欄位或元件，無需指派。
- 靜態驗證：必要檔案已納入 Git；快取資料夾未被追蹤，`git check-ignore` 確認 Library、Temp、Obj、Logs、UserSettings、Builds 與常見本機秘密設定會被排除；已追蹤檔案未找到常見 GitHub Token、AWS Access Key 或私鑰格式，這項掃描不保證涵蓋所有秘密。
- 編譯 / Play Mode / Build：本次未執行，**待 Editor 驗證**；不宣稱通過，亦不為本次考試增加遊戲測試或 Build 工作。
- README 心得：目前是明確標示的 AI 概念草稿，待本人理解與改寫，不冒充個人心得。
- 尚缺資料：自己的 GitHub Repository 網址，以及 GitHub 登入授權；Git Credential Manager 目前沒有列出 GitHub 帳號。
- 下一步：建立自己的 Public Repository、設定 origin、Push，確認遠端必要檔案，再以瀏覽器無痕模式驗證免登入可查看。依實際結果更新 README 後再次 Commit / Push，最後繳交 Repository 網址。

每次交接更新：任務、分支、commit、已完成、驗證證據、問題、下一步。不要在公開文件記錄秘密或個資。
