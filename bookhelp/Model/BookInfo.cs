using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys.Common.Business.Data;
using System.ComponentModel;

namespace bookhelp.Model
{
    [TableName("BookInfo")]
    [Description("书籍")]

    public class BookInfo
    {        
        public int ID { get; set; }       
        public string BookName { get; set; }        
        public string BookSNCode { get; set; }        
        public decimal? BookPrice { get; set; }
        public int PressID { get; set; }
        public override string ToString()
        {
            return Sys.Common.Business.JSONhelper.ToJson(this);
        }

    }
}
