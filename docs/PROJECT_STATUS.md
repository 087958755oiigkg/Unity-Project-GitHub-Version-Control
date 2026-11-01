# 專案現況

- 更新日期：2026-11-01。
- 本次任務：期中考版本控制與自主學習，只上傳空的 Unity 專案及 README，不新增遊戲功能。
- 階段：已完成 Commit 與 Push；自己的 Public Repository 已包含必要專案檔案，匿名讀取驗證通過。
- 分支：`main`。
- 首次 Commit：`21c5bcc`（建立 Unity 6000.3.17f1 空白專案版本紀錄）；後續文件提交可用 `git log --oneline` 查閱。
- Unity 版本：`ProjectSettings/ProjectVersion.txt` 為 `6000.3.17f1`，本機也存在對應 Editor 安裝目錄。
- 實際專案：已有 `Assets/`、`Packages/manifest.json`、`Packages/packages-lock.json` 與 Unity 產生的 `ProjectSettings/`，不是只有文件骨架。
- 已有遊戲程式、場景、Prefab：無，維持空專案。
- 套件與輸入現況：manifest 與 lock 均未包含 `com.unity.inputsystem`；`activeInputHandler: 0` 為舊模式，尚未符合共用輸入規範。本次只處理交件文件與 Git，未變更套件或輸入設定；若日後開發遊戲，須先依初始化指南在 Editor 安裝相容 Input System 並改成 New，不可沿用舊輸入 API。
- 本次文件修改：README 依本人要求精簡為「專案資訊」、「Step-by-step 操作紀錄」、「學習心得」三個部分，使用簡單口語，移除原始範本說明與過期的待完成標記；本文件同步更新驗證結果。
- Inspector 設定：本次未新增欄位或元件，無需指派。
- 靜態驗證：必要檔案已納入 Git；快取資料夾未被追蹤，`git check-ignore` 確認 Library、Temp、Obj、Logs、UserSettings、Builds 與常見本機秘密設定會被排除；已追蹤檔案未找到常見 GitHub Token、AWS Access Key 或私鑰格式，這項掃描不保證涵蓋所有秘密。
- 編譯 / Play Mode / Build：本次未執行，**待 Editor 驗證**；不宣稱通過，亦不為本次考試增加遊戲測試或 Build 工作。
- README 心得：以本人提供的 Commit、Push、Pull 理解為基礎，簡單補充各自的用途。
- GitHub 帳號：`087958755oiigkg`；Repository 名稱：`Unity-Project-GitHub-Version-Control`。
- 遠端網址：https://github.com/087958755oiigkg/Unity-Project-GitHub-Version-Control 。
- 登入現況：本人已完成 Git Credential Manager 授權，首次 Push 成功建立遠端 `main` 並設定追蹤。
- 問題排除：第一次 Push 發生本機 CA 憑證信任錯誤；改用 Windows 憑證存放區（本機 Git 設定 `http.sslBackend=schannel`）後成功，全程保留 HTTPS 憑證驗證。
- 公開存取驗證：以不帶登入資訊的 HTTP 請求取得 Repository 頁面，回應為 HTTP 200；GitHub API 回報 `private: false`，擁有者為本人帳號；停用 credential helper 的 `git ls-remote` 可讀取遠端 `main`。
- 遠端檔案驗證：GitHub API 確認根目錄包含 Assets、Packages、ProjectSettings、.gitignore、README.md，且未包含 Library、Temp、Obj、Logs、UserSettings、Builds。
- 無痕視窗驗證：未完成。原生瀏覽器工具再次因無法可靠辨識目前網址而停止；已完成匿名 HTTP/API/Git 公開讀取檢查，但不將它冒稱為實際無痕視窗操作。
- 下一步：本人用瀏覽器無痕視窗開啟遠端網址確認後，將 Repository 網址貼到課程平台繳交。平台繳交未由 AI 執行。

每次交接更新：任務、分支、commit、已完成、驗證證據、問題、下一步。不要在公開文件記錄秘密或個資。
