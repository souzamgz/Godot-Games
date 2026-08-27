using Godot;
using System.Threading.Tasks;

public static class Transitions
{
    public static async Task AnimateAnswer(
        Button button,
        bool correct
    )
    {
        if (button == null)
        {
            return;
        }

        Color originalModulate =
            button.Modulate;

        Vector2 originalPosition =
            button.Position;

        Vector2 originalScale =
            button.Scale;

        Color targetColor =
            correct
                ? new Color("#62E67A")
                : new Color("#F05C64");

        button.Modulate =
            targetColor;

        if (correct)
        {
            button.Scale =
                originalScale *
                1.10f;

            await Task.Delay(
                120
            );

            button.Scale =
                originalScale *
                0.96f;

            await Task.Delay(
                80
            );

            button.Scale =
                originalScale;
        }
        else
        {
            float elapsed =
                0f;

            float duration =
                0.28f;

            while (
                elapsed < duration
            )
            {
                float delta =
                    (float)
                    button.GetProcessDeltaTime();

                elapsed += delta;

                float intensity =
                    1f -
                    elapsed / duration;

                float shake =
                    Mathf.Sin(
                        elapsed * 100f
                    ) *
                    10f *
                    intensity;

                button.Position =
                    originalPosition +
                    new Vector2(
                        shake,
                        0
                    );

                await Task.Yield();
            }

            button.Position =
                originalPosition;
        }

        await Task.Delay(
            150
        );

        button.Modulate =
            originalModulate;

        button.Scale =
            originalScale;

        button.Position =
            originalPosition;
    }

    public static async Task Attack(
        Control target,
        Button sourceButton,
        bool correct
    )
    {
        if (
            target == null ||
            sourceButton == null
        )
        {
            return;
        }

        await CreateProjectile(
            target,
            sourceButton,
            correct
        );

        if (correct)
        {
            await CorrectImpact(
                target
            );
        }
        else
        {
            await WrongImpact(
                target
            );
        }
    }

    private static async Task CreateProjectile(
        Control target,
        Button sourceButton,
        bool correct
    )
    {
        Node parent =
            target.GetParent();

        if (
            parent == null
        )
        {
            return;
        }

        Label projectile =
            new Label();

        projectile.Text =
            correct
                ? "✦"
                : "·";

        projectile.AddThemeFontSizeOverride(
            "font_size",
            correct
                ? 34
                : 26
        );

        parent.AddChild(
            projectile
        );

        Vector2 start =
            sourceButton.Position +
            sourceButton.Size / 2f;

        Vector2 end =
            target.Position +
            target.Size / 2f;

        projectile.Position =
            start;

        float duration =
            correct
                ? 0.25f
                : 0.35f;

        float elapsed =
            0f;

        while (
            elapsed < duration
        )
        {
            float delta =
                (float)
                target.GetProcessDeltaTime();

            elapsed += delta;

            float progress =
                Mathf.Clamp(
                    elapsed / duration,
                    0f,
                    1f
                );

            progress =
                1f -
                Mathf.Pow(
                    1f - progress,
                    3f
                );

            projectile.Position =
                start.Lerp(
                    end,
                    progress
                );

            await Task.Yield();
        }

        projectile.QueueFree();
    }

    private static async Task CorrectImpact(
        Control target
    )
    {
        Vector2 originalPosition =
            target.Position;

        Vector2 originalScale =
            target.Scale;

        Color originalColor =
            target.Modulate;

        target.Modulate =
            new Color("#FFF59D");

        target.Scale =
            originalScale *
            1.14f;

        await Task.Delay(
            90
        );

        target.Scale =
            originalScale *
            0.93f;

        target.Modulate =
            new Color("#FFFFFF");

        await Task.Delay(
            80
        );

        target.Scale =
            originalScale *
            1.06f;

        await Task.Delay(
            70
        );

        target.Scale =
            originalScale;

        target.Modulate =
            originalColor;

        await Shake(
            target,
            originalPosition,
            12f,
            0.20f
        );
    }

    private static async Task WrongImpact(
        Control target
    )
    {
        Vector2 originalPosition =
            target.Position;

        Color originalColor =
            target.Modulate;

        target.Modulate =
            new Color("#FF7777");

        await Shake(
            target,
            originalPosition,
            5f,
            0.16f
        );

        target.Modulate =
            originalColor;
    }

