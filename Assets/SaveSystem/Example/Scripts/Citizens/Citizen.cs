using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class Citizen : MonoBehaviour, ISaveableComponent<VirtualCitizen>
{
  [System.NonSerialized] public VirtualCitizen virtualCitizen;
  public BehaviorTreeGraph citizenBehaviorTree;
  public float speed = 10;

  public void Start()
  {
    // Debug.Log("Citizen Start");
    // virtualCitizen = GetComponent<DataSyncer>().objectData as VirtualCitizen;  ?? do we need to do this? it is one option but we're already attaching it in the Virtual Object factory
    // initialize listeners
  }

  public void Load(SaveData dataToSave)
  {

  }

  public void Save(SaveData dataToLoad)
  {

  }


  private void Update()
  {
    if (!virtualCitizen.citizenBTInstance)
    {
      Debug.LogError("Citizen has not BT on VirtualCitizen");
      return;
    }
    if (virtualCitizen.citizenData == null) return;
    // maybe put behind an update timer
    virtualCitizen.RunBehaviorTree();
    Vector3 gamePosition = ZoneSystem.instance.WorldToGamePosition(virtualCitizen.citizenData.currentTargetPosition);
    if (Vector3.Distance(transform.position, gamePosition) > 0.5f)
    {
      transform.position = Vector3.MoveTowards(transform.position, gamePosition, Time.deltaTime * speed);
      // need to update world position of data. this could get expensive
      // alternatively, we could have the VirtualCitizen deferring to gameObject (Citizen) when its attached
      // to do checks like IsTargetReached
      virtualCitizen.worldPosition = ZoneSystem.instance.GameToWorldPosition(transform.position);
    }
    else
    {
      Debug.Log("Citizen reached destination");
    }

  }

  private void ChooseRandomPosition()
  {
    float randomZ = Random.Range(-30, 30);
    float randomX = Random.Range(-30, 30);
    virtualCitizen.citizenData.currentTargetPosition = new Vector3(randomX, 0, randomZ);
  }
  public void HydrateData(VirtualCitizen virtualCitizen)
  {
    this.virtualCitizen = virtualCitizen;
  }

  private void OnDrawGizmos()
  {
    if (virtualCitizen == null) return;
    if (virtualCitizen.citizenData == null) return;
    Gizmos.color = Color.green;
    Gizmos.DrawCube(ZoneSystem.instance.WorldToGamePosition(virtualCitizen.citizenData.currentTargetPosition), Vector3.one);
  }

  private void OnDestroy()
  {
    // Debug.Log("CITIZEN: destroying in zone: " + GetComponent<DataSyncer>().objectData.zoneID);
  }
}