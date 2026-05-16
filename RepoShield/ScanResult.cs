using System.Collections.Generic;

namespace RepoShield
{
    public class ScanResult
    {
        public int RiskScore { get; set; }
        public string? Report { get; set; }
        public List<RiskFinding> Findings { get; set; } = new();
    }
}