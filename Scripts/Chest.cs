using Godot;
using System;

public class Chest : StaticBody2D
{
    [Export]
    private int goldToGive = 5;

    public void OnInteract(Player player)
    {
        player.GiveGold(goldToGive);
        QueueFree();
    }
}
