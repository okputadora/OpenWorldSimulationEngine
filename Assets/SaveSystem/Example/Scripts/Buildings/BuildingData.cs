using System.Collections.Generic;
using UnityEngine;
public class BuildingData : ISaveableData
{
  public List<BuildPieceData> buildPieces;
  public Vector3 origin;
  public SharedBuildingData sharedBuildingData;

  public BuildingData(SharedBuildingData building, Vector3 origin)
  {
    buildPieces = new List<BuildPieceData>();
    foreach (VirtualBuildPiece virtualBuildPiece in building.buildPieces)
    {
      buildPieces.Add(virtualBuildPiece.buildPieceData);
    }
  }

  public void Load(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }

  public void Save(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }

  public Dictionary<SharedItemData, int> CalculateBuildCost()
  {
    BuildCost buildCost = new BuildCost();
    foreach (BuildPieceData buildPiece in buildPieces)
    {
      foreach (Ingredient ingredient in buildPiece.recipe.ingredients)
      {
        buildCost.AddItem(ingredient);
      }
    }
    return buildCost.cost;
  }
}
