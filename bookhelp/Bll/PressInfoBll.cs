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
using Sys.Common.Business;

namespace bookhelp.Bll
{
    public class PressInfoBll
    {
        public static PressInfoBll Instance
        {
            get { return SingletonProvider<PressInfoBll>.Instance; }
        }
        public string GetPressInfo()
        {
            return JSONhelper.ToJson( PressInfoDal.Instance.GetAll());
        }

        public string SavePress(PressInfo u)
        {
            string msg = "操作成功。";
            int k = PressInfoDal.Instance.Insert(u);
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

        public string UpdatePress(PressInfo u)
        {
            string msg = "操作成功。";
            int k = PressInfoDal.Instance.Update(u);
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

        public bool DeletePress(PressInfo u)
        {
            int k = PressInfoDal.Instance.Delete(u.ID);
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
