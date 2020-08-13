using IGTManagementTicket.Api.Interfaces.Repository;
using IGTManagementTicket.Api.Models;
using IGTManagementTicket.Api.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Repository
{
    public class StatusDBRepository : Repository<StatusDB> , IStatusDBRepository
    {
        #region API Methods
        public StatusDB DatabaseCreate(int jobId, EnvironmentType environment)
        {
            var statusDB = new StatusDB();
            var dt = new DataTable();

            string connection = ConnectionStringName(jobId, environment);

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();

                using (var command = new SqlCommand("SELECT SERVERPROPERTY('instancedefaultdatapath') AS [DefaultFile], SERVERPROPERTY('instancedefaultlogpath') AS [DefaultLog]", conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                }

                conn.Close();
            }

            FileInfo fi = new FileInfo(GetScriptFullpath(SCRIPT_CREATE_DATABASE));
            string dbName = DbName(jobId, environment);
            string script = fi.OpenText().ReadToEnd()
                .Replace(STR_TAG_DBNAME, dbName)
                .Replace(STR_TAG_PATH_MDF, (string)dt.Rows[0][dt.Columns["DefaultFile"].Ordinal])
                .Replace(STR_TAG_PATH_LDF, (string)dt.Rows[0][dt.Columns["DefaultLog"].Ordinal]);

            try
            {
                ExecuteSqlScript(script, jobId, environment);
            }
            catch (Exception ex)
            {
                statusDB.ErrorMessage = ex.Message;
            }

            return statusDB;
        }

        public StatusDB DatabaseDelete(int jobId, EnvironmentType environment)
        {
            var statusDB = new StatusDB();

            FileInfo fi = new FileInfo(GetScriptFullpath($"{SCRIPT_DELETE_DATABASE}"));
            string dbName = DbName(jobId, environment);
            string script = fi.OpenText().ReadToEnd().Replace(STR_TAG_DBNAME, dbName);

            try
            {
                ExecuteSqlScript(script, jobId, environment);
            }
            catch (Exception ex)
            {
                statusDB.ErrorMessage = ex.Message;
            }

            return statusDB;
        }

        public StatusDB DatabaseClear(int jobId, EnvironmentType environment)
        {
            var statusDB = new StatusDB();

            try
            {
                string connection = ConnectionStringName(jobId, environment);

                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();

                    using (var command = new SqlCommand("[dbo].[dbClear]", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                statusDB.ErrorMessage = ex.Message;
            }
                 
            return statusDB;
        }

        #endregion
    }
}
