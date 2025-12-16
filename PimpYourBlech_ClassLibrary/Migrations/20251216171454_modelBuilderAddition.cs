using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PimpYourBlech_ClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class modelBuilderAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityAnswers_CommunityAnswers_CommunityAnswerId",
                table: "CommunityAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityAnswers_CommunityQuestions_CommunityQuestionId",
                table: "CommunityAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityQuestions_CommunityQuestions_QuestionId",
                table: "CommunityQuestions");

            migrationBuilder.DropIndex(
                name: "IX_CommunityQuestions_QuestionId",
                table: "CommunityQuestions");

            migrationBuilder.DropIndex(
                name: "IX_CommunityAnswers_CommunityAnswerId",
                table: "CommunityAnswers");

            migrationBuilder.DropIndex(
                name: "IX_CommunityAnswers_CommunityQuestionId",
                table: "CommunityAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "CommunityQuestions");

            migrationBuilder.DropColumn(
                name: "CommunityAnswerId",
                table: "CommunityAnswers");

            migrationBuilder.DropColumn(
                name: "CommunityQuestionId",
                table: "CommunityAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityAnswers_QuestionId",
                table: "CommunityAnswers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityAnswers_CommunityQuestions_QuestionId",
                table: "CommunityAnswers",
                column: "QuestionId",
                principalTable: "CommunityQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityAnswers_CommunityQuestions_QuestionId",
                table: "CommunityAnswers");

            migrationBuilder.DropIndex(
                name: "IX_CommunityAnswers_QuestionId",
                table: "CommunityAnswers");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "CommunityQuestions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommunityAnswerId",
                table: "CommunityAnswers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommunityQuestionId",
                table: "CommunityAnswers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunityQuestions_QuestionId",
                table: "CommunityQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityAnswers_CommunityAnswerId",
                table: "CommunityAnswers",
                column: "CommunityAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityAnswers_CommunityQuestionId",
                table: "CommunityAnswers",
                column: "CommunityQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityAnswers_CommunityAnswers_CommunityAnswerId",
                table: "CommunityAnswers",
                column: "CommunityAnswerId",
                principalTable: "CommunityAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityAnswers_CommunityQuestions_CommunityQuestionId",
                table: "CommunityAnswers",
                column: "CommunityQuestionId",
                principalTable: "CommunityQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityQuestions_CommunityQuestions_QuestionId",
                table: "CommunityQuestions",
                column: "QuestionId",
                principalTable: "CommunityQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
