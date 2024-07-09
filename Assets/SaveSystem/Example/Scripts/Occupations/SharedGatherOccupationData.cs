using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedGatherOccupationData", menuName = "Occuppation/SharedGatherOccupationData", order = 2)]
public class SharedGatherOccupationData : SharedOccupationData
{
  [SerializeField] private List<ItemData> pickupTargets = new List<ItemData>();
  [SerializeField] private bool hasToAttackToPickup = false;
  [SerializeField] private List<GameObject> attackTargets = new List<GameObject>();

}