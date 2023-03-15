﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafariGo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesToApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table:"Roles",
                schema:"Identity",
                columns: new[] {"Id", "Name", "NormalizedName" , "ConcurrencyStamp" },
                values:new object[] {Guid.NewGuid().ToString(),"Admin","Admin".ToUpper(),Guid.NewGuid().ToString()}
                ); 
            migrationBuilder.InsertData(
                table: "Roles",
                schema: "Identity",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "User", "User".ToUpper(), Guid.NewGuid().ToString() }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [Identity].[Roles]");
        }
    }
}
