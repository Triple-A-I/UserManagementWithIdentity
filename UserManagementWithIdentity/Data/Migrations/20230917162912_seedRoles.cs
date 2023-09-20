using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementWithIdentity.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
              schema: "security",
              table: "Roles",
              columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
              values: new[] { Guid.NewGuid().ToString(), "user", "user".ToUpper(), Guid.NewGuid().ToString(),
              });
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString(),
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Roles]");
        }
    }
}
