using UnityEngine;
using TMPro;
public class WorkforceDebugUI : MonoBehaviour
{
  public WorkforceData workforce;
  [SerializeField] private TextMeshProUGUI workforceNameText;
  [SerializeField] private TextMeshProUGUI itemsText;
  [SerializeField] private TextMeshProUGUI zonesText;
  [SerializeField] private TextMeshProUGUI citizenCountText;
  [SerializeField] private GameObject foodGatherDetails;
  [SerializeField] private TextMeshProUGUI animalTargetsText;
  [SerializeField] private TextMeshProUGUI pickablesText;
  [SerializeField] private TextMeshProUGUI storageCountText;

  public void SetActive(WorkforceData workforce)
  {
    this.workforce = workforce;
    gameObject.SetActive(true);
    UpdateUI();
  }
  public void UpdateUI()
  {
    Debug.Log("Workforce: " + workforce.workforceName);
    workforceNameText.text = workforce.workforceName;
    string items = "";
    foreach (SharedItemData item in workforce.itemTargets)
    {
      items += item.itemName + ", ";
    }
    itemsText.text = items;
    if (workforce.GetType() == typeof(FoodGatherWorkforceData))
    {
      UpdateFoodGatherWorkforceUI();
    }
    string zones = "";
    foreach (Vector2Int zone in workforce.zones)
    {
      zones += zone + ", ";
    }
    zonesText.text = zones;
    citizenCountText.text = workforce.citizens.Count + "/" + workforce.maxWorkers;
  }

  private void UpdateFoodGatherWorkforceUI()
  {
    FoodGatherWorkforceData foodGatherWorkforce = (FoodGatherWorkforceData)workforce;
    foodGatherDetails.SetActive(true);
    if (foodGatherWorkforce.isHuntingWorkforce)
    {
      Debug.Log("Hunting Workforce");
      Debug.Log(foodGatherWorkforce.animalTargets);
    }
    else
    {
      Debug.Log("Gathering Workforce");
      Debug.Log(foodGatherWorkforce.pickables.Count);
      string pickableName = foodGatherWorkforce.pickables[0].pickableData.sharedPickableData.pickableName;
      pickablesText.text = pickableName + ": " + foodGatherWorkforce.pickables.Count.ToString();
    }
  }
}