using UnityEngine;
public class ExampleObjectSpawner : ObjectSpawner
{
  public override void CreateObjectsInZone(SpawnData spawnData)
  {
    base.CreateObjectsInZone(spawnData);
  }

  public override void CreateVirtualObjectsInZone(Vector3 zonePosition)
  {
    int halfZoneSize = ZoneSystem.instance.zoneSize / 2;
    Vector2Int zoneID = ZoneSystem.instance.GetZoneFromPosition(zonePosition);
    Vector3 origin = ZoneSystem.instance.GetPositionFromZone(zoneID);

    foreach (Vegetation veg in vegetation)
    {
      float population = 0;
      if (veg.maxPopulation < 1f)
      {
        if (UnityEngine.Random.value < veg.maxPopulation)
        {
          population = 1;
        }
        else
        {
          continue;
        }
      }
      else
      {
        population = UnityEngine.Random.Range(veg.minPopulation, veg.maxPopulation);
      }
      float offset = halfZoneSize - veg.groupRadius;
      for (int i = 0; i <= population; i++)
      {
        float x = UnityEngine.Random.Range(origin.x - offset, origin.x + offset);
        float z = UnityEngine.Random.Range(origin.z - offset, origin.z + offset);
        int groupSize = UnityEngine.Random.Range(veg.minGroupSize, veg.maxGroupSize + 1);
        Vector3 center = new Vector3(x, 0, z);
        for (int j = 0; j < groupSize; j++)
        {
          Vector3 position = j == 0 ? center : GetRandomPointInCircle(center, veg.groupRadius);
          // position.y = WorldGenerator.GetNoise(center.x, center.z) + veg.yOffset;
          // Vector3 normal;
          // bool gotGroundData = GetGroundData(ref position, out normal);
          // if (!gotGroundData)
          // {
          //     position.y = WorldGenerator.GetNoise(position.x, position.z) + veg.yOffset;
          // }
          if (position.y <= veg.maxAltitude && position.y >= veg.minAltitude)
          {
            position.y += veg.yOffset;
            // if (IsBlocked(position)) continue;
            Quaternion rot = Quaternion.identity;
            // if (gotGroundData && veg.useGroundTilt)
            // { // should configure for sometimes using ground tilt and other times not
            //     if (Mathf.Abs(normal.x) < veg.maxSteepness && Mathf.Abs(normal.z) < veg.maxSteepness)
            //     {
            //         Quaternion rot1 = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            //         rot = Quaternion.FromToRotation(Vector3.up, (Vector3)normal) * rot1;
            //     }
            //     else
            //     {
            //         continue;
            //     }
            // }
            // else
            // {
            // would be cool to make trees lean towards the water when on the coast
            rot = Quaternion.Euler(UnityEngine.Random.Range(0, veg.maxTilt), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, veg.maxTilt));
            // }
            // GameObject instance = ObjectDB.instance.InstantiateNew(veg.prefab, position, rot);
            Vector3 scale = veg.prefab.transform.localScale * UnityEngine.Random.Range(veg.minScale, veg.maxScale);
            // System.Type objectType = veg.isItem ? typeof(Item) : typeof(IDamageable); // could potentially get this off the prefab instead
            // GameObject instance = SpawnNew(veg.prefab, position, rot, scale, zone.zoneID);
            CreateVirtualGameObject(veg.prefab, position, rot, scale, zoneID);
            // instance.transform.parent = zone.root.transform;
          }
        }
      }
    }
  }
}

