using MessageService.IRepository;
using MessageService.Repository.SQL;
using MessageService.Repository.SQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MessageService.Repository.SQL
{
    public class RepositoryFromSql : IRepositoryFromSql
    {
        protected readonly string _connectionString;

        public RepositoryFromSql(string connectionString)
        {
            _connectionString = connectionString;

        }
        public TSp Get<TParams, TSp>(TParams parameters) where TParams : class
        where TSp : class
        {
            TSp sp = (TSp)Activator.CreateInstance(typeof(TSp));
            var spName = $"[dbo].[{typeof(TSp).Name}]";
            var results = DapperSqlHelper.QueryMultipleSP(spName, param: parameters, connectionString: _connectionString).ToList();
            var props = typeof(TSp).GetProperties();
            // for each table
            for (int i = 0; i < results.Count(); i++)
            {
                var resultList = results[i].ToList();
                PropertyInfo info = props[i];
                var propValue = info.GetValue(sp);
                if (propValue == null)
                {
                    info.SetValue(sp, Activator.CreateInstance(info.PropertyType));
                    propValue = info.GetValue(sp);
                }
                var infoList = (IList)propValue;
                var infoArgType = info.PropertyType.GetGenericArguments()[0];
                // for each row
                for (var j = 0; j < resultList.Count; j++)
                {
                    var item = (IDictionary<string, object>)resultList[j];
                    var infoItem = Activator.CreateInstance(infoArgType);

                    // for each field
                    foreach (var pInfo in infoArgType.GetProperties())
                    {
                        if (item.ContainsKey(pInfo.Name))
                        {
                            pInfo.SetValue(infoItem, item[pInfo.Name]);

                        }
                    }
                    infoList.Add(infoItem);

                }
                info.SetValue(sp, infoList);


            }

            return sp;

        }
        public TSp Get<TSp>()
        where TSp : class
        {
            TSp sp = (TSp)Activator.CreateInstance(typeof(TSp));
            var spName = $"[dbo].[{typeof(TSp).Name}]";
            var results = DapperSqlHelper.QueryMultipleSP(spName, connectionString: _connectionString).ToList();
            var props = typeof(TSp).GetProperties();
            // for each table
            for (int i = 0; i < results.Count(); i++)
            {
                var resultList = results[i].ToList();
                PropertyInfo info = props[i];
                var propValue = info.GetValue(sp);
                if (propValue == null)
                {
                    info.SetValue(sp, Activator.CreateInstance(info.PropertyType));
                    propValue = info.GetValue(sp);
                }
                var infoList = (IList)propValue;
                var infoArgType = info.PropertyType.GetGenericArguments()[0];
                // for each row
                for (var j = 0; j < resultList.Count; j++)
                {
                    var item = (IDictionary<string, object>)resultList[j];
                    var infoItem = Activator.CreateInstance(infoArgType);

                    // for each field
                    foreach (var pInfo in infoArgType.GetProperties())
                    {
                        if (item.ContainsKey(pInfo.Name))
                        {
                            pInfo.SetValue(infoItem, item[pInfo.Name]);

                        }
                    }
                    infoList.Add(infoItem);

                }
                info.SetValue(sp, infoList);


            }

            return sp;

        }
        //public TSp ExecuteProcedure<TParams>(string spName, TParams parameters) where TParams : class
        public int ExecuteProcedure<TParams>(string spName, TParams parameters) where TParams : class
        {
            return DapperSqlHelper.ExecuteSP(spName, param: parameters, connectionString: _connectionString);
        }
        //public int ExecuteProcedureWithOutputParam<TParams, TOutParams>(string spName, TParams parameters, TOutParams outparameters)
        //    where TParams : class
        //    where TOutParams : class
        //{
        //    return DapperSqlHelper.ExecuteSP(spName, param: parameters, outParam: outparameters, connectionString: _connectionString);
        //}
        public int ExecuteProcedure(string spName)
        {
            return DapperSqlHelper.ExecuteSP(spName, param: null, connectionString: _connectionString);
        }
        public DataTable QuerySPToDataTable(string spName)
        {
            return DapperSqlHelper.QuerySP(spName, connectionString: _connectionString).ToDataTable();
        }
        public DataTable QuerySPToDataTable<TParams>(string spName, TParams parameters) where TParams : class
        {
            return DapperSqlHelper.QuerySP(spName, param: parameters, connectionString: _connectionString).ToDataTable();
        }
    }
}
