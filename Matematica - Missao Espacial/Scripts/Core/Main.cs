using Godot;
using System;

public partial class Main : Control
{
    private const int PlanetCount = 5;

    private readonly string[] planetNames =
    {
        "🌍 TERRA",
        "🌙 LUA",
        "🔴 MARTE",
        "🟣 JÚPITER",
        "🌌 BOSS"
    };

    private readonly string[] planetButtonsText =
    {
        "🌍\nTERRA\n+",
        "🌙\nLUA\n−",
        "🔴\nMARTE\n×",
        "🟣\nJÚPITER\n÷",
        "🌌\nBOSS\n★"
    };

    private static readonly bool[] planetCompleted =
    {
        false,
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
        0,
        0
    };

    private static readonly int[] planetAttempts =
    {
        0,
        0,
        0,
        0,
        0
    };

    private ColorRect background;

    private Panel introPanel;
    private Panel startPanel;

    private Label introTitleLabel;
    private Label introSubtitleLabel;

    private Label titleLabel;
    private Label subtitleLabel;
    private Label difficultyLabel;
    private Label selectedPlanetLabel;

    private Button introStartButton;
    private Button introExitButton;

    private Button startButton;
    private Button exitButton;

    private Button planetButton1;
    private Button planetButton2;
    private Button planetButton3;
    private Button planetButton4;
    private Button planetButton5;

    private Button previousDifficultyButton;
    private Button nextDifficultyButton;

    private int selectedPlanet = 0;

    public override void _Ready()
    {
        CreateBackground();
        CreateStars();
        CreateIntroScreen();
    }

    private void CreateBackground()
    {
        background = new ColorRect();

        background.SetAnchorsAndOffsetsPreset(
            Control.LayoutPreset.FullRect
        );

        background.Color =
            Style.BackgroundColor;

        AddChild(background);
        MoveChild(background, 0);
    }

    private void CreateIntroScreen()
    {
        introPanel = new Panel();

        introPanel.Position =
            new Vector2(260, 120);

        introPanel.Size =
            new Vector2(680, 440);

        Style.ApplyIntroPanelStyle(
            introPanel
        );

        AddChild(introPanel);

        introTitleLabel = new Label();

        introTitleLabel.Text =
            "🚀 MISSÃO ESPACIAL";

        Style.CenterLabel(
            introTitleLabel
        );

        introTitleLabel.Position =
            new Vector2(40, 55);

        introTitleLabel.Size =
            new Vector2(600, 90);

        Style.SetFontSize(
            introTitleLabel,
            44
        );

        introPanel.AddChild(
            introTitleLabel
        );

        introSubtitleLabel = new Label();

        introSubtitleLabel.Text =
            "Prepare-se para uma aventura\n" +
            "pelo Sistema Solar!";

        Style.CenterLabel(
            introSubtitleLabel
        );

        introSubtitleLabel.Position =
            new Vector2(60, 155);

        introSubtitleLabel.Size =
            new Vector2(560, 100);

        Style.SetFontSize(
            introSubtitleLabel,
            25
        );

        introPanel.AddChild(
            introSubtitleLabel
        );

        introStartButton = new Button();

        introStartButton.Text =
            "COMEÇAR MISSÃO";

        introStartButton.Position =
            new Vector2(130, 300);

        introStartButton.Size =
            new Vector2(260, 70);

        Style.SetFontSize(
            introStartButton,
            22
        );

        Style.ApplyIntroStartButtonStyle(
            introStartButton
        );

        introPanel.AddChild(
            introStartButton
        );

        introStartButton.Pressed += OpenLobby;

        introExitButton = new Button();

        introExitButton.Text =
            "SAIR";

        introExitButton.Position =
            new Vector2(420, 300);

        introExitButton.Size =
            new Vector2(130, 70);

        Style.SetFontSize(
            introExitButton,
            20
        );

        Style.ApplyExitButtonStyle(
            introExitButton
        );

        introPanel.AddChild(
            introExitButton
        );

        introExitButton.Pressed += ExitGame;
    }

    private void OpenLobby()
    {
        introPanel.Visible = false;

        CreateLobby();

        selectedPlanet = 0;

        UpdatePlanetVisuals();
    }

    private void CreateLobby()
    {
        if (startPanel != null)
        {
            startPanel.QueueFree();
        }

        startPanel = new Panel();

        startPanel.Position =
            new Vector2(100, 55);

        startPanel.Size =
            new Vector2(1040, 620);

        Style.ApplyPanelStyle(
            startPanel,
            Style.PanelColor,
            Style.PanelBorderColor
        );

        AddChild(startPanel);

        CreateTitle();
        CreateSubtitle();
        CreateDifficulty();
        CreatePlanetButtons();
        CreateDifficultyButtons();
        CreateSelectedPlanetLabel();
        CreateStartButton();
        CreateExitButton();
    }

