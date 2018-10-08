namespace DNSPod4NETCore2.Cli
{
    public class CommandLineArgs
    {
        public string Token { get; set; }
        public string DomainName { get; set; }
        public int RecordId { get; set; }
        public string Value { get; set; }
        public string SubDomain { get; set; }
        public string RecordType { get; set; }
    }
}
