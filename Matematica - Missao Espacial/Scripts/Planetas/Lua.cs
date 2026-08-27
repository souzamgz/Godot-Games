using Godot;

public partial class Lua : EasyPlanet
{
    protected override void GenerateOperation()
    {
        int number1 =
            random.Next(1, 10);

        int number2 =
            random.Next(1, number1 + 1);

        correctAnswer =
            number1 - number2;

        ui.OperationLabel.Text =
            $"{number1} - {number2}";
    }
}