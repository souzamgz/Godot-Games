using Godot;
using System;

public partial class Main : Control
{
	private Label titleLabel;
	private Label xpLabel;
	private Label livesLabel;
	private Label questionLabel;
	private Label progressLabel;
	private Label messageLabel;

	private Panel topPanel;
	private Panel questionPanel;

	private Button answerButton1;
	private Button answerButton2;
	private Button answerButton3;

	private Random random = new Random();

	private int correctAnswer;
	private int xp = 0;
	private int lives = 3;
	private int currentQuestion = 1;

	private const int TotalQuestions = 10;

	private bool canAnswer = true;

	public override void _Ready()
	{
		CreateInterface();
		StartQuestion();
	}

	private void CreateInterface()
	{
		
		// FUNDO

		ColorRect background = new ColorRect();

		background.Color = new Color("#080D24");

		background.SetAnchorsAndOffsetsPreset(
			Control.LayoutPreset.FullRect
		);

		AddChild(background);

		// Estrelas decorativas
		CreateStars();
		
		// PAINEL SUPERIOR

		topPanel = new Panel();

		topPanel.Position = new Vector2(25, 20);
		topPanel.Size = new Vector2(1150, 100);

		topPanel.AddThemeStyleboxOverride(
			"panel",
			Style.CreateBox(
				new Color("#111A3D"),
				new Color("#283A7A"),
				2,
				20
			)
		);

		AddChild(topPanel);

		// TÍTULO

		titleLabel = new Label();

		titleLabel.Text = "🚀 MISSÃO ESPACIAL";

		titleLabel.Position = new Vector2(25, 18);
		titleLabel.Size = new Vector2(500, 45);

		titleLabel.AddThemeFontSizeOverride(
			"font_size",
			30
		);

		topPanel.AddChild(titleLabel);

		// XP

		xpLabel = new Label();

		xpLabel.Text = "⭐ XP: 0";

		xpLabel.Position = new Vector2(30, 58);
		xpLabel.Size = new Vector2(250, 35);

		xpLabel.AddThemeFontSizeOverride(
			"font_size",
			20
		);

		topPanel.AddChild(xpLabel);

		// VIDAS

		livesLabel = new Label();

		livesLabel.Text = "❤️ ❤️ ❤️";

		livesLabel.HorizontalAlignment =
			HorizontalAlignment.Right;

		livesLabel.Position = new Vector2(800, 25);
		livesLabel.Size = new Vector2(320, 40);

		livesLabel.AddThemeFontSizeOverride(
			"font_size",
			24
		);

		topPanel.AddChild(livesLabel);

		// PROGRESSO

		progressLabel = new Label();

		progressLabel.Text = "QUESTÃO 1 / 10";

		progressLabel.HorizontalAlignment =
			HorizontalAlignment.Center;

		progressLabel.Position = new Vector2(800, 62);
		progressLabel.Size = new Vector2(320, 30);

		progressLabel.AddThemeFontSizeOverride(
			"font_size",
			16
		);

		topPanel.AddChild(progressLabel);

		// PAINEL DA QUESTÃO

		questionPanel = new Panel();

		questionPanel.Position = new Vector2(300, 160);
		questionPanel.Size = new Vector2(600, 180);

		questionPanel.AddThemeStyleboxOverride(
			"panel",
			Style.CreateBox(
				new Color("#151F4A"),
				new Color("#465CA8"),
				3,
				30
			)
		);

		AddChild(questionPanel);

		// TEXTO DA QUESTÃO

		questionLabel = new Label();

		questionLabel.HorizontalAlignment =
			HorizontalAlignment.Center;

		questionLabel.VerticalAlignment =
			VerticalAlignment.Center;

		questionLabel.Position = new Vector2(20, 25);
		questionLabel.Size = new Vector2(560, 130);

		questionLabel.AddThemeFontSizeOverride(
			"font_size",
			54
		);

		questionPanel.AddChild(questionLabel);

		// =========================================================
		// TEXTO AUXILIAR
		// =========================================================

		Label instructionLabel = new Label();

		instructionLabel.Text =
			"☄️ Escolha o asteroide com a resposta correta!";

		instructionLabel.HorizontalAlignment =
			HorizontalAlignment.Center;

		instructionLabel.Position = new Vector2(200, 355);
		instructionLabel.Size = new Vector2(800, 40);

		instructionLabel.AddThemeFontSizeOverride(
			"font_size",
			18
		);

		AddChild(instructionLabel);

		// BOTÕES DE RESPOSTA

		answerButton1 = CreateAnswerButton();
		answerButton2 = CreateAnswerButton();
		answerButton3 = CreateAnswerButton();

		answerButton1.Position = new Vector2(145, 415);
		answerButton2.Position = new Vector2(490, 415);
		answerButton3.Position = new Vector2(835, 415);

		AddChild(answerButton1);
		AddChild(answerButton2);
		AddChild(answerButton3);

		answerButton1.Pressed += () =>
			CheckAnswer(answerButton1);

		answerButton2.Pressed += () =>
			CheckAnswer(answerButton2);

		answerButton3.Pressed += () =>
			CheckAnswer(answerButton3);

		// MENSAGEM
		
		messageLabel = new Label();

		messageLabel.HorizontalAlignment =
			HorizontalAlignment.Center;

		messageLabel.VerticalAlignment =
			VerticalAlignment.Center;

		messageLabel.Position = new Vector2(200, 545);
		messageLabel.Size = new Vector2(800, 100);

		messageLabel.AddThemeFontSizeOverride(
			"font_size",
			27
		);

		AddChild(messageLabel);
	}

