using System.Collections.Generic;

namespace DNSPod4NETCore2.Models
{
    public class DnsPodRecordList
    {
        public Status status { get; set; }
        public Domain domain { get; set; }
        public Info info { get; set; }
        public List<Record> records { get; set; }
    }

    public class Status
    {
        public string code { get; set; }
        public string message { get; set; }
        public string created_at { get; set; }
    }

    public class Domain
    {
        public int id { get; set; }
        public string name { get; set; }
        public string punycode { get; set; }
        public string grade { get; set; }
        public string owner { get; set; }
        public string ext_status { get; set; }
        public int ttl { get; set; }
    }

    public class Info
    {
        public string sub_domains { get; set; }
        public string record_total { get; set; }
        public string records_num { get; set; }
    }

    public class Record
    {
        public string id { get; set; }
        public string name { get; set; }
        public string line { get; set; }
        public string line_id { get; set; }
        public string type { get; set; }
        public string ttl { get; set; }
        public string value { get; set; }
        public object weight { get; set; }
        public string mx { get; set; }
        public string enabled { get; set; }
        public string status { get; set; }
        public string monitor_status { get; set; }
        public string remark { get; set; }
        public string updated_on { get; set; }
        public string use_aqb { get; set; }
    }

}
