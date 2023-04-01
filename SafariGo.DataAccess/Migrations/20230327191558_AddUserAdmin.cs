using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafariGo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Identity].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Bio], [CoverPic], [FirstName], [LastName], [ProfilePic]) VALUES (N'cb6b725a-be78-40d1-9383-ec0efb7b5b8d', N'0788304304', N'0788304304', N'awad.520@outlook.com', N'AWAD.520@OUTLOOK.COM', 1, N'AQAAAAIAAYagAAAAEPr2JOiJbGi3CjlJ+04EK5f+Xia0NLgt6sTI03Jz7POxoNx00V+xMnaUuefraCkhaA==', N'MWJT3DJC46QIWYY5NCN4U5JLH6XQZYKB', N'a2bb219f-927e-4372-aaf6-7ec5ce16c7d5', N'0788304304', 0, 0, NULL, 1, 0, NULL, NULL, N'Mohammad', N'Awad', NULL)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [Identity].[Users] Where Id = 'cb6b725a-be78-40d1-9383-ec0efb7b5b8d'");

        }
    }
}
