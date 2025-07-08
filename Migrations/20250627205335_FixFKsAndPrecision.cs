using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModuloProduccionPiscina.Migrations
{
    /// <inheritdoc />
    public partial class FixFKsAndPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoDetalles_Pedidos_PedidoIdPedido",
                table: "PedidoDetalles");

            migrationBuilder.DropIndex(
                name: "IX_PedidoDetalles_PedidoIdPedido",
                table: "PedidoDetalles");

            migrationBuilder.DropColumn(
                name: "PedidoIdPedido",
                table: "PedidoDetalles");

            migrationBuilder.AddColumn<int>(
                name: "PiscinaId",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "PedidoDetalles",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdPiscina",
                table: "Pedidos",
                column: "IdPiscina");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDetalles_IdPedido",
                table: "PedidoDetalles",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDetalles_IdProducto",
                table: "PedidoDetalles",
                column: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoDetalles_Pedidos_IdPedido",
                table: "PedidoDetalles",
                column: "IdPedido",
                principalTable: "Pedidos",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoDetalles_Productos_IdProducto",
                table: "PedidoDetalles",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Piscinas_IdPiscina",
                table: "Pedidos",
                column: "IdPiscina",
                principalTable: "Piscinas",
                principalColumn: "IdPiscina",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoDetalles_Pedidos_IdPedido",
                table: "PedidoDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoDetalles_Productos_IdProducto",
                table: "PedidoDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Piscinas_IdPiscina",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdPiscina",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_PedidoDetalles_IdPedido",
                table: "PedidoDetalles");

            migrationBuilder.DropIndex(
                name: "IX_PedidoDetalles_IdProducto",
                table: "PedidoDetalles");

            migrationBuilder.DropColumn(
                name: "PiscinaId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "PedidoDetalles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "PedidoIdPedido",
                table: "PedidoDetalles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDetalles_PedidoIdPedido",
                table: "PedidoDetalles",
                column: "PedidoIdPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoDetalles_Pedidos_PedidoIdPedido",
                table: "PedidoDetalles",
                column: "PedidoIdPedido",
                principalTable: "Pedidos",
                principalColumn: "IdPedido");
        }
    }
}
