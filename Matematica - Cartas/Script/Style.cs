using Godot;

public static class Style
{
	public static void AplicarEstilo(
		Control numberPanel,
		Control operationPanel,
		Control answerPanel,
		Control finalPanel,
		Button[] numberButtons,
		Button[] operationButtons,
		Label equationLabel,
		Label messageLabel,
		LineEdit answerInput,
		Button confirmButton,
		Label finalLabel,
		Button tryAgainButton,
		Button exitButton)
	{
		EstilizarPainel(
			numberPanel,
			new Vector2(35, 110),
			new Vector2(530, 430),
			new Color(0.95f, 0.97f, 1.0f),
			new Color(0.25f, 0.55f, 0.95f)
		);

		EstilizarPainel(
			operationPanel,
			new Vector2(590, 110),
			new Vector2(300, 430),
			new Color(1.0f, 0.95f, 0.82f),
			new Color(1.0f, 0.65f, 0.15f)
		);

		EstilizarPainel(
			answerPanel,
			new Vector2(35, 570),
			new Vector2(855, 220),
			new Color(0.90f, 0.98f, 0.92f),
			new Color(0.25f, 0.75f, 0.45f)
		);

		EstilizarNumeros(numberButtons);

		EstilizarOperacoes(operationButtons);

		EstilizarResposta(
			equationLabel,
			messageLabel,
			answerInput,
			confirmButton
		);

		EstilizarFinal(
			finalPanel,
			finalLabel,
			tryAgainButton,
			exitButton
		);
	}


	private static void EstilizarNumeros(
		Button[] buttons)
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			Button button = buttons[i];

			int coluna = i % 3;
			int linha = i / 3;

			button.Position =
				new Vector2(
					35 + coluna * 160,
					30 + linha * 125
				);

			button.Size =
				new Vector2(
					130,
					100
				);

			button.AddThemeFontSizeOverride(
				"font_size",
				38
			);

			button.AddThemeColorOverride(
				"font_color",
				Colors.White
			);

			button.AddThemeColorOverride(
				"font_hover_color",
				Colors.White
			);

			button.AddThemeColorOverride(
				"font_pressed_color",
				Colors.White
			);

			button.AddThemeColorOverride(
				"font_disabled_color",
				new Color(
					1.0f,
					1.0f,
					1.0f,
					1.0f
				)
			);

			button.AddThemeStyleboxOverride(
				"normal",
				CriarBotao(
					new Color(
						0.25f,
						0.60f,
						1.0f
					),
					new Color(
						0.12f,
						0.38f,
						0.78f
					),
					18
				)
			);

			button.AddThemeStyleboxOverride(
				"hover",
				CriarBotao(
					new Color(
						0.40f,
						0.72f,
						1.0f
					),
					new Color(
						0.12f,
						0.38f,
						0.78f
					),
					18
				)
			);

			button.AddThemeStyleboxOverride(
				"pressed",
				CriarBotao(
					new Color(
						0.18f,
						0.48f,
						0.90f
					),
					new Color(
						0.08f,
						0.28f,
						0.65f
					),
					18
				)
			);

