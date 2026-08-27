using Godot;
using System;

public partial class Main : Control
{
    private const int PlanetCount = 4;

    private readonly string[] planetNames =
    {
        "🌍 TERRA",
        "🌙 LUA",
        "🔴 MARTE",
        "🟣 JÚPITER"
    };

    private readonly string[] planetButtonsText =
    {
        "🌍\nTERRA\n+",
        "🌙\nLUA\n−",
        "🔴\nMARTE\n×",
        "🟣\nJÚPITER\n÷"
    };

    private static readonly bool[] planetCompleted =
    {
        false,
        false,
        false,
        false
    };

    private static readonly int[] planetScores =
    {
        0,
        0,
        0,
        0
    };

    private static readonly int[] planetAttempts =
    {
        0,
        0,
        0,
        0
    };

    private ColorRect background;
    private Panel startPanel;
    private Label titleLabel;
    private Label subtitleLabel;
    private Label difficultyLabel;
    private Label selectedPlanetLabel;

    private Button startButton;
    private Button exitButton;

    private Button planetButton1;
    private Button planetButton2;
    private Button planetButton3;
    private Button planetButton4;

    private Button previousDifficultyButton;
    private Button nextDifficultyButton;

    private int selectedPlanet = 0;

    public override void _Ready()
    {
        CreateLobby();
        SelectPlanet(0);
    }

    private void CreateLobby()
    {
        CreateBackground();
        CreateStars();
        CreateStartPanel();
        CreateTitle();
        CreateSubtitle();
        CreateDifficulty();
        CreatePlanetButtons();
        CreateDifficultyButtons();
        CreateSelectedPlanetLabel();
        CreateStartButton();
        CreateExitButton();
    }

    private void CreateBackground()
    {
        background = new ColorRect();

        background.SetAnchorsAndOffsetsPreset(
            Control.LayoutPreset.FullRect
        );

        background.Color = new Color("#080D24");

        AddChild(background);
        MoveChild(background, 0);
    }

    private void CreateStartPanel()
    {
        startPanel = new Panel();

        startPanel.Position = new Vector2(180, 70);
        startPanel.Size = new Vector2(920, 600);

        ApplyPanelTheme(
            startPanel,
            new Color("#101938"),
            new Color("#5D73D8")
        );

        AddChild(startPanel);
    }

    private void CreateTitle()
    {
        titleLabel = new Label();

        titleLabel.Text = "MISSÃO ESPACIAL";

        titleLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        titleLabel.Position = new Vector2(40, 35);
        titleLabel.Size = new Vector2(840, 80);

        titleLabel.AddThemeFontSizeOverride(
            "font_size",
            44
        );

        startPanel.AddChild(titleLabel);
    }

    private void CreateSubtitle()
    {
        subtitleLabel = new Label();

        subtitleLabel.Text =
            "Escolha um planeta para iniciar sua missão";

        subtitleLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        subtitleLabel.Position = new Vector2(40, 105);
        subtitleLabel.Size = new Vector2(840, 45);

        subtitleLabel.AddThemeFontSizeOverride(
            "font_size",
            21
        );

        startPanel.AddChild(subtitleLabel);
    }

    private void CreateDifficulty()
    {
        difficultyLabel = new Label();

        difficultyLabel.Text = "FÁCIL";

        difficultyLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        difficultyLabel.Position = new Vector2(40, 155);
        difficultyLabel.Size = new Vector2(840, 45);

        difficultyLabel.AddThemeFontSizeOverride(
            "font_size",
            27
        );

        startPanel.AddChild(difficultyLabel);
    }

    private void CreatePlanetButtons()
    {
        planetButton1 = CreatePlanetButton(
            planetButtonsText[0]
        );

        planetButton2 = CreatePlanetButton(
            planetButtonsText[1]
        );

        planetButton3 = CreatePlanetButton(
            planetButtonsText[2]
        );

        planetButton4 = CreatePlanetButton(
            planetButtonsText[3]
        );

        planetButton1.Position = new Vector2(80, 210);
        planetButton2.Position = new Vector2(285, 210);
        planetButton3.Position = new Vector2(490, 210);
        planetButton4.Position = new Vector2(695, 210);

        startPanel.AddChild(planetButton1);
        startPanel.AddChild(planetButton2);
        startPanel.AddChild(planetButton3);
        startPanel.AddChild(planetButton4);

        planetButton1.Pressed += () => SelectPlanet(0);
        planetButton2.Pressed += () => SelectPlanet(1);
        planetButton3.Pressed += () => SelectPlanet(2);
        planetButton4.Pressed += () => SelectPlanet(3);
    }

