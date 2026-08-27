using Godot;
using System;

public partial class GameUI : Control
{
    public const int EasyMeteorMaxHealth = 30;

    public const int EasyDamagePerCorrectAnswer = 10;

    public const int MaxLives = 3;

    public Label OperationLabel { get; private set; }

    public Label MeteorHealthLabel { get; private set; }

    public Label LivesLabel { get; private set; }

    public Label MessageLabel { get; private set; }

    public Button AnswerButton1 { get; private set; }

    public Button AnswerButton2 { get; private set; }

    public Button AnswerButton3 { get; private set; }

    public Button RestartButton { get; private set; }

    public Button ExitButton { get; private set; }

    public event Action RestartRequested;

    public event Action ExitRequested;

    public event Action DefeatRestartRequested;

    public event Action DefeatExitRequested;

    private Panel defeatPanel;

    public override void _Ready()
    {
        CreateInterface();
    }

    private void CreateInterface()
    {
        CreateTopButtons();

        CreateLives();

        CreateQuestionPanel();

        CreateMeteor();

        CreateAnswers();

        CreateMessage();

        CreateDefeatPanel();
    }

    private void CreateTopButtons()
    {
        RestartButton =
            new Button();

        RestartButton.Text =
            "REINICIAR";

        RestartButton.Position =
            new Vector2(900, 25);

        RestartButton.Size =
            new Vector2(135, 45);

        RestartButton.AddThemeFontSizeOverride(
            "font_size",
            16
        );

        RestartButton.Modulate =
            new Color(
                1f,
                1f,
                1f,
                0.4f
            );

        AddChild(
            RestartButton
        );

        RestartButton.Pressed +=
            () => RestartRequested?.Invoke();

        ExitButton =
            new Button();

        ExitButton.Text =
            "SAIR";

        ExitButton.Position =
            new Vector2(1045, 25);

        ExitButton.Size =
            new Vector2(110, 45);

        ExitButton.AddThemeFontSizeOverride(
            "font_size",
            16
        );

        AddChild(
            ExitButton
        );

        ExitButton.Pressed +=
            () => ExitRequested?.Invoke();
    }

    public void EnableRestartButton()
    {
        RestartButton.Modulate =
            new Color(
                1f,
                1f,
                1f,
                1f
            );
    }

    public void DisableRestartButtonVisual()
    {
        RestartButton.Modulate =
            new Color(
                1f,
                1f,
                1f,
                0.4f
            );
    }

    private void CreateLives()
    {
        LivesLabel =
            new Label();

        LivesLabel.Text =
            "VIDAS: ❤️ ❤️ ❤️";

        LivesLabel.Position =
            new Vector2(30, 25);

        LivesLabel.Size =
            new Vector2(300, 45);

        LivesLabel.AddThemeFontSizeOverride(
            "font_size",
            22
        );

        AddChild(
            LivesLabel
        );
    }

    private void CreateQuestionPanel()
    {
        ColorRect background =
            new ColorRect();

        background.Position =
            new Vector2(350, 145);

        background.Size =
            new Vector2(500, 125);

        background.Color =
            new Color("#151F4A");

        AddChild(
            background
        );

        OperationLabel =
            new Label();

        OperationLabel.Position =
            Vector2.Zero;

        OperationLabel.Size =
            new Vector2(500, 125);

        OperationLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        OperationLabel.VerticalAlignment =
            VerticalAlignment.Center;

        OperationLabel.AddThemeFontSizeOverride(
            "font_size",
            52
        );

        background.AddChild(
            OperationLabel
        );
    }

    private void CreateMeteor()
    {
        Panel meteorPanel =
            new Panel();

        meteorPanel.Position =
            new Vector2(480, 295);

        meteorPanel.Size =
            new Vector2(240, 135);

        AddChild(
            meteorPanel
        );

        ColorRect background =
            new ColorRect();

        background.Position =
            Vector2.Zero;

        background.Size =
            new Vector2(240, 135);

        background.Color =
            new Color("#271C43");

        meteorPanel.AddChild(
            background
        );

        Label meteor =
            new Label();

        meteor.Text =
            "☄️";

        meteor.HorizontalAlignment =
            HorizontalAlignment.Center;

        meteor.Position =
            new Vector2(20, 5);

        meteor.Size =
            new Vector2(200, 60);

        meteor.AddThemeFontSizeOverride(
            "font_size",
            44
        );

        meteorPanel.AddChild(
            meteor
        );

        MeteorHealthLabel =
            new Label();

        MeteorHealthLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        MeteorHealthLabel.Position =
            new Vector2(10, 70);

        MeteorHealthLabel.Size =
            new Vector2(220, 40);

        MeteorHealthLabel.AddThemeFontSizeOverride(
            "font_size",
            20
        );

        meteorPanel.AddChild(
            MeteorHealthLabel
        );
    }

