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

  public void Simulate(float deltaTime)
  {
    // if (inDanger) {

    // }
    // if (isHungry) {
    //    if(!eating) {
    // 
    // }
    // }
    // if (isTired) {z
    //  if (!asleep) {
    //      StartSleep(Time.time)
    //  } else {
    //    UpdateSleep(deltaTime)
    // }
    //      return;
    //  }
    // }
    // get data.currentTask
    // if (!currentTask) {
    // Simulate idle behavior, 
    // }
    // else {
    //  SimulateOccupation();
    // }
  }
}

public enum CitizenState
{
  sleeping,
  eating,
  idle,
  working
}