    private Button CreatePlanetButton(string text)
    {
        Button button = new Button();

        button.Text = text;
        button.Size = new Vector2(190, 210);

        button.AddThemeFontSizeOverride(
            "font_size",
            20
        );

        ApplyButtonTheme(
            button,
            new Color("#18244C"),
            new Color("#5368B7")
        );

        return button;
    }

    private void CreateDifficultyButtons()
    {
        previousDifficultyButton =
            CreateDifficultyButton("◀");

        nextDifficultyButton =
            CreateDifficultyButton("▶");

        previousDifficultyButton.Position =
            new Vector2(15, 275);

        nextDifficultyButton.Position =
            new Vector2(850, 275);

        startPanel.AddChild(previousDifficultyButton);
        startPanel.AddChild(nextDifficultyButton);

        previousDifficultyButton.Disabled = true;
        nextDifficultyButton.Disabled = true;
    }

    private Button CreateDifficultyButton(string text)
    {
        Button button = new Button();

        button.Text = text;
        button.Size = new Vector2(55, 80);

        button.AddThemeFontSizeOverride(
            "font_size",
            28
        );

        ApplyButtonTheme(
            button,
            new Color("#18244C"),
            new Color("#5368B7")
        );

        return button;
    }

    private void CreateSelectedPlanetLabel()
    {
        selectedPlanetLabel = new Label();

        selectedPlanetLabel.HorizontalAlignment =
            HorizontalAlignment.Center;

        selectedPlanetLabel.Position =
            new Vector2(40, 435);

        selectedPlanetLabel.Size =
            new Vector2(840, 45);

        selectedPlanetLabel.AddThemeFontSizeOverride(
            "font_size",
            22
        );

        startPanel.AddChild(selectedPlanetLabel);
    }

    private void CreateStartButton()
    {
        startButton = new Button();

        startButton.Position =
            new Vector2(300, 500);

        startButton.Size =
            new Vector2(320, 70);

        startButton.AddThemeFontSizeOverride(
            "font_size",
            24
        );

        ApplyButtonTheme(
            startButton,
            new Color("#3155A6"),
            new Color("#7E95F0")
        );

        startPanel.AddChild(startButton);

        startButton.Pressed += StartSelectedPlanet;
    }

    private void CreateExitButton()
    {
        exitButton = new Button();

        exitButton.Text = "SAIR";

        exitButton.Position =
            new Vector2(640, 515);

        exitButton.Size =
            new Vector2(150, 55);

        exitButton.AddThemeFontSizeOverride(
            "font_size",
            18
        );

        ApplyButtonTheme(
            exitButton,
            new Color("#7A3151"),
            new Color("#A9446B")
        );

        startPanel.AddChild(exitButton);

        exitButton.Pressed += ExitGame;
    }

    private void SelectPlanet(int planet)
    {
        if (planet < 0 || planet >= PlanetCount)
        {
            return;
        }

        if (!IsPlanetUnlocked(planet))
        {
            selectedPlanet = planet;
            UpdatePlanetVisuals();
            return;
        }

        selectedPlanet = planet;

        UpdatePlanetVisuals();
    }

    private void UpdatePlanetVisuals()
    {
        UpdatePlanetButton(
            planetButton1,
            0,
            planetButtonsText[0]
        );

        UpdatePlanetButton(
            planetButton2,
            1,
            planetButtonsText[1]
        );

        UpdatePlanetButton(
            planetButton3,
            2,
            planetButtonsText[2]
        );

        UpdatePlanetButton(
            planetButton4,
            3,
            planetButtonsText[3]
        );

        string status;

        if (planetCompleted[selectedPlanet])
        {
            status =
                $"{planetNames[selectedPlanet]}  •  CONCLUÍDO\n" +
                $"⭐ Pontuação: {planetScores[selectedPlanet]}   " +
                $"🔄 Tentativas: {planetAttempts[selectedPlanet]}";
        }
        else if (IsPlanetUnlocked(selectedPlanet))
        {
            status =
                $"{planetNames[selectedPlanet]}  •  DISPONÍVEL";
        }
        else
        {
            status =
                $"{planetNames[selectedPlanet]}  •  🔒 BLOQUEADO";
        }

        selectedPlanetLabel.Text = status;

        UpdateStartButton();
        UpdatePlanetAvailability();
    }

