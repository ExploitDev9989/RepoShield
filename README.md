# RepoShield

RepoShield is a Windows desktop security tool built with C# and WPF. It scans local folders and downloaded repositories for suspicious files, risky scripts, and potentially dangerous code patterns. It can also use a local AI model through Ollama to explain suspicious findings in plain English.

## Features

- Recursive folder scanning
- Detects risky file types like `.exe`, `.bat`, `.cmd`, `.ps1`, `.js`, and `.py`
- Scans readable scripts for suspicious keywords
- Flags behavior such as:
  - Remote downloads
  - PowerShell abuse
  - Base64/encoded commands
  - Discord/browser token references
  - Startup persistence
  - Registry changes
  - Scheduled tasks
- Local AI analysis using Ollama
- No cloud AI required
- Files are analyzed locally unless optional online scanning is added later

## Requirements

- Windows 10 or Windows 11
- RepoShield.exe
- Optional for AI Scan:
  - Ollama
  - qwen2.5-coder:7b model

## How to Run RepoShield

1. Download the latest release.
2. Extract the ZIP.
3. Run `RepoShield.exe`.
4. Click **Browse** to choose a folder.
5. Click **Scan**.
6. Select a finding and use **AI Scan** if Ollama is installed.

## Install AI Support

RepoShield uses Ollama to run the AI model locally on your PC.

Official Ollama links:

- Official website: https://ollama.com/
- Official Windows docs: https://docs.ollama.com/windows
- Official GitHub repo: https://github.com/ollama/ollama

### Option 1: Manual Install

Open PowerShell and run:

```powershell
irm https://ollama.com/install.ps1 | iex
ollama pull qwen2.5-coder:7b
