using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys.Common;
using Sys.Common.Business.Data;
using Sys.Common.Business.Data.Filter;
using Sys.Common.Business.Provider;
using bookhelp.Model;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace bookhelp.Dal
{
    public class PressInfoDal : BaseRepository<PressInfo>
    {
        public static PressInfoDal Instance
        {
            get { return SingletonProvider<PressInfoDal>.Instance; }
        }        
    }
}