    private void CreateTitle()
    {
        titleLabel = new Label();

        titleLabel.Text =
            "MISSÃO ESPACIAL";

        Style.CenterLabel(
            titleLabel
        );

        titleLabel.Position =
            new Vector2(40, 25);

        titleLabel.Size =
            new Vector2(960, 70);

        Style.SetFontSize(
            titleLabel,
            40
        );

        startPanel.AddChild(
            titleLabel
        );
    }

    private void CreateSubtitle()
    {
        subtitleLabel = new Label();

        subtitleLabel.Text =
            "Escolha um planeta para iniciar sua missão";

        Style.CenterLabel(
            subtitleLabel
        );

        subtitleLabel.Position =
            new Vector2(40, 90);

        subtitleLabel.Size =
            new Vector2(960, 40);

        Style.SetFontSize(
            subtitleLabel,
            20
        );

        startPanel.AddChild(
            subtitleLabel
        );
    }

    private void CreateDifficulty()
    {
        difficultyLabel = new Label();

        difficultyLabel.Text =
            "MODO FÁCIL";

        Style.CenterLabel(
            difficultyLabel
        );

        difficultyLabel.Position =
            new Vector2(40, 130);

        difficultyLabel.Size =
            new Vector2(960, 40);

        Style.SetFontSize(
            difficultyLabel,
            25
        );

        startPanel.AddChild(
            difficultyLabel
        );
    }

    private void CreatePlanetButtons()
    {
        planetButton1 =
            CreatePlanetButton(
                planetButtonsText[0]
            );

        planetButton2 =
            CreatePlanetButton(
                planetButtonsText[1]
            );

        planetButton3 =
            CreatePlanetButton(
                planetButtonsText[2]
            );

        planetButton4 =
            CreatePlanetButton(
                planetButtonsText[3]
            );

        planetButton5 =
            CreateBossButton(
                planetButtonsText[4]
            );

        planetButton1.Position =
            new Vector2(35, 185);

        planetButton2.Position =
            new Vector2(235, 185);

        planetButton3.Position =
            new Vector2(435, 185);

        planetButton4.Position =
            new Vector2(635, 185);

        planetButton5.Position =
            new Vector2(835, 185);

        startPanel.AddChild(
            planetButton1
        );

        startPanel.AddChild(
            planetButton2
        );

        startPanel.AddChild(
            planetButton3
        );

        startPanel.AddChild(
            planetButton4
        );

        startPanel.AddChild(
            planetButton5
        );

        planetButton1.Pressed +=
            () => SelectPlanet(0);

        planetButton2.Pressed +=
            () => SelectPlanet(1);

        planetButton3.Pressed +=
            () => SelectPlanet(2);

        planetButton4.Pressed +=
            () => SelectPlanet(3);

        planetButton5.Pressed +=
            () => SelectPlanet(4);
    }

    private Button CreatePlanetButton(
        string text
    )
    {
        Button button =
            new Button();

        button.Text =
            text;

        button.Size =
            new Vector2(175, 205);

        Style.SetFontSize(
            button,
            18
        );

        Style.ApplyPlanetNormalStyle(
            button
        );

        return button;
    }

    private Button CreateBossButton(
        string text
    )
    {
        Button button =
            new Button();

        button.Text =
            text;

        button.Size =
            new Vector2(175, 205);

        Style.SetFontSize(
            button,
            18
        );

        Style.ApplyBossNormalStyle(
            button
        );

        return button;
    }

    private void CreateDifficultyButtons()
    {
        previousDifficultyButton =
            CreateDifficultyButton(
                "◀"
            );

        nextDifficultyButton =
            CreateDifficultyButton(
                "▶"
            );

        previousDifficultyButton.Position =
            new Vector2(10, 255);

        nextDifficultyButton.Position =
            new Vector2(975, 255);

        startPanel.AddChild(
            previousDifficultyButton
        );

        startPanel.AddChild(
            nextDifficultyButton
        );

        previousDifficultyButton.Disabled =
            true;

        nextDifficultyButton.Disabled =
            true;
    }

    private Button CreateDifficultyButton(
        string text
    )
    {
        Button button =
            new Button();

        button.Text =
            text;

        button.Size =
            new Vector2(50, 75);

        Style.SetFontSize(
            button,
            25
        );

        Style.ApplyPlanetNormalStyle(
            button
        );

        return button;
    }

    private void CreateSelectedPlanetLabel()
    {
        selectedPlanetLabel =
            new Label();

        Style.CenterLabel(
            selectedPlanetLabel
        );

        selectedPlanetLabel.Position =
            new Vector2(40, 415);

        selectedPlanetLabel.Size =
            new Vector2(960, 55);

        Style.SetFontSize(
            selectedPlanetLabel,
            20
        );

        startPanel.AddChild(
            selectedPlanetLabel
        );
    }

