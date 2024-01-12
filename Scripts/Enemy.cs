using Godot;
using System;

public class Enemy : KinematicBody2D
{
    private int curHP = 5;
    private int maxHP = 5;
    private int moveSpeed = 150;

    [Export]
    private int xpReward = 30;

    private int damage = 1;
    private float attackRate = 1.0f;
    private int attackDist = 80;
    private int chaseDist = 400;

    private Timer timer;
    private KinematicBody2D target;

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        target = GetNode<KinematicBody2D>("/root/MainScene/Player");
    }

    public override void _PhysicsProcess(float delta)
    {
        float dist = Position.DistanceTo(target.Position);

        if (dist > attackDist && dist < chaseDist)
        {
            Vector2 vel = (target.Position - Position).Normalized();

            MoveAndSlide(vel * moveSpeed);
        }
    }
}
