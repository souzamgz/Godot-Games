using System;

public static class ProgressManager
{
    public const int PlanetCount = 4;

    private static readonly bool[] completed =
    {
        false,
        false,
        false,
        false
    };

    private static readonly int[] scores =
    {
        0,
        0,
        0,
        0
    };

    public static bool IsCompleted(int planetIndex)
    {
        if (planetIndex < 0 || planetIndex >= PlanetCount)
        {
            return false;
        }

        return completed[planetIndex];
    }

    public static int GetScore(int planetIndex)
    {
        if (planetIndex < 0 || planetIndex >= PlanetCount)
        {
            return 0;
        }

        return scores[planetIndex];
    }

    public static bool CanPlay(int planetIndex)
    {
        if (planetIndex < 0 || planetIndex >= PlanetCount)
        {
            return false;
        }

        // O primeiro planeta sempre pode ser jogado.
        if (planetIndex == 0)
        {
            return true;
        }

        // Os demais precisam do anterior concluído.
        return completed[planetIndex - 1];
    }

    public static void CompletePlanet(
        int planetIndex,
        int score
    )
    {
        if (planetIndex < 0 || planetIndex >= PlanetCount)
        {
            return;
        }

        completed[planetIndex] = true;
        scores[planetIndex] = score;
    }

    public static void UpdateScore(
        int planetIndex,
        int score
    )
    {
        if (planetIndex < 0 || planetIndex >= PlanetCount)
        {
            return;
        }

        scores[planetIndex] = score;
        completed[planetIndex] = true;
    }

    public static bool HasNextPlanet(int planetIndex)
    {
        return planetIndex < PlanetCount - 1;
    }

    public static int GetNextPlanet(int planetIndex)
    {
        if (!HasNextPlanet(planetIndex))
        {
            return -1;
        }

        return planetIndex + 1;
    }
}