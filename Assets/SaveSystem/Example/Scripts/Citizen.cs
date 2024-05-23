using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class Citizen : MonoBehaviour, ISaveableComponent<CitizenData>
{
  public CitizenData citizenData = null;
  public VirtualCitizen virtualCitizen;
  public float speed = 10;

  public void Start()
  {
    virtualCitizen = GetComponent<DataSyncer>().objectData as VirtualCitizen;
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
    if (citizenData == null) return;
    Vector3 gamePosition = ZoneSystem.instance.GetGamePositionFromWorldPosition(citizenData.currentTargetPosition);
    if (Vector3.Distance(transform.position, gamePosition) <= 1)
    {
      ChooseRandomPosition();
      return;
    }
    transform.position = Vector3.MoveTowards(transform.position, gamePosition, Time.deltaTime * speed);

  }

  private void ChooseRandomPosition()
  {
    float randomZ = Random.Range(-30, 30);
    float randomX = Random.Range(-30, 30);
    citizenData.currentTargetPosition = new Vector3(randomX, 0, randomZ);
  }
  public void HydrateData(CitizenData data)
  {
    citizenData = data;
  }

  public CitizenData CreateNewData()
  {
    return new CitizenData();
  }

  private void OnDrawGizmos()
  {
    if (citizenData == null) return;
    Gizmos.color = Color.green;
    Gizmos.DrawCube(ZoneSystem.instance.GetGamePositionFromWorldPosition(citizenData.currentTargetPosition), Vector3.one);
  }

  private void OnDestroy()
  {
    // Debug.Log("CITIZEN: destroying in zone: " + GetComponent<DataSyncer>().objectData.zoneID);
  }
}