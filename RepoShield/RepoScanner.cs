using System.IO;

namespace RepoShield
{
    public class RepoScanner
    {
        public ScanResult ScanRepo(string input)
        {
            ScanResult result = new ScanResult();

            string targetFolder = input;

            // Check if folder exists
            if (!Directory.Exists(targetFolder))
            {
                result.RiskScore = 0;
                result.Report = "Folder not found.";

                return result;
            }

            string[] files = Directory.GetFiles(
                targetFolder,
                "*.*",
                SearchOption.AllDirectories);

            foreach (string file in files)
            {
                 string extension = Path.GetExtension(file).ToLower();
                { 

                    if (extension == ".exe" ||
                        extension == ".ps1" ||
                        extension == ".bat" ||
                        extension == ".cmd")
                    {
                        result.Findings.Add(new RiskFinding
                        {
                            FilePath = file,
                            Severity = "High",
                            Reason = $"Suspicious file type detected: {extension}"
                        });

                        result.RiskScore += 25;
                    }
                    else if (extension == ".py" ||
                             extension == ".js")
                    {
                        result.Findings.Add(new RiskFinding
                        {
                            FilePath = file,
                            Severity = "Medium",
                            Reason = $"Script file detected: {extension}"
                        });

                        result.RiskScore += 10;
                    }

                    if (extension == ".ps1" ||
                        extension == ".bat" ||
                        extension == ".cmd" ||
                        extension == ".py" ||
                        extension == ".js" ||
                        extension == ".vbs")
                    {
                        ScanFileContents(file, result);
                    }
                }
            
            }

            // Cap score at 100
            if (result.RiskScore > 100)
            {
                result.RiskScore = 100;
            }

            result.Report =
    $"Scan complete.\n\n" +
    $"Files scanned: {files.Length}\n" +
    $"Threats detected: {result.Findings.Count}\n\n";

            foreach (RiskFinding finding in result.Findings)
            {
                result.Report +=
                    $"[{finding.Severity}] " +
                    $"{finding.Reason}\n" +
                    $"{finding.FilePath}\n\n";
            }

            return result;
        }
        private void ScanFileContents(string file, ScanResult result)
        {
            try
            {
                string content = File.ReadAllText(file).ToLower();

                CheckKeyword(content, file, result, "invoke-webrequest", "High", "Downloads content from the internet", 20);
                CheckKeyword(content, file, result, "downloadstring", "High", "Downloads and runs remote code", 25);
                CheckKeyword(content, file, result, "encodedcommand", "High", "Uses encoded PowerShell command", 30);
                CheckKeyword(content, file, result, "frombase64string", "High", "Decodes Base64 content", 25);

                CheckKeyword(content, file, result, "appdata", "Medium", "Accesses AppData folder", 10);
                CheckKeyword(content, file, result, "localappdata", "Medium", "Accesses LocalAppData folder", 10);
                CheckKeyword(content, file, result, "cookies", "High", "References browser cookies", 30);
                CheckKeyword(content, file, result, "login data", "High", "References browser saved login database", 35);
                CheckKeyword(content, file, result, "discord", "High", "References Discord files or tokens", 30);
                CheckKeyword(content, file, result, "webhook", "High", "References webhook communication", 25);

                CheckKeyword(content, file, result, "schtasks", "High", "Creates or modifies scheduled tasks", 25);
                CheckKeyword(content, file, result, "reg add", "High", "Modifies Windows registry", 25);
                CheckKeyword(content, file, result, "runonce", "High", "References RunOnce persistence", 30);
                CheckKeyword(content, file, result, "startup", "Medium", "References startup behavior", 15);
            }
            catch
            {
                result.Findings.Add(new RiskFinding
                {
                    FilePath = file,
                    Severity = "Info",
                    Reason = "Could not read file contents"
                });
            }
        }
        private void CheckKeyword(
    string content,
    string file,
    ScanResult result,
    string keyword,
    string severity,
    string reason,
    int score)
        {
            if (content.Contains(keyword))
            {
                result.Findings.Add(new RiskFinding
                {
                    FilePath = file,
                    Severity = severity,
                    Reason = reason
                });

                result.RiskScore += score;
            }
        }
    }
}