using DatabaseCodeBase.Model;
using DatabaseCodeBase.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.DatabaseCode
{
    public class ProcurementTypeDataBase : BaseDatabase, IProcurementType<ProcurementTypeModel>
    {
        public ProcurementTypeDataBase(string connectionString) : base(connectionString) { }

        public async Task<string> CreateProcurement(string storedProcedure, ProcurementTypeModel procurmentTypeModel)
        {
            string output = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 255) { Value = procurmentTypeModel.Type });
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0) output = "Procurement Type Added SuccessFully";
                        else output = "Failed To Add Procurement";
                    }
                }
            }
            catch(SqlException ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            } 
            return output;
        }

        public async Task<List<ProcurementTypeModel>> ReadProcurementType(string stroredProcdure)
        {
            var ProcurementList = new List<ProcurementTypeModel>();
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using(SqlCommand cmd = new SqlCommand(stroredProcdure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var Reader = await cmd.ExecuteReaderAsync();
                        while (Reader.Read())
                        {
                            var Procurement = new ProcurementTypeModel()
                            {
                                ID = (int)Reader["ProcurementTypeId"],
                                Type = Reader["ProcurementType"].ToString(),
                            };
                            ProcurementList.Add(Procurement);
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                OnQueryFail($"Unexpected Error{ex.Message}");
            }
            return ProcurementList;
        }
    }
}
