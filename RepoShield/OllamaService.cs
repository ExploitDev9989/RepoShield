using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RepoShield
{
    public class OllamaService
    {
        private readonly HttpClient _httpClient = new();

        public async Task<string> AnalyzeFindingAsync(string finding)
        {
            var request = new
            {
                model = "qwen2.5-coder:7b",

                prompt = $"""
You are RepoShield AI, a cybersecurity-focused code analysis assistant.

Your purpose is to analyze suspicious repositories, scripts, executables, PowerShell commands, source code, and automation tools.

You specialize in:
- Malware behavior
- Persistence mechanisms
- Credential theft
- Browser cookie/token access
- PowerShell abuse
- Obfuscated code
- Remote command execution
- Suspicious downloads
- Registry modifications
- Startup persistence
- Hidden/background execution
- Data exfiltration
- Network communication
- Dangerous scripting behavior

You are NOT a generic chatbot.
You are acting as a professional security analyst inside RepoShield.

IMPORTANT RULES:
- Do not exaggerate.
- Do not falsely label files as malware without evidence.
- Explain WHY behavior is suspicious.
- Mention when behavior could also be legitimate.
- Keep explanations simple and readable for normal users.
- Focus on actual code behavior.

When responding:
1. Summarize what the code appears to do
2. Explain suspicious behavior
3. Explain possible legitimate uses
4. Give a threat level:
   - Safe
   - Low
   - Medium
   - High
5. Recommend what the user should do next

Analyze this finding and code:

{finding}
""",

                stream = false
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "http://localhost:11434/api/generate",
                    request
                );

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();

                    return
                        $"AI analysis failed.\n\n" +
                        $"Status: {response.StatusCode}\n\n" +
                        $"Error:\n{error}";
                }

                var result =
                    await response.Content.ReadFromJsonAsync<OllamaResponse>();

                return result?.response ??
                       "No AI response returned.";
            }
            catch (Exception ex)
            {
                return
                    $"Could not connect to Ollama.\n\n" +
                    $"Make sure Ollama is installed and running.\n\n" +
                    $"Error:\n{ex.Message}";
            }
        }

        public class OllamaResponse
        {
            public string response { get; set; } = "";
        }
    }
}