[System.Serializable]
public class DestructibleData
{
  public int currentHealth;
  // When this is attached to a player or citizen we need a way to modify resistances based on equipment, buffs, etc
  public SharedDestructibleData sharedDestructibleData;
  public DestructibleData(SharedDestructibleData sharedDestructibleData)
  {
    this.sharedDestructibleData = sharedDestructibleData;
    currentHealth = sharedDestructibleData.initialHealth;
  }

}