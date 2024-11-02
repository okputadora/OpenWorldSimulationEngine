using System.Collections.Generic;
using UnityEngine;
public class HitData // @TODO move to attack
{
  public Dictionary<DamageType, int> damages;
  public int damage;
  public Vector3 direction;
  public Vector3 hitPoint;
}