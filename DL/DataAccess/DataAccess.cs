using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DL
{
    public class DataAccess
    {
        private SqlConnection sqlCon;
        private SqlCommand sqlCmd;
        private SqlTransaction sqlCurrentTrans;
        public string ConString;
        public DataAccess()
        {
            ConString = ConfigurationManager.AppSettings["dbConnection"].ToString();
        }
        private void ConnectionOpen(bool IsTransRequeired)
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(ConString);
                sqlCon.Open();
                if (IsTransRequeired)
                    sqlCurrentTrans = sqlCon.BeginTransaction();
            }
            else if (sqlCon.State == ConnectionState.Closed)
            {
                if (sqlCon.ConnectionString == string.Empty)
                    sqlCon = new SqlConnection(ConString);

                sqlCon.Open();
                if (IsTransRequeired)
                    sqlCurrentTrans = sqlCon.BeginTransaction();
            }
            else if (IsTransRequeired)
            {
                if (sqlCurrentTrans == null)
                {
                    sqlCurrentTrans = sqlCon.BeginTransaction();
                }
            }
        }
        private void ConnectionClose()
        {
            if (sqlCon != null)
            {
                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
                sqlCon.Dispose();
                sqlCon = null;
            }
        }
        protected void FinishTransaction(bool IsCommit)
        {
            try
            {
                if (IsCommit == true)
                {
                    if (sqlCurrentTrans != null)
                    {
                        if (sqlCurrentTrans.Connection != null && sqlCurrentTrans.Connection.State != ConnectionState.Closed)
                            sqlCurrentTrans.Commit();
                    }
                }
                else
                {
                    if (sqlCurrentTrans != null)
                    {
                        try
                        {
                            sqlCurrentTrans.Rollback();
                        }
                        catch { }
                    }
                }

                if (sqlCon != null)
                {
                    if (sqlCon.State == ConnectionState.Open)
                        sqlCon.Close();
                    sqlCon.Dispose();
                    sqlCon = null;
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (sqlCurrentTrans != null)
                {
                    sqlCurrentTrans.Dispose();
                    sqlCurrentTrans = null;
                }

                if (sqlCon != null)
                {
                    if (sqlCon.State == ConnectionState.Open)
                        sqlCon.Close();
                    sqlCon.Dispose();
                    sqlCon = null;
                }
            }
        }
        public DataTable FetchDataTable(string SP, bool IsTransRequired)
        {
            try
            {
                ConnectionOpen(IsTransRequired);
                sqlCmd = new SqlCommand(SP, sqlCon, sqlCurrentTrans);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (IsTransRequired)
                    FinishTransaction(false);
                ConnectionClose();
                throw ex;
            }
            finally
            {
                if (!IsTransRequired)
                    ConnectionClose();
            }
        }
        public DataTable FetchDataTable(string SP, string[] ParamNames, object[] ParamVals, bool IsTransRequired)
        {
            try
            {
                ConnectionOpen(IsTransRequired);
                sqlCmd = new SqlCommand(SP, sqlCon, sqlCurrentTrans);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                SetParameters(sqlCmd, ParamNames, ParamVals);
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                if (IsTransRequired)
                    FinishTransaction(false);
                ConnectionClose();
                throw ex;
            }
            finally
            {
                if (!IsTransRequired)
                    ConnectionClose();
            }
        }
        public int Execute(string SP, string[] ParamNames, object[] ParamVals, bool IsTransRequired)
        {
            try
            {
                ConnectionOpen(IsTransRequired);
                sqlCmd = new SqlCommand(SP, sqlCon, sqlCurrentTrans);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SetParameters(sqlCmd, ParamNames, ParamVals);
                return sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (IsTransRequired)
                    FinishTransaction(false);
                ConnectionClose();
                throw ex;
            }
            finally
            {
                if (!IsTransRequired)
                    ConnectionClose();
            }
        }
        private void SetParameters(SqlCommand sqlCmd, string[] ParamNames, object[] ParamVals)
        {
            if (ParamNames != null)
            {
                for (int i = 0; i < ParamNames.Length; i++)
                {
                    sqlCmd.Parameters.AddWithValue(ParamNames[i], ParamVals[i]);
                }
            }
        }
    }
}
