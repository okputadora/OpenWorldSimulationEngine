using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedGatherOccupationData", menuName = "Occuppation/SharedGatherOccupationData", order = 2)]
public class SharedGatherOccupationData : SharedOccupationData
{
  [SerializeField] private bool hasToAttackToPickup = false;
  [SerializeField] private List<SharedDestructibleData> attackTargets = new List<SharedDestructibleData>();

  // public override WorkforceData CreateWorkforceData(string name, List<VirtualCitizen> citizens, List<VirtualItem> items)
  // {
  //   return new GatherWorkforceData(name, this, citizens, items);
  // }

}