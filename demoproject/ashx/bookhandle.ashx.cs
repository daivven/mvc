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
    /// 图书
    /// </summary>
    public class bookhandle : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";            

            var json = HttpContext.Current.Request["json"];
            var rpm = new RequestParamModel<bookhelp.Model.BookInfo>(context)
            {
                CurrentContext = context,
                Action = context.Request["action"],
                KeyId = PublicMethod.GetInt(context.Request["id"])
            };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<bookhelp.Model.BookInfo>>(json);
                rpm.CurrentContext = context;
                //rpm.KeyId = PublicMethod.GetInt(context.Request["id"]);
            }
            int k = 0;            
            switch (rpm.Action)
            {
                case "add"://添加                   

                    string textBookName = context.Request["textBookName"];
                    string textBookSN = context.Request["textBookSN"];
                    string textBookPrice = context.Request["textBookPrice"];
                    string textBookPress = context.Request["textBookPress"];
                    BookInfo info = new BookInfo();
                    info.BookName = textBookName;
                    info.BookPrice = decimal.Parse(textBookPrice);
                    info.BookSNCode = textBookSN;
                    info.PressID = Int32.Parse(textBookPress);

                    BookInfoBll.Instance.SaveBook(info);

                    context.Response.Write(new JsonMessage { Data = k.ToString(), Message = "添加成功", Success = true }.ToString());
                    break;
                case "edit"://修改
                    string uid = context.Request["ID"];
                    string utextBookName = context.Request["textBookName"];
                    string utextBookSN = context.Request["textBookSN"];
                    string utextBookPrice = context.Request["textBookPrice"];
                    string utextBookPress = context.Request["textBookPress"];

                    BookInfo uinfo = new BookInfo();
                    uinfo.ID = Int32.Parse(uid);
                    uinfo.BookName = utextBookName;
                    uinfo.BookPrice = decimal.Parse(utextBookPrice);
                    uinfo.BookSNCode = utextBookSN;
                    uinfo.PressID = Int32.Parse(utextBookPress);

                    BookInfoBll.Instance.UpdateBook(uinfo);

                    context.Response.Write(new JsonMessage { Data = k.ToString(), Message = "修改成功", Success = true }.ToString());

                    break;
                case "delete":
                    string did = context.Request["id"];
                    BookInfo dinfo = new BookInfo();
                    dinfo.ID = Int32.Parse(did);
                    //bool result = BookInfoBll.DeleteBook(dinfo);
                    BookInfoBll bll = new BookInfoBll();
                    //bll.DeleteBook(dinfo);
                    if (bll.DeleteBook(dinfo))
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
                    break;
                case "list2":
                    var r2 = PressInfoBll.Instance.GetPressInfo();
                    context.Response.Write(r2);
                    break;
                case "list3":
                    var r3 = PressInfoBll.Instance.GetPressInfo();
                    context.Response.Write(r3);
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
                    where = FilterTranslator.ToSql(filterJSON) ;
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