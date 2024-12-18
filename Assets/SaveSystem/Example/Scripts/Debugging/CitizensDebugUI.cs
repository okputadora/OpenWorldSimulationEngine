using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
public class CitizensDebugUI : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] private GameObject citizenListItemPrefab;
  [SerializeField] private GameObject citizenList;
  [SerializeField] private GameObject itemUIPrefab;
  [SerializeField] private GameObject inventoryDetails;

  public void SetActive(List<VirtualCitizen> citizens)
  {
    Debug.Log("setting active");
    gameObject.SetActive(true);
    UpdateUI(citizens);
  }

  private void UpdateUI(List<VirtualCitizen> citizens)
  {
    // clear ui first
    // make this a static method somewhere 
    UIUtils.RemoveChildren(citizenList.transform);
    foreach (VirtualCitizen citizen in citizens)
    {
      GameObject go = Instantiate(citizenListItemPrefab, citizenList.transform);
      go.GetComponent<CitizenListItemDebugUI>().Initialize(citizen);
    }
  }

}