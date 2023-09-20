using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementWithIdentity.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssignAdminToAllUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[UserRoles] (UserId, RoleId) SELECT '4d3552ce-0d5d-4f43-b8e5-57db429686bc', Id FROM  [security].Roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE From [dbo].[UserRoles] WHERE UserId = '4d3552ce-0d5d-4f43-b8e5-57db429686bc'");
        }
    }
}
