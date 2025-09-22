namespace fox_internet_security.Models
{
    internal class ScanModel
    {
        public string FilePath { get; set; }
        public bool IsInfected { get; set; }
        public string VirusName { get; set; }
        public long FileSize { get; set; }
    }

}
