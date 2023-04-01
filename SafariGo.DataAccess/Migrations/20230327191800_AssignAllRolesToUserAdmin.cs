using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafariGo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AssignAllRolesToUserAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Identity].[UserRoles] (UserId,RoleId) SELECT 'cb6b725a-be78-40d1-9383-ec0efb7b5b8d', Id From [Identity].[Roles]");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Identity].[Users] WHERE Id ='cb6b725a-be78-40d1-9383-ec0efb7b5b8d'");
        }
    }
}
