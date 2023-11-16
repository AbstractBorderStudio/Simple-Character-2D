using Godot;
using System;

public class CharacterInput : InputHandler
{
    /// <summary>
    /// Return a value between [-1, 1] based on the input pressed
    /// </summary>
    /// <returns></returns>
    public override float RetriveMoveInput()
    {
        return Input.GetAxis("left", "right");
    }
    
    /// <summary>
    /// Return True if jump button was just pressed
    /// </summary>
    /// <returns></returns>
	public override bool RetriveJumpInput()
    {
        return Input.IsActionPressed("jump");
    }

    public bool RetriveDashInput()
    {
        return Input.IsActionJustPressed("dash");
    }
}
