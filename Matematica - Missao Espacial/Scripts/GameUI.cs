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

    private Button defeatRestartButton;
    private Button defeatExitButton;

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
        RestartButton = CreateActionButton(
            "REINICIAR",
            new Vector2(900, 25),
            new Vector2(135, 45),
            new Color("#27346B")
        );

        AddChild(
            RestartButton
        );

        RestartButton.Pressed +=
            OnRestartPressed;

        ExitButton = CreateActionButton(
            "SAIR",
            new Vector2(1045, 25),
            new Vector2(110, 45),
            new Color("#A9446B")
        );

        AddChild(
            ExitButton
        );

        ExitButton.Pressed +=
            OnExitPressed;
    }

    private Button CreateActionButton(
        string text,
        Vector2 position,
        Vector2 size,
        Color color
    )
    {
        Button button =
            new Button();

        button.Text =
            text;

        button.Position =
            position;

        button.Size =
            size;

        button.AddThemeFontSizeOverride(
            "font_size",
            16
        );

        ApplyButtonTheme(
            button,
            color,
            new Color("#AFC0FF")
        );

        return button;
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
        Panel questionPanel =
            new Panel();

        questionPanel.Position =
            new Vector2(350, 145);

        questionPanel.Size =
            new Vector2(500, 125);

        ApplyPanelTheme(
            questionPanel,
            new Color("#151F4A"),
            new Color("#6176D0")
        );

        AddChild(
            questionPanel
        );

        OperationLabel =
            new Label();

        OperationLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        OperationLabel.VerticalAlignment =
            VerticalAlignment.Center;

        OperationLabel.SetAnchorsAndOffsetsPreset(
            Control.LayoutPreset.FullRect
        );

        OperationLabel.AddThemeFontSizeOverride(
            "font_size",
            52
        );

        questionPanel.AddChild(
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

        ApplyPanelTheme(
            MeteorPanel,
            new Color("#271C43"),
            new Color("#8060C3")
        );

        AddChild(
            MeteorPanel
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

        ApplyButtonTheme(
            button,
            new Color("#27346B"),
            new Color("#657BD2")
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

        ApplyPanelTheme(
            defeatPanel,
            new Color("#351C3D"),
            new Color("#B85C9A")
        );

        AddChild(
            defeatPanel
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

        defeatRestartButton =
            CreateDefeatButton(
                "REINICIAR",
                new Vector2(95, 310),
                new Color("#3155A6")
            );

        defeatPanel.AddChild(
            defeatRestartButton
        );

        defeatRestartButton.Pressed +=
            OnDefeatRestartPressed;

        defeatExitButton =
            CreateDefeatButton(
                "SAIR",
                new Vector2(315, 310),
                new Color("#A9446B")
            );

        defeatPanel.AddChild(
            defeatExitButton
        );

        defeatExitButton.Pressed +=
            OnDefeatExitPressed;

        defeatPanel.Visible =
            false;

        MoveChild(
            defeatPanel,
            GetChildCount() - 1
        );
    }

    private Button CreateDefeatButton(
        string text,
        Vector2 position,
        Color color
    )
    {
        Button button =
            new Button();

        button.Text =
            text;

        button.Position =
            position;

        button.Size =
            new Vector2(190, 70);

        button.AddThemeFontSizeOverride(
            "font_size",
            22
        );

        ApplyButtonTheme(
            button,
            color,
            new Color("#AFC0FF")
        );

        return button;
    }

    private void ApplyButtonTheme(
        Button button,
        Color normalColor,
        Color borderColor
    )
    {
        Theme theme =
            new Theme();

        StyleBoxFlat normal =
            CreateBox(
                normalColor,
                borderColor,
                3,
                25
            );

        StyleBoxFlat hover =
            CreateBox(
                normalColor.Lightened(0.15f),
                borderColor.Lightened(0.15f),
                4,
                25
            );

        StyleBoxFlat pressed =
            CreateBox(
                normalColor.Darkened(0.12f),
                borderColor,
                4,
                25
            );

        StyleBoxFlat disabled =
            CreateBox(
                normalColor.Darkened(0.35f),
                borderColor.Darkened(0.25f),
                2,
                25
            );

        theme.SetStylebox(
            "normal",
            "Button",
            normal
        );

        theme.SetStylebox(
            "hover",
            "Button",
            hover
        );

        theme.SetStylebox(
            "pressed",
            "Button",
            pressed
        );

        theme.SetStylebox(
            "disabled",
            "Button",
            disabled
        );

        button.Theme =
            theme;
    }

    private void ApplyPanelTheme(
        Panel panel,
        Color backgroundColor,
        Color borderColor
    )
    {
        Theme theme =
            new Theme();

        StyleBoxFlat panelStyle =
            CreateBox(
                backgroundColor,
                borderColor,
                3,
                30
            );

        theme.SetStylebox(
            "panel",
            "Panel",
            panelStyle
        );

        panel.Theme =
            theme;
    }

    private StyleBoxFlat CreateBox(
        Color backgroundColor,
        Color borderColor,
        int borderWidth,
        int cornerRadius
    )
    {
        StyleBoxFlat style =
            new StyleBoxFlat();

        style.BgColor =
            backgroundColor;

        style.BorderColor =
            borderColor;

        style.BorderWidthLeft =
            borderWidth;

        style.BorderWidthRight =
            borderWidth;

        style.BorderWidthTop =
            borderWidth;

        style.BorderWidthBottom =
            borderWidth;

        style.CornerRadiusTopLeft =
            cornerRadius;

        style.CornerRadiusTopRight =
            cornerRadius;

        style.CornerRadiusBottomLeft =
            cornerRadius;

        style.CornerRadiusBottomRight =
            cornerRadius;

        return style;
    }

    private void OnRestartPressed()
    {
        RestartRequested?.Invoke();
    }

    private void OnExitPressed()
    {
        ExitRequested?.Invoke();
    }

    private void OnDefeatRestartPressed()
    {
        DefeatRestartRequested?.Invoke();
    }

    private void OnDefeatExitPressed()
    {
        DefeatExitRequested?.Invoke();
    }

    public void ShowDefeat()
    {
        defeatPanel.Visible =
            true;

        MoveChild(
            defeatPanel,
            GetChildCount() - 1
        );

        AnswerButton1.Disabled =
            true;

        AnswerButton2.Disabled =
            true;

        AnswerButton3.Disabled =
            true;

        RestartButton.Disabled =
            true;

        ExitButton.Disabled =
            true;
    }

    public void HideDefeat()
    {
        defeatPanel.Visible =
            false;

        AnswerButton1.Disabled =
            false;

        AnswerButton2.Disabled =
            false;

        AnswerButton3.Disabled =
            false;

        RestartButton.Disabled =
            false;

        ExitButton.Disabled =
            false;
    }

    public void UpdateLives(
        int lives
    )
    {
        lives =
            Mathf.Clamp(
                lives,
                0,
                MaxLives
            );

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
        health =
            Mathf.Clamp(
                health,
                0,
                EasyMeteorMaxHealth
            );

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

        HideDefeat();

        MeteorPanel.Visible =
            true;

        MeteorPanel.Scale =
            Vector2.One;

        MeteorPanel.Modulate =
            Colors.White;

        AnswerButton1.Modulate =
            Colors.White;

        AnswerButton2.Modulate =
            Colors.White;

        AnswerButton3.Modulate =
            Colors.White;

        AnswerButton1.Scale =
            Vector2.One;

        AnswerButton2.Scale =
            Vector2.One;

        AnswerButton3.Scale =
            Vector2.One;

        AnswerButton1.Position =
            new Vector2(145, 505);

        AnswerButton2.Position =
            new Vector2(490, 505);

        AnswerButton3.Position =
            new Vector2(835, 505);
    }
}