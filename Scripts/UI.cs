using Godot;
using System;

public class UI : Control
{
    private Label levelText;
    private Label goldText;
    private TextureProgress healthBar;
    private TextureProgress xpBar;
    public override void _Ready()
    {
        levelText = GetNode<Label>("BG/LevelBG/LevelText");
        goldText = GetNode<Label>("BG/GoldText");
        healthBar = GetNode<TextureProgress>("BG/HealthBar");
        xpBar = GetNode<TextureProgress>("BG/XPBar");
    }

    public void UpdateLevelText(int level)
    {
        levelText.Text = level.ToString();
    }

    public void UpdateGoldText(int curGold)
    {
        goldText.Text = "Gold: " + curGold.ToString();
    }

    public void UpdateHealthBar(int curHP, int maxHP)
    {
        healthBar.Value = (100 / maxHP) * curHP;
    }

    public void UpdateXPBar(float curXP, float xpToNextLvl)
    {
        xpBar.Value = (100 / xpToNextLvl) * curXP;
    }
}
