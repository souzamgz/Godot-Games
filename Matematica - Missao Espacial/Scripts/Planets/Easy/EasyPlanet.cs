using Godot;
using System;
using System.Threading.Tasks;

public abstract partial class EasyPlanet : Control
{
    protected Random random = new Random();
    protected GameUI ui;
    protected int correctAnswer;
    protected int meteorHealth;
    protected int lives;
    protected bool questionAnswered;
    protected bool gameOver;

    protected int meteorsDestroyed;
    protected const int MeteorsRequired = 3;
    protected int missionScore;

    public abstract int PlanetIndex { get; }

    public override void _Ready()
    {
        ui = new GameUI();
        AddChild(ui);

        ui.RestartRequested += RestartGame;
        ui.ExitRequested += ExitToMenu;
        ui.DefeatRestartRequested += RestartGame;
        ui.DefeatExitRequested += ExitToMenu;
        ui.VictoryNextRequested += GoToNextPlanet;
        ui.VictoryMenuRequested += ExitToMenu;

        StartGame();
    }

    protected virtual void StartGame()
    {
        lives = GameUI.MaxLives;
        meteorHealth = GameUI.EasyMeteorMaxHealth;
        meteorsDestroyed = 0;
        missionScore = 0;
        questionAnswered = false;
        gameOver = false;

        ui.ResetGameVisuals();
        ui.HideVictory();

        StartQuestion();
    }

    protected void StartQuestion()
    {
        if (gameOver)
        {
            return;
        }

        questionAnswered = false;

        ui.MessageLabel.Text = "";

        ui.AnswerButton1.Disabled = false;
        ui.AnswerButton2.Disabled = false;
        ui.AnswerButton3.Disabled = false;

        GenerateOperation();
        GenerateAnswers();

        ui.UpdateMeteorHealth(meteorHealth);
        ui.UpdateLives(lives);
    }

    protected abstract void GenerateOperation();

    protected virtual void GenerateAnswers()
    {
        int answer1 = correctAnswer;
        int answer2;
        int answer3;

        do
        {
            answer2 = Math.Max(
                0,
                correctAnswer + random.Next(-5, 6)
            );
        }
        while (answer2 == correctAnswer);

        do
        {
            answer3 = Math.Max(
                0,
                correctAnswer + random.Next(-7, 8)
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

        ShuffleAnswers(answers);

        ui.AnswerButton1.Text = answers[0].ToString();
        ui.AnswerButton2.Text = answers[1].ToString();
        ui.AnswerButton3.Text = answers[2].ToString();

        ConnectAnswerButtons();
    }

    private void ShuffleAnswers(int[] answers)
    {
        for (int i = answers.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            int temp = answers[i];
            answers[i] = answers[j];
            answers[j] = temp;
        }
    }

    private void ConnectAnswerButtons()
    {
        ui.AnswerButton1.Pressed -= Answer1Pressed;
        ui.AnswerButton2.Pressed -= Answer2Pressed;
        ui.AnswerButton3.Pressed -= Answer3Pressed;

        ui.AnswerButton1.Pressed += Answer1Pressed;
        ui.AnswerButton2.Pressed += Answer2Pressed;
        ui.AnswerButton3.Pressed += Answer3Pressed;
    }

    private void Answer1Pressed()
    {
        Shoot(ui.AnswerButton1);
    }

    private void Answer2Pressed()
    {
        Shoot(ui.AnswerButton2);
    }

    private void Answer3Pressed()
    {
        Shoot(ui.AnswerButton3);
    }

    protected async void Shoot(Button button)
    {
        if (questionAnswered || gameOver)
        {
            return;
        }

        questionAnswered = true;

        ui.AnswerButton1.Disabled = true;
        ui.AnswerButton2.Disabled = true;
        ui.AnswerButton3.Disabled = true;

        int selectedAnswer = int.Parse(button.Text);

        bool correct = selectedAnswer == correctAnswer;

        Task buttonAnimation =
            Transitions.AnimateAnswer(button, correct);

        if (correct)
        {
            missionScore += 10;

            meteorHealth -=
                GameUI.EasyDamagePerCorrectAnswer;

            if (meteorHealth < 0)
            {
                meteorHealth = 0;
            }

            ui.MessageLabel.Text = "🎯 Acertou!";

            ui.UpdateMeteorHealth(meteorHealth);
        }
        else
        {
            lives--;

            ui.MessageLabel.Text = "💥 Ops!";

            ui.UpdateLives(lives);
        }

        await buttonAnimation;

        if (gameOver)
        {
            return;
        }

        await Transitions.Attack(
            ui.MeteorPanel,
            button,
            correct
        );

        if (gameOver)
        {
            return;
        }

        await ToSignal(
            GetTree().CreateTimer(0.20),
            SceneTreeTimer.SignalName.Timeout
        );

        if (lives <= 0)
        {
            Defeat();
            return;
        }

        if (meteorHealth <= 0)
        {
            meteorsDestroyed++;

            ui.MessageLabel.Text =
                $"💥 Meteoro destruído! {meteorsDestroyed}/{MeteorsRequired}";

            if (meteorsDestroyed >= MeteorsRequired)
            {
                await Transitions.MeteorDestroyed(
                    ui.MeteorPanel
                );

                if (gameOver)
                {
                    return;
                }

                await ToSignal(
                    GetTree().CreateTimer(0.25),
                    SceneTreeTimer.SignalName.Timeout
                );

                if (gameOver)
                {
                    return;
                }

                Victory();

                return;
            }

            await Transitions.MeteorDestroyed(
                ui.MeteorPanel
            );

            if (gameOver)
            {
                return;
            }

            await ToSignal(
                GetTree().CreateTimer(0.25),
                SceneTreeTimer.SignalName.Timeout
            );

            if (gameOver)
            {
                return;
            }

            meteorHealth =
                GameUI.EasyMeteorMaxHealth;

            ui.ResetMeteorVisual();

            StartQuestion();

            return;
        }

        StartQuestion();
    }

    private void Victory()
    {
        gameOver = true;
        questionAnswered = true;

        ui.AnswerButton1.Disabled = true;
        ui.AnswerButton2.Disabled = true;
        ui.AnswerButton3.Disabled = true;

        ui.MessageLabel.Text = "";

        Node current = GetParent();

        while (current != null)
        {
            if (current is Main main)
            {
                main.RegisterPlanetCompletion(
                    PlanetIndex,
                    missionScore
                );

                break;
            }

            current = current.GetParent();
        }

        ui.ShowVictory(
            meteorsDestroyed,
            missionScore
        );
    }

    private void Defeat()
    {
        gameOver = true;
        questionAnswered = true;

        ui.AnswerButton1.Disabled = true;
        ui.AnswerButton2.Disabled = true;
        ui.AnswerButton3.Disabled = true;

        ui.MessageLabel.Text = "";

        ui.ShowDefeat();
    }

    private void RestartGame()
    {
        gameOver = false;
        questionAnswered = false;

        StartGame();
    }

    private void GoToNextPlanet()
    {
        Node current = GetParent();

        while (current != null)
        {
            if (current is Main main)
            {
                main.SelectNextPlanet();
                return;
            }

            current = current.GetParent();
        }
    }

    private void ExitToMenu()
    {
        gameOver = true;
        questionAnswered = true;

        Node current = GetParent();

        while (current != null)
        {
            if (current is Main main)
            {
                main.ReturnToLobby();
                return;
            }

            current = current.GetParent();
        }

        GD.PrintErr(
            "Não foi possível encontrar o Main para voltar ao menu."
        );
    }
}