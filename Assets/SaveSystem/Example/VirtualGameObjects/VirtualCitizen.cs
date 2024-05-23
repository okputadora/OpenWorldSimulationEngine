using System;
using UnityEngine;

[Serializable]
public class VirtualCitizen : VirtualGameObject, ISimulatable
{

  public CitizenData data;

  public override void Initialize(GameObject instance, Vector3 worldPosition, Vector2Int zoneID)
  {
    base.Initialize(instance, worldPosition, zoneID);
    data = new CitizenData();
    SyncGameObjectWithData(instance);
  }

  public override void SyncGameObjectWithData(GameObject go)
  {
    base.SyncGameObjectWithData(go);
    go.GetComponent<Citizen>().HydrateData(data);
  }

  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
    data.Save(dataToSave);
  }

  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
    data = new CitizenData();
    data.Load(dataToLoad);
  }

  // For debugging only
  public void PickNewDestination()
  {
    float randomZ = UnityEngine.Random.Range(-30, 30);
    float randomX = UnityEngine.Random.Range(-30, 30);
    data.currentTargetPosition = new Vector3(randomX, 0, randomZ);
  }

  public void Simulate(float deltaTime)
  {
    if (data.currentTargetPosition != null)
    {
      if (Vector3.Distance(data.currentTargetPosition, worldPosition) > 0.5f)
      {
        // @TODO Need to think about sharing methods like this between VirutalCitizen and Citizen
        // with configurable space (world for VirtualCitizen vs. game for Citizen)
        worldPosition = Vector3.MoveTowards(worldPosition, data.currentTargetPosition, deltaTime * 5);
      }
      else
      {
        PickNewDestination();
      }
      // UpdateCitizenBehaviourTree();
      CheckRespawn();
    }
  }

  // should these be on citizen data? 

  public void SetCurrentTarget(GameObject target)
  {

  }

  public void SetCurrentTargetPosition(Vector3 worldPosition)
  {

  }
  public void ClearCurrentTarget()
  {

  }

  public bool HasMoreInteractTargets()
  {
    // if citizen is working check pickup targets on Occupation.workforce
    // else ...tbd
    return false;
  }

  public void TryEquipItem(SharedItemData.ItemType itemType)
  {

  }

}

public enum CitizenState
{
  defending,
  attacking,
  sleeping,
  eating,
  idle,
  working
}