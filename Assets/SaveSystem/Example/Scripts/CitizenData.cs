using UnityEngine;
using System;

// [System.Serializable]
public class CitizenData : ISaveableData
{
  public Vector3 currentTargetPosition;
  public string id;
  public CitizenData()
  {
    currentTargetPosition = Vector3.zero;
    id = Guid.NewGuid().ToString();
  }
  public void Save(SaveData dataToSave)
  {
    dataToSave.Write(currentTargetPosition);
  }

  public void Load(SaveData dataToLoad)
  {
    currentTargetPosition = dataToLoad.ReadVector3();
  }
}