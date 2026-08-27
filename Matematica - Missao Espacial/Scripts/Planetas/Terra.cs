using Godot;
using System;

public partial class Terra : Control
{
    private Random random =
        new Random();

    private Label operationLabel;
    private Label meteorHealthLabel;
    private Label messageLabel;

    private Button answerButton1;
    private Button answerButton2;
    private Button answerButton3;

    private Panel meteorPanel;

    private int correctAnswer;
    private int meteorHealth;
    private int meteorMaxHealth;

    private int currentQuestion = 1;

    public override void _Ready()
    {
        CreateInterface();

        StartQuestion();
    }

    private void CreateInterface()
    {
        Label planet =
            new Label();

        planet.Text =
            "🌍 TERRA";

        planet.Position =
            new Vector2(30, 25);

        planet.AddThemeFontSizeOverride(
            "font_size",
            26
        );

        AddChild(planet);

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

        operationLabel =
            new Label();

        operationLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        operationLabel.VerticalAlignment =
            VerticalAlignment.Center;

        operationLabel.SetAnchorsAndOffsetsPreset(
            Control.LayoutPreset.FullRect
        );

        operationLabel.AddThemeFontSizeOverride(
            "font_size",
            52
        );

        questionPanel.AddChild(
            operationLabel
        );

        CreateMeteor();

        CreateAnswers();

        messageLabel =
            new Label();

        messageLabel.Position =
            new Vector2(200, 620);

        messageLabel.Size =
            new Vector2(800, 60);

        messageLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        messageLabel.AddThemeFontSizeOverride(
            "font_size",
            23
        );

        AddChild(
            messageLabel
        );
    }

    private void CreateMeteor()
    {
        meteorPanel =
            new Panel();

        meteorPanel.Position =
            new Vector2(480, 295);

        meteorPanel.Size =
            new Vector2(240, 135);

        meteorPanel.AddThemeStyleBoxOverride(
            "panel",
            Style.CreateBox(
                new Color("#271C43"),
                new Color("#8060C3"),
                3,
                30
            )
        );

        AddChild(
            meteorPanel
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

        meteorHealthLabel =
            new Label();

        meteorHealthLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        meteorHealthLabel.Position =
            new Vector2(10, 70);

        meteorHealthLabel.Size =
            new Vector2(220, 40);

        meteorHealthLabel.AddThemeFontSizeOverride(
            "font_size",
            20
        );

        meteorPanel.AddChild(
            meteorHealthLabel
        );
    }

    private void CreateAnswers()
    {
        answerButton1 =
            CreateAnswerButton();

        answerButton2 =
            CreateAnswerButton();

        answerButton3 =
            CreateAnswerButton();

        answerButton1.Position =
            new Vector2(145, 505);

        answerButton2.Position =
            new Vector2(490, 505);

        answerButton3.Position =
            new Vector2(835, 505);

        AddChild(
            answerButton1
        );

        AddChild(
            answerButton2
        );

        AddChild(
            answerButton3
        );

        answerButton1.Pressed +=
            () => Shoot(answerButton1);

        answerButton2.Pressed +=
            () => Shoot(answerButton2);

        answerButton3.Pressed +=
            () => Shoot(answerButton3);
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

        button.AddThemeStyleboxOverride(
            "normal",
            Style.CreateBox(
                new Color("#27346B"),
                new Color("#657BD2"),
                3,
                25
            )
        );

        button.AddThemeStyleboxOverride(
            "hover",
            Style.CreateBox(
                new Color("#35498A"),
                new Color("#9BABFF"),
                4,
                25
            )
        );

        return button;
    }

    private void StartQuestion()
    {
        GenerateOperation();

        GenerateAnswers();

        meteorMaxHealth =
            30;

        meteorHealth =
            meteorMaxHealth;

        UpdateMeteorHealth();

        messageLabel.Text =
            "";
    }

    private void GenerateOperation()
    {
        int number1 =
            random.Next(1, 10);

        int number2 =
            random.Next(1, 10);

        correctAnswer =
            number1 + number2;

        operationLabel.Text =
            $"{number1} + {number2}";
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

        answerButton1.Text =
            answers[0].ToString();

        answerButton2.Text =
            answers[1].ToString();

        answerButton3.Text =
            answers[2].ToString();
    }

    private async void Shoot(
        Button button
    )
    {
        int selectedAnswer =
            int.Parse(button.Text);

        if (
            selectedAnswer ==
            correctAnswer
        )
        {
            meteorHealth -=
                10;

            messageLabel.Text =
                "🎯 Acertou!";
        }
        else
        {
            meteorHealth -=
                5;

            messageLabel.Text =
                "💥 Quase!";
        }

        UpdateMeteorHealth();

        await ToSignal(
            GetTree().CreateTimer(0.5),
            SceneTreeTimer.SignalName.Timeout
        );

        if (
            meteorHealth <= 0
        )
        {
            messageLabel.Text =
                "💥 Meteoro destruído!";

            await ToSignal(
                GetTree().CreateTimer(0.5),
                SceneTreeTimer.SignalName.Timeout
            );

            currentQuestion++;

            StartQuestion();

            return;
        }

        // A conta sempre muda
        // depois de cada disparo.
        StartQuestion();
    }

    private void UpdateMeteorHealth()
    {
        meteorHealthLabel.Text =
            $"❤️ {meteorHealth} / {meteorMaxHealth}";
    }
}