using UnityEngine;
using System.Collections.Generic;
public class TheSimulation : MonoBehaviour
{
  [SerializeField] private float simulationTick;
  private float timer = 0;
  private TheSimulation instance;
  private List<Vector2Int> zonesToSimulate = new List<Vector2Int>();

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
  }

  private void Update()
  {
    timer += Time.deltaTime;
    if (timer >= simulationTick)
    {
      Simulate();
    }
  }

  private void Simulate()
  {
    foreach (Vector2Int zoneID in zonesToSimulate)
    {
      SimulateObjectsInZone(ObjectSpawner.instance.GetObjectsInZone(zoneID));
    }
  }

  private void SimulateObjectsInZone(List<VirtualGameObject> vgos)
  {
    foreach (VirtualGameObject vgo in vgos)
    {
      if (vgo is ISimulatable)
      {
        ((ISimulatable)vgo).Simulate(Time.deltaTime);
      }
    }
  }
}