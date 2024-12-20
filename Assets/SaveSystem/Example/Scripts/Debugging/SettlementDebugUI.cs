using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettlementDebugUI : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] private GameObject workforceListItemPrefab;
  [SerializeField] private GameObject workforceList;
  [SerializeField] private WorkforceDebugUI workforceDetails;

  public void SetActive(SettlementData settlement)
  {
    Debug.Log("setting active");
    gameObject.SetActive(true);
    UpdateUI(settlement);
  }

  private void UpdateUI(SettlementData settlement)
  {
    Debug.Log("Updating settlement UI" + settlement.settlementName);
    foreach (WorkforceData workforce in settlement.workforces)
    {
      GameObject go = Instantiate(workforceListItemPrefab, workforceList.transform);
      Debug.Log("workforce: " + workforce.sharedOccupationData);
      go.GetComponentInChildren<TextMeshProUGUI>().text = workforce.workforceName;
      go.GetComponent<Button>().onClick.AddListener(() =>
      {
        workforceDetails.SetActive(workforce);
        gameObject.SetActive(false);
      });


    }
  }
}