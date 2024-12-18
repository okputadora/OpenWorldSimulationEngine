using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

public class PickableData : ISaveableData
{
  public SharedPickableData sharedPickableData;
  public string sharedPickableDataId;
  public int currentCount = 1;
  public int dropsRemaining = 1;
  public float depletionTime;
  public float timeSinceDepletion = 0f;
  public Action<Dictionary<SharedItemData, int>>  callback;

  public PickableData(SharedPickableData sharedData)
  {
    dropsRemaining = sharedData.GetDropCount();
    sharedPickableData = sharedData;
    sharedPickableDataId = sharedData.id;
  }

  void ISaveableData.Load(SaveData dataToLoad)
  {
    throw new System.NotImplementedException();
  }

  void ISaveableData.Save(SaveData dataToSave)
  {
    throw new System.NotImplementedException();
  }


}