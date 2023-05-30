using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsAndTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDos",
                table: "ToDos");

            migrationBuilder.RenameTable(
                name: "ToDos",
                newName: "todos");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "todos",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "todos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DoneTime",
                table: "todos",
                newName: "done_time");

            migrationBuilder.AddPrimaryKey(
                name: "PK_todos",
                table: "todos",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_todos",
                table: "todos");

            migrationBuilder.RenameTable(
                name: "todos",
                newName: "ToDos");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ToDos",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ToDos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "done_time",
                table: "ToDos",
                newName: "DoneTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDos",
                table: "ToDos",
                column: "Id");
        }
    }
}