    private void UpdatePlanetButton(
        Button button,
        int planetIndex,
        string originalText
    )
    {
        button.Text = originalText;

        if (!IsPlanetUnlocked(planetIndex))
        {
            button.Text =
                $"{originalText}\n🔒 BLOQUEADO";

            button.Disabled = true;

            ApplyButtonTheme(
                button,
                new Color("#11172F"),
                new Color("#30385C"),
                3
            );

            return;
        }

        button.Disabled = false;

        if (selectedPlanet == planetIndex)
        {
            ApplyButtonTheme(
                button,
                new Color("#3155A6"),
                new Color("#AFC0FF"),
                5
            );
        }
        else if (planetCompleted[planetIndex])
        {
            button.Text =
                $"{originalText}\n✓ CONCLUÍDO";

            ApplyButtonTheme(
                button,
                new Color("#244E45"),
                new Color("#5BC7A6"),
                3
            );
        }
        else
        {
            ApplyButtonTheme(
                button,
                new Color("#18244C"),
                new Color("#5368B7"),
                3
            );
        }
    }
    
    private void UpdatePlanetAvailability()
    {
        if (planetButton1 == null)
        {
            return;
        }

        planetButton1.Disabled =
            !IsPlanetUnlocked(0);

        planetButton2.Disabled =
            !IsPlanetUnlocked(1);

        planetButton3.Disabled =
            !IsPlanetUnlocked(2);

        planetButton4.Disabled =
            !IsPlanetUnlocked(3);
    }

    private void UpdateStartButton()
    {
        if (planetCompleted[selectedPlanet])
        {
            startButton.Text = "REFAZER MISSÃO";

            ApplyButtonTheme(
                startButton,
                new Color("#3155A6"),
                new Color("#AFC0FF"),
                4
            );

            startButton.Disabled = false;

            return;
        }

        if (IsPlanetUnlocked(selectedPlanet))
        {
            startButton.Text = "INICIAR MISSÃO";

            ApplyButtonTheme(
                startButton,
                new Color("#3155A6"),
                new Color("#7E95F0"),
                3
            );

            startButton.Disabled = false;

            return;
        }

        startButton.Text = "🔒 PLANETA BLOQUEADO";

        ApplyButtonTheme(
            startButton,
            new Color("#252A40"),
            new Color("#444B69"),
            3
        );

        startButton.Disabled = true;
    }

    public bool IsPlanetUnlocked(int planet)
    {
        if (planet < 0 || planet >= PlanetCount)
        {
            return false;
        }

        if (planet == 0)
        {
            return true;
        }

        return planetCompleted[planet - 1];
    }

    public bool IsPlanetCompleted(int planet)
    {
        if (planet < 0 || planet >= PlanetCount)
        {
            return false;
        }

        return planetCompleted[planet];
    }

    private void StartSelectedPlanet()
    {
        if (!IsPlanetUnlocked(selectedPlanet))
        {
            return;
        }

        startPanel.Visible = false;

        background.Color =
            GetPlanetColor();

        switch (selectedPlanet)
        {
            case 0:
                StartPlanet(new Terra());
                break;

            case 1:
                StartPlanet(new Lua());
                break;

            case 2:
                StartPlanet(new Marte());
                break;

            case 3:
                StartPlanet(new Jupiter());
                break;
        }
    }

    public void OpenPlanet(int planet)
    {
        if (planet < 0 || planet >= PlanetCount)
        {
            return;
        }

        if (!IsPlanetUnlocked(planet))
        {
            return;
        }

        ReturnToLobby();

        selectedPlanet = planet;

        UpdatePlanetVisuals();

        StartSelectedPlanet();
    }