    private void CreateStartButton()
    {
        startButton =
            new Button();

        startButton.Position =
            new Vector2(330, 500);

        startButton.Size =
            new Vector2(300, 65);

        Style.SetFontSize(
            startButton,
            22
        );

        Style.ApplyStartButtonStyle(
            startButton
        );

        startPanel.AddChild(
            startButton
        );

        startButton.Pressed +=
            StartSelectedPlanet;
    }

    private void CreateExitButton()
    {
        exitButton =
            new Button();

        exitButton.Text =
            "SAIR";

        exitButton.Position =
            new Vector2(665, 505);

        exitButton.Size =
            new Vector2(140, 55);

        Style.SetFontSize(
            exitButton,
            18
        );

        Style.ApplyExitButtonStyle(
            exitButton
        );

        startPanel.AddChild(
            exitButton
        );

        exitButton.Pressed +=
            ExitGame;
    }

    private void SelectPlanet(
        int planet
    )
    {
        if (
            planet < 0 ||
            planet >= PlanetCount
        )
        {
            return;
        }

        selectedPlanet =
            planet;

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

        UpdateBossButton();

        string status;

        if (
            planetCompleted[selectedPlanet]
        )
        {
            status =
                $"{planetNames[selectedPlanet]}  •  CONCLUÍDO\n" +
                $"⭐ Pontuação: {planetScores[selectedPlanet]}   " +
                $"🔄 Tentativas: {planetAttempts[selectedPlanet]}";
        }
        else if (
            IsPlanetUnlocked(selectedPlanet)
        )
        {
            status =
                $"{planetNames[selectedPlanet]}  •  DISPONÍVEL";
        }
        else
        {
            status =
                $"{planetNames[selectedPlanet]}  •  🔒 BLOQUEADO";
        }

        selectedPlanetLabel.Text =
            status;

        UpdateStartButton();
        UpdatePlanetAvailability();
    }

    private void UpdatePlanetButton(
        Button button,
        int planetIndex,
        string originalText
    )
    {
        button.Text =
            originalText;

        if (
            !IsPlanetUnlocked(
                planetIndex
            )
        )
        {
            button.Text =
                $"{originalText}\n🔒 BLOQUEADO";

            button.Disabled =
                true;

            Style.ApplyPlanetLockedStyle(
                button
            );

            return;
        }

        button.Disabled =
            false;

        if (
            selectedPlanet ==
            planetIndex
        )
        {
            Style.ApplyPlanetSelectedStyle(
                button
            );
        }
        else if (
            planetCompleted[planetIndex]
        )
        {
            button.Text =
                $"{originalText}\n✓ CONCLUÍDO";

            Style.ApplyPlanetCompletedStyle(
                button
            );
        }
        else
        {
            Style.ApplyPlanetNormalStyle(
                button
            );
        }
    }

    private void UpdateBossButton()
    {
        planetButton5.Text =
            planetButtonsText[4];

        if (!IsPlanetUnlocked(4))
        {
            planetButton5.Text =
                $"{planetButtonsText[4]}\n🔒 DERROTE JÚPITER";

            planetButton5.Disabled =
                true;

            Style.ApplyBossLockedStyle(
                planetButton5
            );

            return;
        }

        planetButton5.Disabled =
            false;

        if (
            selectedPlanet == 4
        )
        {
            planetButton5.Text =
                $"{planetButtonsText[4]}\n⚠ DESAFIO FINAL";

            Style.ApplyBossSelectedStyle(
                planetButton5
            );

            return;
        }

        if (
            planetCompleted[4]
        )
        {
            planetButton5.Text =
                $"{planetButtonsText[4]}\n✓ DERROTADO";

            Style.ApplyBossCompletedStyle(
                planetButton5
            );

            return;
        }

        planetButton5.Text =
            $"{planetButtonsText[4]}\n⚠ DESAFIO FINAL";

        Style.ApplyBossNormalStyle(
            planetButton5
        );
    }

    private void UpdatePlanetAvailability()
    {
        if (
            planetButton1 == null
        )
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

        planetButton5.Disabled =
            !IsPlanetUnlocked(4);
    }

