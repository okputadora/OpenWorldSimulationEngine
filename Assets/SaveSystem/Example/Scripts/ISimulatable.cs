using UnityEngine;

interface ISimulatable
{
  public void Simulate(float deltaTime)
  {

  }

  public bool ShouldUpdateZone();
}

public class VirtualSimulatable : VirtualGameObject
{
  private Vector2Int? newParentZone;
  public virtual void Simulate(float deltaTime)
  {

  }

  public bool ShouldUpdateZone()
  {
    Vector2Int testZoneID = ZoneSystem.instance.GetZoneFromPosition(worldPosition);
    if (testZoneID != zoneID)
    {
      newParentZone = testZoneID;
      return true;
    }
    return false;
  }

  public void ReparentZone()
  {
    if (newParentZone != null)
    {
      ObjectSpawner.instance.ReparentObject((Vector2Int)newParentZone, null, this);
    }
    newParentZone = null;
  }
}