using Godot;
using System;

public abstract partial class EasyPlanet : Control
{
    protected Random random = new Random();

    protected GameUI ui;

    protected int correctAnswer;

    protected int meteorHealth;

    protected int lives;

    protected bool questionAnswered;

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

    protected virtual void StartGame()
    {
        lives =
            GameUI.MaxLives;

        meteorHealth =
            GameUI.EasyMeteorMaxHealth;

        questionAnswered =
            false;

        ui.ResetGameVisuals();

        StartQuestion();
    }

    protected void StartQuestion()
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

    protected abstract void GenerateOperation();

    protected virtual void GenerateAnswers()
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

        ShuffleAnswers(
            answers
        );

        ui.AnswerButton1.Text =
            answers[0].ToString();

        ui.AnswerButton2.Text =
            answers[1].ToString();

        ui.AnswerButton3.Text =
            answers[2].ToString();

        ConnectAnswerButtons();
    }

    private void ShuffleAnswers(
        int[] answers
    )
    {
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
    }

    private void ConnectAnswerButtons()
    {
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

    protected async void Shoot(
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

        bool correct =
            selectedAnswer ==
            correctAnswer;

        if (correct)
        {
            HandleCorrectAnswer();
        }
        else
        {
            HandleWrongAnswer();
        }

        await ToSignal(
            GetTree().CreateTimer(0.5),
            SceneTreeTimer.SignalName.Timeout
        );

        if (!correct && lives <= 0)
        {
            Defeat();

            return;
        }

        if (meteorHealth <= 0)
        {
            await HandleMeteorDestroyed();

            return;
        }

        StartQuestion();
    }

    private void HandleCorrectAnswer()
    {
        meteorHealth -=
            GameUI.EasyDamagePerCorrectAnswer;

        if (
            meteorHealth < 0
        )
        {
            meteorHealth =
                0;
        }

        ui.MessageLabel.Text =
            "🎯 Acertou!";

        ui.UpdateMeteorHealth(
            meteorHealth
        );
    }

    private void HandleWrongAnswer()
    {
        lives--;

        ui.MessageLabel.Text =
            "💥 Ops! Tente novamente.";

        ui.UpdateLives(
            lives
        );

        // Erros NÃO causam dano ao meteoro.
        ui.UpdateMeteorHealth(
            meteorHealth
        );
    }

    private async System.Threading.Tasks.Task HandleMeteorDestroyed()
    {
        ui.MessageLabel.Text =
            "💥 Meteoro destruído!";

        ui.UpdateMeteorHealth(
            0
        );

        await ToSignal(
            GetTree().CreateTimer(0.8),
            SceneTreeTimer.SignalName.Timeout
        );

        StartGame();
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