    private void UpdateStartButton()
    {
        if (
            planetCompleted[
                selectedPlanet
            ]
        )
        {
            startButton.Text =
                "REFAZER MISSÃO";

            if (selectedPlanet == 4)
            {
                Style.ApplyBossStartStyle(
                    startButton
                );
            }
            else
            {
                Style.ApplyStartButtonCompletedStyle(
                    startButton
                );
            }

            startButton.Disabled =
                false;

            return;
        }

        if (
            IsPlanetUnlocked(
                selectedPlanet
            )
        )
        {
            startButton.Text =
                selectedPlanet == 4
                    ? "ENFRENTAR BOSS"
                    : "INICIAR MISSÃO";

            if (selectedPlanet == 4)
            {
                Style.ApplyBossStartStyle(
                    startButton
                );
            }
            else
            {
                Style.ApplyStartButtonStyle(
                    startButton
                );
            }

            startButton.Disabled =
                false;

            return;
        }

        startButton.Text =
            selectedPlanet == 4
                ? "🔒 BOSS BLOQUEADO"
                : "🔒 PLANETA BLOQUEADO";

        Style.ApplyDisabledActionStyle(
            startButton
        );

        startButton.Disabled =
            true;
    }

    public bool IsPlanetUnlocked(
        int planet
    )
    {
        if (
            planet < 0 ||
            planet >= PlanetCount
        )
        {
            return false;
        }

        if (planet == 0)
        {
            return true;
        }

        return planetCompleted[
            planet - 1
        ];
    }

    public bool IsPlanetCompleted(
        int planet
    )
    {
        if (
            planet < 0 ||
            planet >= PlanetCount
        )
        {
            return false;
        }

        return planetCompleted[
            planet
        ];
    }

    private void StartSelectedPlanet()
    {
        if (
            !IsPlanetUnlocked(
                selectedPlanet
            )
        )
        {
            return;
        }

        startPanel.Visible =
            false;

        background.Color =
            GetPlanetColor();

        switch (selectedPlanet)
        {
            case 0:
                StartPlanet(
                    new Terra()
                );
                break;

            case 1:
                StartPlanet(
                    new Lua()
                );
                break;

            case 2:
                StartPlanet(
                    new Marte()
                );
                break;

            case 3:
                StartPlanet(
                    new Jupiter()
                );
                break;

            case 4:
                StartPlanet(
                    new Boss()
                );
                break;
        }
    }

    private void StartPlanet(
        Control planet
    )
    {
        AddChild(
            planet
        );

        planet.SetAnchorsAndOffsetsPreset(
            Control.LayoutPreset.FullRect
        );

        MoveChild(
            planet,
            GetChildCount() - 1
        );
    }

    public void OpenPlanet(
        int planet
    )
    {
        if (
            planet < 0 ||
            planet >= PlanetCount
        )
        {
            return;
        }

        if (
            !IsPlanetUnlocked(
                planet
            )
        )
        {
            return;
        }

        ReturnToLobby();

        selectedPlanet =
            planet;

        UpdatePlanetVisuals();

        StartSelectedPlanet();
    }

    public void RegisterPlanetCompletion(
        int planet,
        int newScore
    )
    {
        if (
            planet < 0 ||
            planet >= PlanetCount
        )
        {
            return;
        }

        planetAttempts[planet]++;

        bool wasAlreadyCompleted =
            planetCompleted[planet];

        int oldScore =
            planetScores[planet];

        planetCompleted[planet] =
            true;

        if (!wasAlreadyCompleted)
        {
            planetScores[planet] =
                newScore;
        }
        else if (
            newScore > oldScore
        )
        {
            planetScores[planet] =
                newScore;
        }
    }

    public int GetPlanetScore(
        int planet
    )
    {
        if (
            planet < 0 ||
            planet >= PlanetCount
        )
        {
            return 0;
        }

        return planetScores[planet];
    }

    public int GetPlanetAttempts(
        int planet
    )
    {
        if (
            planet < 0 ||
            planet >= PlanetCount
        )
        {
            return 0;
        }

        return planetAttempts[planet];
    }

    public void SelectNextPlanet()
    {
        if (
            selectedPlanet >=
            PlanetCount - 1
        )
        {
            ReturnToLobby();
            return;
        }

        int nextPlanet =
            selectedPlanet + 1;

        if (
            !IsPlanetUnlocked(
                nextPlanet
            )
        )
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
                child is Jupiter ||
                child is Boss
            )
            {
                child.QueueFree();
            }
        }

        background.Color =
            Style.BackgroundColor;

        startPanel.Visible =
            true;

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
                return Style.TerraBackgroundColor;

            case 1:
                return Style.LuaBackgroundColor;

            case 2:
                return Style.MarteBackgroundColor;

            case 3:
                return Style.JupiterBackgroundColor;

            case 4:
                return Style.BossBackgroundColor;

            default:
                return Style.BackgroundColor;
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
            Label star =
                new Label();

            star.Text =
                stars[i];

            star.Position =
                positions[i];

            Style.SetFontSize(
                star,
                i % 3 == 0
                    ? 22
                    : 14
            );

            AddChild(star);
        }
    }
}

