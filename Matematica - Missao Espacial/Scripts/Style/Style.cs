using Godot;

public static class Style
{
    public static readonly Color BackgroundColor =
        new Color("#080D24");

    public static readonly Color PanelColor =
        new Color("#101938");

    public static readonly Color PanelBorderColor =
        new Color("#5D73D8");

    public static readonly Color ButtonNormalColor =
        new Color("#18244C");

    public static readonly Color ButtonBorderColor =
        new Color("#5368B7");

    public static readonly Color ButtonHoverColor =
        new Color("#30477F");

    public static readonly Color ButtonSelectedColor =
        new Color("#3155A6");

    public static readonly Color ButtonSelectedBorderColor =
        new Color("#AFC0FF");

    public static readonly Color ButtonCompletedColor =
        new Color("#244E45");

    public static readonly Color ButtonCompletedBorderColor =
        new Color("#5BC7A6");

    public static readonly Color ButtonLockedColor =
        new Color("#11172F");

    public static readonly Color ButtonLockedBorderColor =
        new Color("#30385C");

    public static readonly Color StartButtonColor =
        new Color("#3155A6");

    public static readonly Color StartButtonBorderColor =
        new Color("#7E95F0");

    public static readonly Color ExitButtonColor =
        new Color("#7A3151");

    public static readonly Color ExitButtonBorderColor =
        new Color("#A9446B");

    public static readonly Color DisabledButtonColor =
        new Color("#252A40");

    public static readonly Color DisabledButtonBorderColor =
        new Color("#444B69");

    // Tela de jogo

    public static readonly Color GameQuestionPanelColor =
        new Color("#151F4A");

    public static readonly Color GameQuestionPanelBorderColor =
        new Color("#6176D0");

    public static readonly Color MeteorPanelColor =
        new Color("#271C43");

    public static readonly Color MeteorPanelBorderColor =
        new Color("#8060C3");

    public static readonly Color AnswerButtonColor =
        new Color("#27346B");

    public static readonly Color AnswerButtonBorderColor =
        new Color("#657BD2");

    public static readonly Color DefeatPanelColor =
        new Color("#351C3D");

    public static readonly Color DefeatPanelBorderColor =
        new Color("#B85C9A");

    public static readonly Color VictoryPanelColor =
        new Color("#183B38");

    public static readonly Color VictoryPanelBorderColor =
        new Color("#5BC7A6");

    // Boss

    public static readonly Color BossPanelColor =
        new Color("#421D4F");

    public static readonly Color BossPanelBorderColor =
        new Color("#D06BE8");

    public static readonly Color BossButtonColor =
        new Color("#542568");

    public static readonly Color BossButtonBorderColor =
        new Color("#D06BE8");

    public static readonly Color BossButtonHoverColor =
        new Color("#74388A");

    // Caixa padrão

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

    // Botões

