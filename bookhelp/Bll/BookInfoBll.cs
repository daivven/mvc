using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys.Common.Business.Provider;
using Sys.Common;
using bookhelp.Model;
using bookhelp.Dal;
using Newtonsoft.Json.Linq;
using Sys.Common.Business.Data.SqlServer;
using System.Data.SqlClient;
using Sys.BPM.Core;

namespace bookhelp.Bll
{
    public class BookInfoBll
    {
        public static BookInfoBll Instance
        {
            get { return SingletonProvider<BookInfoBll>.Instance; }
        }      
        
        public string SaveBook(BookInfo u)
        {            
            string msg = "操作成功。";          
            int k = BookInfoDal.Instance.Insert(u);
            if (k > 0)
            {
                msg = "操作成功。";
            }
            else
            {
                msg = "操作失败。";
            }           
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        public string UpdateBook(BookInfo u)
        {
            string msg = "操作成功。";
            int k = BookInfoDal.Instance.Update(u);
            if (k > 0)
            {
                msg = "操作成功。";
            }
            else
            {
                msg = "操作失败。";
            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        public bool DeleteBook(BookInfo u)
        {            
            int k = BookInfoDal.Instance.Delete(u.ID);
            if (k > 0)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
     
    }
}
