using System.Data;

namespace MessageService.IRepository
{
    public interface IRepositoryFromSql
    {
        TSp Get<TParams, TSp>(TParams parameters)
            where TParams : class
            where TSp : class;
        TSp Get<TSp>()
            where TSp : class;
        int ExecuteProcedure<TParams>(string spName, TParams parameters) where TParams : class;
        int ExecuteProcedure(string spName);
        DataTable QuerySPToDataTable(string spName);
        DataTable QuerySPToDataTable<TParams>(string spName, TParams parameters) where TParams : class;
    }
}
