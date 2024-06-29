using System;
using UnityEngine;
using XNode;

[Serializable]
public class VirtualCitizen : VirtualSimulatable
{

  public CitizenData data;

  public BehaviorTreeGraph citizenBT;
  private BehaviorTreeGraph citizenBTInstance;

  public override void Initialize(GameObject instance, Vector3 worldPosition, Vector2Int zoneID)
  {
    base.Initialize(instance, worldPosition, zoneID);
    Debug.Log("Initializing");
    data = new CitizenData();
    InitializeBehaviorTree(instance);
    Debug.Log("initialized citizenBTInstance");
    SyncGameObjectWithData(instance);
  }

  public override void SyncGameObjectWithData(GameObject go)
  {
    Debug.Log("VirtualCitizen: SyncingGameObjectWithData");
    base.SyncGameObjectWithData(go);
    go.GetComponent<Citizen>().HydrateData(data);
  }

  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
    data.Save(dataToSave);
  }

  public override void Load(SaveData dataToLoad)
  {
    Debug.Log("load");
    base.Load(dataToLoad);
    data = new CitizenData();
    data.Load(dataToLoad);
    InitializeBehaviorTree();

  }

  private void InitializeBehaviorTree(GameObject go)
  {

    citizenBTInstance = (BehaviorTreeGraph)go.GetComponent<Citizen>().virtualCitizen.citizenBT.Copy();
    BTCitizenNode rootNode = citizenBTInstance.nodes[0] as BTCitizenNode;
    BTCitizenContext context = new BTCitizenContext
    {
      citizen = this
    };
    rootNode.context = context;
    Debug.Log("intiialized citizenBTInstance");
  }

  private void InitializeBehaviorTree()
  {
    if (citizenBTInstance != null)
    {
      return;
    }
    ObjectSpawner.instance.prefabsByID.TryGetValue(prefabID, out GameObject prefab);
    if (prefab == null)
    {
      Debug.LogError("Prefab nout found: " + prefabID + " in VirtualCitizen");
      return;
    };
    InitializeBehaviorTree(prefab);

  }

  // For debugging only
  public void PickNewDestination()
  {
    float randomZ = UnityEngine.Random.Range(-30, 30);
    float randomX = UnityEngine.Random.Range(-30, 30);
    data.currentTargetPosition = new Vector3(randomX, 0, randomZ);
  }

  public override void Simulate(float deltaTime)
  {
    if (citizenBTInstance == null) return;
    BTCitizenNode rootNode = citizenBTInstance.nodes[0] as BTCitizenNode;
    rootNode.GetValue(rootNode.GetPort("inResult"));


    // BASIC TEST
    if (data.currentTargetPosition != null)
    {
      if (Vector3.Distance(data.currentTargetPosition, worldPosition) > 0.5f)
      {
        // @TODO Need to think about sharing methods like this between VirutalCitizen and Citizen
        // with configurable space (world for VirtualCitizen vs. game for Citizen)
        worldPosition = Vector3.MoveTowards(worldPosition, data.currentTargetPosition, deltaTime * 5);
      }
      else
      {
        // PickNewDestination();
      }
    }


  }

  // should these be on citizen data? 

  public void SetCurrentTarget(GameObject target)
  {
    data.SetCurrentTargetPosition(ZoneSystem.instance.GetWorldPositionFromGamePosition(target.transform.position));
    // Debug.Log("HasMoreInteractTargets");
  }

  public void SetCurrentTargetPosition(Vector3 worldPosition)
  {
    data.SetCurrentTargetPosition(worldPosition);
    // Debug.Log("SetCurrentTargetPosition: " + worldPosition);
  }

  public void ClearCurrentTarget()
  {
    data.ClearCurrentTarget();
  }

  public bool HasMoreInteractTargets()
  {
    // if citizen is working check pickup targets on Occupation.workforce
    // else ...tbd
    // Debug.Log("HasMoreInteractTargets");
    return false;
  }

  public void TryEquipItem(SharedItemData.ItemType itemType)
  {
    Debug.Log("TryEquipItem");
  }

  public bool IsTargetReached()
  {
    Debug.Log("checking is target reached for : " + data.id);
    if (data.hasCurrentTarget)
    {
      Debug.Log("Distance check pass: " + (Vector3.Distance(data.currentTargetPosition, worldPosition) < 1f));
      Debug.Log("Distance: " + Vector3.Distance(data.currentTargetPosition, worldPosition));
      return Vector3.Distance(data.currentTargetPosition, worldPosition) < 1f;
    }
    Debug.LogWarning("Checking is target reached with no target set");
    return false;
  }

}

public enum CitizenState
{
  defending,
  attacking,
  sleeping,
  eating,
  idle,
  working
}