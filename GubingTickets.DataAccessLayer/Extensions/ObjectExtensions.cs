using Dapper;
using GubingTickets.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GubingTickets.DataAccessLayer.Extensions
{
    public static class ObjectExtensions
    {
        private static DateTime _MinimumSqlDateTime = new DateTime(1953, 01, 10);

        public static DynamicParameters GetDynamicParameters<T>(this IEnumerable<T> objs, string tableParameterName, string tvpTableName, long? foreignKey = null)
        {
            string foreignKeyName = null;
            bool hasForeignKeyName = false;

            Type objectType = typeof(T);
            IEnumerable<PropertyInfo> columnPropertyInfos = objectType.GetProperties().Where(pr => pr.GetCustomAttribute(typeof(TVPAttribute), false) != null).ToList().OrderBy(pr => pr.GetCustomAttribute<TVPAttribute>().TvpOrder);

            DataTable dataTable = new DataTable
            {
                TableName = objectType.Name
            };

            foreach (PropertyInfo propertyInfo in columnPropertyInfos)
            {
                TVPAttribute tvpAttribute = propertyInfo.GetCustomAttribute<TVPAttribute>();

                if (tvpAttribute.IsForeignKey && foreignKey.HasValue)
                {
                    hasForeignKeyName = true;
                    foreignKeyName = propertyInfo.Name;
                }

                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, tvpAttribute.PropertyOverride ?? propertyInfo.PropertyType));
            }

            foreach (T obj in objs)
            {
                DataRow newRow = dataTable.NewRow();

                foreach (PropertyInfo propertyInfo in columnPropertyInfos)
                {
                    if (hasForeignKeyName && propertyInfo.Name.Equals(foreignKeyName))
                    {
                        newRow[propertyInfo.Name] = foreignKey.Value;
                    }
                    else
                    {
                        var value = propertyInfo.GetValue(obj);

                        if (propertyInfo.PropertyType == typeof(DateTime))
                        {
                            value = ((DateTime?)value)?.GetGetMinimunDate();
                        }

                        newRow[propertyInfo.Name] = value ?? DBNull.Value;
                    }
                }

                dataTable.Rows.Add(newRow);
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{tableParameterName}", dataTable.AsTableValuedParameter(tvpTableName));

            return parameters;
        }

        

        public static DateTime GetGetMinimunDate(this DateTime date)
        {
            if (date < _MinimumSqlDateTime)
                return _MinimumSqlDateTime;

            return date;
        }

    }
}
