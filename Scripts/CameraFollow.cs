using Godot;
using System;
using static Godot.GD;

public class CameraFollow : Camera2D
{
    private KinematicBody2D target;
    public override void _Ready()
    {
        target = GetNode<KinematicBody2D>("/root/MainScene/Player");
    }

    public override void _Process(float delta)
    {
        Position = target.Position;
    }
}
