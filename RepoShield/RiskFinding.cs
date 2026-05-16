namespace RepoShield
{
    public class RiskFinding
    {
        public string FilePath { get; set; } = "";
        public string Issue { get; set; } = "";
        public string Reason { get; set; } = "";
        public string Severity { get; set; } = "";
        public string MatchedText { get; set; } = "";

        public override string ToString()
        {
            return $"[{Severity}] {Issue} - {FilePath}";
        }
    }
}