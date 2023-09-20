using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementWithIdentity.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'4d3552ce-0d5d-4f43-b8e5-57db429686bc', N'Admin', N'ADMIN', N'Admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAENEGFRmaMsMZjEnnQq28ycukKLZQQovuZBFbsyrUfwy7JxsEHgp/TtTApMuUrEVrRw==', N'YDNQK5ZLNLWY2YHHUZEAO4GMAN67ZOBI', N'a26db6b1-c175-4a0b-b385-19eb99b0f280', N'+249118582527', 0, 0, NULL, 1, 0, N'Admin', N'Admin', NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Roles] WHERE Id = '4d3552ce-0d5d-4f43-b8e5-57db429686bc'");
        }
    }
}
