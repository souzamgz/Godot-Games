using Godot;

public partial class Jupiter : EasyPlanet
{
    public override int PlanetIndex => 3;

    protected override void GenerateOperation()
    {
        int number2 = random.Next(1, 10);
        int result = random.Next(1, 10);

        int number1 = number2 * result;

        correctAnswer = result;

        ui.OperationLabel.Text =
            $"{number1} ÷ {number2}";
    }
}