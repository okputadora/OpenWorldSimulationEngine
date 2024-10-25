using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private int seed;
    [SerializeField] private GameObject citizenPrefab;
    public List<VirtualGameObject>[] objectsByZone;
    public ObjectDB objectDB;
    public Dictionary<int, GameObject> prefabsByID;
    public static ObjectSpawner instance;
    // private bool stopNow = false;
    private int zoneWidth = 512; // should be defined in zone system
    [SerializeField] protected List<Vegetation> vegetation = new List<Vegetation>();

    // Debugging
    public List<VirtualCitizen> allCitizens = new List<VirtualCitizen>();

    // EVENT HANDLING
    public delegate void OnCreateNewZone(Vector2Int zoneID);
    public OnCreateNewZone onCreateNewZone;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        prefabsByID = objectDB.GetPrefabDict();
        objectsByZone = new List<VirtualGameObject>[zoneWidth * zoneWidth];
        UnityEngine.Random.InitState(seed);
    }

    public void Load(SaveData dataToLoad)
    {
        int zoneCount = dataToLoad.ReadInt();
        for (int i = 0; i < zoneCount; i++)
        {
            int objectCount = dataToLoad.ReadInt();
            for (int j = 0; j < objectCount; j++)
            {
                string objectType = dataToLoad.ReadString();
                VirtualGameObject vgo = (VirtualGameObject)Activator.CreateInstance(Type.GetType(objectType)); // use VirtualObjectType
                // we need to call Init on the vgo which will require fetching the corresponding prefab from the db using the vgo.prefabId
                vgo.Load(dataToLoad);
                int index = GetIndexFromZone(vgo.zoneID);
                if (objectsByZone[index] == null)
                {
                    objectsByZone[index] = new List<VirtualGameObject>();

                }
                objectsByZone[index].Add(vgo);
            }
        }
    }

    public void Save(SaveData dataToSave)
    {
        dataToSave.Write(objectsByZone.Length);
        for (int i = 0; i < objectsByZone.Length; i++)
        {
            int count = objectsByZone[i] != null ? objectsByZone[i].Count : 0;
            dataToSave.Write(count);
            for (int j = 0; j < count; j++)
            {
                VirtualGameObject sgo = objectsByZone[i][j];
                dataToSave.Write(sgo.GetType().FullName);
                objectsByZone[i][j].Save(dataToSave);
            }
        }
    }

    public void LoadObjectsInZone(Vector2Int zoneID, GameObject zone, bool shouldLoadLocal, bool shouldLoadDistant)
    {
        List<VirtualGameObject> vgos = objectsByZone[GetIndexFromZone(zoneID)];
        if (vgos != null)
        {
            foreach (VirtualGameObject vgo in vgos)
            {
                if (!shouldLoadLocal && !vgo.isDistant) continue;
                if (!shouldLoadDistant && vgo.isDistant) continue;
                InstantiateFromVirtualGameObject(vgo, zone.transform, zoneID);
            }
        }
    }

    public virtual void CreateObjectsInZone(SpawnData spawnData)
    {
        // Needed to load moving objects that potentially moved into a zone that has not been generated yet
        if (objectsByZone[GetIndexFromZone(spawnData.zone.zoneID)] != null)
        {
            LoadObjectsInZone(spawnData.zone.zoneID, spawnData.zone.root, spawnData.shouldCreateLocal, spawnData.shouldCreateLocal);
            // might want to yield to end of frame here to prevent duplicate creation with LoadObjectsInZone
        }
        ZoneSystem.Zone zone = spawnData.zone;
        if (zone.zoneID != new Vector2Int(0, 0)) return;
        bool shouldCreateDistant = spawnData.shouldCreateDistant;
        bool shouldCreateLocal = spawnData.shouldCreateLocal;
        int halfZoneSize = spawnData.halfZoneSize;
        Vector3 origin = zone.root.transform.position; // gamePosition, zone.center = world position


        // int spawnCount = 2;
        foreach (Vegetation veg in vegetation)
        {
            DataSyncer ods = veg.prefab.GetComponent<DataSyncer>();

            bool isDistant = ods == null ? false : ods.isDistant;
            if (!veg.enabled || (!shouldCreateDistant && isDistant) || (!shouldCreateLocal && !isDistant)) continue;
            int population;
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
                population = UnityEngine.Random.Range(veg.minPopulation, (int)veg.maxPopulation + 1);
            }
            float offset = halfZoneSize - veg.groupRadius;
            for (int i = 0; i < population; i++)
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
                        GameObject instance = SpawnNew(veg.prefab, position, rot, scale, zone.zoneID, origin);
                        instance.transform.parent = zone.root.transform;
                        // spawnCount -= 1;
                        // if (spawnCount == 0)
                        // {
                        //     spawnCount = 2;
                        //     // yield return new WaitForSeconds(1f);
                        // }
                        // if (stopNow == false) stopNow = true;
                        // return;
                    }
                }
            }
        }
    }

    public void CreateVirtualObjectsInZone(Vector3 zonePosition)
    {
        Vector2Int zoneID = ZoneSystem.instance.GetZoneFromPosition(zonePosition);
        CreateVirtualObjectsInZone(zoneID);

    }

    public void CreateVirtualObjectsInZone(Vector2Int zoneID)
    {
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
            int halfZoneSize = ZoneSystem.instance.zoneSize / 2;
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

    protected void CreateVirtualGameObject(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, Vector2Int zoneID)
    {
        VirtualGameObject vgo = prefab.GetComponent<DataSyncer>().CreateVirtualGameObject(prefab, position, rotation, scale, zoneID);
        AddVirtualGameObjectToZone(vgo, zoneID);

    }
    private void InstantiateFromVirtualGameObject(VirtualGameObject vgo, Transform parent, Vector2Int zoneID)
    {
        prefabsByID.TryGetValue(vgo.prefabID, out GameObject prefab);
        if (prefab == null)
        {
            throw new Exception("Prefab not found: " + vgo.prefabID);
        }
        // @TODO we are wasting calls to GetGamePositionFromWOrldPosition, this also happens in vgo.SyncGameObjectWithData
        // we need to call sync gameObject with data to syn stuff like ItemData but we dont really need to set position
        // maybe we change SyncGameObjectWithData to abstract in Base VGO and then just do data transfers and not position, scale, rotation
        GameObject instance = Instantiate(prefab, ZoneSystem.instance.WorldToGamePosition(vgo.worldPosition), vgo.rotation);
        instance.transform.SetParent(parent);
        vgo.SyncGameObjectWithData(instance);
        instance.GetComponent<DataSyncer>().AttachVirtualGameObject(vgo);
    }

    public void DestroyGameObject(GameObject instance)
    {
        instance.GetComponent<DataSyncer>().shouldDestroyPermanently = true;
        Destroy(instance);
    }

    private GameObject SpawnNew(GameObject prefab, Vector3 gamePosition, Quaternion rotation, Vector3 scale, Vector2Int zoneID, Vector3 zoneGamePosition)
    {
        GameObject instance = Instantiate(prefab, gamePosition, rotation);
        instance.transform.localScale = scale;
        DataSyncer dataSyncer = instance.GetComponent<DataSyncer>();
        Vector2Int adjustedZoneID = ZoneSystem.instance.GetZoneFromGamePosition(instance.transform.position);
        VirtualGameObject vgo = dataSyncer.CreateVirtualGameObject(instance, ZoneSystem.instance.GameToWorldPosition(instance.transform.position), adjustedZoneID);
        // Because objects are spawned within a certain radius, they may actually lie outside of the currently generating zone.
        // if thats the case, we want to reparent the object to the proper zone...may need to call ZoneSystem.instance.ReparentObject() but it also might not matter
        // thats its parented to a nearby zone
        // vgo.Initialize(instance, ZoneSystem.instance.GameToWorldPosition(instance.transform.position), adjustedZoneID);
        AddVirtualGameObjectToZone(vgo, adjustedZoneID);
        return instance;
    }

    public List<VirtualGameObject> GetObjectsInZone(Vector2Int zoneID)
    {
        List<VirtualGameObject> vgos = objectsByZone[GetIndexFromZone(zoneID)];
        if (vgos == null) return null;
        return vgos;
    }

    public List<T> GetObjectsInZone<T>(Vector2Int zoneID)
    {
        List<T> objects = objectsByZone[GetIndexFromZone(zoneID)]?.OfType<T>()?.ToList();
        return objects;
    }

    // If a moving object enters a new zone, reorganize the list of virtualGameObjectsByZone
    public void ReparentObject(Vector2Int newZoneID, GameObject go, VirtualGameObject vgo)
    {
        if (go != null)
        {
            // do we actually need to do this or only while destroying
            vgo.SyncDataWithGameObject(go);
        }
        if (vgo.zoneID == newZoneID) return;
        objectsByZone[GetIndexFromZone(vgo.zoneID)].Remove(vgo);
        if (vgo is VirtualCitizen)
        {
            allCitizens.Remove((VirtualCitizen)vgo);
        }
        // @TODO need logic based on is distant 
        if (!ZoneSystem.instance.IsZoneLocal(newZoneID))
        {
            Destroy(go);
        }
        else if (!ZoneSystem.instance.IsZoneLocal(vgo.zoneID))
        {
            InstantiateFromVirtualGameObject(vgo, ZoneSystem.instance.GetZoneRoot(newZoneID).transform, newZoneID);
        }
        vgo.zoneID = newZoneID;
        AddVirtualGameObjectToZone(vgo, newZoneID);
    }

    private void AddVirtualGameObjectToZone(VirtualGameObject vgo, Vector2Int zoneID)
    {
        int index = GetIndexFromZone(zoneID);
        if (objectsByZone[index] == null)
        {
            objectsByZone[index] = new List<VirtualGameObject>();
            if (onCreateNewZone != null)
            {
                onCreateNewZone(zoneID);
            }
        }
        objectsByZone[index].Add(vgo);
        // Debugging
        if (vgo is VirtualCitizen)
        {
            allCitizens.Add((VirtualCitizen)vgo);
        }
    }
    private int GetIndexFromZone(Vector2Int zoneID)
    { // this is duplicated in CivilizationManager, maybe move it to static method on ZoneSystem
        int row = zoneID.x + zoneWidth / 2;
        int col = zoneID.y + zoneWidth / 2;
        return row < 0 || col < 0 || row >= zoneWidth || col >= zoneWidth ? -1 : col * zoneWidth + row;
    }

    protected Vector3 GetRandomPointInCircle(Vector3 center, float radius)
    {
        // https://stackoverflow.com/questions/5837572/generate-a-random-point-within-a-circle-uniformly
        // is this even worth it, what if we just pick random points within a square
        float theta = UnityEngine.Random.Range(0f, 1f) * Mathf.PI * 2.0f;
        float point = radius * Mathf.Sqrt(UnityEngine.Random.Range(0f, 1f));
        return center + radius * new Vector3(Mathf.Cos(theta) * point, 0.0f, Mathf.Sin(theta) * point);
    }

    public VirtualCitizen CreateVirtualCitizen(Vector3 worldPosition, Vector2Int zoneID, SettlementData settlement, CivilizationData civilization)
    {
        if (GetObjectsInZone(zoneID) == null)
        {
            CreateVirtualObjectsInZone(zoneID);
        }
        VirtualCitizen citizen = new VirtualCitizen();
        citizen.Initialize(citizenPrefab, worldPosition, zoneID); // pass civilization data so this citizen knows where it belongs
        AddVirtualGameObjectToZone(citizen, zoneID);
        return citizen;
    }
    private void OnDrawGizmos()
    {
        // Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
    [System.Serializable]
    public class Vegetation
    {
        [Tooltip("Needs to match the value of isDistant on the Vegetation prefabs ObjectDataSync component")] public bool isDistant;
        public bool isItem;
        public bool enabled;
        // Biome, this would be user defined. how can we extend this?
        public GameObject prefab;

        public float minScale = 1;
        public float maxScale;

        [Tooltip("Min group count")] public int minPopulation;
        [Tooltip("Max group count. If less than one, value represents the chance of spawning")] public float maxPopulation;
        public int minGroupSize;
        [Tooltip("")]
        public int maxGroupSize;
        public float groupRadius;
        public float yOffset;
        [Range(0, 45)]
        public int maxTilt;

        [Range(0, 1)]
        public float maxSteepness;

        public int maxAltitude;
        public int minAltitude;
        public int maxDistance;

        public bool snapToWater;

        public bool useGroundTilt;
    }
}


public struct SpawnData
{
    public ZoneSystem.Zone zone;
    public bool shouldCreateDistant;
    public bool shouldCreateLocal;
    public int halfZoneSize;

}