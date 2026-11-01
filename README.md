# Unity 期中考：版本控制與自主學習

## 專案資訊

- 專案名稱：`Unity6000-AI-Template`
- Unity 版本：**6000.3.17f1**（依 `ProjectSettings/ProjectVersion.txt` 確認）
- 使用範本：課堂 Unity AI Project Template v0.2。
- Git 工具：Git for Windows 2.51.0（PowerShell 命令列），由 AI 協助執行與檢查。
- 目標：將空的 Unity 專案與學習筆記上傳到自己的公開 GitHub Repository。
- Repository 網址：**待填入自己的 Repository 網址，目前尚未 Push。**

## Step-by-step 操作紀錄

以下區分已完成的操作與待完成步驟，未完成項目不代表已成功上傳。

### 1. 準備 Unity 專案（本機檔案檢查完成）

使用現有的課堂範本專案，確認根目錄包含 `Assets/`、`Packages/`、`ProjectSettings/` 與 `.gitignore`，版本檔為 `6000.3.17f1`。本次不新增遊戲程式、場景或功能；尚未在 Editor 內驗證開啟結果。

檢查 `.gitignore` 後，確認 `Library/`、`Temp/`、`Obj/`、`Logs/`、`UserSettings/` 與 `Builds/` 都會被排除，保留 Unity 需要的專案檔案與既有 `.meta`。

### 2. 建立本機版本紀錄並 Commit（已完成）

在專案根目錄使用下列命令建立 `main` 分支、加入檔案並提交：

```powershell
git init -b main
git add -- .
git commit -m "建立 Unity 6000.3.17f1 空白專案版本紀錄"
git log --oneline
```

首次 Commit：`21c5bcc`。提交前已確認快取資料夾不在追蹤清單內。這些本機操作由 AI 協助完成，需自行閱讀與理解命令用途。

### 3. 連接 GitHub（待完成）

接下來使用自己的 GitHub 帳號建立 **Public** Repository。因為本機已有專案與 README，建立遠端時不另外初始化 README、`.gitignore` 或 License，以免產生另一份初始歷史。

取得真正的 Repository 網址後，再將它設定為 `origin`：

```powershell
# 先將下方範例中的 YOUR-USERNAME 與 YOUR-REPOSITORY 換成自己的資料。
git remote add origin https://github.com/YOUR-USERNAME/YOUR-REPOSITORY.git
git remote -v
```

登入時使用 Git 工具提供的 GitHub 授權流程，不把密碼或 Access Token 寫進檔案、命令或遠端網址。

### 4. Push 到 GitHub（待完成）

確認 `origin` 指向自己的 Repository，且最新 README 已 Commit 後，執行：

```powershell
git push -u origin main
```

必須看到 Push 成功的結果，才能把這一步改為已完成；Commit 成功本身不代表已上傳。

### 5. 確認成功上傳（待完成）

- 在 GitHub 的 `main` 分支檢查 `Assets/`、`Packages/`、`ProjectSettings/`、`.gitignore` 與 `README.md`。
- 查看 Commits 頁面，確認能看到本機提交的紀錄。
- 確認沒有 `Library/`、`Temp/`、`Obj/` 等快取資料夾。
- 用瀏覽器無痕視窗開啟 Repository 網址，確認不需登入即可查看。
- 將本文件的 Repository 網址與操作結果補齊、Commit 並再次 Push，最後繳交 Repository 網址。

## 學習心得（待本人完成）

下列是 AI 提供的概念說明草稿，**尚未代表我自己的學習心得**。繳交前需理解後，用自己的話改寫，並補上實際操作遇到的問題與解決方法。

- **Commit**：把選進暫存區的變更保存成一筆本機版本紀錄，附上說明，方便之後比較修改或找回舊版本。沒有先 `git add` 的修改不會自動收進一般的 Commit。
- **Push**：把本機分支上的 Commit 傳到 GitHub 等遠端儲存庫，讓老師或其他人能看到已提交的內容。只修改檔案、沒有 Commit 的內容不會隨 Push 上傳。
- **Pull**：取得遠端分支的更新，並整合到目前的本機分支，讓本機跟上遠端的變更；遇到衝突時需要處理。本次尚未執行 Pull。
- **我的實際心得與問題排除**：待本人填寫。

## 交件檢查

- [x] 版本檔記錄為 Unity 6000.3.17f1。
- [x] 已追蹤必要的專案資料夾與 `.gitignore`。
- [x] 至少完成一次本機 Commit。
- [ ] README 心得已由本人理解並改寫。
- [ ] Repository 屬於自己的帳號，且設定為 Public。
- [ ] 已成功 Push，GitHub 上能看到專案與最新版 README。
- [ ] 已使用無痕視窗確認免登入可查看。
- [ ] 已填入並繳交自己的 Repository 網址。

---

## 原始範本說明（保留供參考）

下方是課堂範本原始說明，其中「尚未初始化」描述的是原始骨架；目前本機已存在 Unity 產生的 Packages 與 ProjectSettings，詳細檢查結果見 [專案現況](docs/PROJECT_STATUS.md)。範本中的遊戲示範與發布驗證不屬於本次期中考要求。

適用 Editor：**Unity 6000.3.17f1**。文件使用繁體中文，輸入統一使用 **Input System Package (New)**。

這是資料夾與協作文件骨架，尚不是能直接執行的完整 Unity 專案，未包含遊戲程式、場景與 Unity 產生的設定。請先依 [初始化指南](docs/02_初始化與InputSystem.md) 建立真正的專案，再合併本骨架。

## 閱讀順序

1. [資料夾與文件說明](docs/01_資料夾與文件說明.md)
2. [初始化與 Input System](docs/02_初始化與InputSystem.md)
3. [AI 協作流程](docs/03_AI協作流程.md)
4. [教學路線與發布檢查](docs/04_教學與發布檢查.md)

AI 開始工作前須讀取根目錄 `AGENTS.md`；不會自動讀取的工具，請使用 `docs/05_AI啟動提示詞.md`。

建議 Repo 名稱：`Unity6-AI-Teaching-Template`。建立 GitHub 公開 Repository 後，在 Settings 勾選 Template repository，讓同學以 Use this template 建立各自專案。正式開放前，先完成 Unity 初始化與 Editor 驗證。

## 授權

尚未選定授權。建議自有程式與文件採 MIT；公開前由維護者加入正式 LICENSE。第三方素材須各自確認可否再散布，不能直接套用專案授權。

5. [Agent 相容性與解壓縮指南](docs/06_Agent相容性與解壓縮指南.md)

已提供 Codex、Kiro、Antigravity 與 Claude Code 入口，以及 .gitignore。請完整解壓縮，包含以點開頭的檔案與資料夾。

## 課程資產架構 v0.2

3D 資產以 `Assets/_Project/Art/Models/<模型名稱>/` 為單位，FBX、專屬貼圖／材質直接同層存放；可共用的 Material 放在 `Art/Materials/`。2D Sprite 與逐格動畫依角色放在 `Art/Sprites/<角色名稱>/`。詳見 `docs/01_資料夾與文件說明.md`。本 ZIP 不包含實際 Cat 模型或 Roll a Ball 程式碼。
