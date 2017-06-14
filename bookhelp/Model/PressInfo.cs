using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys.Common.Business.Data;
using System.ComponentModel;

namespace bookhelp.Model
{
    [TableName("PressInfo")]
    [Description("出版社")]

    public class PressInfo
    {        
        public int ID { get; set; }
        public string PressName { get; set; }
        public string PressAddress { get; set; }
        public string PressPhone { get; set; }
       
        public override string ToString()
        {
            return Sys.Common.Business.JSONhelper.ToJson(this);
        }

    }
}
