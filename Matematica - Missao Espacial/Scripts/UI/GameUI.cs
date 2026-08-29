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
    public event Action VictoryNextRequested;
    public event Action VictoryMenuRequested;

    private Panel defeatPanel;
    private Button defeatRestartButton;
    private Button defeatExitButton;

    private Panel victoryPanel;
    private Button victoryNextButton;
    private Button victoryMenuButton;

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
        CreateVictoryPanel();
    }

    private void CreateTopButtons()
    {
        RestartButton = CreateActionButton(
            "REINICIAR",
            new Vector2(900, 25),
            new Vector2(135, 45),
            Style.AnswerButtonColor
        );

        AddChild(RestartButton);

        RestartButton.Pressed += OnRestartPressed;

        ExitButton = CreateActionButton(
            "SAIR",
            new Vector2(1045, 25),
            new Vector2(110, 45),
            Style.ExitButtonColor
        );

        AddChild(ExitButton);

        ExitButton.Pressed += OnExitPressed;
    }

    private Button CreateActionButton(
        string text,
        Vector2 position,
        Vector2 size,
        Color color
    )
    {
        Button button = new Button();

        button.Text = text;
        button.Position = position;
        button.Size = size;

        Style.SetFontSize(
            button,
            16
        );

        Style.ApplyButtonStyle(
            button,
            color,
            Style.ButtonSelectedBorderColor,
            color.Lightened(0.15f),
            3
        );

        return button;
    }

    private void CreateLives()
    {
        LivesLabel = new Label();

        LivesLabel.Text =
            "VIDAS: ❤️ ❤️ ❤️";

        LivesLabel.Position =
            new Vector2(30, 25);

        LivesLabel.Size =
            new Vector2(300, 45);

        Style.SetFontSize(
            LivesLabel,
            22
        );

        AddChild(LivesLabel);
    }

    private void CreateQuestionPanel()
    {
        Panel questionPanel =
            new Panel();

        questionPanel.Position =
            new Vector2(350, 145);

        questionPanel.Size =
            new Vector2(500, 125);

        Style.ApplyQuestionPanelStyle(
            questionPanel
        );

        AddChild(questionPanel);

        OperationLabel =
            new Label();

        Style.CenterLabel(
            OperationLabel
        );

        OperationLabel.SetAnchorsAndOffsetsPreset(
            Control.LayoutPreset.FullRect
        );

        Style.SetFontSize(
            OperationLabel,
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

        Style.ApplyMeteorPanelStyle(
            MeteorPanel
        );

        AddChild(MeteorPanel);

        Label meteor =
            new Label();

        meteor.Text =
            "☄️";

        Style.CenterLabel(
            meteor
        );

        meteor.Position =
            new Vector2(20, 5);

        meteor.Size =
            new Vector2(200, 60);

        Style.SetFontSize(
            meteor,
            44
        );

        MeteorPanel.AddChild(
            meteor
        );

        MeteorHealthLabel =
            new Label();

        Style.CenterLabel(
            MeteorHealthLabel
        );

        MeteorHealthLabel.Position =
            new Vector2(10, 70);

        MeteorHealthLabel.Size =
            new Vector2(220, 40);

        Style.SetFontSize(
            MeteorHealthLabel,
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

        AddChild(AnswerButton1);
        AddChild(AnswerButton2);
        AddChild(AnswerButton3);
    }

    private Button CreateAnswerButton()
    {
        Button button =
            new Button();

        button.Size =
            new Vector2(220, 100);

        Style.SetFontSize(
            button,
            30
        );

        Style.ApplyAnswerButtonStyle(
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

        Style.SetFontSize(
            MessageLabel,
            23
        );

        AddChild(MessageLabel);
    }

    private void CreateDefeatPanel()
    {
        defeatPanel =
            new Panel();

        defeatPanel.Position =
            new Vector2(300, 120);

        defeatPanel.Size =
            new Vector2(600, 500);

        Style.ApplyDefeatPanelStyle(
            defeatPanel
        );

        AddChild(defeatPanel);

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

        Style.SetFontSize(
            title,
            42
        );

        defeatPanel.AddChild(
            title
        );

        Label message =
            new Label();

        message.Text =
            "Você ficou sem vidas!\nTente novamente para continuar sua missão.";

        Style.CenterLabel(
            message
        );

        message.Position =
            new Vector2(50, 140);

        message.Size =
            new Vector2(500, 100);

        Style.SetFontSize(
            message,
            21
        );

        defeatPanel.AddChild(
            message
        );

        defeatRestartButton =
            CreateDefeatButton(
                "REINICIAR",
                new Vector2(95, 310),
                Style.StartButtonColor
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
                Style.ExitButtonColor
            );

        defeatPanel.AddChild(
            defeatExitButton
        );

        defeatExitButton.Pressed +=
            OnDefeatExitPressed;

        defeatPanel.Visible = false;
    }

    private void CreateVictoryPanel()
    {
        victoryPanel =
            new Panel();

        victoryPanel.Position =
            new Vector2(300, 120);

        victoryPanel.Size =
            new Vector2(600, 500);

        Style.ApplyVictoryPanelStyle(
            victoryPanel
        );

        AddChild(victoryPanel);

        Label title =
            new Label();

        title.Text =
            "🎉 MISSÃO CONCLUÍDA!";

        title.HorizontalAlignment =
            HorizontalAlignment.Center;

        title.Position =
            new Vector2(40, 45);

        title.Size =
            new Vector2(520, 70);

        Style.SetFontSize(
            title,
            34
        );

        victoryPanel.AddChild(
            title
        );

        Label message =
            new Label();

        Style.CenterLabel(
            message
        );

        message.Position =
            new Vector2(50, 135);

        message.Size =
            new Vector2(500, 130);

        Style.SetFontSize(
            message,
            22
        );

        victoryPanel.AddChild(
            message
        );

        victoryNextButton =
            CreateDefeatButton(
                "PRÓXIMO PLANETA",
                new Vector2(65, 330),
                Style.StartButtonColor
            );

        victoryPanel.AddChild(
            victoryNextButton
        );

        victoryNextButton.Pressed +=
            OnVictoryNextPressed;

        victoryMenuButton =
            CreateDefeatButton(
                "PLANETAS",
                new Vector2(345, 330),
                Style.ExitButtonColor
            );

        victoryPanel.AddChild(
            victoryMenuButton
        );

        victoryMenuButton.Pressed +=
            OnVictoryMenuPressed;

        victoryPanel.Visible = false;
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

        Style.SetFontSize(
            button,
            19
        );

        Style.ApplyButtonStyle(
            button,
            color,
            Style.ButtonSelectedBorderColor,
            color.Lightened(0.15f),
            3
        );

        return button;
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

    private void OnVictoryNextPressed()
    {
        VictoryNextRequested?.Invoke();
    }

    private void OnVictoryMenuPressed()
    {
        VictoryMenuRequested?.Invoke();
    }

    public void ShowDefeat()
    {
        defeatPanel.Visible = true;

        MoveChild(
            defeatPanel,
            GetChildCount() - 1
        );

        AnswerButton1.Disabled = true;
        AnswerButton2.Disabled = true;
        AnswerButton3.Disabled = true;

        RestartButton.Disabled = true;
        ExitButton.Disabled = true;
    }

    public void HideDefeat()
    {
        defeatPanel.Visible = false;

        AnswerButton1.Disabled = false;
        AnswerButton2.Disabled = false;
        AnswerButton3.Disabled = false;

        RestartButton.Disabled = false;
        ExitButton.Disabled = false;
    }

    public void ShowVictory(
        int meteorsDestroyed,
        int score
    )
    {
        victoryPanel.Visible = true;

        MoveChild(
            victoryPanel,
            GetChildCount() - 1
        );

        victoryPanel
            .GetChild<Label>(1)
            .Text =
            $"Você destruiu {meteorsDestroyed} meteoros!\n\n" +
            $"⭐ Pontuação da missão: {score}";

        AnswerButton1.Disabled = true;
        AnswerButton2.Disabled = true;
        AnswerButton3.Disabled = true;

        RestartButton.Disabled = true;
        ExitButton.Disabled = true;
    }

    public void HideVictory()
    {
        victoryPanel.Visible = false;
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

        string hearts = "";

        for (
            int i = 0;
            i < lives;
            i++
        )
        {
            hearts += "❤️ ";
        }

        for (
            int i = lives;
            i < MaxLives;
            i++
        )
        {
            hearts += "🖤 ";
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

    public void ResetMeteorVisual()
    {
        MeteorPanel.Visible = true;

        MeteorPanel.Scale =
            Vector2.One;

        MeteorPanel.Modulate =
            Colors.White;
    }

    public void ResetGameVisuals()
    {
        UpdateLives(
            MaxLives
        );

        UpdateMeteorHealth(
            EasyMeteorMaxHealth
        );

        MessageLabel.Text = "";

        HideDefeat();
        HideVictory();

        MeteorPanel.Visible = true;

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