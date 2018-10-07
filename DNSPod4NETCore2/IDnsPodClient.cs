namespace DNSPod4NETCore2
{
    public interface IDnsPodClient
    {
        string Token { get; set; }
        dynamic PostApiRequest(string address, object param = null);
    }
}
