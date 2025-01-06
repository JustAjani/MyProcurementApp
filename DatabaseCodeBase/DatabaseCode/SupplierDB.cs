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
    public class SupplierDB : BaseDatabase, ISupplier<SupplierModel>
    {
        public SupplierDB(string Connection) : base(Connection)
        {
        }

        public async Task<string> CreateSupplier(string storeProcedure, SupplierModel supplier)
        {
            string output = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storeProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@SupplierName", SqlDbType.NVarChar, 256) { Value = supplier.SupplierName });
                        cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = supplier.IsActive });

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0) output = "Supplier Added Successfully";
                        else output = "Failed to Add Supplier";
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

        public async Task<List<SupplierModel>> ReadSupplier(string storeProcedure)
        {
            var output = new List<SupplierModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storeProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var reader = await cmd.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            var supplier = new SupplierModel()
                            {
                                SupplierId = (int)reader["SupplierId"],
                                SupplierName = (string)reader["SupplierName"],
                                IsActive = (bool)reader["IsActive"]
                            };
                            output.Add(supplier);
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

        public async Task<string> UpdateSupplier(string storeProcedure, SupplierModel supplier)
        {
            string output = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storeProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@SupplierId", SqlDbType.Int) { Value = supplier.SupplierId });
                        cmd.Parameters.Add(new SqlParameter("@SupplierName", SqlDbType.NVarChar, 256) { Value = supplier.SupplierName });
                        cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = supplier.IsActive });

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0) output = "Supplier Updated Successfully";
                        else output = "Failed to Update Supplier";
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