    private void CreateAnswers()
    {
        AnswerButton1 =
            CreateAnswerButton();

        AnswerButton2 =
            CreateAnswerButton();

        AnswerButton3 =
            CreateAnswerButton();

        AnswerButton1.Position =
            new Vector2(145, 505);

        AnswerButton2.Position =
            new Vector2(490, 505);

        AnswerButton3.Position =
            new Vector2(835, 505);

        AddChild(
            AnswerButton1
        );

        AddChild(
            AnswerButton2
        );

        AddChild(
            AnswerButton3
        );
    }

    private Button CreateAnswerButton()
    {
        Button button =
            new Button();

        button.Size =
            new Vector2(220, 100);

        button.AddThemeFontSizeOverride(
            "font_size",
            30
        );

        return button;
    }

    private void CreateMessage()
    {
        MessageLabel =
            new Label();

        MessageLabel.Position =
            new Vector2(200, 620);

        MessageLabel.Size =
            new Vector2(800, 60);

        MessageLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        MessageLabel.AddThemeFontSizeOverride(
            "font_size",
            23
        );

        AddChild(
            MessageLabel
        );
    }

    private void CreateDefeatPanel()
    {
        defeatPanel =
            new Panel();

        defeatPanel.Position =
            new Vector2(300, 120);

        defeatPanel.Size =
            new Vector2(600, 500);

        AddChild(
            defeatPanel
        );

        ColorRect background =
            new ColorRect();

        background.Position =
            Vector2.Zero;

        background.Size =
            new Vector2(600, 500);

        background.Color =
            new Color("#17162F");

        defeatPanel.AddChild(
            background
        );

        Label title =
            new Label();

        title.Text =
            "DERROTA";

        title.HorizontalAlignment =
            HorizontalAlignment.Center;

        title.Position =
            new Vector2(40, 55);

        title.Size =
            new Vector2(520, 70);

        title.AddThemeFontSizeOverride(
            "font_size",
            42
        );

        defeatPanel.AddChild(
            title
        );

        Label message =
            new Label();

        message.Text =
            "Você ficou sem vidas!\nTente novamente para continuar sua missão.";

        message.HorizontalAlignment =
            HorizontalAlignment.Center;

        message.VerticalAlignment =
            VerticalAlignment.Center;

        message.Position =
            new Vector2(50, 140);

        message.Size =
            new Vector2(500, 100);

        message.AddThemeFontSizeOverride(
            "font_size",
            21
        );

        defeatPanel.AddChild(
            message
        );

        Button restart =
            new Button();

        restart.Text =
            "REINICIAR";

        restart.Position =
            new Vector2(95, 310);

        restart.Size =
            new Vector2(190, 70);

        restart.AddThemeFontSizeOverride(
            "font_size",
            22
        );

        defeatPanel.AddChild(
            restart
        );

        restart.Pressed +=
            () => DefeatRestartRequested?.Invoke();

        Button exit =
            new Button();

        exit.Text =
            "SAIR";

        exit.Position =
            new Vector2(315, 310);

        exit.Size =
            new Vector2(190, 70);

        exit.AddThemeFontSizeOverride(
            "font_size",
            22
        );

        defeatPanel.AddChild(
            exit
        );

        exit.Pressed +=
            () => DefeatExitRequested?.Invoke();

        defeatPanel.Visible =
            false;
    }

    public void ShowDefeat()
    {
        defeatPanel.Visible =
            true;

        SetGameButtonsEnabled(
            false
        );
    }

    public void HideDefeat()
    {
        defeatPanel.Visible =
            false;

        SetGameButtonsEnabled(
            true
        );
    }

    public void UpdateLives(
        int lives
    )
    {
        string hearts =
            "";

        for (
            int i = 0;
            i < lives;
            i++
        )
        {
            hearts +=
                "❤️ ";
        }

        for (
            int i = lives;
            i < MaxLives;
            i++
        )
        {
            hearts +=
                "🖤 ";
        }

        LivesLabel.Text =
            $"VIDAS: {hearts}";
    }

    public void UpdateMeteorHealth(
        int health
    )
    {
        if (
            health < 0
        )
        {
            health = 0;
        }

        MeteorHealthLabel.Text =
            $"❤️ {health} / {EasyMeteorMaxHealth}";
    }

    public void ResetGameVisuals()
    {
        UpdateLives(
            MaxLives
        );

        UpdateMeteorHealth(
            EasyMeteorMaxHealth
        );

        MessageLabel.Text =
            "";

        DisableRestartButtonVisual();

        HideDefeat();
    }

    private void SetGameButtonsEnabled(
        bool enabled
    )
    {
        AnswerButton1.Disabled =
            !enabled;

        AnswerButton2.Disabled =
            !enabled;

        AnswerButton3.Disabled =
            !enabled;

        RestartButton.Disabled =
            !enabled;

        ExitButton.Disabled =
            !enabled;
    }
}