﻿using Microsoft.EntityFrameworkCore.Migrations;
using Project.Domain.Constants;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(

                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values:new object[] {Guid.NewGuid().ToString(),UserRoles.User,UserRoles.User.ToUpper(),Guid.NewGuid().ToString()}

                );
            migrationBuilder.InsertData(

             table: "AspNetRoles",
             columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
             values: new object[] { Guid.NewGuid().ToString(), UserRoles.Admin, UserRoles.Admin.ToUpper(), Guid.NewGuid().ToString() }

             );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");  
        }
    }
}
