using Godot;
using System;

public partial class Marte : Control
{
    private GameUI ui;

    private Random random =
        new Random();

    private int correctAnswer;

    private int meteorHealth;

    private int meteorMaxHealth;

    private int lives;

    private int currentQuestion =
        1;

    private bool canAnswer =
        true;

    public override void _Ready()
    {
        ui =
            new GameUI();

        AddChild(
            ui
        );

        ui.SetAnchorsAndOffsetsPreset(
            Control.LayoutPreset.FullRect
        );

        ui.RestartRequested +=
            RestartGame;

        ui.ExitRequested +=
            ExitGame;

        ui.DefeatRestartRequested +=
            RestartGame;

        ui.DefeatExitRequested +=
            ExitGame;

        ui.AnswerButton1.Pressed +=
            () => Shoot(
                ui.AnswerButton1
            );

        ui.AnswerButton2.Pressed +=
            () => Shoot(
                ui.AnswerButton2
            );

        ui.AnswerButton3.Pressed +=
            () => Shoot(
                ui.AnswerButton3
            );

        meteorMaxHealth =
            30;

        RestartGame();
    }

    private void GenerateNewQuestion()
    {
        GenerateOperation();

        GenerateAnswers();

        ui.MessageLabel.Text =
            "";

        canAnswer =
            true;
    }

    private void GenerateOperation()
    {
        int number1 =
            random.Next(1, 6);

        int number2 =
            random.Next(1, 6);

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
                    random.Next(-5, 6)
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
                    random.Next(-7, 8)
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
    }

    private async void Shoot(
        Button button
    )
    {
        if (!canAnswer)
            return;

        canAnswer =
            false;

        SetAnswerButtonsEnabled(
            false
        );

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
                10;

            ui.MessageLabel.Text =
                "🎯 Acertou!";

            ui.MeteorHealthLabel.Text =
                $"❤️ {meteorHealth} / {meteorMaxHealth}";
        }
        else
        {
            lives--;

            ui.MessageLabel.Text =
                "💥 Resposta errada!";

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
                ShowDefeat();

                return;
            }
        }

        await ToSignal(
            GetTree().CreateTimer(0.5),
            SceneTreeTimer.SignalName.Timeout
        );

        if (
            meteorHealth <= 0
        )
        {
            ui.MessageLabel.Text =
                "💥 Meteoro destruído!";

            await ToSignal(
                GetTree().CreateTimer(0.7),
                SceneTreeTimer.SignalName.Timeout
            );

            meteorHealth =
                meteorMaxHealth;

            currentQuestion++;
        }

        currentQuestion++;

        GenerateNewQuestion();

        SetAnswerButtonsEnabled(
            true
        );
    }

    private void ShowDefeat()
    {
        canAnswer =
            false;

        ui.ShowDefeat();
    }

    private void RestartGame()
    {
        meteorHealth =
            meteorMaxHealth;

        lives =
            3;

        currentQuestion =
            1;

        ui.HideDefeat();

        ui.UpdateLives(
            lives
        );

        ui.MeteorHealthLabel.Text =
            $"❤️ {meteorHealth} / {meteorMaxHealth}";

        GenerateNewQuestion();

        SetAnswerButtonsEnabled(
            true
        );
    }

    private void ExitGame()
    {
        Main main =
            GetParent() as Main;

        if (main != null)
        {
            main.ReturnToLobby();
        }
    }

    private void SetAnswerButtonsEnabled(
        bool enabled
    )
    {
        ui.AnswerButton1.Disabled =
            !enabled;

        ui.AnswerButton2.Disabled =
            !enabled;

        ui.AnswerButton3.Disabled =
            !enabled;
    }
}