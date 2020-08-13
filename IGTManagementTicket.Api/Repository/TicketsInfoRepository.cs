using IGTManagementTicket.Api.Interfaces.Repository;
using IGTManagementTicket.Api.Models;
using IGTManagementTicket.Api.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IGTManagementTicket.Api.Repository
{
    public class TicketsInfoRepository : Repository<TicketsInfo>, ITicketsInfoRepository
    {
        #region API Methods
        public TicketsInfo DatabaseExists(int jobId, EnvironmentType environment)
        {
            var ticketsInfo = new TicketsInfo();
            var dt = new DataTable();
            string dbName = DbName(jobId, environment);

            try
            {
                using (var conn = new SqlConnection(STR_CONN_STAG))
                {
                    conn.Open();  

                    using (var command = new SqlCommand($"select name from sys.databases where name = '{dbName}'", conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        dt.Load(reader);

                        if (dt.Rows.Count > 0)
                        {
                            using (var command2 = new SqlCommand("SELECT cast(SUM(st.row_count) as int) FROM sys.dm_db_partition_stats st WHERE object_name(object_id) = 'TicketInstanceJob' AND (index_id < 2)", conn))
                            {
                                reader = command2.ExecuteReader();
                                dt.Load(reader);

                                if (dt.Rows.Count > 0)
                                    ticketsInfo.Tickets = (int)dt.Rows[0][0];

                                var command3 = new SqlCommand("select sum(Books) from BookCounts", conn);
                                reader = command3.ExecuteReader();
                                dt.Load(reader);

                                if (dt.Rows.Count > 0)
                                    ticketsInfo.Books = (int)dt.Rows[0][0];
                            }
                        }
                        else
                        {
                            ticketsInfo.ErrorMessage = $"Database for job: {jobId} does not exist.";
                        }

                        reader.Close();   
                    }
                    conn.Close(); 
                }                
            }
            catch (SqlException ex)
            {
                ticketsInfo.ErrorMessage = ex.Message;
            }

            dt.Dispose();
            return ticketsInfo;
        }

        #endregion
    }
}
