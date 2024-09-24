using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class TheSimulation : RootSaveable
{
  [SerializeField] private float simulationTick;
  private float timer = 0;
  private TheSimulation instance;
  [SerializeField] private List<Vector2Int> zonesToSimulate = new List<Vector2Int>();

  private List<VirtualSimulatable> objectsToUpdateZone = new List<VirtualSimulatable>();
  // DEBUGGING
  private List<VirtualGameObject> objectsToSimulate = new List<VirtualGameObject>();

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
  }

  private void Start()
  {
    ObjectSpawner.instance.onCreateNewZone += AddZoneToSimulation;
  }

  private void LateUpdate()
  {
    timer += Time.deltaTime;
    if (timer >= simulationTick)
    {
      Simulate();
      if (objectsToUpdateZone.Count > 0)
      {
        UpdateParentZones();
      }
      timer = 0f;
    }
  }

  private void Simulate()
  {
    objectsToUpdateZone.Clear();
    foreach (Vector2Int zoneID in zonesToSimulate)
    {
      if (!ZoneSystem.instance.IsZoneLocal(zoneID))
      {
        SimulateObjectsInZone(ObjectSpawner.instance.GetObjectsInZone(zoneID)?.ToList());
      }
    }
  }

  private void AddZoneToSimulation(Vector2Int zoneID)
  {
    if (zonesToSimulate.Contains(zoneID))
    {
      return;
    }
    zonesToSimulate.Add(zoneID);
  }

  private void RemoveZoneFromSimulation(Vector2Int zoneID)
  {
    zonesToSimulate.Remove(zoneID);
  }

  private void SimulateObjectsInZone(List<VirtualGameObject> vgos)
  {
    if (vgos == null) return;
    foreach (VirtualGameObject vgo in vgos)
    {
      // DEBUG
      if (!objectsToSimulate.Contains(vgo))
      {
        objectsToSimulate.Add(vgo);
      }

      if (vgo is VirtualSimulatable)
      {
        VirtualSimulatable vso = vgo as VirtualSimulatable;
        vso.Simulate(simulationTick);
        if (vso.ShouldUpdateZone())
        {
          objectsToUpdateZone.Add(vso);
        }
      }
    }
  }

  private void UpdateParentZones()
  {
    foreach (VirtualSimulatable vso in objectsToUpdateZone)
    {
      vso.ReparentZone();
    }
  }

  public override void Save(SaveData dataToSave)
  {
    dataToSave.Write(zonesToSimulate.Count);
    foreach (Vector2Int zoneID in zonesToSimulate)
    {
      dataToSave.Write(zoneID);
    }
  }

  public override void Load(SaveData dataToLoad)
  {
    int count = dataToLoad.ReadInt();
    for (int i = 0; i < count; i++)
    {
      Vector2Int zoneID = dataToLoad.ReadVector2Int();
      zonesToSimulate.Add(zoneID);
    }
  }

  private void OnDrawGizmos()
  {
    foreach (VirtualGameObject vgo in objectsToSimulate)
    {
      if (vgo is VirtualCitizen)
      {
        VirtualCitizen vc = (VirtualCitizen)vgo;
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(ZoneSystem.instance.GetGamePositionFromWorldPosition(vc.data.currentTargetPosition), Vector3.one);
        Gizmos.DrawSphere(ZoneSystem.instance.GetGamePositionFromWorldPosition(vc.worldPosition), 1);
      }
    }
  }
}