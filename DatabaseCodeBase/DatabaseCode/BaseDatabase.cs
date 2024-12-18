using DatabaseCodeBase.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.DatabaseCode
{
    public class BaseDatabase
    {
        protected readonly string _ConnectionString;

        //Private Lock Objects and Event Handlers (prevent new delegate chain to made made each time when a event is added or removed) Locking them prevents the threads from overridng
        //each other resulting in unsuspecting behaviour
        private EventHandler<string> _connectionFail;
        private readonly object _connectionFailLock = new object();

        private EventHandler<string> _tableCreationFail;
        private readonly object _tableCreationFailLock = new object();

        protected EventHandler<string> _QueryFailed;
        protected readonly object _QueryFailedLock = new object();


        public event EventHandler<string> ConnectionFail
        {
            add { lock (_connectionFailLock) _connectionFail += value; }
            remove { lock (_connectionFailLock) _connectionFail -= value; }
        }

        public event EventHandler<string> TableCreationFail
        {
            add { lock (_tableCreationFailLock) _tableCreationFail += value; }
            remove { lock (_tableCreationFailLock) _tableCreationFail -= value; }
        }

        public event EventHandler<string> QueryFailed
        {
            add { lock (_QueryFailedLock) _QueryFailed += value; }
            remove { lock (_QueryFailedLock) _QueryFailed -= value; }
        }

        // Caches the database string and test if the connection is valid or string empty
        public BaseDatabase(string Connection)
        {
            try
            {
                if (!string.IsNullOrEmpty(Connection)) _ConnectionString = Connection;
                else throw new InitializationException($"{nameof(Connection)} is Empty!");

                using (var conn = new SqlConnection(_ConnectionString)) 
                {
                    conn.Open();
                }
            }
            catch(InitializationException ex)
            {
                OnConnectionFail($"Initialization fail: {ex.Message}");
            }
            catch(Exception ex)
            {
                OnConnectionFail($"Unexpected error: {ex.Message}");
            }
        }

        public async Task MakeDatabaseTables(string storedProcedure)
        {
            try
            {
                using (var conn = new SqlConnection(_ConnectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException)
            {
                var qex = new QuertFailedException("Table Creation Failed");
                OnTableCreationFail($"Unexpected Error {qex.Message}");
            }
            catch(Exception ex)
            {
                OnTableCreationFail($"Unexpected Error {ex.Message}");
            }
        }

        //handles invoking of the events
        private void OnConnectionFail(string message)
        {
            EventHandler<string> handler;
            lock (_connectionFailLock)
            {
                handler = _connectionFail;
            }
            handler?.Invoke(this, message);
        }
        private void OnTableCreationFail(string message)
        {
            EventHandler<string> handler;
            lock (_tableCreationFailLock)
            {
                handler = _tableCreationFail;
            }
            handler?.Invoke(this, message);
        }
        protected void OnQueryFail(string message)
        {
            EventHandler<string> handler;
            lock (_QueryFailedLock)
            {
                handler = _QueryFailed;
            }
            handler?.Invoke(this, message);
        }
    }
}
