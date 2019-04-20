using Microsoft.EntityFrameworkCore;
using MyERP.Common.CSComment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyERP.EntityFrameworkCore.Seed.SyncDesp
{
    //            new Seed.SyncDesp.DBDescriptionUpdater<MyERPDbContext>(context).UpdateDatabaseDescriptions();

    public class DBDescriptionUpdater<TContext> where TContext : DbContext
    {
        private TContext context;

        public DBDescriptionUpdater(TContext context)
        {
            this.context = context;
        }

        public void UpdateDatabaseDescriptions()
        {
            var contextType = typeof(TContext);
            var props = contextType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            try
            {
                context.Database.OpenConnection();

                foreach (var prop in props)
                {
                    if (prop.PropertyType.InheritsOrImplements((typeof(DbSet<>))))
                    {
                        var tableType = prop.PropertyType.GetGenericArguments()[0];
                        SetTableDescriptions(tableType);
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        private void SetTableDescriptions(Type tableType)
        {
            var tableName = tableType.Name;

            var tableAttrs = tableType.GetCustomAttributes(typeof(TableAttribute), false);
            if (tableAttrs.Length > 0)
                tableName = ((TableAttribute)tableAttrs[0]).Name;

            var tableComment = CSCommentReader.Create(tableType);

            if (tableComment != null)
                SetDBDescription(tableName, null, tableComment.Summary);

            var props = tableType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var propComment = CSCommentReader.Create(prop);

                if (propComment == null) continue;

                var columnNameAttr = prop.GetCustomAttribute(typeof(ColumnAttribute), false);

                if (columnNameAttr != null)
                {
                    var columnName = ((ColumnAttribute)columnNameAttr).Name;
                    SetDBDescription(tableName, string.IsNullOrEmpty(columnName) ? prop.Name : columnName,
                         propComment.Summary);
                }

                else
                {
                    SetDBDescription(tableName, prop.Name, propComment.Summary);
                }
            }

        }

        private void SetDBDescription(string tableName, string columnName, string description)
        {
            string desc = string.Empty;

            if (string.IsNullOrEmpty(columnName))
                desc = "select [value] from fn_listextendedproperty('MS_Description','schema','dbo','table',N'" + tableName + "',null,null);";
            else
                desc = "select [value] from fn_listextendedproperty('MS_Description','schema','dbo','table',N'" + tableName + "','column',null) where objname = N'" + columnName + "';";

            var prevDesc = (string)RunSqlScalar(desc);

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@table", tableName),
                new SqlParameter("@desc", description)
            };

            string funcName = "sp_addextendedproperty";

            if (!string.IsNullOrEmpty(prevDesc))
                funcName = "sp_updateextendedproperty";

            string query = @"EXEC " + funcName + @" N'MS_Description', @desc, N'Schema', 'dbo', N'Table', @table";
         
            if (!string.IsNullOrEmpty(columnName))
            {
                query += ", N'Column', @column";
                parameters.Add(new SqlParameter("@column", columnName));
            }

            RunSql(query, parameters.ToArray());
        }

        DbCommand CreateCommand(string cmdText, params SqlParameter[] parameters)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = cmdText;

            foreach (var p in parameters)
                cmd.Parameters.Add(p);

            return cmd;
        }

        void RunSql(string cmdText, params SqlParameter[] parameters)
        {
            var cmd = CreateCommand(cmdText, parameters);
            cmd.ExecuteNonQuery();
        }
        object RunSqlScalar(string cmdText, params SqlParameter[] parameters)
        {
            var cmd = CreateCommand(cmdText, parameters);
            return cmd.ExecuteScalar();
        }
    }
}
