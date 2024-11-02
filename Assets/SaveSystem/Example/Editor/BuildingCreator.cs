using UnityEngine;
using System.Collections.Generic;
public class BuildingCreator : MonoBehaviour
{
  public SharedBuildingData sharedBuildingData;
  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.S))
    {
      Save();
    }

  }

  public void GetInput()
  {

  }

  public void AddBuildPiece(BuildPiece buildPiece)
  {
    sharedBuildingData.buildPieces.Add(buildPiece.virtualBuildPiece);
  }

  public void Save()
  {

  }
}
