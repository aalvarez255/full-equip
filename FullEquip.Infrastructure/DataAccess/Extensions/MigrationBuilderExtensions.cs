using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace FullEquip.Infrastructure.DataAccess.Extensions
{
    public static class MigrationBuilderExtensions
    {
        public static MigrationBuilder RunFile(this MigrationBuilder builder, string filename)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var sqlPath = Path.Combine(
                AppContext.BaseDirectory,
                "DataAccess/SqlScripts",
                filename);

            if (File.Exists(sqlPath))
                builder.Sql(File.ReadAllText(sqlPath));

            else
                throw new Exception($"Migration .sql file not found: ${filename}");

            return builder;
        }
    }
}
