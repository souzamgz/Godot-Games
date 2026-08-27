using Godot;

public partial class Terra : EasyPlanet
{
    public override int PlanetIndex => 0;

    protected override void GenerateOperation()
    {
        int number1 = random.Next(1, 10);
        int number2 = random.Next(1, 10);

        correctAnswer = number1 + number2;

        ui.OperationLabel.Text =
            $"{number1} + {number2}";
    }
}