	private Button CreateAnswerButton()
	{
		Button button = new Button();

		button.Text = "☄️";

		button.CustomMinimumSize =
			new Vector2(220, 105);

		button.AddThemeFontSizeOverride(
			"font_size",
			32
		);

		// Estado normal
		button.AddThemeStyleboxOverride(
			"normal",
			Style.CreateBox(
				new Color("#27346B"),
				new Color("#6174C9"),
				3,
				30
			)
		);

		// Mouse em cima
		button.AddThemeStyleboxOverride(
			"hover",
			Style.CreateBox(
				new Color("#35458C"),
				new Color("#8A9BFF"),
				4,
				30
			)
		);

		// Pressionado
		button.AddThemeStyleboxOverride(
			"pressed",
			Style.CreateBox(
				new Color("#1B2550"),
				new Color("#AAB6FF"),
				4,
				30
			)
		);

		return button;
	}

	private void CreateStars()
	{
		string[] stars =
		{
			"✦", "·", "✧", "·", "✦",
			"·", "✧", "·", "✦", "·",
			"✧", "·", "✦", "·", "✧"
		};

		Vector2[] positions =
		{
			new Vector2(70, 150),
			new Vector2(180, 250),
			new Vector2(1040, 170),
			new Vector2(1100, 300),
			new Vector2(80, 400),
			new Vector2(1050, 500),
			new Vector2(250, 600),
			new Vector2(950, 630),
			new Vector2(1150, 380),
			new Vector2(400, 130),
			new Vector2(750, 130),
			new Vector2(50, 620),
			new Vector2(1120, 650),
			new Vector2(200, 520),
			new Vector2(1000, 250)
		};

		for (int i = 0; i < stars.Length; i++)
		{
			Label star = new Label();

			star.Text = stars[i];

			star.Position = positions[i];

			star.AddThemeFontSizeOverride(
				"font_size",
				i % 3 == 0 ? 22 : 14
			);

			AddChild(star);
		}
	}

	private void StartQuestion()
	{
		canAnswer = true;

		messageLabel.Text = "";

		progressLabel.Text =
			$"QUESTÃO {currentQuestion} / {TotalQuestions}";

		int number1 = random.Next(1, 10);
		int number2 = random.Next(1, 10);

		int operation = random.Next(0, 3);

		string operationSymbol;

		switch (operation)
		{
			case 0:

				operationSymbol = "+";

				correctAnswer =
					number1 + number2;

				break;

			case 1:

				if (number2 > number1)
				{
					int temp = number1;

					number1 = number2;
					number2 = temp;
				}

				operationSymbol = "−";

				correctAnswer =
					number1 - number2;

				break;

			case 2:

				operationSymbol = "×";

				correctAnswer =
					number1 * number2;

				break;

			default:

				operationSymbol = "+";

				correctAnswer =
					number1 + number2;

				break;
		}

		questionLabel.Text =
			$"{number1} {operationSymbol} {number2}";

		GenerateAnswers();
	}

