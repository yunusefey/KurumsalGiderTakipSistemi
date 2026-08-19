using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KurumsalGiderTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseCategories_HarcamaKategoriId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_KullaniciId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmanId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseCategories",
                table: "ExpenseCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Kullanicilar");

            migrationBuilder.RenameTable(
                name: "Expenses",
                newName: "Harcamalar");

            migrationBuilder.RenameTable(
                name: "ExpenseCategories",
                newName: "HarcamaKategorileri");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Departmanlar");

            migrationBuilder.RenameColumn(
                name: "Rol",
                table: "Kullanicilar",
                newName: "Role");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DepartmanId",
                table: "Kullanicilar",
                newName: "IX_Kullanicilar_DepartmanId");

            migrationBuilder.RenameColumn(
                name: "RedSebebi",
                table: "Harcamalar",
                newName: "RedNedeni");

            migrationBuilder.RenameColumn(
                name: "OlusturmaTarihi",
                table: "Harcamalar",
                newName: "OlusturulmaTarihi");

            migrationBuilder.RenameColumn(
                name: "Aralik",
                table: "Harcamalar",
                newName: "Tutar");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_KullaniciId",
                table: "Harcamalar",
                newName: "IX_Harcamalar_KullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_HarcamaKategoriId",
                table: "Harcamalar",
                newName: "IX_Harcamalar_HarcamaKategoriId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Harcamalar",
                table: "Harcamalar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HarcamaKategorileri",
                table: "HarcamaKategorileri",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departmanlar",
                table: "Departmanlar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Harcamalar_HarcamaKategorileri_HarcamaKategoriId",
                table: "Harcamalar",
                column: "HarcamaKategoriId",
                principalTable: "HarcamaKategorileri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Harcamalar_Kullanicilar_KullaniciId",
                table: "Harcamalar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Departmanlar_DepartmanId",
                table: "Kullanicilar",
                column: "DepartmanId",
                principalTable: "Departmanlar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Harcamalar_HarcamaKategorileri_HarcamaKategoriId",
                table: "Harcamalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Harcamalar_Kullanicilar_KullaniciId",
                table: "Harcamalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Departmanlar_DepartmanId",
                table: "Kullanicilar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Harcamalar",
                table: "Harcamalar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HarcamaKategorileri",
                table: "HarcamaKategorileri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departmanlar",
                table: "Departmanlar");

            migrationBuilder.RenameTable(
                name: "Kullanicilar",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Harcamalar",
                newName: "Expenses");

            migrationBuilder.RenameTable(
                name: "HarcamaKategorileri",
                newName: "ExpenseCategories");

            migrationBuilder.RenameTable(
                name: "Departmanlar",
                newName: "Departments");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Rol");

            migrationBuilder.RenameIndex(
                name: "IX_Kullanicilar_DepartmanId",
                table: "Users",
                newName: "IX_Users_DepartmanId");

            migrationBuilder.RenameColumn(
                name: "Tutar",
                table: "Expenses",
                newName: "Aralik");

            migrationBuilder.RenameColumn(
                name: "RedNedeni",
                table: "Expenses",
                newName: "RedSebebi");

            migrationBuilder.RenameColumn(
                name: "OlusturulmaTarihi",
                table: "Expenses",
                newName: "OlusturmaTarihi");

            migrationBuilder.RenameIndex(
                name: "IX_Harcamalar_KullaniciId",
                table: "Expenses",
                newName: "IX_Expenses_KullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_Harcamalar_HarcamaKategoriId",
                table: "Expenses",
                newName: "IX_Expenses_HarcamaKategoriId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseCategories",
                table: "ExpenseCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseCategories_HarcamaKategoriId",
                table: "Expenses",
                column: "HarcamaKategoriId",
                principalTable: "ExpenseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_KullaniciId",
                table: "Expenses",
                column: "KullaniciId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmanId",
                table: "Users",
                column: "DepartmanId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
