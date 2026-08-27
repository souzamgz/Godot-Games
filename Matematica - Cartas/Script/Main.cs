using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Control
{
	private Random random = new Random();

	private Button[] numberButtons;
	private Button[] operationButtons;

	private Button confirmButton;
	private Button tryAgainButton;
	private Button exitButton;

	private Label equationLabel;
	private Label messageLabel;
	private Label finalLabel;

	private LineEdit answerInput;

	private Control numberPanel;
	private Control operationPanel;
	private Control answerPanel;
	private Control finalPanel;

	private List<int> availableNumbers = new List<int>();
	private List<string> availableOperations = new List<string>();

	private int firstNumber = -1;
	private int secondNumber = -1;

	private Button firstNumberButton = null;
	private Button secondNumberButton = null;

	private string selectedOperation = "";

	private Button selectedOperationButton = null;


	public override void _Ready()
	{
		numberPanel =
			GetNode<Control>("pnlNumeros");

		operationPanel =
			GetNode<Control>("pnlOperacoes");

		answerPanel =
			GetNode<Control>("pnlResposta");

		finalPanel =
			GetNode<Control>("pnlFinal");

		numberButtons = new Button[]
		{
			GetNode<Button>("pnlNumeros/btnNum1"),
			GetNode<Button>("pnlNumeros/btnNum2"),
			GetNode<Button>("pnlNumeros/btnNum3"),
			GetNode<Button>("pnlNumeros/btnNum4"),
			GetNode<Button>("pnlNumeros/btnNum5"),
			GetNode<Button>("pnlNumeros/btnNum6"),
			GetNode<Button>("pnlNumeros/btnNum7"),
			GetNode<Button>("pnlNumeros/btnNum8"),
			GetNode<Button>("pnlNumeros/btnNum9")
		};

		operationButtons = new Button[]
		{
			GetNode<Button>("pnlOperacoes/btnAdd"),
			GetNode<Button>("pnlOperacoes/btnSub"),
			GetNode<Button>("pnlOperacoes/btnMult")
		};

		equationLabel =
			GetNode<Label>("pnlResposta/lblEquacao");

		messageLabel =
			GetNode<Label>("pnlResposta/lblMensagem");

		answerInput =
			GetNode<LineEdit>("pnlResposta/txtResposta");

		confirmButton =
			GetNode<Button>("pnlResposta/btnConfirmar");

		finalLabel =
			GetNode<Label>("pnlFinal/lblTextoFinal");

		tryAgainButton =
			GetNode<Button>("pnlFinal/btnTentarNovamente");

		exitButton =
			GetNode<Button>("pnlFinal/btnSair");

		foreach (Button button in numberButtons)
		{
			Button currentButton = button;

			currentButton.Pressed += () =>
			{
				ClicarNumero(currentButton);
			};
		}

		foreach (Button button in operationButtons)
		{
			Button currentButton = button;

			currentButton.Pressed += () =>
			{
				ClicarOperacao(currentButton);
			};
		}

		confirmButton.Pressed += VerificarResposta;

		tryAgainButton.Pressed += TentarNovamente;

		exitButton.Pressed += SairDoJogo;

		Style.AplicarEstilo(
			numberPanel,
			operationPanel,
			answerPanel,
			finalPanel,
			numberButtons,
			operationButtons,
			equationLabel,
			messageLabel,
			answerInput,
			confirmButton,
			finalLabel,
			tryAgainButton,
			exitButton
		);

		IniciarJogo();
	}


	private void IniciarJogo()
	{
		firstNumber = -1;
		secondNumber = -1;

		firstNumberButton = null;
		secondNumberButton = null;

		selectedOperation = "";

		selectedOperationButton = null;

		equationLabel.Text = "";

		messageLabel.Text = "";

		answerInput.Text = "";

		answerInput.Editable = true;

		confirmButton.Disabled = true;

		availableNumbers.Clear();

		for (int i = 1; i <= 9; i++)
		{
			availableNumbers.Add(i);
		}

		availableOperations.Clear();

		availableOperations.Add("+");
		availableOperations.Add("-");
		availableOperations.Add("×");

		foreach (Button button in numberButtons)
		{
			button.Text = "?";
			button.Disabled = false;
		}

		foreach (Button button in operationButtons)
		{
			button.Text = "?";
			button.Disabled = true;
		}

		finalPanel.Visible = false;
	}


	private void ClicarNumero(Button button)
	{
		if (firstNumberButton != null &&
			secondNumberButton != null)
		{
			return;
		}

		if (availableNumbers.Count == 0)
		{
			return;
		}

		int randomIndex =
			random.Next(availableNumbers.Count);

		int selectedNumber =
			availableNumbers[randomIndex];

		availableNumbers.RemoveAt(randomIndex);

		if (firstNumberButton == null)
		{
			firstNumberButton = button;

			firstNumber = selectedNumber;

			button.Text =
				selectedNumber.ToString();

			button.Disabled = true;

			return;
		}

		secondNumberButton = button;

		secondNumber = selectedNumber;

		button.Text =
			selectedNumber.ToString();

		button.Disabled = true;

		foreach (Button numberButton in numberButtons)
		{
			if (numberButton != firstNumberButton &&
				numberButton != secondNumberButton)
			{
				numberButton.Disabled = true;
			}
		}

		foreach (Button operationButton in operationButtons)
		{
			operationButton.Disabled = false;
		}
	}


	private void ClicarOperacao(Button button)
	{
		if (firstNumberButton == null ||
			secondNumberButton == null)
		{
			return;
		}

		if (selectedOperationButton != null)
		{
			return;
		}

		if (availableOperations.Count == 0)
		{
			return;
		}

		int randomIndex =
			random.Next(availableOperations.Count);

		selectedOperation =
			availableOperations[randomIndex];

		availableOperations.RemoveAt(randomIndex);

		selectedOperationButton = button;

		button.Text =
			selectedOperation;

		button.Disabled = true;

		foreach (Button operationButton in operationButtons)
		{
			if (operationButton != button)
			{
				operationButton.Disabled = true;
			}
		}

		equationLabel.Text =
			firstNumber +
			" " +
			selectedOperation +
			" " +
			secondNumber +
			" = ?";

		confirmButton.Disabled = false;

		answerInput.GrabFocus();
	}


	private void VerificarResposta()
	{
		if (selectedOperation == "")
		{
			messageLabel.Text =
				"Escolha os números e a operação!";

			return;
		}

		string answerText =
			answerInput.Text.Trim();

		if (answerText == "")
		{
			messageLabel.Text =
				"Digite uma resposta!";

			return;
		}

		if (!double.TryParse(
			answerText,
			out double playerAnswer))
		{
			messageLabel.Text =
				"Digite apenas números!";

			return;
		}

		double correctAnswer = 0;

		switch (selectedOperation)
		{
			case "+":
				correctAnswer =
					firstNumber + secondNumber;
				break;

			case "-":
				correctAnswer =
					firstNumber - secondNumber;
				break;

			case "×":
				correctAnswer =
					firstNumber * secondNumber;
				break;
		}

		if (Math.Abs(
			playerAnswer - correctAnswer
		) < 0.0001)
		{
			finalLabel.Text =
				"PARABÉNS!\n\nVocê acertou!";

			finalLabel.AddThemeColorOverride(
				"font_color",
				new Color(
					0.10f,
					0.65f,
					0.30f
				)
			);
		}
		else
		{
			finalLabel.Text =
				"QUASE!\n\nTente novamente!";

			finalLabel.AddThemeColorOverride(
				"font_color",
				new Color(
					0.90f,
					0.35f,
					0.25f
				)
			);
		}

		confirmButton.Disabled = true;

		answerInput.Editable = false;

		foreach (Button button in numberButtons)
		{
			button.Disabled = true;
		}

		foreach (Button button in operationButtons)
		{
			button.Disabled = true;
		}

		finalPanel.Visible = true;

		Style.AtualizarPosicaoPainelFinal(
			finalPanel
		);
	}


	private void TentarNovamente()
	{
		IniciarJogo();
	}


	private void SairDoJogo()
	{
		GetTree().Quit();
	}


	public override void _Process(double delta)
	{
		if (finalPanel.Visible)
		{
			Style.AtualizarPosicaoPainelFinal(
				finalPanel
			);
		}
	}
}
