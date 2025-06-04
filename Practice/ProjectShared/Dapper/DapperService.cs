using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PracticeModel.DBcontext;
namespace AMS.SERVICES.Dapper;

  public class DapperService (BaseContext databaseContext) : IDapperService
    {
        #region PROPERTIES
        private readonly BaseContext _databaseContext =databaseContext;
        #endregion

        #region FUNCTIONS
        /// <summary>
        /// This method is used to dispose application context
        /// </summary>
        public void Dispose()
        {
            _databaseContext.Dispose();
        }
        /// <summary>
        /// This method is used for execute query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> ExecuteAsync(string query, DynamicParameters parameters)
        {
            var dbConnection = _databaseContext.Database.GetDbConnection();
            var result = await dbConnection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// This methid is used to get single select value. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync<T>(string query, DynamicParameters parameters)
        {
            var dbConnection = _databaseContext.Database.GetDbConnection();
            var result = await dbConnection.ExecuteScalarAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
            return result ?? default;
        }
        /// <summary>
        /// This method is used to get query first or default data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<T> ReturnRowAsync<T>(string query, DynamicParameters parameters)
        {
            var _dbConnection = _databaseContext.Database.GetDbConnection();
            var result = await _dbConnection.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
            return result ?? default;
        }
    /// <summary>
    /// This method is used to return list of type object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="procedureName"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<T>> ReturnListAsync<T>(string procedureName, DynamicParameters parameters)
    {
        var _dbConnection = _databaseContext.Database.GetDbConnection();
        return await _dbConnection.QueryAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
    }
    #endregion
}