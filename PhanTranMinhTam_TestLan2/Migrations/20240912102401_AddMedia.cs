using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanTranMinhTam_TestLan2.Migrations
{
    public partial class AddMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.MediaId);
                });

            migrationBuilder.CreateTable(
                name: "RecurrenceRules",
                columns: table => new
                {
                    RecurrenceRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pattern = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrenceRules", x => x.RecurrenceRuleId);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlot",
                columns: table => new
                {
                    TimeSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlot", x => x.TimeSlotId);
                });

            migrationBuilder.CreateTable(
                name: "PlaySchedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusicId = table.Column<int>(type: "int", nullable: false),
                    RecurrenceRuleId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeSlotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaySchedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_PlaySchedule_Musics_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Musics",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaySchedule_RecurrenceRules_RecurrenceRuleId",
                        column: x => x.RecurrenceRuleId,
                        principalTable: "RecurrenceRules",
                        principalColumn: "RecurrenceRuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaySchedule_TimeSlot_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlot",
                        principalColumn: "TimeSlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaySchedule_MusicId",
                table: "PlaySchedule",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaySchedule_RecurrenceRuleId_MusicId_ScheduleId",
                table: "PlaySchedule",
                columns: new[] { "RecurrenceRuleId", "MusicId", "ScheduleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaySchedule_TimeSlotId",
                table: "PlaySchedule",
                column: "TimeSlotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaySchedule");

            migrationBuilder.DropTable(
                name: "Musics");

            migrationBuilder.DropTable(
                name: "RecurrenceRules");

            migrationBuilder.DropTable(
                name: "TimeSlot");
        }
    }
}
