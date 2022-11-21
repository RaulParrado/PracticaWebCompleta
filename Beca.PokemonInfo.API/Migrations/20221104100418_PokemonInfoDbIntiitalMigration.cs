using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beca.PokemonInfo.API.Migrations
{
    public partial class PokemonInfoDbIntiitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attacks_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pokemons",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Pokemon tipo planta de la primera generacion, te lanza un látigo cepa y te deja moratón", "Bulbasaur" });

            migrationBuilder.InsertData(
                table: "Pokemons",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Pokemon tipo de agua de la primera generación, es el pokemon favorito de los verdaderos amantes de pokemon.", "Charmander" });

            migrationBuilder.InsertData(
                table: "Pokemons",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Pokemon tipo de agua de la primera generación, tiene una concha a su espalda y está siempre calmado.", "Squirtle" });

            migrationBuilder.InsertData(
                table: "Attacks",
                columns: new[] { "Id", "Description", "Name", "PokemonId" },
                values: new object[] { 1, "Placaje causa daño y no tiene ningún efecto secundario. Este movimiento tiene una potencia de 35 y una precisión del 95%.", "Placaje", 1 });

            migrationBuilder.InsertData(
                table: "Attacks",
                columns: new[] { "Id", "Description", "Name", "PokemonId" },
                values: new object[] { 2, "Látigo cepa causa daño y no tiene ningún efecto secundario. El movimiento tiene una potencia de 35 y 10 PP.", "Látigo Cepa", 1 });

            migrationBuilder.InsertData(
                table: "Attacks",
                columns: new[] { "Id", "Description", "Name", "PokemonId" },
                values: new object[] { 3, "Látigo baja en un nivel la defensa del oponente. En combates dobles y triples afecta a todos los oponentes adyacentes. No afecta a Pokémon con las habilidades cuerpo puro, humo blanco o sacapecho.\r\n\r\n", "Látigo", 2 });

            migrationBuilder.InsertData(
                table: "Attacks",
                columns: new[] { "Id", "Description", "Name", "PokemonId" },
                values: new object[] { 4, "Lanzallamas causa daño y tiene una probabilidad del 10% de quemar al objetivo. Lanzallamas tiene una potencia de 95.\r\n", "Lanzallamas", 2 });

            migrationBuilder.InsertData(
                table: "Attacks",
                columns: new[] { "Id", "Description", "Name", "PokemonId" },
                values: new object[] { 5, "Arañazo causa daño y no tiene ningún efecto secundario.", "Arañazo", 3 });

            migrationBuilder.InsertData(
                table: "Attacks",
                columns: new[] { "Id", "Description", "Name", "PokemonId" },
                values: new object[] { 6, "Pistola agua causa daño y no tiene ningún efecto secundario.", "Pistola Agua", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_PokemonId",
                table: "Attacks",
                column: "PokemonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attacks");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
