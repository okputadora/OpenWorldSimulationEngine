using UnityEngine;
public class BuildPiece : Destructible, ISaveableComponent<BuildPieceData>
{
  public BuildPieceRecipe recipe;
  [System.NonSerialized] public VirtualBuildPiece virtualBuildPiece;

  public void Load(SaveData dataToSave)
  {

  }

  public void Save(SaveData dataToLoad)
  {

  }

  public void HydrateData(BuildPieceData data)
  {
    // buildPieceData = data;
  }

  // public BuildPieceData CreateNewData()
  // {
  //   return new BuildPieceData();
  // }
}