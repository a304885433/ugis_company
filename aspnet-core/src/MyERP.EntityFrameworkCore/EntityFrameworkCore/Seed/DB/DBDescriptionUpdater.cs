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
            var sql = @"
BEGIN
     IF OBJECT_ID(@tableName) IS NULL
        RETURN;
    -- 忽略不存在的列
    IF @colName <> ''
        AND @colName IS NOT NULL
        BEGIN
            IF NOT EXISTS ( SELECT  1
                            FROM    syscolumns
                            WHERE   id = OBJECT_ID(@tableName)
                                    AND name = @colName )
                RETURN;
        END;
    -- 没有列名，视为增加表注释
    IF ( @colName = ''
         OR @colName IS NULL
       )
        BEGIN
            IF NOT EXISTS ( SELECT  A.name ,
                                    C.value
                            FROM    sys.tables A
                                    INNER JOIN sys.extended_properties C ON C.major_id = A.object_id
                                                              AND minor_id = 0
                            WHERE   A.name = @tableName )
                EXEC sys.sp_addextendedproperty @name = N'MS_Description',
                    @value = @desp, @level0type = N'SCHEMA',
                    @level0name = N'dbo', @level1type = N'TABLE',
                    @level1name = @tableName;
            ELSE
                EXEC sp_updateextendedproperty @name = N'MS_Description',
                    @value = @desp, @level0type = N'SCHEMA',
                    @level0name = N'dbo', @level1type = N'TABLE',
                    @level1name = @tableName;
        END;
    ELSE
        BEGIN
            IF NOT EXISTS ( SELECT  C.value AS column_description
                            FROM    sys.tables A
                                    INNER JOIN sys.columns B ON B.object_id = A.object_id
                                    INNER JOIN sys.extended_properties C ON C.major_id = B.object_id
                                                              AND C.minor_id = B.column_id
                            WHERE   A.name = @tableName
                                    AND B.name = @colName )
                EXEC sys.sp_addextendedproperty @name = N'MS_Description',
                    @value = @desp, @level0type = N'SCHEMA',
                    @level0name = N'dbo', @level1type = N'TABLE',
                    @level1name = @tableName, @level2type = N'COLUMN',
                    @level2name = @colName;
            ELSE
                EXEC sp_updateextendedproperty @name = N'MS_Description',
                    @value = @desp, @level0type = N'SCHEMA',
                    @level0name = N'dbo', @level1type = N'TABLE',
                    @level1name = @tableName, @level2type = N'COLUMN',
                    @level2name = @colName;

        END;
END
";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@tableName", tableName),
                new SqlParameter("@colName", description),
                new SqlParameter("@desp", description),
            };

            RunSql(sql, parameters.ToArray());
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
