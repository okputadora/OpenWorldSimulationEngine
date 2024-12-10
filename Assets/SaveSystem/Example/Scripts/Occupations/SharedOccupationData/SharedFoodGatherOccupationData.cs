using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedFoodGatherOccupationData", menuName = "Occuppation/SharedFoodGatherOccupationData", order = 2)]
public class SharedFoodGatherOccupationData : SharedGatherOccupationData
{
  public bool dynamicPickableItems = false;
  public List<SharedPickableData> pickableItems;

  public FoodGatherWorkforceData CreateFoodGatherWorkforceData(
    string name,
    List<VirtualCitizen> citizens,
    List<Vector2Int> zones,
    List<VirtualStorage> foodContainers,
    List<VirtualStorage> toolContainers
  )
  {
    Debug.Log("creating food gather workforce from shared data");
    return new FoodGatherWorkforceData(this, pickableItems, name, citizens, zones, foodContainers, toolContainers);
  }
}