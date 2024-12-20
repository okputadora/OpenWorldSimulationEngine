using System;
using UnityEngine;
using XNode;

// [Serializable]
public class VirtualCitizen : VirtualSimulatable
{

  public CitizenData citizenData;

  public BehaviorTreeGraph citizenBT;
  public BehaviorTreeGraph citizenBTInstance;

  public VirtualCitizen()
  {
    citizenData = new CitizenData();
  }
  // public override void Initialize(GameObject instance, Vector3 worldPosition, Vector2Int zoneID)
  // {
  //   base.Initialize(instance, worldPosition, zoneID);
  //   // Debug.Log("Initializing");
  //   citizenData = new CitizenData();
  //   InitializeBehaviorTree(instance);
  //   // Debug.Log("initialized citizenBTInstance");
  //   // need to differentiate Initializing virtual and and loading virtual into the game world
  //   // i think we can actually just remove this
  //   SyncGameObjectWithData(instance);
  // }

  public override void SyncGameObjectWithData(GameObject go)
  {
    base.SyncGameObjectWithData(go);
    go.GetComponent<Citizen>().HydrateData(this);
    // destructible hydrate data
  }

  public override void Save(SaveData dataToSave)
  {
    base.Save(dataToSave);
    citizenData.Save(dataToSave);
  }

  public override void Load(SaveData dataToLoad)
  {
    // Debug.Log("load");
    base.Load(dataToLoad);
    citizenData = new CitizenData();
    citizenData.Load(dataToLoad);
    InitializeBehaviorTree();

  }

  public void InitializeBehaviorTree(GameObject go)
  {

    citizenBTInstance = (BehaviorTreeGraph)go.GetComponent<Citizen>().citizenBehaviorTree.Copy();
    BTCitizenNode rootNode = citizenBTInstance.nodes[0] as BTCitizenNode;
    BTCitizenContext context = new BTCitizenContext
    {
      citizen = this
    };
    rootNode.context = context;
  }

  private void InitializeBehaviorTree()
  {
    if (citizenBTInstance != null)
    {
      return;
    }
    // we might be able to pass this in as a parameter and not have to get it from the object spawner
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
    citizenData.currentTargetPosition = new Vector3(randomX, 0, randomZ);
  }

  public override void Simulate(float deltaTime)
  {
    if (citizenBTInstance == null)
    {
      Debug.Log("NO BT INSTANCE");
      return;
    }
    RunBehaviorTree();


    // BASIC TEST
    if (citizenData.currentTargetPosition != null)
    {
      if (Vector3.Distance(citizenData.currentTargetPosition, worldPosition) > 0.5f)
      {
        // @TODO Need to think about sharing methods like this between VirutalCitizen and Citizen
        // with configurable space (world for VirtualCitizen vs. game for Citizen)
        worldPosition = Vector3.MoveTowards(worldPosition, citizenData.currentTargetPosition, deltaTime * 5);
      }
      else
      {
        // PickNewDestination();
      }
    }


  }

  public void AssignWorkforce(WorkforceData workforce)
  {
    Debug.Log(workforce.sharedOccupationData);
    citizenData.AssignWorkforce(workforce);
    citizenBTInstance.SetOccupationNode(workforce.sharedOccupationData.behaviorTree);
    // set subnode of behavior tree with workforce shared data behavior tree
    // citizenBTInstance.Set
  }
  public void RunBehaviorTree()
  {
    BTCitizenNode rootNode = citizenBTInstance.nodes[0] as BTCitizenNode;
    rootNode.GetValue(rootNode.GetPort("inResult"));
  }

  // should these be on citizen data? i think no because we need the transform and stuff and we might need other data objects like destructibel data

  public void SetCurrentTarget(GameObject target)
  {
    citizenData.SetCurrentTargetPosition(ZoneSystem.instance.GameToWorldPosition(target.transform.position));
    // Debug.Log("HasMoreInteractTargets");
  }

  public void SetCurrentTargetPosition(Vector3 worldPosition)
  {
    citizenData.SetCurrentTargetPosition(worldPosition);
    // Debug.Log("SetCurrentTargetPosition (" + data.id + "): " + worldPosition);
  }

  public void ClearCurrentTarget()
  {
    citizenData.ClearCurrentTarget();
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
    // Debug.Log("checking is target reached for : " + data.id);
    if (citizenData.hasCurrentTarget)
    {
      // Debug.Log("Distance check pass: " + (Vector3.Distance(data.currentTargetPosition, worldPosition) < 1f));
      // Debug.Log("Distance: " + Vector3.Distance(data.currentTargetPosition, worldPosition));
      return Vector3.Distance(citizenData.currentTargetPosition, worldPosition) < 1f;
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