    public static void ApplyButtonStyle(
        Button button,
        Color normalColor,
        Color borderColor,
        Color hoverColor,
        int borderWidth = 3
    )
    {
        if (button == null)
        {
            return;
        }

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
        if (button == null)
        {
            return;
        }

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

        StyleBoxFlat disabled =
            CreateBox(
                backgroundColor.Darkened(0.20f),
                borderColor.Darkened(0.20f),
                3,
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

        theme.SetStylebox(
            "disabled",
            "Button",
            disabled
        );

        button.Theme =
            theme;
    }

    // Painéis

    public static void ApplyPanelStyle(
        Panel panel,
        Color backgroundColor,
        Color borderColor
    )
    {
        if (panel == null)
        {
            return;
        }

        StyleBoxFlat panelStyle =
            CreateBox(
                backgroundColor,
                borderColor,
                3,
                30
            );

        Theme theme =
            new Theme();

        theme.SetStylebox(
            "panel",
            "Panel",
            panelStyle
        );

        panel.Theme =
            theme;
    }

    // Planetas

    public static void ApplyPlanetNormalStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            ButtonNormalColor,
            ButtonBorderColor,
            ButtonHoverColor,
            3
        );
    }

    public static void ApplyPlanetSelectedStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            ButtonSelectedColor,
            ButtonSelectedBorderColor,
            ButtonSelectedColor.Lightened(0.15f),
            5
        );
    }

    public static void ApplyPlanetCompletedStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            ButtonCompletedColor,
            ButtonCompletedBorderColor,
            ButtonCompletedColor.Lightened(0.15f),
            3
        );
    }

    public static void ApplyPlanetLockedStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            ButtonLockedColor,
            ButtonLockedBorderColor,
            ButtonLockedColor,
            3
        );
    }

    // Boss na seleção de planetas

    public static void ApplyBossNormalStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            BossButtonColor,
            BossButtonBorderColor,
            BossButtonHoverColor,
            4
        );
    }

    public static void ApplyBossSelectedStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            BossButtonHoverColor,
            new Color("#F1A7FF"),
            new Color("#8C4FA5"),
            5
        );
    }

    public static void ApplyBossCompletedStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            new Color("#3E2850"),
            new Color("#C77ADB"),
            new Color("#5D3B70"),
            4
        );
    }

    public static void ApplyBossLockedStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            BossButtonColor.Darkened(0.35f),
            BossButtonBorderColor.Darkened(0.35f),
            BossButtonColor.Darkened(0.25f),
            3
        );
    }

    // Painel principal do Boss

    public static void ApplyBossPanelStyle(
        Panel panel
    )
    {
        ApplyPanelStyle(
            panel,
            BossPanelColor,
            BossPanelBorderColor
        );
    }

    // Botões principais

    public static void ApplyStartButtonStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            StartButtonColor,
            StartButtonBorderColor,
            StartButtonColor.Lightened(0.15f),
            3
        );
    }

    public static void ApplyStartButtonCompletedStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            ButtonSelectedColor,
            ButtonSelectedBorderColor,
            ButtonSelectedColor.Lightened(0.15f),
            4
        );
    }

    public static void ApplyDisabledActionStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            DisabledButtonColor,
            DisabledButtonBorderColor,
            DisabledButtonColor,
            3
        );
    }

    public static void ApplyExitButtonStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            ExitButtonColor,
            ExitButtonBorderColor,
            ExitButtonColor.Lightened(0.15f),
            3
        );
    }

    // Tela de jogo

    public static void ApplyQuestionPanelStyle(
        Panel panel
    )
    {
        ApplyPanelStyle(
            panel,
            GameQuestionPanelColor,
            GameQuestionPanelBorderColor
        );
    }

    public static void ApplyMeteorPanelStyle(
        Panel panel
    )
    {
        ApplyPanelStyle(
            panel,
            MeteorPanelColor,
            MeteorPanelBorderColor
        );
    }

    public static void ApplyAnswerButtonStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            AnswerButtonColor,
            AnswerButtonBorderColor,
            ButtonHoverColor,
            3
        );
    }

    public static void ApplyGameActionButtonStyle(
        Button button
    )
    {
        ApplyAnswerButtonStyle(
            button
        );
    }

    // Tela de jogo do Boss

    public static void ApplyBossQuestionPanelStyle(
        Panel panel
    )
    {
        ApplyPanelStyle(
            panel,
            BossPanelColor.Darkened(0.10f),
            BossPanelBorderColor
        );
    }

    public static void ApplyBossAnswerButtonStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            BossButtonColor,
            BossButtonBorderColor,
            BossButtonHoverColor,
            4
        );
    }

    // Derrota

    public static void ApplyDefeatPanelStyle(
        Panel panel
    )
    {
        ApplyPanelStyle(
            panel,
            DefeatPanelColor,
            DefeatPanelBorderColor
        );
    }

    public static void ApplyDefeatRestartStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            StartButtonColor,
            StartButtonBorderColor,
            StartButtonColor.Lightened(0.15f),
            3
        );
    }

    public static void ApplyDefeatExitStyle(
        Button button
    )
    {
        ApplyExitButtonStyle(
            button
        );
    }

    // Vitória

    public static void ApplyVictoryPanelStyle(
        Panel panel
    )
    {
        ApplyPanelStyle(
            panel,
            VictoryPanelColor,
            VictoryPanelBorderColor
        );
    }

    public static void ApplyVictoryNextStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            StartButtonColor,
            StartButtonBorderColor,
            StartButtonColor.Lightened(0.15f),
            3
        );
    }

    public static void ApplyVictoryMenuStyle(
        Button button
    )
    {
        ApplyButtonStyle(
            button,
            ExitButtonColor,
            ExitButtonBorderColor,
            ExitButtonColor.Lightened(0.15f),
            3
        );
    }

    public static void ApplyUnlockAllButtonStyle(
    Button button
)
    {
        ApplyButtonStyle(
            button,
            new Color("#6B4FA3"),
            new Color("#C9A7FF"),
            new Color("#8A68C7"),
            3
        );
    }

    // Fonte

    public static void SetFontSize(
        Control control,
        int size
    )
    {
        if (control == null)
        {
            return;
        }

        control.AddThemeFontSizeOverride(
            "font_size",
            size
        );
    }

    public static void CenterLabel(
        Label label
    )
    {
        if (label == null)
        {
            return;
        }

        label.HorizontalAlignment =
            HorizontalAlignment.Center;

        label.VerticalAlignment =
            VerticalAlignment.Center;
    }
}

