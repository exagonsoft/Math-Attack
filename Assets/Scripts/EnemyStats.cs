internal class EnemyStats
{
    public enum enemyType
    {
        DUMB,
        FIZZ,
        BUZZ,
        FIZZBUZZ
    }

    enemyType type { get; set; }
    static float muveSpedd;
    static float hitDamage;
    static float maxHealth;
}