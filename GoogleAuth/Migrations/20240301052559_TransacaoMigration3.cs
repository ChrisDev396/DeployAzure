using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoogleAuth.Migrations
{
    /// <inheritdoc />
    public partial class TransacaoMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Usuarios_UsuarioNomeUsuario",
                table: "Itens");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Usuarios_UsuarioNomeUsuario",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_UsuarioNomeUsuario",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Itens_UsuarioNomeUsuario",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "UsuarioNomeUsuario",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Shild",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "UsuarioNomeUsuario",
                table: "Itens");

            migrationBuilder.RenameColumn(
                name: "NomeUsuario",
                table: "Transacoes",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "NomeUsuario",
                table: "Itens",
                newName: "ShildNome");

            migrationBuilder.UpdateData(
                table: "Transacoes",
                keyColumn: "NomeTitular",
                keyValue: null,
                column: "NomeTitular",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NomeTitular",
                table: "Transacoes",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ShildNome",
                table: "Transacoes",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Itens",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Modelo",
                table: "Itens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShildNome",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Itens");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Transacoes",
                newName: "NomeUsuario");

            migrationBuilder.RenameColumn(
                name: "ShildNome",
                table: "Itens",
                newName: "NomeUsuario");

            migrationBuilder.AlterColumn<string>(
                name: "NomeTitular",
                table: "Transacoes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "Transacoes",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioNomeUsuario",
                table: "Transacoes",
                type: "varchar(10)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Shild",
                table: "Itens",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioNomeUsuario",
                table: "Itens",
                type: "varchar(10)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_UsuarioNomeUsuario",
                table: "Transacoes",
                column: "UsuarioNomeUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_UsuarioNomeUsuario",
                table: "Itens",
                column: "UsuarioNomeUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Usuarios_UsuarioNomeUsuario",
                table: "Itens",
                column: "UsuarioNomeUsuario",
                principalTable: "Usuarios",
                principalColumn: "NomeUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Usuarios_UsuarioNomeUsuario",
                table: "Transacoes",
                column: "UsuarioNomeUsuario",
                principalTable: "Usuarios",
                principalColumn: "NomeUsuario");
        }
    }
}
