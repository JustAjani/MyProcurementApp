﻿using DatabaseCodeBase.Model;
using DatabaseCodeBase.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.DatabaseCode
{
    public class CostCenterDB : BaseDatabase, ICostCenter<CostCenterModel>
    {
        public CostCenterDB(string Connection) : base(Connection){}

        public async Task<string> CreateCostCenter(string storeProcedure, CostCenterModel costCenter)
        {
            string output = string.Empty;
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using(SqlCommand cmd = new SqlCommand(storeProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@CostCentreName", SqlDbType.NVarChar, 256) { Value = costCenter.CostCenterName});
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0) output = "Cost Center Added Successfully";
                        else output = "Failed to Add";
                    }
                }
            }
            catch(SqlException ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            return output;
        }

        public async Task<List<CostCenterModel>> ReadCostCenter(string storeProcedure)
        {
            var output = new List<CostCenterModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storeProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var reader = await cmd.ExecuteReaderAsync();
                        while(reader.Read())
                        {
                            var costCenter = new CostCenterModel()
                            {
                                CostCenterId = (int)reader["CostCenterId"],
                                CostCenterName = (string)reader["CostCenterName"],
                            };
                            output.Add(costCenter);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            return output;
        }
    }
}
