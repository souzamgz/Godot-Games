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
        Color borderColor,
        Color hoverColor,
        int borderWidth = 3
    )
    {
        StyleBoxFlat normal =
            CreateBox(
                normalColor,
                borderColor,
                borderWidth,
                25
            );

        StyleBoxFlat hover =
            CreateBox(
                hoverColor,
                borderColor.Lightened(0.15f),
                borderWidth + 1,
                25
            );

        StyleBoxFlat pressed =
            CreateBox(
                normalColor.Darkened(0.12f),
                borderColor,
                borderWidth + 1,
                25
            );

        StyleBoxFlat disabled =
            CreateBox(
                normalColor.Darkened(0.20f),
                borderColor.Darkened(0.20f),
                borderWidth,
                25
            );

        Theme theme =
            new Theme();

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

    public static void ApplyActionButton(
        Button button,
        Color backgroundColor,
        Color borderColor
    )
    {
        StyleBoxFlat normal =
            CreateBox(
                backgroundColor,
                borderColor,
                3,
                15
            );

        StyleBoxFlat hover =
            CreateBox(
                backgroundColor.Lightened(0.15f),
                borderColor.Lightened(0.15f),
                4,
                15
            );

        StyleBoxFlat pressed =
            CreateBox(
                backgroundColor.Darkened(0.10f),
                borderColor,
                4,
                15
            );

        Theme theme =
            new Theme();

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

        button.Theme =
            theme;
    }
}