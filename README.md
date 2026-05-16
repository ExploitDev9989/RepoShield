# 🛡️ REPOSHIELD

### AI Powered Repository & Script Threat Scanner

Detect suspicious scripts, risky repositories, dangerous PowerShell behavior, and potentially malicious code using rule based scanning combined with local AI analysis.

---

![RepoShield Banner](images/banner.png)

---

# ⚡ FEATURES

✅ Recursive folder and repository scanning  
✅ Suspicious file detection  
✅ PowerShell abuse detection  
✅ Encoded command detection  
✅ Registry and persistence detection  
✅ Browser token and cookie reference detection  
✅ Local AI powered code analysis  
✅ Hacker inspired WPF interface  
✅ Offline AI analysis using Ollama  
✅ No cloud AI required  

---

# 🧠 AI ANALYSIS

RepoShield uses:

## 🔥 Ollama + qwen2.5-coder:7b

The AI scanner can explain:

✅ What the code appears to do  
✅ Why behavior may be suspicious  
✅ Whether the behavior could be legitimate  
✅ Threat level assessment  
✅ Recommended next steps  

---

# 🖼️ SCREENSHOTS

## Main Scanner UI
![Scanner UI](images/scanner-ui.png)

## AI Threat Analysis
![AI Analysis](images/ai-analysis.png)

## Suspicious Findings
![Threat Findings](images/threat-findings.png)

---

# 🚀 INSTALLATION

## Download RepoShield

Download the latest release from the Releases section.

Extract the ZIP and run:

```text
RepoShield.exe
```

---

# 🤖 INSTALL AI SUPPORT

RepoShield uses Ollama for local AI analysis.

Official Ollama sources:

🌐 https://ollama.com/  
📖 https://docs.ollama.com/windows  
💻 https://github.com/ollama/ollama  

---

## Manual Install

Open PowerShell and run:

```powershell
irm https://ollama.com/install.ps1 | iex
ollama pull qwen2.5-coder:7b
```

Test it:

```powershell
ollama run qwen2.5-coder:7b
```

---

## Included Install Script

RepoShield includes:

```text
install-ai.ps1
```

Before running it, open the script and verify that it matches the official Ollama commands shown above.

Run:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
.\install-ai.ps1
```

---

# 🛡️ WHAT REPOSHIELD DETECTS

| Detection Type | Examples |
|---|---|
| Remote Downloads | Invoke WebRequest, curl, wget |
| PowerShell Abuse | EncodedCommand, hidden execution |
| Persistence | RunOnce, schtasks, startup |
| Credential Theft | Cookies, Login Data, Discord tokens |
| Obfuscation | Base64 decoding, encoded scripts |
| Dangerous Scripts | .bat, .cmd, .ps1, .js, .py |

---

# 🔍 HOW IT WORKS

```text
Scan Folder or Repository
        ↓
Detect Suspicious Files
        ↓
Analyze Script Contents
        ↓
Flag Dangerous Behavior
        ↓
Optional AI Analysis
        ↓
User Reviews Findings
```

---

# 🧠 LOCAL AI PRIVACY

RepoShield AI analysis runs locally through Ollama.

Scanned code is not uploaded to online AI services by RepoShield.

AI requests are sent locally to:

```text
http://localhost:11434
```

---

# ⚠️ DISCLAIMER

RepoShield is a defensive learning and analysis tool.

It may flag suspicious behavior, but it cannot guarantee that a file is safe or malicious.

Always review unknown files carefully and avoid running untrusted code.

---

# 🧰 TECH STACK

C#  
WPF  
.NET  
Ollama  
qwen2.5-coder:7b  
Recursive filesystem scanning  
Rule based threat heuristics  

---

# 🛠️ BUILDING FROM SOURCE

Clone the repository:

```powershell
git clone https://github.com/YOUR_USERNAME/RepoShield.git
cd RepoShield
```

Open the solution in Visual Studio.

Recommended publish settings:

```text
Self contained
win x64
Single file publish
```

---

# 📂 RECOMMENDED .gitignore

```gitignore
bin/
obj/
.vs/
*.user
*.suo
*.log
```

---

# ⭐ FUTURE FEATURES

VirusTotal integration  
GitHub repository cloning  
EXE hash analysis  
AI powered risk scoring  
Severity color coding  
Obfuscation detection  
Sandboxed analysis  
Exportable reports  
Live process scanning  

---

# 👨‍💻 DEVELOPER

Built by Miguel Quiroz

---

# ⭐ SUPPORT THE PROJECT

If you like RepoShield:

⭐ Star the repo  
🐛 Report issues  
🔧 Contribute improvements  
🛡️ Help improve defensive tooling  

---
