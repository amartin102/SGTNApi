using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class NominaRepository : INominaRepository
    {
        private readonly SqlDbContext _context;

        public NominaRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> CalcularNominaCompletaAsync(string strIdPeriodo, string strIdCliente)
        {
            var parameters = new[]
            {
                new SqlParameter("@strIdPeriodo", SqlDbType.NVarChar, 50) { Value = strIdPeriodo },
                new SqlParameter("@strIdCliente", SqlDbType.UniqueIdentifier) { Value = Guid.Parse(strIdCliente) }
            };

            var results = new List<Dictionary<string, object>>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[sp_CalcularNominaCompleta]";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                if (command.Connection.State != ConnectionState.Open)
                    await command.Connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        }
                        results.Add(row);
                    }
                }
            }

            return results;
        }
    }
}
