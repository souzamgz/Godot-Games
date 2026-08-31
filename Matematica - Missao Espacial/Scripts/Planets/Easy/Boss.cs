using Godot;

public partial class Boss : EasyPlanet
{
    public override int PlanetIndex =>
        4;

    protected override bool IsBoss =>
        true;

    protected override int MaxMeteorHealth =>
        GameUI.BossMeteorMaxHealth;

    protected override int RequiredMeteors =>
        1;

    protected override void GenerateOperation()
    {
        int operation =
            random.Next(0, 4);

        int number1;
        int number2;

        switch (operation)
        {
            case 0:
                number1 =
                    random.Next(1, 10);

                number2 =
                    random.Next(1, 10);

                correctAnswer =
                    number1 + number2;

                ui.OperationLabel.Text =
                    $"{number1} + {number2}";

                break;

            case 1:
                number1 =
                    random.Next(1, 10);

                number2 =
                    random.Next(
                        1,
                        number1 + 1
                    );

                correctAnswer =
                    number1 - number2;

                ui.OperationLabel.Text =
                    $"{number1} − {number2}";

                break;

            case 2:
                number1 =
                    random.Next(1, 10);

                number2 =
                    random.Next(1, 10);

                correctAnswer =
                    number1 * number2;

                ui.OperationLabel.Text =
                    $"{number1} × {number2}";

                break;

            default:
                number2 =
                    random.Next(1, 10);

                int result =
                    random.Next(1, 10);

                number1 =
                    number2 * result;

                correctAnswer =
                    result;

                ui.OperationLabel.Text =
                    $"{number1} ÷ {number2}";

                break;
        }
    }
}