using DatabaseCodeBase.Model;
using DatabaseCodeBase.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.DatabaseCode
{
    public class RoleDB : BaseDatabase, IRole<RoleModel>
    {
        public RoleDB(string Connection) : base(Connection) { }
        public async Task<string> CreateRoles(string storedProcedure, RoleModel RoleModel)
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
                        cmd.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.NVarChar, 255) { Value = RoleModel.RoleName });
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0) output = "Role Created Successfully";
                        else output = "Role Creation Failed";
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            return output;
        }

        public async Task<List<RoleModel>> ReadRoles(string storedProcedure)
        {
            var ouput = new List<RoleModel>();
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var reader = await  cmd.ExecuteReaderAsync();
                        while(reader.Read())
                        {
                            var role = new RoleModel()
                            {
                                RoleID = (int)reader["RoleId"],
                                RoleName = (string)reader["RoleName"]
                            };
                            ouput.Add(role);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            return ouput;
        }
    }      
}

