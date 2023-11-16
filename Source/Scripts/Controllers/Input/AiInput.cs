using Godot;
using System;

public partial class AiInput : InputHandler
{
    public override float RetriveMoveInput()
    {
        return 1.0f;
    }

	public override bool RetriveJumpInput()
    {
        return true;
    }
}