			button.AddThemeStyleboxOverride(
				"disabled",
				CriarBotao(
					new Color(
						0.55f,
						0.65f,
						0.78f
					),
					new Color(
						0.40f,
						0.48f,
						0.60f
					),
					18
				)
			);
		}
	}


	private static void EstilizarOperacoes(
		Button[] buttons)
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			Button button = buttons[i];

			button.Position =
				new Vector2(
					45,
					35 + i * 120
				);

			button.Size =
				new Vector2(
					210,
					85
				);

			button.AddThemeFontSizeOverride(
				"font_size",
				38
			);

			button.AddThemeColorOverride(
				"font_color",
				Colors.White
			);

			button.AddThemeColorOverride(
				"font_hover_color",
				Colors.White
			);

			button.AddThemeColorOverride(
				"font_pressed_color",
				Colors.White
			);

			button.AddThemeColorOverride(
				"font_disabled_color",
				new Color(
					1.0f,
					1.0f,
					1.0f,
					1.0f
				)
			);

			button.AddThemeStyleboxOverride(
				"normal",
				CriarBotao(
					new Color(
						0.95f,
						0.55f,
						0.18f
					),
					new Color(
						0.78f,
						0.36f,
						0.08f
					),
					20
				)
			);

			button.AddThemeStyleboxOverride(
				"hover",
				CriarBotao(
					new Color(
						1.0f,
						0.68f,
						0.28f
					),
					new Color(
						0.78f,
						0.36f,
						0.08f
					),
					20
				)
			);

			button.AddThemeStyleboxOverride(
				"pressed",
				CriarBotao(
					new Color(
						0.90f,
						0.42f,
						0.10f
					),
					new Color(
						0.65f,
						0.25f,
						0.05f
					),
					20
				)
			);

			button.AddThemeStyleboxOverride(
				"disabled",
				CriarBotao(
					new Color(
						0.78f,
						0.68f,
						0.52f
					),
					new Color(
						0.58f,
						0.46f,
						0.30f
					),
					20
				)
			);
		}
	}


	private static void EstilizarResposta(
		Label equationLabel,
		Label messageLabel,
		LineEdit answerInput,
		Button confirmButton)
	{
		equationLabel.Position =
			new Vector2(
				25,
				20
			);

		equationLabel.Size =
			new Vector2(
				805,
				55
			);

		equationLabel.HorizontalAlignment =
			HorizontalAlignment.Center;

		equationLabel.VerticalAlignment =
			VerticalAlignment.Center;

		equationLabel.AddThemeFontSizeOverride(
			"font_size",
			30
		);

		equationLabel.AddThemeColorOverride(
			"font_color",
			new Color(
				0.12f,
				0.35f,
				0.55f
			)
		);

		answerInput.Position =
			new Vector2(
				100,
				90
			);

		answerInput.Size =
			new Vector2(
				270,
				65
			);

		answerInput.Alignment =
			HorizontalAlignment.Center;

		answerInput.AddThemeFontSizeOverride(
			"font_size",
			28
		);

		answerInput.AddThemeColorOverride(
			"font_color",
			new Color(
				0.12f,
				0.25f,
				0.35f
			)
		);

		answerInput.AddThemeStyleboxOverride(
			"normal",
			CriarBotao(
				Colors.White,
				new Color(
					0.25f,
					0.65f,
					0.45f
				),
				15
			)
		);

		confirmButton.Position =
			new Vector2(
				410,
				90
			);

		confirmButton.Size =
			new Vector2(
				300,
				65
			);

		confirmButton.AddThemeFontSizeOverride(
			"font_size",
			25
		);

		confirmButton.AddThemeColorOverride(
			"font_color",
			Colors.White
		);

		confirmButton.AddThemeColorOverride(
			"font_hover_color",
			Colors.White
		);

		confirmButton.AddThemeColorOverride(
			"font_pressed_color",
			Colors.White
		);

		confirmButton.AddThemeStyleboxOverride(
			"normal",
			CriarBotao(
				new Color(
					0.25f,
					0.78f,
					0.42f
				),
				new Color(
					0.10f,
					0.50f,
					0.22f
				),
				20
			)
		);

		confirmButton.AddThemeStyleboxOverride(
			"hover",
			CriarBotao(
				new Color(
					0.38f,
					0.88f,
					0.53f
				),
				new Color(
					0.10f,
					0.50f,
					0.22f
				),
				20
			)
		);

		confirmButton.AddThemeStyleboxOverride(
			"pressed",
			CriarBotao(
				new Color(
					0.16f,
					0.65f,
					0.32f
				),
				new Color(
					0.07f,
					0.38f,
					0.15f
				),
				20
			)
		);

		confirmButton.AddThemeStyleboxOverride(
			"disabled",
			CriarBotao(
				new Color(
					0.55f,
					0.70f,
					0.60f
				),
				new Color(
					0.38f,
					0.50f,
					0.42f
				),
				20
			)
		);

		messageLabel.Position =
			new Vector2(
				25,
				165
			);

		messageLabel.Size =
			new Vector2(
				805,
				40
			);

		messageLabel.HorizontalAlignment =
			HorizontalAlignment.Center;

		messageLabel.VerticalAlignment =
			VerticalAlignment.Center;

		messageLabel.AddThemeFontSizeOverride(
			"font_size",
			19
		);
	}


	private static void EstilizarFinal(
		Control panel,
		Label label,
		Button tryAgain,
		Button exit)
	{
		panel.Visible = false;

		panel.ZIndex = 100;

		panel.SetAsTopLevel(true);

		panel.Size =
			new Vector2(
				560,
				420
			);

		label.Position =
			new Vector2(
				40,
				35
			);

		label.Size =
			new Vector2(
				480,
				145
			);

		label.HorizontalAlignment =
			HorizontalAlignment.Center;

		label.VerticalAlignment =
			VerticalAlignment.Center;

		label.AddThemeFontSizeOverride(
			"font_size",
			36
		);

		tryAgain.Position =
			new Vector2(
				80,
				205
			);

		tryAgain.Size =
			new Vector2(
				400,
				70
			);

		tryAgain.AddThemeFontSizeOverride(
			"font_size",
			24
		);

		tryAgain.AddThemeColorOverride(
			"font_color",
			Colors.White
		);

		tryAgain.AddThemeColorOverride(
			"font_hover_color",
			Colors.White
		);

		tryAgain.AddThemeColorOverride(
			"font_pressed_color",
			Colors.White
		);

		tryAgain.AddThemeStyleboxOverride(
			"normal",
			CriarBotao(
				new Color(
					0.20f,
					0.78f,
					0.42f
				),
				new Color(
					0.08f,
					0.48f,
					0.20f
				),
				20
			)
		);

		tryAgain.AddThemeStyleboxOverride(
			"hover",
			CriarBotao(
				new Color(
					0.35f,
					0.90f,
					0.55f
				),
				new Color(
					0.08f,
					0.48f,
					0.20f
				),
				20
			)
		);

		tryAgain.AddThemeStyleboxOverride(
			"pressed",
			CriarBotao(
				new Color(
					0.12f,
					0.62f,
					0.30f
				),
				new Color(
					0.05f,
					0.36f,
					0.14f
				),
				20
			)
		);

		exit.Position =
			new Vector2(
				80,
				300
			);

		exit.Size =
			new Vector2(
				400,
				65
			);

		exit.AddThemeFontSizeOverride(
			"font_size",
			23
		);

		exit.AddThemeColorOverride(
			"font_color",
			Colors.White
		);

		exit.AddThemeColorOverride(
			"font_hover_color",
			Colors.White
		);

		exit.AddThemeColorOverride(
			"font_pressed_color",
			Colors.White
		);

		exit.AddThemeStyleboxOverride(
			"normal",
			CriarBotao(
				new Color(
					0.92f,
					0.30f,
					0.30f
				),
				new Color(
					0.65f,
					0.12f,
					0.12f
				),
				20
			)
		);

		exit.AddThemeStyleboxOverride(
			"hover",
			CriarBotao(
				new Color(
					1.0f,
					0.45f,
					0.45f
				),
				new Color(
					0.65f,
					0.12f,
					0.12f
				),
				20
			)
		);

		exit.AddThemeStyleboxOverride(
			"pressed",
			CriarBotao(
				new Color(
					0.78f,
					0.20f,
					0.20f
				),
				new Color(
					0.50f,
					0.08f,
					0.08f
				),
				20
			)
		);

		StyleBoxFlat panelStyle =
			new StyleBoxFlat();

		panelStyle.BgColor =
			new Color(
				1.0f,
				0.97f,
				0.82f
			);

		panelStyle.BorderColor =
			new Color(
				1.0f,
				0.65f,
				0.12f
			);

		panelStyle.BorderWidthLeft = 7;
		panelStyle.BorderWidthRight = 7;
		panelStyle.BorderWidthTop = 7;
		panelStyle.BorderWidthBottom = 7;

		panelStyle.CornerRadiusTopLeft = 35;
		panelStyle.CornerRadiusTopRight = 35;
		panelStyle.CornerRadiusBottomLeft = 35;
		panelStyle.CornerRadiusBottomRight = 35;

		panelStyle.ShadowColor =
			new Color(
				0,
				0,
				0,
				0.30f
			);

		panelStyle.ShadowSize = 18;

		panel.AddThemeStyleboxOverride(
			"panel",
			panelStyle
		);
	}


	private static void EstilizarPainel(
		Control panel,
		Vector2 position,
		Vector2 size,
		Color background,
		Color border)
	{
		panel.Position =
			position;

		panel.Size =
			size;

		StyleBoxFlat style =
			new StyleBoxFlat();

		style.BgColor =
			background;

		style.BorderColor =
			border;

		style.BorderWidthLeft = 5;
		style.BorderWidthRight = 5;
		style.BorderWidthTop = 5;
		style.BorderWidthBottom = 5;

		style.CornerRadiusTopLeft = 28;
		style.CornerRadiusTopRight = 28;
		style.CornerRadiusBottomLeft = 28;
		style.CornerRadiusBottomRight = 28;

		style.ShadowColor =
			new Color(
				0,
				0,
				0,
				0.18f
			);

		style.ShadowSize = 10;

		panel.AddThemeStyleboxOverride(
			"panel",
			style
		);
	}


	private static StyleBoxFlat CriarBotao(
		Color background,
		Color border,
		int radius)
	{
		StyleBoxFlat style =
			new StyleBoxFlat();

		style.BgColor =
			background;

		style.BorderColor =
			border;

		style.BorderWidthLeft = 4;
		style.BorderWidthRight = 4;
		style.BorderWidthTop = 4;
		style.BorderWidthBottom = 4;

		style.CornerRadiusTopLeft =
			radius;

		style.CornerRadiusTopRight =
			radius;

		style.CornerRadiusBottomLeft =
			radius;

		style.CornerRadiusBottomRight =
			radius;

		style.ContentMarginLeft = 10;
		style.ContentMarginRight = 10;
		style.ContentMarginTop = 10;
		style.ContentMarginBottom = 10;

		style.ShadowColor =
			new Color(
				0,
				0,
				0,
				0.20f
			);

		style.ShadowSize = 6;

		return style;
	}


	public static void AtualizarPosicaoPainelFinal(
		Control panel)
	{
		Vector2 tamanhoTela =
			panel.GetViewportRect().Size;

		panel.Position =
			(tamanhoTela - panel.Size) / 2.0f;
	}
}
