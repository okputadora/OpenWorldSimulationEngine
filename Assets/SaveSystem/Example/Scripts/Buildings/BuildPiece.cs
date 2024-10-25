using UnityEngine;
public class BuildPiece : MonoBehaviour, ISaveableComponent<BuildPieceData>
{
  public VirtualBuildPiece virtualBuildPiece;

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

  public BuildPieceData CreateNewData()
  {
    return new BuildPieceData();
  }
}