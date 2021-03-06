﻿using IGTManagementTicket.Api.Interfaces.Repository;
using IGTManagementTicket.Api.Models;
using IGTManagementTicket.Api.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Repository
{
    public partial class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Constants

        //TODO: Change paths using final Model store

        public const string STR_TAG_DBNAME = "IGT.Gaming.Model";
        public const string STR_TAG_PATH_MDF = @"D:\Data\SQL\DEV";               //"F:\\SQLData\\"
        public const string STR_TAG_PATH_LDF = @"D:\Data\SQL\DEV";               //"L:\\SQLLogData\\"
        public const string STR_DATABASE_BASENAME = "IGT.Gaming";
        public const string SCRIPT_CREATE_DATABASE = "CreateDB";
        public const string SCRIPT_DELETE_DATABASE = "DropDB";
        public const string STR_CONN_CONF = "Data Source=localhost;Initial Catalog=master; User Id=sa;Password=wildcats@33;Connection Timeout=120;";
        public const string STR_CONN_STAG = "Data Source=localhost;Initial Catalog=master; User Id=sa;Password=wildcats@33;Connection Timeout=120;";
        public const string STR_CONN_PROD = "Data Source=localhost;Initial Catalog=master; User Id=sa;Password=wildcats@33;Connection Timeout=120;";

        #endregion

        #region Members

        public Guid InstanceID { get; set; } = Guid.NewGuid();

        //private SqlTransaction transaction;
        #endregion

        #region Constructors

        #endregion   

        #region Internal Methods

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public string GetConnectionStringName(int jobId, EnvironmentType environment, string dbName=null)
        {
            switch (environment)
            {
                case EnvironmentType.Config:
                    return dbName == null ? STR_CONN_CONF : STR_CONN_CONF.Replace("master", dbName);
                case EnvironmentType.Staging:
                    return dbName == null ? STR_CONN_STAG : STR_CONN_STAG.Replace("master", dbName);
                case EnvironmentType.Prod:
                    return dbName == null ? STR_CONN_PROD : STR_CONN_PROD.Replace("master", dbName);
                default:
                    return string.Empty;
            }
        }

        public string GetDbName(int jobId, EnvironmentType environment)
        {
            switch (environment)
            {
                case EnvironmentType.Config:
                    return STR_DATABASE_BASENAME;
                case EnvironmentType.Staging:
                case EnvironmentType.Prod:
                    return $"{STR_DATABASE_BASENAME}.{jobId}";
                default:
                    return string.Empty;
            }
        }
        internal void ExecuteSqlScript(string script, int jobId, EnvironmentType environment)
        {
            try
            {
                string connection = GetConnectionStringName(jobId, environment);

                using (var conn = new SqlConnection(connection))
                {
                    conn.Open();

                    using (var command = new SqlCommand(script, conn))
                    {
                        foreach (string s in script.Split(new string[] { "\r\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            command.CommandText = s.Trim();
                            command.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal string GetScriptFullpath(string name)
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            DirectoryInfo root = di.Parent.Parent.Parent.Parent;

            return Path.Combine(root.FullName, $"D:\\Data\\SQL\\DEV\\{name}.sql");
        }

        public void SetJobAndEnvironment(SettingsDB settings)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
