
[System.Serializable]
public class BuildPieceData : ISaveableData
{

  public BuildPieceRecipe recipe;
  public BuildPieceData(BuildPieceRecipe recipe)
  {
    this.recipe = recipe;
  }
  public void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }
}