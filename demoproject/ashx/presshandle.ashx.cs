using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sys.BPM.Core.Bll;
using Sys.BPM.Core;
using Sys.BPM.Core.Model;
using Sys.Common.Business;
using Sys.Common.Business.Data.Filter;
using Sys.Common.Business.Data;
//using Omu.ValueInjecter;
using bookhelp.Model;
using bookhelp.Dal;
using bookhelp.Bll;
using System.Data;
using System.Web.SessionState;
using Sys.BPM.Core.Dal;

namespace demoproject.ashx
{
    /// <summary>
    /// 出版社
    /// </summary>
    public class presshandle : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var json = HttpContext.Current.Request["json"];
            var rpm = new RequestParamModel<bookhelp.Model.PressInfo>(context)
            {
                CurrentContext = context,
                Action = context.Request["action"],
                KeyId = PublicMethod.GetInt(context.Request["id"])
            };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<bookhelp.Model.PressInfo>>(json);
                rpm.CurrentContext = context;
                //rpm.KeyId = PublicMethod.GetInt(context.Request["id"]);
            }
            int k = 0;
            switch (rpm.Action)
            {
                case "add"://添加                   

                    string textPressName = context.Request["textPressName"];
                    string textPressAddress = context.Request["textPressAddress"];
                    string textPressPhone = context.Request["textPressPhone"];                    
                    PressInfo info = new PressInfo();
                    info.PressName = textPressName;
                    info.PressAddress = textPressAddress;
                    info.PressPhone = textPressPhone;                   

                    PressInfoBll.Instance.SavePress(info);

                    context.Response.Write(new JsonMessage { Data = k.ToString(), Message = "添加成功", Success = true }.ToString());
                    break;
                case "edit"://修改
                    string uid = context.Request["ID"];
                    string utextPressName = context.Request["textPressName"];
                    string utextPressAddress = context.Request["textPressAddress"];
                    string utextPressPhone = context.Request["textPressPhone"];

                    PressInfo uinfo = new PressInfo();
                    uinfo.ID = Int32.Parse(uid);
                    uinfo.PressName = utextPressName;
                    uinfo.PressAddress = utextPressAddress;
                    uinfo.PressPhone = utextPressPhone;

                    PressInfoBll.Instance.UpdatePress(uinfo);

                    context.Response.Write(new JsonMessage { Data = k.ToString(), Message = "修改成功", Success = true }.ToString());

                    break;
                case "delete":
                    string did = context.Request["id"];
                    PressInfo dinfo = new PressInfo();
                    dinfo.ID = Int32.Parse(did);
                    //bool result = BookInfoBll.DeleteBook(dinfo);
                    PressInfoBll bll = new PressInfoBll();
                    //bll.DeleteBook(dinfo);
                    if (bll.DeletePress(dinfo))
                    {
                        context.Response.Write("ok");
                    }
                    else
                    {
                        context.Response.Write("no");
                    }
                    break;

                case "search":
                    //string bookName = context.Request["bookName"];
                    //string pressName = context.Request["pressName"];
                    //string sn = context.Request["sn"];
                    //string priceLow = context.Request["priceLow"];
                    //string priceHigh = context.Request["priceHigh"];
                    context.Response.Write(JsonDataForEasyUIdataGrid(rpm.Pageindex, rpm.Pagesize, rpm.Filter));

                    break;

                case "list":
                    var r = PressInfoBll.Instance.GetPressInfo();
                    context.Response.Write(r); 
                    //context.Response.Write(JsonDataForEasyUIdataGrid2(rpm.Pageindex, rpm.Pagesize, rpm.Filter));
                    break;
                default:
                    context.Response.Write(JsonDataForEasyUIdataGrid(rpm.Pageindex, rpm.Pagesize, rpm.Filter));
                    break;
            }
        }


        private string JsonDataForEasyUIdataGrid(int pageindex, int pagesize, string filterJSON)
        {

            string where = string.Empty;

            if (filterJSON != "")
            {
                where = FilterTranslator.ToSql(filterJSON);
            }
            else
            {
                where = " ";
            }

            var pcp = new ProcCustomPage()
            {
                Sp_PagerName = "ProcCustomPageUnion",
                TableName = @"(select ID, PressName, PressAddress, PressPhone
    from  PressInfo )",
                PageIndex = pageindex,
                PageSize = pagesize,
                ShowFields = "*",
                OrderFields = "ID desc",
                KeyFields = "ID",
                WhereString = where
            };

            int recordCount;
            DataTable dt = DbUtils.GetPageWithSp(pcp, out recordCount);
            return JSONhelper.FormatJSONForEasyuiDataGrid(recordCount, dt);
        }


        private string JsonDataForEasyUIdataGrid2(int pageindex, int pagesize, string filterJSON)
        {

            string where = string.Empty;

            if (filterJSON != "")
            {
                where = FilterTranslator.ToSql(filterJSON);
            }
            else
            {
                where = " ";
            }

            var pcp = new ProcCustomPage()
            {
                Sp_PagerName = "ProcCustomPageUnion",
                TableName = @"(select a.ID, a.BookName, a.BookSNCode, a.BookPrice, a.PressID, PressName, PressAddress, PressPhone
    from BookInfo a inner join PressInfo b on a.PressID= b.ID)",
                PageIndex = pageindex,
                PageSize = pagesize,
                ShowFields = "*",
                OrderFields = "ID desc",
                KeyFields = "ID",
                WhereString = where
            };

            int recordCount;
            DataTable dt = DbUtils.GetPageWithSp(pcp, out recordCount);
            return JSONhelper.FormatJSONForEasyuiDataGrid(recordCount, dt);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }




}