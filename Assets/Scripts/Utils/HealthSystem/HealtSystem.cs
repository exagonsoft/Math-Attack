using System;

public class HealtSystem
{
    #region Properties
    public event EventHandler onHealthChanged;
    private int health;
    private int maxHealth;
    
    public HealtSystem(int totalHealth)
    {
        maxHealth = totalHealth;
        this.health = maxHealth;
    }

    #endregion

    #region Events

    #endregion

    #region Functions

    public int GetCurrentMaxHealth()
    {
        return maxHealth;
    }
    
    public float GetHealthPercent()
    {
        return (float)health / maxHealth;
    }

    public int GethHealt()
    {
        return health;
    }

    public void Damage(int damegeAmount)
    {
        health -= damegeAmount;
        if (health < 0) health = 0;
        if(onHealthChanged != null)onHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
        if(onHealthChanged != null)onHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RenewHealthSystem(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        this.health = maxHealth;
        if (onHealthChanged != null) onHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    #endregion
}
