using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class ZoneSystem : MonoBehaviour
{
    public static ZoneSystem instance;
    [SerializeField] private bool debugMode;
    [SerializeField] public int zoneSize { get; private set; } = 11;
    [SerializeField] private float zoneTTL = 4f;
    [SerializeField] private int localArea = 3;
    [SerializeField] private int distantArea = 3;
    [SerializeField] private GameObject zonePrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private float originResetThreshold = 16;
    [SerializeField][ReadOnly] private Vector3 currentOffset = Vector3.zero;
    public Vector2Int centerZoneID;
    private Dictionary<Vector2Int, Zone> localZones = new Dictionary<Vector2Int, Zone>();
    private Dictionary<Vector2Int, Zone> distantZones = new Dictionary<Vector2Int, Zone>();
    private List<Vector2Int> generatedDistantZones = new List<Vector2Int>();
    private List<Vector2Int> generatedLocalZones = new List<Vector2Int>();
    private float updateTimer = 0;
    private bool isFirstRender = true;
    public Bounds2D localBounds = new Bounds2D();

    public void Save(SaveData dataToSave)
    {
        dataToSave.Write(currentOffset);
        dataToSave.Write(generatedDistantZones.Count);
        foreach (Vector2Int zoneID in generatedDistantZones)
        {
            dataToSave.Write(zoneID);
        }
        dataToSave.Write(generatedLocalZones.Count);
        foreach (Vector2Int zoneID in generatedLocalZones)
        {
            dataToSave.Write(zoneID);
        }
    }

    public void Load(SaveData dataToLoad)
    {
        // print("loading data for zone system");
        currentOffset = dataToLoad.ReadVector3();
        int count = dataToLoad.ReadInt();
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2Int zone = dataToLoad.ReadVector2Int();
                generatedDistantZones.Add(zone);
            }
        }
        int localCount = dataToLoad.ReadInt();
        if (localCount > 0)
        {
            for (int i = 0; i < localCount; i++)
            {
                Vector2Int zone = dataToLoad.ReadVector2Int();
                generatedLocalZones.Add(zone);
            }
        }
    }

    private void Awake()
    {
        centerZoneID = new Vector2Int(99999, 99999);
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer > 0.25f)
        { // greatly increases performance
            UpdateTTL(0.25f);
            CreateZones(player.transform.position);
            CheckResetOrigin();
            updateTimer = 0;
            if (isFirstRender) isFirstRender = false;
        }
    }
    private void CreateZones(Vector3 spawnCenter)
    {
        Vector2Int zone = GetZoneFromPosition(spawnCenter + currentOffset);
        centerZoneID = zone;
        localBounds.center = centerZoneID * zoneSize;
        for (int y = zone.y - localArea - distantArea; y <= zone.y + localArea + distantArea; ++y)
        {
            for (int x = zone.x - localArea - distantArea; x <= zone.x + localArea + distantArea; ++x)
            {
                Vector2Int zoneID = new Vector2Int(x, y);
                if (x >= zone.x - localArea && x <= zone.x + localArea && y >= zone.y - localArea && y <= zone.y + localArea)
                {
                    if (distantZones.TryGetValue(zoneID, out Zone oldDistantZone))
                    {
                        CreateLocalZoneFromDistantZone(oldDistantZone);
                    }
                    if (localZones.TryGetValue(zoneID, out Zone existingZone))
                    {
                        existingZone.ttl = 0;
                    }
                    else
                    {
                        CreateLocalZone(zoneID);
                    }
                }
                else if (localZones.TryGetValue(zoneID, out Zone oldLocalZone))
                {
                    CreateDistantZoneFromLocalZone(oldLocalZone);
                }
                else if (distantZones.TryGetValue(zoneID, out Zone existingDistantZone))
                {
                    existingDistantZone.ttl = 0;
                }
                else
                {
                    CreateDistantZone(zoneID);
                }
            }
        }
    }

    private void CreateLocalZone(Vector2Int zoneID)
    {
        Vector3 zoneWorldPosition = GetPositionFromZone(zoneID);
        Zone zoneData = new Zone(zoneID, zoneWorldPosition);
        localZones.Add(zoneID, zoneData);
        Vector3 zoneGamePosition = zoneWorldPosition - currentOffset;
        bool isZoneNew = !generatedLocalZones.Contains(zoneID);
        GameObject zoneRoot = Instantiate(zonePrefab, zoneGamePosition, Quaternion.identity);
        zoneData.root = zoneRoot;
        zoneRoot.GetComponent<MeshRenderer>().material.color = Color.green;
        if (!isZoneNew)
        {
            ObjectSpawner.instance.LoadObjectsInZone(zoneID, zoneRoot, true, isFirstRender);
        }
        else
        {
            // generatedDistantZones.Add(zoneID);
            generatedLocalZones.Add(zoneID);
            ObjectSpawner.instance.CreateObjectsInZone(new SpawnData()
            {
                zone = zoneData,
                shouldCreateDistant = isFirstRender,
                shouldCreateLocal = true,
                halfZoneSize = zoneSize / 2,
            }
            );
        }
        // Pathfinding.instance.Rebuild(); // fire onLocalZoneCreated event
    }

    private void CreateDistantZone(Vector2Int zoneID)
    {
        // ***** This is shared between createDistant and create local zone...combine
        Zone zoneData = new Zone(zoneID, GetPositionFromZone(zoneID));
        distantZones.Add(zoneID, zoneData);
        Vector3 spawnPosition = GetPositionFromZone(zoneID) - currentOffset; ;
        GameObject zoneRoot = Instantiate(zonePrefab, spawnPosition, Quaternion.identity);
        zoneRoot.GetComponent<MeshRenderer>().material.color = Color.blue;
        bool isZoneNew = !generatedDistantZones.Contains(zoneID);
        zoneData.root = zoneRoot;
        // ******
        if (generatedDistantZones.Contains(zoneID))
        {
            if (!isZoneNew)
            {

                ObjectSpawner.instance.LoadObjectsInZone(zoneID, zoneRoot, false, true);
            }
        }
        else
        {
            generatedDistantZones.Add(zoneID);
            ObjectSpawner.instance.CreateObjectsInZone(new SpawnData()
            {
                zone = zoneData,
                shouldCreateDistant = true,
                shouldCreateLocal = false,
                halfZoneSize = zoneSize / 2,
            }
            );
        }
    }

    private void CreateLocalZoneFromDistantZone(Zone zoneToConvert)
    {
        Vector2Int zoneID = zoneToConvert.zoneID;
        distantZones.Remove(zoneID);
        localZones.Add(zoneID, zoneToConvert);
        zoneToConvert.root.GetComponent<MeshRenderer>().material.color = Color.green;
        if (generatedLocalZones.Contains(zoneID))
        {
            ObjectSpawner.instance.LoadObjectsInZone(zoneID, zoneToConvert.root, true, false);
        }
        else
        {
            generatedLocalZones.Add(zoneID);
            ObjectSpawner.instance.CreateObjectsInZone(
                new SpawnData()
                {
                    zone = zoneToConvert,
                    shouldCreateDistant = false,
                    shouldCreateLocal = true,
                    halfZoneSize = zoneSize / 2,
                }
            );
        }
    }

    private void CreateDistantZoneFromLocalZone(Zone zoneToConvert)
    {
        Vector2Int zoneID = zoneToConvert.zoneID;
        localZones.Remove(zoneID);
        distantZones.Add(zoneID, zoneToConvert);
        int childCount = zoneToConvert.root.transform.childCount;
        zoneToConvert.root.GetComponent<MeshRenderer>().material.color = Color.blue;
        // @TODO This is not working because we're modifying the list as we iterate over it
        List<GameObject> toDestroy = new List<GameObject>();
        for (int i = 0; i < childCount; i++)
        {
            Transform child = zoneToConvert.root.transform.GetChild(i);
            DataSyncer ods = child.GetComponent<DataSyncer>();
            bool isDistant = ods == null ? false : ods.isDistant;
            if (!isDistant)
            {
                toDestroy.Add(child.gameObject);
            }
        }
        // Debug.Log("ZONE: destroying " + zoneID);
        foreach (GameObject obj in toDestroy)
        {
            Destroy(obj);
        }
        // if we destroy distant zone will it destroy all of the children we just moved to ZonePrefab?
        if (generatedDistantZones.Contains(zoneID))
        {
            // ObjectSpawner.instance.LoadDistantObjectsInZone(zoneID, zoneRoot);
            // No vegetation to create when going from local to distant
        }
        else
        {
            generatedDistantZones.Add(zoneID);
            // No vegetation to create when going from local to distant
        }
    }

    public Zone CreateVirtualZone(Vector3 position)
    {
        return new Zone(GetZoneFromPosition(position), position);

    }

    private void UpdateTTL(float dt)
    {
        // NOT SURE WE NEED LOCAL ZONES AT ALL...LOCAL ZONES WOULD NEVER BE DESTROYED WITHOUT FIRST 
        // BEING CONVERTED TO DISTANT ZONE
        foreach (KeyValuePair<Vector2Int, Zone> zone in localZones)
        {
            zone.Value.ttl += dt;
        }
        // Remove below: a local zone would always have to become a distant zone before being destroyed
        // foreach (KeyValuePair<Vector2Int, Zone> zone in localZones)
        // {
        //     if (zone.Value.ttl > zoneTTL)
        //     {
        //         // serialize children?
        //         // print("Saving");
        //         // print(SaveData.path + "/zones");
        //         // Directory.CreateDirectory(SaveData.path + "/zones");
        //         // for (int i = 0; i < zone.Value.root.transform.childCount; i++)
        //         // {
        //         //   Destroy(zone.Value.root.transform.GetChild(i).gameObject); 

        //         // }
        //         Destroy(zone.Value.root);
        //         localZones.Remove(zone.Key);
        //         // Pathfinding.instance.Rebuild();
        //         break;
        //     }
        // }
        foreach (KeyValuePair<Vector2Int, Zone> zone in distantZones)
        {
            zone.Value.ttl += dt;
        }
        foreach (KeyValuePair<Vector2Int, Zone> zone in distantZones)
        {
            if (zone.Value.ttl > zoneTTL)
            {
                for (int i = 0; i < zone.Value.root.transform.childCount; i++)
                {
                    Destroy(zone.Value.root.transform.GetChild(i).gameObject);
                }
                Destroy(zone.Value.root); // for distant zones there is no root, we need to destroy the veg...we should create a root to group them probably
                distantZones.Remove(zone.Key);
                break;
            }
        }
    }

    public void TryUpdateZone(GameObject go, VirtualGameObject vgo)
    {
        if (GetZoneFromGamePosition(go.transform.position) != vgo.zoneID)
        {
            Vector2Int zoneID = GetZoneFromGamePosition(go.transform.position);
            ReparentObject(go, vgo, zoneID);
        }
    }
    public void ReparentObject(GameObject go, VirtualGameObject vgo, Vector2Int zoneID)
    {
        // Reparenting the physical object
        localZones.TryGetValue(zoneID, out Zone zone);
        if (zone != null)
        {
            go.transform.SetParent(zone.root.transform);
        }
        // Reparenting the virtual object
        ObjectSpawner.instance.ReparentObject(zoneID, go, vgo);
    }
    public Vector3 GetPositionFromZone(Vector2Int zoneId)
    {
        float x = Mathf.FloorToInt(zoneId.x * zoneSize);
        float z = Mathf.FloorToInt(zoneId.y * zoneSize);
        return new Vector3(x, 0, z);
    }
    public Vector2Int GetZoneFromPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x + zoneSize / 2f) / zoneSize);
        int y = Mathf.FloorToInt((position.z + zoneSize / 2f) / zoneSize);
        return new Vector2Int(x, y);
    }

    public Vector2Int GetZoneFromGamePosition(Vector3 position)
    {
        Vector3 worldPosition = position + currentOffset;
        return GetZoneFromPosition(worldPosition);
    }
    public Vector3 GetGamePositionFromWorldPosition(Vector3 worldPosition)
    {
        return worldPosition - currentOffset;
    }

    public Vector3 GetWorldPositionFromGamePosition(Vector3 gamePosition)
    {
        return gamePosition + currentOffset;
    }
    public bool IsInLocalZone(Vector3 gamePosition)
    {
        Vector2Int zoneID = GetZoneFromGamePosition(gamePosition);
        return localZones.ContainsKey(zoneID);
    }

    public bool IsInAnyZone(Vector3 gamePosition)
    {
        Vector2Int zoneID = GetZoneFromGamePosition(gamePosition);
        return IsZoneActive(zoneID);
    }

    public bool IsZoneActive(Vector2Int zoneID)
    {
        return IsZoneLocal(zoneID) || IsZoneDistant(zoneID);
    }

    public bool IsZoneLocal(Vector2Int zoneID)
    {
        return localZones.ContainsKey(zoneID);
    }

    public bool IsZoneDistant(Vector2Int zoneID)
    {
        return distantZones.ContainsKey(zoneID);
    }

    public GameObject GetZoneRoot(Vector2Int zoneID)
    {
        localZones.TryGetValue(zoneID, out Zone zone);
        if (zone != null)
        {
            return zone.root;
        }
        return null;
    }

    private void CheckResetOrigin()
    {
        // probably dont want to check along the y axis
        Vector3 currentPosition = GetPositionFromZone(centerZoneID) - currentOffset;
        if (currentPosition.magnitude > originResetThreshold)
        {
            ResetOrigin(currentPosition);
        }
    }
    private void ResetOrigin(Vector3 currentPosition)
    {
        Vector3 prevOffset = currentOffset;
        currentOffset += currentPosition;
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            go.transform.position = go.transform.position - (currentOffset - prevOffset);
        }

    }

    public void OnDrawGizmos()
    {
        if (!debugMode) return;
        Gizmos.color = new Color(1, 0, 0, 0.5f);

        // Draw Cube at center of each zone
        // -------------------------------
        DrawZone(centerZoneID);
        Gizmos.color = new Color(0, 0, 1, 0.5f); ;
        foreach (KeyValuePair<Vector2Int, ZoneSystem.Zone> zone in localZones)
        {
            if (zone.Key != centerZoneID)
            {
                DrawZone(zone.Key);
            }
        }

        Gizmos.color = new Color(0, 1, 0, 0.5f);
        foreach (KeyValuePair<Vector2Int, ZoneSystem.Zone> zone in distantZones)
        {
            DrawZone(zone.Key);
        }

        Gizmos.color = Color.cyan;
        // if (gizmoZones.Count > 0)
        // {
        //   Gizmos.color = Color.magenta;
        //   foreach (Vector2Int zone in gizmoZones)
        //   {
        //     DrawZone(zone);
        //   }
        // }
        // // Draw GetGroundData RayCast
        // // --------------------------
        // if (debugRay != null) {
        //     Gizmos.DrawLine(debugPos1, debugPos2);
        // }
        // foreach (Vector3 raycast in raycasts)
        // Gizmos.DrawLine(raycast, raycast + (Vector3.down * 5));

    }

    private void DrawZone(Vector2Int zone)
    {
        float y1 = 0; // WorldGenerator.instance == null ? -0.1f : WorldGenerator.GetNoise(zone.x * zoneSize, zone.y * zoneSize);
        Vector3 pos = new Vector3(zone.x * zoneSize, y1, zone.y * zoneSize) - currentOffset;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        Handles.Label(pos, zone.ToString(), style);
        // Gizmos.DrawCube(pos, new Vector3(zoneSize, 0.1f, zoneSize));
    }
    public class Zone
    {
        public Vector2Int zoneID;
        public Vector3 worldPosition;
        public float ttl;
        public GameObject root;
        public bool isDistant;
        public Zone(Vector2Int zoneID, Vector3 worldPosition)
        {
            this.worldPosition = worldPosition;
            this.zoneID = zoneID;
        }
    }
}

public class Bounds2D
{
    public Vector2 center;
    public Vector2 size;

    // Only for points OUTSIDE of the Bounds
    public Vector2 FindClosestPoint(Vector2 target)
    {
        Vector2 closestPoint = new Vector2()
        {
            x = Mathf.Max(center.x - size.x, Mathf.Min(target.x, center.x + size.x)),
            y = Mathf.Max(center.y - size.y, Mathf.Min(target.y, center.y + size.y))
        };
        return closestPoint;
    }
}