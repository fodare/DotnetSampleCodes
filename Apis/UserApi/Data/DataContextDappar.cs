using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace APIBasics.Data
{
    public class DataContextDappar
    {
        private readonly IConfiguration _configuration;
        public DataContextDappar(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<T> LoadData<T>(string sqlCommand)
        {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("devDbConnectionString"));
            return dbConnection.Query<T>(sqlCommand);
        }

        public T LoadDataSingle<T>(string sqlCommand)
        {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("devDbConnectionString"));
            return dbConnection.QuerySingle<T>(sqlCommand);
        }

        public bool ExecuteSql(string sqlCommand)
        {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("devDbConnectionString"));
            return dbConnection.Execute(sqlCommand) > 0;
        }

        public int ExecuteSqlWithRowCount(string sqlCommand)
        {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("devDbConnectionString"));
            return dbConnection.Execute(sqlCommand);
        }

        public bool UpdateSql(string sqlCommand)
        {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("devDbConnectionString"));
            return dbConnection.Execute(sqlCommand) > 0;
        }
    }
}