using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeppersBpmn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CamundaDeploymentId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CamundaDefinitionId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Key = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    ResourceName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DeployedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CamundaInstanceId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ProcessKey = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    BusinessKey = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInstances", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDefinitions_Key",
                table: "ProcessDefinitions",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_CamundaInstanceId",
                table: "ProcessInstances",
                column: "CamundaInstanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_ProcessKey",
                table: "ProcessInstances",
                column: "ProcessKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessDefinitions");

            migrationBuilder.DropTable(
                name: "ProcessInstances");
        }
    }
}
