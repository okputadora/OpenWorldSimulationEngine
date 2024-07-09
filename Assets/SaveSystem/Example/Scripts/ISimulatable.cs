using UnityEngine;

interface ISimulatable
{
  public void Simulate(float deltaTime);

}

public class VirtualSimulatable : VirtualGameObject // (VirtualDesctructible) will all simulatables be destructible?
{
  private Vector2Int? newParentZone;
  protected float lastUpdateTime;
  public virtual void Simulate(float deltaTime)
  {

  }

  public bool ShouldUpdateZone() // only relevant for moving objects
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