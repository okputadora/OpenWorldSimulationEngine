using System.Collections.Generic;
using UnityEngine;
public class ConstructionWorkforceData : WorkforceData
{
  public Dictionary<Vector3, SharedBuildingData> targetBuildings;
  public ConstructionWorkforceData(string workforceName, List<VirtualCitizen> citizens, SharedBuildingData targetBuilding, Vector3 buildingOrigin) : base(citizens, workforceName, null)
  {
    this.targetBuildings.Add(buildingOrigin, targetBuilding);
  }
  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
  }
  public override void Load(SaveData dataToLoad)
  {
    base.Load(dataToLoad);
  }
}