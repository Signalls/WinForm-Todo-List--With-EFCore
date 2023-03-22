using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMe.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedToremo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Statuses(Name) VALUES ('Todo');");
            migrationBuilder.Sql("INSERT INTO Statuses(Name) VALUES ('In Progress');");
            migrationBuilder.Sql("INSERT INTO Statuses(Name) VALUES ('Done');");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //remember to include delete statements
        }
    }
}
