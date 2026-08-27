using Godot;
using System;
using System.Collections.Generic;

public static class GameProgress
{
    private const string SavePath =
        "user://matematica_missao_espacial.json";

    [Serializable]
    public class PlanetData
    {
        public bool Completed { get; set; }
        public int Score { get; set; }
        public int Attempts { get; set; }
    }

    private static readonly Dictionary<int, PlanetData> planets =
        new Dictionary<int, PlanetData>();

    static GameProgress()
    {
        Load();
    }

    public static bool IsCompleted(int planet)
    {
        return GetPlanet(planet).Completed;
    }

    public static int GetScore(int planet)
    {
        return GetPlanet(planet).Score;
    }

    public static int GetAttempts(int planet)
    {
        return GetPlanet(planet).Attempts;
    }

    public static void CompletePlanet(
        int planet,
        int score
    )
    {
        PlanetData data = GetPlanet(planet);

        bool firstCompletion =
            !data.Completed;

        data.Completed = true;

        data.Attempts++;

        if (firstCompletion)
        {
            data.Score = score;
        }
        else if (score > data.Score)
        {
            data.Score = score;
        }

        Save();
    }

    public static bool TryReplaceScore(
        int planet,
        int newScore
    )
    {
        PlanetData data = GetPlanet(planet);

        if (!data.Completed)
        {
            return false;
        }

        if (newScore <= data.Score)
        {
            return false;
        }

        data.Score = newScore;

        Save();

        return true;
    }

    public static bool IsPlanetUnlocked(int planet)
    {
        if (planet <= 0)
        {
            return true;
        }

        return IsCompleted(planet - 1);
    }

    private static PlanetData GetPlanet(int planet)
    {
        if (!planets.ContainsKey(planet))
        {
            planets[planet] =
                new PlanetData
                {
                    Completed = false,
                    Score = 0,
                    Attempts = 0
                };
        }

        return planets[planet];
    }

    private static void Save()
    {
        Godot.Collections.Dictionary saveData =
            new Godot.Collections.Dictionary();

        for (int i = 0; i < 4; i++)
        {
            PlanetData data =
                GetPlanet(i);

            Godot.Collections.Dictionary planetData =
                new Godot.Collections.Dictionary();

            planetData["completed"] =
                data.Completed;

            planetData["score"] =
                data.Score;

            planetData["attempts"] =
                data.Attempts;

            saveData[i.ToString()] =
                planetData;
        }

        string json =
            Json.Stringify(saveData);

        using FileAccess file =
            FileAccess.Open(
                SavePath,
                FileAccess.ModeFlags.Write
            );

        if (file != null)
        {
            file.StoreString(json);
        }
    }

    private static void Load()
    {
        planets.Clear();

        if (!FileAccess.FileExists(SavePath))
        {
            return;
        }

        using FileAccess file =
            FileAccess.Open(
                SavePath,
                FileAccess.ModeFlags.Read
            );

        if (file == null)
        {
            return;
        }

        string json =
            file.GetAsText();

        Variant parsed =
            Json.ParseString(json);

        if (parsed.VariantType != Variant.Type.Dictionary)
        {
            return;
        }

        Godot.Collections.Dictionary saveData =
            (Godot.Collections.Dictionary)parsed;

        for (int i = 0; i < 4; i++)
        {
            string key =
                i.ToString();

            if (!saveData.ContainsKey(key))
            {
                continue;
            }

            if (
                saveData[key].VariantType !=
                Variant.Type.Dictionary
            )
            {
                continue;
            }

            Godot.Collections.Dictionary planetData =
                (Godot.Collections.Dictionary)saveData[key];

            PlanetData data =
                new PlanetData();

            if (planetData.ContainsKey("completed"))
            {
                data.Completed =
                    Convert.ToBoolean(
                        planetData["completed"]
                    );
            }

            if (planetData.ContainsKey("score"))
            {
                data.Score =
                    Convert.ToInt32(
                        planetData["score"]
                    );
            }

            if (planetData.ContainsKey("attempts"))
            {
                data.Attempts =
                    Convert.ToInt32(
                        planetData["attempts"]
                    );
            }

            planets[i] =
                data;
        }
    }
}