using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Omu.ValueInjecter;
using System.Configuration;
using Sys.Common.Business.Data.SqlServer;


namespace Sys.Common.Business.Data
{
    public static class DbUtils
    {
        static string cs = SqlEasy.connString; //数据库连接字符串
        public static IEnumerable<T> GetWhere<T>(object where) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where "
                        .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=@{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);
                    cmd.InjectFrom<SetParamsValues>(where);
                    conn.Open();
                    
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                    }
                }
            }
        }

        public static int CountWhere<T>(object where) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(*) from " + TableConvention.Resolve(typeof(T)) + " where "
                        .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=@{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);
                    cmd.InjectFrom<SetParamsValues>(where);
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static int Delete<T>(int id)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where id=@id";

                cmd.InjectFrom<SetParamsValues>(new { id = id});
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static int Delete<T>(string ids)
        { 
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where charindex(',' + cast(id AS varchar(50)) + ',',','  + @id + ',') > 0";

                cmd.InjectFrom<SetParamsValues>(new { id = ids});
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        ///<returns> the id of the inserted object </returns>
        public static int Insert(object o)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                    .InjectFrom(new FieldsBy().IgnoreFields("id"), o) + ") values("
                    .InjectFrom(new FieldsBy().IgnoreFields("id").SetFormat("@{0}"), o)
                    + ") select @@identity";

                cmd.InjectFrom(new SetParamsValues().IgnoreFields("id"), o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static int Update(object o)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(o) + " set "
                    .InjectFrom(new FieldsBy().IgnoreFields("id").SetFormat("{0}=@{0}"), o)
                    + " where id = @id";

                cmd.InjectFrom<SetParamsValues>(o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteNonQuery());
            }
        }

        public static int Update(object o,params string[] fields)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(o) + " set "
                    .InjectFrom(new FieldsBy().IgnoreFields(fields).SetFormat("{0}=@{0}"), o)
                    + " where id = @id";

                cmd.InjectFrom<SetParamsValues>(o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteNonQuery());
            }
        }

        public static int UpdateWhatWhere<T>(object what, object where)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(typeof(T)) + " set "
                    .InjectFrom(new FieldsBy().SetFormat("{0}=@{0}"), what)
                    + " where "
                    .InjectFrom(new FieldsBy()
                    .SetFormat("{0}=@wp{0}")
                    .SetNullFormat("{0} is null")
                    .SetGlue("and"),
                    where);

                cmd.InjectFrom<SetParamsValues>(what);
                cmd.InjectFrom(new SetParamsValues().Prefix("wp"), where);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

       
        public static int InsertNoIdentity(object o)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                    .InjectFrom(new FieldsBy().IgnoreFields("id"), o) + ") values("
                    .InjectFrom(new FieldsBy().IgnoreFields("id").SetFormat("@{0}"), o) + ")";

                cmd.InjectFrom<SetParamsValues>(o);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <returns>rows affected</returns>
        public static int ExecuteNonQuerySp(string sp, object parameters)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static int ExecuteNonQuery(string commendText, object parameters)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = commendText;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<T> ExecuteReader<T>(string sql, object parameters) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                }
            }
        }


        public static IEnumerable<T> ExecuteReaderSp<T>(string sp, object parameters) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                }
            }
        }

        public static IEnumerable<T> ExecuteReaderSpValueType<T>(string sp, object parameters)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            yield return (T)dr.GetValue(0);
                        }
                }
            }
        }

        public static int Count<T>()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(*) from " + TableConvention.Resolve(typeof(T));
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static int GetPageCount(int pageSize, int count)
        {
            var pages = count / pageSize;
            if (count % pageSize > 0) pages++;
            return pages;
        }

        public static IEnumerable<T> GetAll<T>() where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T));
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> GetList<T>(string sql, object parameters) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                    }
                }
            }
        }

        public static DataTable GetPageWithSp(ProcCustomPage pcp,out int recordCount)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = pcp.Sp_PagerName;
                    cmd.InjectFrom(new SetParamsValues().IgnoreFields("sp_pagername"), pcp);

                    SqlParameter outputPara = new SqlParameter("@RecordCount", SqlDbType.Int);
                    outputPara.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputPara);
                    
                    conn.Open();
                    
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        cmd.Parameters.Clear();
                        recordCount = PublicMethod.GetInt(outputPara.Value);
                        conn.Close();
                        return ds.Tables[0];
                    }
                }
            }
        }


        public static IEnumerable<T> GetPage<T>(int page, int pageSize) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    var name = TableConvention.Resolve(typeof(T));

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format(@"with result as(select *, ROW_NUMBER() over(order by id desc) nr
                            from {0}
                    )
                    select  * 
                    from    result
                    where   nr  between (({1} - 1) * {2} + 1)
                            and ({1} * {2}) ", name, page, pageSize);
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                    }
                }
            }
        }

        public static T Get<T>(long keyid) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where id = " + keyid;
                conn.Open();

                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                    {
                        var o = new T();
                        o.InjectFrom<ReaderInjection>(dr);
                        return o;
                    }
            }
            return default(T);
        }

        public static T Get<T>(string idField, long keyid) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where " + idField + " = " + keyid;
                conn.Open();

                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                    {
                        var o = new T();
                        o.InjectFrom<ReaderInjection>(dr);
                        return o;
                    }
            }
            return default(T);
        }
    }
}
