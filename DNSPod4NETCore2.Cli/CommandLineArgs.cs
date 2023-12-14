namespace DNSPod4NETCore2.Cli
{
    public class CommandLineArgs
    {
        public string SecretId { get; set; }
        public string SecretKey { get; set; }
        public string Domain { get; set; }
        public int RecordId { get; set; }
        public string Value { get; set; }
        public string SubDomain { get; set; }
        public string RecordType { get; set; }
        public string RecordLine { get; set; }
    }
}
