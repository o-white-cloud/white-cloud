using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace white_cloud.data.migrations
{
    public static class FluentNpgExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax AsText(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsCustom("text");
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax AsNumeric(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsCustom("numeric");
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax AsSmallInt(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsCustom("smallint");
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax AsBigSerial(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsCustom("bigserial");
        }
        public static ICreateTableColumnOptionOrWithColumnSyntax AsTimestampWithoutTimezone(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsCustom("timestamp");
        }
        public static ICreateTableColumnOptionOrWithColumnSyntax AsJsonB(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsCustom("jsonb");
        }
        public static ICreateTableColumnOptionOrWithColumnSyntax AsArrayOfText(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsCustom("text[]");
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax AsBigInt(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsCustom("bigint");
        }
        public static ICreateTableColumnOptionOrWithColumnSyntax AsBoolean(this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax)
        {
            return createTableColumnAsTypeSyntax.AsCustom("bigint");
        }

        public static IAlterTableColumnAsTypeSyntax AlterAsText(this IAlterTableColumnAsTypeSyntax alterTableColumnAsTypeSyntax)
        {
            return (IAlterTableColumnAsTypeSyntax)alterTableColumnAsTypeSyntax.AsCustom("text").Nullable();
        }

        public static IAlterTableColumnAsTypeSyntax AlterAsNumeric(this IAlterTableColumnAsTypeSyntax alterTableColumnAsTypeSyntax)
        {
            return (IAlterTableColumnAsTypeSyntax)alterTableColumnAsTypeSyntax.AsCustom("numeric").Nullable();
        }

        public static IAlterTableColumnAsTypeSyntax AlterAsBigSerial(this IAlterTableColumnAsTypeSyntax alterTableColumnAsTypeSyntax)
        {
            return (IAlterTableColumnAsTypeSyntax)alterTableColumnAsTypeSyntax.AsCustom("bigserial").Nullable();
        }
        public static IAlterTableColumnAsTypeSyntax AlterAsTimestampWithoutTimezone(this IAlterTableColumnAsTypeSyntax alterTableColumnAsTypeSyntax)
        {
            return (IAlterTableColumnAsTypeSyntax)alterTableColumnAsTypeSyntax.AsCustom("timestamp").Nullable();
        }
        public static IAlterTableColumnAsTypeSyntax AlterAsJsonB(this IAlterTableColumnAsTypeSyntax alterTableColumnAsTypeSyntax)
        {
            return (IAlterTableColumnAsTypeSyntax)alterTableColumnAsTypeSyntax.AsCustom("jsonb").Nullable();
        }
        public static IAlterTableColumnAsTypeSyntax AlterAsArrayOfText(this IAlterTableColumnAsTypeSyntax alterTableColumnAsTypeSyntax)
        {
            return (IAlterTableColumnAsTypeSyntax)alterTableColumnAsTypeSyntax.AsCustom("text[]").Nullable();
        }

        public static IAlterTableColumnAsTypeSyntax AlterAsBigInt(this IAlterTableColumnAsTypeSyntax alterTableColumnAsTypeSyntax)
        {
            return (IAlterTableColumnAsTypeSyntax)alterTableColumnAsTypeSyntax.AsCustom("bigint").Nullable();
        }
        public static IAlterTableColumnAsTypeSyntax AlterAsBoolean(this IAlterTableColumnAsTypeSyntax alterTableColumnAsTypeSyntax)
        {
            return (IAlterTableColumnAsTypeSyntax)alterTableColumnAsTypeSyntax.AsCustom("boolean").Nullable();
        }

    }
}
