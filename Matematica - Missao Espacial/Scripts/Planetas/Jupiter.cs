using Godot;

public partial class Jupiter : EasyPlanet
{
    protected override void GenerateOperation()
    {
        int divisor =
            random.Next(1, 10);

        int result =
            random.Next(1, 10);

        int dividend =
            divisor * result;

        correctAnswer =
            result;

        ui.OperationLabel.Text =
            $"{dividend} ÷ {divisor}";
    }
}