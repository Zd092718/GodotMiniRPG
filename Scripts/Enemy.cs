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
	private Player target;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		target = GetNode<Player>("/root/MainScene/Player");
		timer.WaitTime = attackRate;
		timer.Start();
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

	private void _on_Timer_timeout()
	{
		if (Position.DistanceTo(target.Position) <= attackDist)
		{
			target.TakeDamage(damage);
		}
	}

	public void TakeDamage(int dmgToTake)
	{
		curHP -= dmgToTake;

		if (curHP <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		target.GiveXP(xpReward);
		QueueFree();
	}

}
