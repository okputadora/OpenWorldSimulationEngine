public class CampFireData : ISaveableData
{
  public float remainingFuel = 50f;
  public float burnRatePerMin = 10; // this would probably be saved in SharedCampFireData

  public void Burn(float deltaTime)
  {
    remainingFuel -= burnRatePerMin * deltaTime / 60f;
  }
  public void Load(SaveData dataToLoad)
  {
    // load remaining fuel
    // remainingFuel = saveData.LoadFloat();
  }

  public void Save(SaveData dataToSave)
  {
    // save remaning fuel
    // saveData.Save(remainingFuel)
  }
}