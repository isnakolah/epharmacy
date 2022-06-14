using Microsoft.EntityFrameworkCore.Migrations;

namespace EPharmacy.Infrastructure.Persistence.Migrations;

public partial class RemoveDefaultValue : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<bool>(
            name: "Stocked",
            table: "PharmaceuticalQuotationItems",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<bool>(
            name: "Stocked",
            table: "PharmaceuticalQuotationItems",
            type: "bit",
            nullable: false,
            defaultValue: true,
            oldClrType: typeof(bool),
            oldType: "bit");
    }
}