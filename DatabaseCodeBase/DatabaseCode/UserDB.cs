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
    public class UserDB : BaseDatabase, IUserDB<UserModel>
    {
        public UserDB(string ConnectionString) : base(ConnectionString) { }

        public async Task<string> CreateUser(string storedprocedure, UserModel userModel)
        {
            string output = string.Empty;
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(storedprocedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = userModel.Name });
                        cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = userModel.RoleID });
                        cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit) { Value = userModel.Active });
                        int rowsaffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsaffected > 0) output = "User Created Successfully";
                        else output = "User Creation Failed";
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

        public async Task<string> DeleteUser(string storedprocedure, int UserID)
        {
            string output = string.Empty;
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(storedprocedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UseraID", SqlDbType.Int) { Value = UserID });
                        int affectedRows = await cmd.ExecuteNonQueryAsync();

                        if (affectedRows > 0) output = "Delete Successful!";
                        else output = "Delete Failed!";
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            return output;
        }

        public async Task<List<UserModel>> ReadUser(string storedprocedure)
        {
            var UserList = new List<UserModel>();
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(storedprocedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var Reader = await cmd.ExecuteReaderAsync();
                        while (Reader.Read())
                        {
                            var newUser = new UserModel
                            {
                                UserId = (int)Reader["UserID"],
                                Name = Reader["Name"].ToString(),
                                RoleID = (int)Reader["RoleId"],
                                Active = (bool)Reader["Active"]
                            };
                            UserList.Add(newUser);
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            catch(Exception ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            return UserList;
        }

        public async Task<string> UpdateUser(string storedprocedure, UserModel userModel)
        {
            string output = string.Empty;
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(storedprocedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userModel.UserId });
                        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = userModel.Name });

                        int affectedRows = await cmd.ExecuteNonQueryAsync();
                        if (affectedRows > 0) output = "Update Successfull";
                        else output = "Update Failed!";
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            return output;
        }

        public async Task<string> UpdateUserActivity(string storedprocedure, UserModel userModel)
        {
            string output = string.Empty;
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(storedprocedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userModel.UserId });
                        cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit) { Value = userModel.Active });

                        int affectedRows = await cmd.ExecuteNonQueryAsync();
                        if (affectedRows > 0) output = "Update Successfull";
                        else output = "Update Failed!";
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            return output;
        }

        public async Task<string> UpdateUserRole(string storedprocedure, UserModel userModel)
        {
            string output = string.Empty;
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(storedprocedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userModel.UserId });
                        cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = userModel.RoleID });

                        int affectedRows = await cmd.ExecuteNonQueryAsync();
                        if (affectedRows > 0) output = "Update Successfull";
                        else output = "Update Failed!";
                    }
                }
            }
            catch (SqlException ex)
            {
                OnQueryFail($"Unexpected Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                OnQueryFail($"Unexpected Error {ex.Message}");
            }
            return output;
        }
    }
}