    private static async Task Shake(
        Control target,
        Vector2 originalPosition,
        float strength,
        float duration
    )
    {
        float elapsed =
            0f;

        while (
            elapsed < duration
        )
        {
            float delta =
                (float)
                target.GetProcessDeltaTime();

            elapsed += delta;

            float progress =
                elapsed /
                duration;

            float intensity =
                1f -
                progress;

            float x =
                Mathf.Sin(
                    elapsed * 90f
                ) *
                strength *
                intensity;

            target.Position =
                originalPosition +
                new Vector2(
                    x,
                    0
                );

            await Task.Yield();
        }

        target.Position =
            originalPosition;
    }

    public static async Task MeteorDestroyed(
        Control target
    )
    {
        if (
            target == null
        )
        {
            return;
        }

        Vector2 originalPosition =
            target.Position;

        Vector2 originalScale =
            target.Scale;

        Color originalColor =
            target.Modulate;

        float elapsed =
            0f;

        float duration =
            0.50f;

        while (
            elapsed < duration
        )
        {
            float delta =
                (float)
                target.GetProcessDeltaTime();

            elapsed += delta;

            float progress =
                elapsed /
                duration;

            float shake =
                Mathf.Sin(
                    elapsed * 110f
                ) *
                (
                    5f +
                    progress * 20f
                );

            target.Position =
                originalPosition +
                new Vector2(
                    shake,
                    0
                );

            target.Scale =
                originalScale *
                (
                    1f +
                    progress * 0.35f
                );

            if (
                ((int)
                (elapsed * 35))
                % 2 == 0
            )
            {
                target.Modulate =
                    new Color("#FFFFFF");
            }
            else
            {
                target.Modulate =
                    new Color("#FF7043");
            }

            await Task.Yield();
        }

        await CreateExplosion(
            target
        );

        target.Position =
            originalPosition;

        target.Scale =
            originalScale;

        target.Modulate =
            originalColor;

        target.Visible =
            false;

        await Task.Delay(
            300
        );

        target.Visible =
            true;

        target.Modulate =
            new Color(
                1,
                1,
                1,
                0
            );

        target.Scale =
            originalScale *
            0.65f;

        for (
            int i = 0;
            i <= 12;
            i++
        )
        {
            float progress =
                i / 12f;

            target.Modulate =
                new Color(
                    1,
                    1,
                    1,
                    progress
                );

            target.Scale =
                originalScale *
                Mathf.Lerp(
                    0.65f,
                    1f,
                    progress
                );

            await Task.Delay(
                30
            );
        }

        target.Modulate =
            originalColor;

        target.Scale =
            originalScale;
    }

    private static async Task CreateExplosion(
        Control target
    )
    {
        Node parent =
            target.GetParent();

        if (
            parent == null
        )
        {
            return;
        }

        string[] fragments =
        {
            "✦",
            "✧",
            "•",
            "✦",
            "·",
            "✧",
            "•",
            "✦"
        };

        Label[] labels =
            new Label[
                fragments.Length
            ];

        Vector2 center =
            target.Position +
            target.Size / 2f;

        for (
            int i = 0;
            i < fragments.Length;
            i++
        )
        {
            Label fragment =
                new Label();

            fragment.Text =
                fragments[i];

            fragment.AddThemeFontSizeOverride(
                "font_size",
                20 +
                (i % 3) * 8
            );

            fragment.Position =
                center;

            parent.AddChild(
                fragment
            );

            labels[i] =
                fragment;
        }

        float elapsed =
            0f;

        float duration =
            0.65f;

        while (
            elapsed < duration
        )
        {
            float delta =
                (float)
                target.GetProcessDeltaTime();

            elapsed += delta;

            float progress =
                elapsed /
                duration;

            for (
                int i = 0;
                i < labels.Length;
                i++
            )
            {
                float angle =
                    (
                        Mathf.Tau /
                        labels.Length
                    ) * i;

                float distance =
                    25f +
                    progress * 145f;

                Vector2 direction =
                    new Vector2(
                        Mathf.Cos(angle),
                        Mathf.Sin(angle)
                    );

                labels[i].Position =
                    center +
                    direction *
                    distance;

                labels[i].Modulate =
                    new Color(
                        1,
                        1,
                        1,
                        1f - progress
                    );

                labels[i].Scale =
                    Vector2.One *
                    (
                        1f +
                        progress * 0.6f
                    );
            }

            await Task.Yield();
        }

        for (
            int i = 0;
            i < labels.Length;
            i++
        )
        {
            labels[i].QueueFree();
        }
    }
}