using Godot;
using System;

public partial class Benchmark : Label
{
	public override void _Process(double delta)
	{
		Text = "FPS : " + Engine.GetFramesPerSecond();
	}
}
