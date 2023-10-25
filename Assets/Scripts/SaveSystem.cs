using System.IO;
using UnityEngine;

public static class SaveSystem
{
  public static void SavePlayer(Player player)
  {
    PlayerData data = new PlayerData(player);

    string json = JsonUtility.ToJson(data);
    string path = Path.Combine(Application.persistentDataPath, "player.json");

    File.WriteAllText(path, json);
  }

  public static PlayerData LoadPlayer()
  {
    string path = Path.Combine(Application.persistentDataPath, "player.json");

    if (File.Exists(path))
    {
      string json = File.ReadAllText(path);
      return JsonUtility.FromJson<PlayerData>(json);
    }
    else
    {
      Debug.LogWarning("Player data file not found.");
      return null;
    }

  }
}