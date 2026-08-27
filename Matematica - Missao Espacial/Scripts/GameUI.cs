using Godot;
using System;

public partial class GameUI : Control
{
    public Label OperationLabel { get; private set; }

    public Label MeteorHealthLabel { get; private set; }

    public Label MessageLabel { get; private set; }

    public Button AnswerButton1 { get; private set; }

    public Button AnswerButton2 { get; private set; }

    public Button AnswerButton3 { get; private set; }

    public Button RestartButton { get; private set; }

    public Button ExitButton { get; private set; }

    public Panel MeteorPanel { get; private set; }

    public event Action RestartRequested;

    public event Action ExitRequested;

    public override void _Ready()
    {
        CreateInterface();
    }

    private void CreateInterface()
    {
        CreateTopButtons();

        CreateQuestionPanel();

        CreateMeteor();

        CreateAnswers();

        CreateMessage();
    }

    private void CreateTopButtons()
    {
        RestartButton =
            new Button();

        RestartButton.Text =
            "🔄 REINICIAR";

        RestartButton.Position =
            new Vector2(900, 25);

        RestartButton.Size =
            new Vector2(135, 45);

        Style.ApplyActionButton(
            RestartButton,
            new Color("#3155A6"),
            new Color("#4569C6")
        );

        AddChild(
            RestartButton
        );

        RestartButton.Pressed +=
            () => RestartRequested?.Invoke();

        ExitButton =
            new Button();

        ExitButton.Text =
            "🚪 SAIR";

        ExitButton.Position =
            new Vector2(1045, 25);

        ExitButton.Size =
            new Vector2(110, 45);

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

    private void CreateQuestionPanel()
    {
        Panel questionPanel =
            new Panel();

        questionPanel.Position =
            new Vector2(350, 145);

        questionPanel.Size =
            new Vector2(500, 125);

        questionPanel.AddThemeStyleboxOverride(
            "panel",
            Style.CreateBox(
                new Color("#151F4A"),
                new Color("#6176D0"),
                3,
                30
            )
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

        MeteorPanel.AddThemeStyleboxOverride(
            "panel",
            Style.CreateBox(
                new Color("#271C43"),
                new Color("#8060C3"),
                3,
                30
            )
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
}