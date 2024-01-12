using Godot;
using System;
using static Godot.GD;

public class Player : KinematicBody2D
{
    private int curHP = 10;
    private int maxHP = 10;
    private int moveSpeed = 250;
    private int damage = 1;

    private int gold = 0;

    private int curLvl = 0;
    private float curXP = 0;
    private float xpToNextLvl = 50;
    private float xpToLvlIncreaseRate = 1.2f;

    private int interactDist = 70;

    private Vector2 vel = Vector2.Zero;
    private Vector2 facingDir = Vector2.Zero;
    private RayCast2D rayCast;
    private AnimatedSprite anim;

    public override void _Ready()
    {
        rayCast = GetNode<RayCast2D>("RayCast2D");
        anim = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        vel = Vector2.Zero;

        //Inputs
        if (Input.IsActionPressed("move_up"))
        {
            vel.y -= 1;
            facingDir = new Vector2(0, -1);
        }
        if (Input.IsActionPressed("move_down"))
        {
            vel.y += 1;
            facingDir = new Vector2(0, 1);
        }
        if (Input.IsActionPressed("move_left"))
        {
            vel.x -= 1;
            facingDir = new Vector2(-1, 0);
        }
        if (Input.IsActionPressed("move_right"))
        {
            vel.x += 1;
            facingDir = new Vector2(1, 0);
        }

        // normalize the velocity to prevent faster movement
        vel = vel.Normalized();

        // moves the player
        MoveAndSlide(vel * moveSpeed);

        ManageAnimations();
    }

    private void ManageAnimations()
    {
        if (vel.x > 0)
        {
            PlayAnimation("WalkRight");
        }
        else if (vel.x < 0)
        {
            PlayAnimation("WalkLeft");
        }
        else if (vel.y < 0)
        {
            PlayAnimation("WalkUp");
        }
        else if (vel.y > 0)
        {
            PlayAnimation("WalkDown");
        }
        else if (facingDir.x == 1)
        {
            PlayAnimation("IdleRight");
        }
        else if (facingDir.x == -1)
        {
            PlayAnimation("IdleLeft");
        }
        else if (facingDir.y == -1)
        {
            PlayAnimation("IdleUp");
        }
        else if (facingDir.y == 1)
        {
            PlayAnimation("IdleDown");
        }
    }

    private void PlayAnimation(string animName)
    {
        if (anim.Animation != animName)
        {
            anim.Play(animName);
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

    public void GiveXP(int xpAmount)
    {
        curXP += xpAmount;

        if (curXP >= xpToNextLvl)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        float overflowXP = curXP - xpToNextLvl;
        xpToNextLvl *= xpToLvlIncreaseRate;
        curXP = overflowXP;
        curLvl++;
    }

    private void Die()
    {
        GetTree().ReloadCurrentScene();
    }
}
