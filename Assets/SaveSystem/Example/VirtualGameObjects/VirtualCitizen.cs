using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XNode;

// [Serializable]
public class VirtualCitizen : VirtualSimulatable
{

  public CitizenData citizenData;
  // destructible data

  public BehaviorTreeGraph citizenBT;
  public BehaviorTreeGraph citizenBTInstance;
  private float? interactionStartTime;
  bool isInteractionPending = false;
  private GoToTarget.TargetType currentTargetType;
  private bool isDestinationReached = false;
  public VirtualCitizen()
  {
    isStatic = false;
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

  public override void Simulate(float deltaTime)
  {
    if (citizenBTInstance == null)
    {
      Debug.Log("NO BT INSTANCE");
      return;
    }
    // Debug.Log("SIMULATION TICK");
    // Debug.Log("------------------------");
    // Debug.Log("current Targert: " + citizenData.currentTarget);
    // Debug.Log("IsInteractionPending: " + isInteractionPending);
    // Debug.Log("has current target: " + citizenData.hasCurrentTarget);
    // Debug.Log("Has Interaction Target: " + HasInteractTarget());
    RunBehaviorTree();
    // Debug.Log("------------------------");
    // Debug.Log("setting inventory");
    // citizenData.inventory.items[0] = new ItemData();

    // BASIC MOVEMENT
    if (citizenData.hasCurrentTarget)
    {
      if (Vector3.Distance(citizenData.currentTargetPosition, worldPosition) > 0.5f)
      {
        // @TODO Need to think about sharing methods like this between VirutalCitizen and Citizen
        // with configurable space (world for VirtualCitizen vs. game for Citizen)
        worldPosition = Vector3.MoveTowards(worldPosition, citizenData.currentTargetPosition, deltaTime * 3);
      }
      else
      {
        // Debug.Log("TARGET REACHED");
        isDestinationReached = true;
        citizenData.hasCurrentTarget = false;
        // PickNewDestination();
      }
    }


  }

  public void AssignWorkforce(WorkforceData workforce)
  {
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

  public bool SetCurrentTargetType(GoToTarget.TargetType targetType)
  {
    if (citizenData.workforce != null)
    {
      if (citizenData.workforce.AssignTargetToCitizen(this, targetType, out VirtualGameObject target)) {
        citizenData.currentTarget = target;
        currentTargetType = targetType;
        citizenData.hasCurrentTarget = true;
        SetCurrentTargetPosition(target.worldPosition);
        isDestinationReached = false;
        return true;
      }
    }
    return false;
    // Debug.Log("SetCurrentTargetType: " + targetType);
  }

  public bool HasMoreInteractTargets(GoToTarget.TargetType targetType)
  {
    if (citizenData.workforce != null)
    {
      return citizenData.workforce.HasMoreTargets(targetType);
    }
    return false;
  }
  public void TryEquipItem(SharedItemData.ItemType itemType)
  {
    Debug.Log("TryEquipItem");
  }

  public bool IsTargetReached()
  {
    return isDestinationReached;
  }

  public bool IsTargetSet()
  {
    return citizenData.hasCurrentTarget;
  }

  public bool HasInteractTarget() {
    return isDestinationReached && citizenData.currentTarget != null;
  }

  public bool InteractWithCurrentTarget()
  {
    if (citizenData.currentTarget == null || isInteractionPending)
    {
      return false;
    }
    InteractionResult result;
    isInteractionPending = false;
    if (citizenData.workforce != null)
    {
      result = citizenData.workforce.InteractWithCurrentTarget(this, citizenData.currentTarget, currentTargetType, CompleteInteraction);
    } else {
      result = citizenData.currentTarget.Interact(CompleteInteraction);
    }
    if (result == InteractionResult.PENDING) {
      isInteractionPending = true;
      return true;
    }
    if (result == InteractionResult.SUCCESS) {
      isInteractionPending = false;
      citizenData.currentTarget = null;
      return true;
    }
    return false;
  }

  public void CompleteInteraction(List<ItemData> items)
  {
    // add these items to the citizens inventory
    if (items != null)
    {
      foreach (ItemData item in items)
      {
        citizenData.inventory.AddItem(item);
      }
    }
    citizenData.currentTarget = null;
    isInteractionPending = false;
  } 

  public bool IsInteractionComplete() {
    return !isInteractionPending;
  }

  public bool IsInventoryFull()
  {
    return citizenData.inventory.IsFull();
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