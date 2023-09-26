using Godot;
using System;

public partial class Trail : Line2D
{
    [Export]
    private int maxLenght = 500,
        offset;
    private Curve2D queue;
    [Export]
    private CharacterBody2D target;

    public override void _Ready()
    {
        queue = new Curve2D();
    }

    public override void _Process(double delta)
    {
        Vector2 pos = target.GlobalPosition;

        queue.AddPoint(pos + new Vector2(0, offset));

        if (queue.PointCount > maxLenght)
        {
            queue.RemovePoint(0);
        }

        ClearPoints();

        foreach (Vector2 point in queue.GetBakedPoints())
        {
            AddPoint(point);
        }
    }	
}
