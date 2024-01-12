using Godot;
using System;
using static Godot.GD;

public class CameraFollow : Camera2D
{
    private Player target;
    public override void _Ready()
    {
        target = GetNode<Player>("/root/MainScene/Player");
    }

    public override void _Process(float delta)
    {
        Position = target.Position;
    }
}