	private void GenerateAnswers()
	{
		int wrongAnswer1;

		int wrongAnswer2;

		do
		{
			wrongAnswer1 =
				correctAnswer + random.Next(-5, 6);

		}
		while (
			wrongAnswer1 == correctAnswer ||
			wrongAnswer1 < 0
		);

		do
		{
			wrongAnswer2 =
				correctAnswer + random.Next(-7, 8);

		}
		while (
			wrongAnswer2 == correctAnswer ||
			wrongAnswer2 == wrongAnswer1 ||
			wrongAnswer2 < 0
		);

		int[] answers =
		{
			correctAnswer,
			wrongAnswer1,
			wrongAnswer2
		};

		for (int i = answers.Length - 1; i > 0; i--)
		{
			int j = random.Next(i + 1);

			int temp = answers[i];

			answers[i] = answers[j];

			answers[j] = temp;
		}

		answerButton1.Text =
			$"☄️\n{answers[0]}";

		answerButton2.Text =
			$"☄️\n{answers[1]}";

		answerButton3.Text =
			$"☄️\n{answers[2]}";
	}

	private async void CheckAnswer(Button clickedButton)
	{
		if (!canAnswer)
			return;

		canAnswer = false;

		int selectedAnswer =
			int.Parse(
				clickedButton.Text
					.Replace("☄️", "")
					.Trim()
			);

		if (selectedAnswer == correctAnswer)
		{
			xp += 10;

			xpLabel.Text =
				$"⭐ XP: {xp}";

			messageLabel.Text =
				"🎉 MUITO BEM!\n+10 XP";

			await ToSignal(
				GetTree().CreateTimer(2.0),
				SceneTreeTimer.SignalName.Timeout
			);

			NextQuestion();
		}
		else
		{
			lives--;

			UpdateLives();

			if (lives <= 0)
			{
				messageLabel.Text =
					"💥 SUAS VIDAS ACABARAM!";

				await ToSignal(
					GetTree().CreateTimer(1.5),
					SceneTreeTimer.SignalName.Timeout
				);

				ShowGameOver();

				return;
			}

			messageLabel.Text =
				"🤔 QUASE!\nA resposta não era essa.";

			await ToSignal(
				GetTree().CreateTimer(2.0),
				SceneTreeTimer.SignalName.Timeout
			);

			NextQuestion();
		}
	}

	private void NextQuestion()
	{
		currentQuestion++;

		if (currentQuestion > TotalQuestions)
		{
			ShowVictory();

			return;
		}

		StartQuestion();
	}

	private void UpdateLives()
	{
		string hearts = "";

		for (int i = 0; i < lives; i++)
		{
			hearts += "❤️ ";
		}

		livesLabel.Text = hearts;
	}

	private void ShowVictory()
	{
		canAnswer = false;

		questionLabel.Text =
			"🏆 MISSÃO CONCLUÍDA!";

		messageLabel.Text =
			$"Você completou a missão!\n⭐ Pontuação: {xp} XP";

		answerButton1.Text =
			"🚀 JOGAR NOVAMENTE";

		answerButton2.Text = "";
		answerButton3.Text = "";

		answerButton2.Disabled = true;
		answerButton3.Disabled = true;

		answerButton1.Pressed -= () =>
			CheckAnswer(answerButton1);

		answerButton1.Pressed += RestartGame;
	}

	private void ShowGameOver()
	{
		canAnswer = false;

		questionLabel.Text =
			"💥 FIM DA MISSÃO";

		messageLabel.Text =
			$"Suas vidas acabaram!\n⭐ Pontuação: {xp} XP";

		answerButton1.Text =
			"🔄 TENTAR NOVAMENTE";

		answerButton2.Text = "";
		answerButton3.Text = "";

		answerButton2.Disabled = true;
		answerButton3.Disabled = true;

		answerButton1.Pressed -= () =>
			CheckAnswer(answerButton1);

		answerButton1.Pressed += RestartGame;
	}

	private void RestartGame()
	{
		xp = 0;

		lives = 3;

		currentQuestion = 1;

		answerButton2.Disabled = false;
		answerButton3.Disabled = false;

		xpLabel.Text =
			"⭐ XP: 0";

		UpdateLives();

		answerButton1.Pressed -= RestartGame;

		answerButton1.Pressed += () =>
			CheckAnswer(answerButton1);

		StartQuestion();
	}
}
