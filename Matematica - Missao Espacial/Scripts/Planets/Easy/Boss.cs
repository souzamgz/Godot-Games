using Godot;

public partial class Boss : EasyPlanet
{
    public override int PlanetIndex =>
        4;

    protected override int MeteorMaxHealth =>
        60;

    protected override int DamagePerCorrectAnswer =>
        10;

    protected override int MeteorsRequired =>
        1;

    protected override int PointsPerCorrectAnswer =>
        20;

    protected override void GenerateOperation()
    {
        int operation =
            random.Next(0, 4);

        switch (operation)
        {
            case 0:
                GenerateAddition();
                break;

            case 1:
                GenerateSubtraction();
                break;

            case 2:
                GenerateMultiplication();
                break;

            case 3:
                GenerateDivision();
                break;
        }
    }

    private void GenerateAddition()
    {
        int number1 =
            random.Next(1, 10);

        int number2 =
            random.Next(1, 10);

        correctAnswer =
            number1 + number2;

        ui.OperationLabel.Text =
            $"{number1} + {number2}";
    }

    private void GenerateSubtraction()
    {
        int number1 =
            random.Next(1, 10);

        int number2 =
            random.Next(1, number1 + 1);

        correctAnswer =
            number1 - number2;

        ui.OperationLabel.Text =
            $"{number1} − {number2}";
    }

    private void GenerateMultiplication()
    {
        int number1 =
            random.Next(1, 10);

        int number2 =
            random.Next(1, 10);

        correctAnswer =
            number1 * number2;

        ui.OperationLabel.Text =
            $"{number1} × {number2}";
    }

    private void GenerateDivision()
    {
        int number2 =
            random.Next(1, 10);

        int result =
            random.Next(1, 10);

        int number1 =
            number2 * result;

        correctAnswer =
            result;

        ui.OperationLabel.Text =
            $"{number1} ÷ {number2}";
    }
}