    private void StartPlanet(Control planet)
    {
        AddChild(planet);

        planet.SetAnchorsAndOffsetsPreset(
            Control.LayoutPreset.FullRect
        );

        MoveChild(
            planet,
            GetChildCount() - 1
        );
    }

    public void RegisterPlanetCompletion(
        int planet,
        int newScore
    )
    {
        if (planet < 0 || planet >= PlanetCount)
        {
            return;
        }

        planetAttempts[planet]++;

        bool wasAlreadyCompleted =
            planetCompleted[planet];

        int oldScore =
            planetScores[planet];

        planetCompleted[planet] = true;

        if (!wasAlreadyCompleted)
        {
            planetScores[planet] = newScore;
        }
        else if (newScore > oldScore)
        {
            planetScores[planet] = newScore;
        }

        UpdatePlanetVisuals();
    }

    public int GetPlanetScore(int planet)
    {
        if (planet < 0 || planet >= PlanetCount)
        {
            return 0;
        }

        return planetScores[planet];
    }

    public int GetPlanetAttempts(int planet)
    {
        if (planet < 0 || planet >= PlanetCount)
        {
            return 0;
        }

        return planetAttempts[planet];
    }

    public void SelectNextPlanet()
    {
        if (selectedPlanet >= PlanetCount - 1)
        {
            ReturnToLobby();
            return;
        }

        int nextPlanet =
            selectedPlanet + 1;

        if (!IsPlanetUnlocked(nextPlanet))
        {
            ReturnToLobby();
            return;
        }

        ReturnToLobby();

        selectedPlanet =
            nextPlanet;

        UpdatePlanetVisuals();

        StartSelectedPlanet();
    }

    public void ReturnToLobby()
    {
        for (
            int i = GetChildCount() - 1;
            i >= 0;
            i--
        )
        {
            Node child =
                GetChild(i);

            if (
                child is Terra ||
                child is Lua ||
                child is Marte ||
                child is Jupiter
            )
            {
                child.QueueFree();
            }
        }

        background.Color =
            new Color("#080D24");

        startPanel.Visible = true;

        UpdatePlanetVisuals();
    }

    private void ExitGame()
    {
        GetTree().Quit();
    }

    private Color GetPlanetColor()
    {
        switch (selectedPlanet)
        {
            case 0:
                return new Color("#102D4A");

            case 1:
                return new Color("#24243A");

            case 2:
                return new Color("#451C1C");

            case 3:
                return new Color("#321F4D");

            default:
                return new Color("#080D24");
        }
    }

    private void CreateStars()
    {
        string[] stars =
        {
            "✦", "·", "✧", "·",
            "✦", "·", "✧", "·",
            "✦", "·", "✧", "·",
            "✦", "·", "✧", "·",
            "✦", "·", "✧", "·"
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
            new Vector2(1000, 250),
            new Vector2(300, 350),
            new Vector2(850, 350),
            new Vector2(600, 130),
            new Vector2(1150, 150),
            new Vector2(100, 550)
        };

        for (
            int i = 0;
            i < stars.Length;
            i++
        )
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

    private void ApplyButtonTheme(
        Button button,
        Color normalColor,
        Color borderColor,
        int borderWidth = 3
    )
    {
        Theme theme = new Theme();

        StyleBoxFlat normal =
            CreateBox(
                normalColor,
                borderColor,
                borderWidth,
                25
            );

        StyleBoxFlat hover =
            CreateBox(
                normalColor.Lightened(0.15f),
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
                normalColor.Darkened(0.35f),
                borderColor.Darkened(0.25f),
                2,
                25
            );

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

        button.Theme = theme;
    }

    private void ApplyPanelTheme(
        Panel panel,
        Color backgroundColor,
        Color borderColor
    )
    {
        Theme theme = new Theme();

        StyleBoxFlat panelStyle =
            CreateBox(
                backgroundColor,
                borderColor,
                3,
                30
            );

        theme.SetStylebox(
            "panel",
            "Panel",
            panelStyle
        );

        panel.Theme = theme;
    }

    private StyleBoxFlat CreateBox(
        Color backgroundColor,
        Color borderColor,
        int borderWidth,
        int cornerRadius
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