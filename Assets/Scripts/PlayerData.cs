[System.Serializable]
public class PlayerData
{
    public int level;
    public float exp;
    public float health;
    public float maxHealth;
    public float[] position;

    public PlayerData(Player player)
    {
        level = player.Level;
        exp = player.Exp;
        health = player.Health;
        maxHealth = player.MaxHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
