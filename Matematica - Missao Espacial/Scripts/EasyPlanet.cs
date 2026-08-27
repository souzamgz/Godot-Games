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

    // Identifica cada partida para impedir que animações
    // antigas interfiram depois de um reinício.
    private int gameSessionId;

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
        // Nova sessão de jogo.
        gameSessionId++;

        lives = GameUI.MaxLives;

        // O meteoro SEMPRE começa com a vida máxima
        // quando uma nova partida é iniciada.
        meteorHealth = GameUI.EasyMeteorMaxHealth;

        questionAnswered = false;
        gameOver = false;

        ui.ResetGameVisuals();

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

        // Mostra a vida ATUAL do meteoro.
        // Não reseta a vida aqui.
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

        // Guarda a sessão atual.
        // Se o jogador reiniciar durante a animação,
        // essa execução antiga será ignorada.
        int currentSession = gameSessionId;

        questionAnswered = true;

        ui.AnswerButton1.Disabled = true;
        ui.AnswerButton2.Disabled = true;
        ui.AnswerButton3.Disabled = true;

        int selectedAnswer;

        if (!int.TryParse(button.Text, out selectedAnswer))
        {
            questionAnswered = false;

            ui.AnswerButton1.Disabled = false;
            ui.AnswerButton2.Disabled = false;
            ui.AnswerButton3.Disabled = false;

            return;
        }

        bool correct =
            selectedAnswer == correctAnswer;

        // Animação do botão:
        // verde se acertou
        // vermelho se errou
        Task buttonAnimation =
            Transitions.AnimateAnswer(
                button,
                correct
            );

        if (correct)
        {
            // ACERTO:
            // causa dano no meteoro.
            meteorHealth -=
                GameUI.EasyDamagePerCorrectAnswer;

            if (meteorHealth < 0)
            {
                meteorHealth = 0;
            }

            ui.MessageLabel.Text =
                "🎯 Acertou!";

            ui.UpdateMeteorHealth(
                meteorHealth
            );
        }
        else
        {
            // ERRO:
            // somente perde uma vida.
            // O meteoro NÃO perde vida.
            lives--;

            ui.MessageLabel.Text =
                "💥 Ops!";

            ui.UpdateLives(
                lives
            );
        }

        await buttonAnimation;

        // Se o jogador reiniciou ou saiu durante
        // a animação, abandona esta execução antiga.
        if (
            gameOver ||
            currentSession != gameSessionId
        )
        {
            return;
        }

        // Ataque visual.
        // No erro ele não causa dano no meteoro.
        await Transitions.Attack(
            ui.MeteorPanel,
            button,
            correct
        );

        if (
            gameOver ||
            currentSession != gameSessionId
        )
        {
            return;
        }

        await ToSignal(
            GetTree().CreateTimer(0.20),
            SceneTreeTimer.SignalName.Timeout
        );

        if (
            gameOver ||
            currentSession != gameSessionId
        )
        {
            return;
        }

        // ==========================================
        // DERROTA
        // ==========================================

        if (lives <= 0)
        {
            Defeat();

            return;
        }

        // ==========================================
        // METEORO DESTRUÍDO
        // ==========================================

        if (meteorHealth <= 0)
        {
            ui.MessageLabel.Text =
                "💥 Meteoro destruído!";

            await Transitions.MeteorDestroyed(
                ui.MeteorPanel
            );

            if (
                gameOver ||
                currentSession != gameSessionId
            )
            {
                return;
            }

            await ToSignal(
                GetTree().CreateTimer(0.25),
                SceneTreeTimer.SignalName.Timeout
            );

            if (
                gameOver ||
                currentSession != gameSessionId
            )
            {
                return;
            }

            // ==========================================
            // IMPORTANTE:
            // O meteoro foi destruído.
            // Agora criamos o próximo com VIDA CHEIA.
            // ==========================================

            meteorHealth =
                GameUI.EasyMeteorMaxHealth;

            ui.UpdateMeteorHealth(
                meteorHealth
            );

            // Prepara uma nova questão.
            // As vidas continuam iguais.
            StartQuestion();

            return;
        }

        // ==========================================
        // METEORO AINDA VIVO
        // ==========================================

        StartQuestion();
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
        // Aumentar o ID invalida qualquer Shoot()
        // antigo que ainda esteja esperando uma animação.
        gameSessionId++;

        gameOver = false;
        questionAnswered = false;

        StartGame();
    }

    private void ExitToMenu()
    {
        // Invalida animações e perguntas pendentes.
        gameSessionId++;

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