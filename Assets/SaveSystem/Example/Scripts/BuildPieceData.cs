public class BuildPieceData : ISaveableData
{
  public int buildPieceHealth = 100;
  public void Load(SaveData dataToSave)
  {
    // buildPieceHealth = saveData.LoadInt();
  }

  public void Save(SaveData dataToLoad)
  {
    // saveData.Save(buildPieceHealth);
  }
}