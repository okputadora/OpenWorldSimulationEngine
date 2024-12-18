using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
  [SerializeField] private GameObject storageUIPrefab;
  [SerializeField] private GameObject storageList;
  [SerializeField] private Button openCitizensMenu;
  [SerializeField] private CitizensDebugUI citizensMenu;

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
    openCitizensMenu.onClick.AddListener(() =>
    {
      Debug.Log("Opening citizens menu");
      gameObject.SetActive(false);
      citizensMenu.SetActive(workforce.citizens);
      //citizensDebugUI.SetActive(workforce.citizens);
    });
  }
  private void UpdateFoodGatherWorkforceUI()
  {
    FoodGatherWorkforceData foodGatherWorkforce = (FoodGatherWorkforceData)workforce;
    foodGatherDetails.SetActive(true);
    if (foodGatherWorkforce.isHuntingWorkforce)
    {
    }
    else
    {
      string pickableName = foodGatherWorkforce.pickables[0].pickableData.sharedPickableData.pickableName;
      pickablesText.text = pickableName + ": " + foodGatherWorkforce.pickables.Count.ToString();
    }
    UIUtils.RemoveChildren(storageList.transform);
    Debug.Log("Rendering food containers");
    Debug.Log(foodGatherWorkforce.foodContainers.Count);
    foodGatherWorkforce.foodContainers.ForEach(container =>
    {
      GameObject storageUI = Instantiate(storageUIPrefab, storageList.transform);
      storageUI.GetComponent<StorageUI>().SetActive(container);
    });
  }
}