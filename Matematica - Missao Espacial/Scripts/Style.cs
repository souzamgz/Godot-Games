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
}
