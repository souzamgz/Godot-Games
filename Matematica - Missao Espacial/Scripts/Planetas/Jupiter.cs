using Godot;
using System;

public partial class Jupiter : Control
{
    private Random random = new Random();

    private GameUI ui;

    private int correctAnswer;

    private int meteorHealth;

    private int lives;

    private int currentQuestion = 1;

    private bool questionAnswered = false;

    public override void _Ready()
    {
        ui = new GameUI();

        AddChild(ui);

        ui.RestartRequested += RestartGame;
        ui.ExitRequested += ExitToMenu;

        ui.DefeatRestartRequested += RestartGame;
        ui.DefeatExitRequested += ExitToMenu;

        StartGame();
    }

    private void StartGame()
    {
        lives =
            GameUI.MaxLives;

        meteorHealth =
            GameUI.EasyMeteorMaxHealth;

        currentQuestion =
            1;

        questionAnswered =
            false;

        ui.ResetGameVisuals();

        StartQuestion();
    }

    private void StartQuestion()
    {
        questionAnswered =
            false;

        ui.MessageLabel.Text =
            "";

        GenerateOperation();

        GenerateAnswers();

        ui.UpdateMeteorHealth(
            meteorHealth
        );
    }

    private void GenerateOperation()
    {
        int number1 =
            random.Next(1, 10);

        int number2 =
            random.Next(1, 10);

        correctAnswer =
            number1 * number2;

        ui.OperationLabel.Text =
            $"{number1} × {number2}";
    }

    private void GenerateAnswers()
    {
        int answer1 =
            correctAnswer;

        int answer2;

        int answer3;

        do
        {
            answer2 =
                Math.Max(
                    0,
                    correctAnswer +
                    random.Next(-7, 8)
                );
        }
        while (
            answer2 == correctAnswer
        );

        do
        {
            answer3 =
                Math.Max(
                    0,
                    correctAnswer +
                    random.Next(-10, 11)
                );
        }
        while (
            answer3 == correctAnswer ||
            answer3 == answer2
        );

        int[] answers =
        {
            answer1,
            answer2,
            answer3
        };

        for (
            int i = answers.Length - 1;
            i > 0;
            i--
        )
        {
            int j =
                random.Next(i + 1);

            int temp =
                answers[i];

            answers[i] =
                answers[j];

            answers[j] =
                temp;
        }

        ui.AnswerButton1.Text =
            answers[0].ToString();

        ui.AnswerButton2.Text =
            answers[1].ToString();

        ui.AnswerButton3.Text =
            answers[2].ToString();

        ui.AnswerButton1.Pressed -=
            Answer1Pressed;

        ui.AnswerButton2.Pressed -=
            Answer2Pressed;

        ui.AnswerButton3.Pressed -=
            Answer3Pressed;

        ui.AnswerButton1.Pressed +=
            Answer1Pressed;

        ui.AnswerButton2.Pressed +=
            Answer2Pressed;

        ui.AnswerButton3.Pressed +=
            Answer3Pressed;
    }

    private void Answer1Pressed()
    {
        Shoot(
            ui.AnswerButton1
        );
    }

    private void Answer2Pressed()
    {
        Shoot(
            ui.AnswerButton2
        );
    }

    private void Answer3Pressed()
    {
        Shoot(
            ui.AnswerButton3
        );
    }

    private async void Shoot(
        Button button
    )
    {
        if (
            questionAnswered
        )
        {
            return;
        }

        questionAnswered =
            true;

        ui.EnableRestartButton();

        int selectedAnswer =
            int.Parse(
                button.Text
            );

        if (
            selectedAnswer ==
            correctAnswer
        )
        {
            meteorHealth -=
                GameUI.EasyDamagePerCorrectAnswer;

            ui.MessageLabel.Text =
                "🎯 Acertou!";

            ui.UpdateMeteorHealth(
                meteorHealth
            );

            await ToSignal(
                GetTree().CreateTimer(0.5),
                SceneTreeTimer.SignalName.Timeout
            );

            if (
                meteorHealth <= 0
            )
            {
                meteorHealth =
                    0;

                ui.UpdateMeteorHealth(
                    meteorHealth
                );

                ui.MessageLabel.Text =
                    "💥 Meteoro destruído!";

                await ToSignal(
                    GetTree().CreateTimer(0.8),
                    SceneTreeTimer.SignalName.Timeout
                );

                currentQuestion++;

                StartGame();

                return;
            }
        }
        else
        {
            lives--;

            ui.MessageLabel.Text =
                "💥 Ops! Tente novamente.";

            ui.UpdateLives(
                lives
            );

            await ToSignal(
                GetTree().CreateTimer(0.5),
                SceneTreeTimer.SignalName.Timeout
            );

            if (
                lives <= 0
            )
            {
                Defeat();

                return;
            }
        }

        await ToSignal(
            GetTree().CreateTimer(0.3),
            SceneTreeTimer.SignalName.Timeout
        );

        StartQuestion();
    }

    private void Defeat()
    {
        questionAnswered =
            true;

        ui.MessageLabel.Text =
            "";

        ui.ShowDefeat();
    }

    private void RestartGame()
    {
        StartGame();
    }

    private void ExitToMenu()
    {
        GetTree().ChangeSceneToFile(
            "res://Cenas/Main.tscn"
        );
    }
}