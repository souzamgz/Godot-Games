using Godot;

public static class Style
{
	public static StyleBoxFlat CreateBox(
		Color backgroundColor,
		Color borderColor,
		int borderWidth = 0,
		int cornerRadius = 15
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

	public static void ApplyButtonStyle(
		Button button,
		Color normalColor,
		Color hoverColor,
		Color borderColor,
		int borderWidth = 3
	)
	{
		button.AddThemeStyleboxOverride(
			"normal",
			CreateBox(
				normalColor,
				borderColor,
				borderWidth,
				20
			)
		);

		button.AddThemeStyleboxOverride(
			"hover",
			CreateBox(
				hoverColor,
				borderColor,
				borderWidth + 1,
				20
			)
		);

		button.AddThemeStyleboxOverride(
			"pressed",
			CreateBox(
				hoverColor,
				borderColor,
				borderWidth + 1,
				20
			)
		);

		button.AddThemeStyleboxOverride(
			"disabled",
			CreateBox(
				new Color("#161B35"),
				new Color("#30385F"),
				2,
				20
			)
		);
	}

	public static void ApplyAnswerButton(
		Button button
	)
	{
		button.Size =
			new Vector2(220, 100);

		button.AddThemeFontSizeOverride(
			"font_size",
			30
		);

		ApplyButtonStyle(
			button,
			new Color("#27346B"),
			new Color("#35498A"),
			new Color("#657BD2")
		);
	}

	public static void ApplyActionButton(
		Button button,
		Color color,
		Color hoverColor
	)
	{
		button.AddThemeFontSizeOverride(
			"font_size",
			19
		);

		ApplyButtonStyle(
			button,
			color,
			hoverColor,
			new Color("#9BABFF")
		);
	}
}
