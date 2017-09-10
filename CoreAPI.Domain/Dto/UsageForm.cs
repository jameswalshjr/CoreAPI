using System;
using System.Collections.Generic;
using System.Text;

namespace CoreAPI.Domain.Dto
{
    public class UsageForm
    {
        public int Client { get; set; }
        public int Org { get; set; }
        public int Qty { get; set; }
        public string LIC { get; set; }
        public int Seq { get; set; }
    }

    public class UsageList
    {
        List<UsageForm> UsageFormList { get; set; }
    }
}
