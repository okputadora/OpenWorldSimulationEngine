using UnityEngine;
using System;

// [System.Serializable]
public class CitizenData : ISaveableData
{
  public Vector3 currentTargetPosition;
  public bool hasCurrentTarget = false;
  public Guid workforceId;
  public WorkforceData workforce;
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

  public void SetCurrentTargetPosition(Vector3 worldPosition)
  {
    currentTargetPosition = worldPosition;
    hasCurrentTarget = true;
  }

  public void ClearCurrentTarget()
  {
    hasCurrentTarget = false;
  }

  public void AssignWorkforce(WorkforceData workforce)
  {
    // Debug.Log("Assigning workforce");
    this.workforce = workforce;
    workforceId = workforce.id;
  }
}