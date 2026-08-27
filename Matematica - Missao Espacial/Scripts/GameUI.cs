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

    public Panel MeteorPanel { get; private set; }

    public event Action RestartRequested;

    public event Action ExitRequested;

    public event Action DefeatRestartRequested;

    public event Action DefeatExitRequested;

    private Panel defeatPanel;

    private ColorRect questionBackground;

    private ColorRect meteorBackground;

    private ColorRect defeatBackground;

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

        Style.ApplyActionButton(
            RestartButton,
            new Color("#3155A6"),
            new Color("#4569C6")
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

        Style.ApplyActionButton(
            ExitButton,
            new Color("#7A3151"),
            new Color("#A9446B")
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
        questionBackground =
            new ColorRect();

        questionBackground.Position =
            new Vector2(350, 145);

        questionBackground.Size =
            new Vector2(500, 125);

        questionBackground.Color =
            new Color("#151F4A");

        questionBackground.MouseFilter =
            Control.MouseFilterEnum.Ignore;

        AddChild(
            questionBackground
        );

        OperationLabel =
            new Label();

        OperationLabel.Position =
            new Vector2(0, 0);

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

        questionBackground.AddChild(
            OperationLabel
        );
    }

    private void CreateMeteor()
    {
        MeteorPanel =
            new Panel();

        MeteorPanel.Position =
            new Vector2(480, 295);

        MeteorPanel.Size =
            new Vector2(240, 135);

        AddChild(
            MeteorPanel
        );

        meteorBackground =
            new ColorRect();

        meteorBackground.Position =
            new Vector2(0, 0);

        meteorBackground.Size =
            new Vector2(240, 135);

        meteorBackground.Color =
            new Color("#271C43");

        meteorBackground.MouseFilter =
            Control.MouseFilterEnum.Ignore;

        MeteorPanel.AddChild(
            meteorBackground
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

        MeteorPanel.AddChild(
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

        MeteorPanel.AddChild(
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

        Style.ApplyAnswerButton(
            button
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

        defeatBackground =
            new ColorRect();

        defeatBackground.Position =
            new Vector2(0, 0);

        defeatBackground.Size =
            new Vector2(600, 500);

        defeatBackground.Color =
            new Color("#17162F");

        defeatBackground.MouseFilter =
            Control.MouseFilterEnum.Ignore;

        defeatPanel.AddChild(
            defeatBackground
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

        Style.ApplyActionButton(
            restart,
            new Color("#3155A6"),
            new Color("#4569C6")
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

        Style.ApplyActionButton(
            exit,
            new Color("#7A3151"),
            new Color("#A9446B")
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