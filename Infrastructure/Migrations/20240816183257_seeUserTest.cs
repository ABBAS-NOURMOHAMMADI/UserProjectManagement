using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeUserTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"USE [UserProject]
GO
INSERT INTO [dbo].[Users] ([UserId],[UserName],[NormalizedUserName],[FirstName],[LastName],[Password],[Description],[CreatedBy],[CreatedAt])
VALUES('44650A30-F628-4FA5-9741-90E4605DB5A1','Test','TEST','test','test','test','user test','11111111-1111-1111-1111-111111111111',GETDATE())
GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
