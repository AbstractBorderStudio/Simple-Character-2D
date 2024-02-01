using Godot;
using System;

public partial class SceneManager : Node2D
{
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
			if (keyEvent.Keycode == Key.Escape)
				GetTree().Quit();

	}
}
