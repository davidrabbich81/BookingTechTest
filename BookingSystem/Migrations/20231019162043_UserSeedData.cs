using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Migrations
{
    /// <summary>
    /// Seeds an initial user into the user table
    /// </summary>
    public partial class UserSeedData : Migration
    {
        private string emailAddress = "dj@djvaleting.com";

        /// <summary>
        /// When the migration is applied
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $@"INSERT INTO [Users] 
                    (UserId,EmailAddress,Password,DateCreated,Deleted) 
                    VALUES 
                    (NEWID(),'{emailAddress}','c73ocOI3ulrl6CDd1CfKce9HUIjnI0KmsJl7Qcq/WGs=',GetDate(),0)"
            );
        }

        /// <summary>
        /// When the migration is un-applied by the system
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $@"DELETE FROM [Users] WHERE EmailAddress = '{emailAddress}'"
            );
        }
    }
}
