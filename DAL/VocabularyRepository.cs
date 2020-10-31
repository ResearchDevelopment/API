using ShadiWebApplication.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
namespace ShadiWebApplication.DAL
{
    public class VocabularyRepository : IVocabularyRepository
    {
        private readonly string _connectionString;
        private IDbConnection GetDbConnection() => new SqlConnection(this._connectionString);

        public VocabularyRepository(string shadiCnn)
        {
            if (string.IsNullOrWhiteSpace(shadiCnn))
                throw new ArgumentNullException(nameof(shadiCnn), nameof(shadiCnn));

            this._connectionString = shadiCnn;
        }
        public List<VW_Vocabulary> GetVocabs(int page, int pageSize)
        {
           
            const string query = @"SELECT * FROM VW_Vocabulary 
                                   ORDER BY Id DESC
                                   OFFSET @Offset ROWS
                                   FETCH NEXT @PageSize ROWS ONLY; ";
            using (var dbConnection = GetDbConnection())
            {
                return dbConnection.Query<VW_Vocabulary>(query, new
                {
                    Offset = (page - 1) * pageSize,
                    PageSize = pageSize
                }).ToList();
            }

        }
    }
}
