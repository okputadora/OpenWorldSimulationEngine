using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
public class CitizenListItemDebugUI : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] private TextMeshProUGUI citizenName;
  [SerializeField] private GameObject inventory;
  [SerializeField] private GameObject itemUIPrefab;
  private VirtualCitizen citizen;
  public void Initialize(VirtualCitizen citizen)
  {
    this.citizen = citizen;
    gameObject.SetActive(true);
    UpdateUI(citizen);
    citizen.citizenData.inventory.RegisterChangeListener(UpdateInventory); // need to unregister this listener
  }

  private void UpdateInventory(InventoryData inventoryData)
  {
    // clear ui first
    // make this a static method somewhere 
    foreach (Transform child in inventory.transform)
    {
      Destroy(child.gameObject);
    }
    
    for (int i = 0; i < inventoryData.items.Count; i++)
    {
      if ( inventoryData.items[i].sharedData != null) {
          GameObject itemUI = Instantiate(itemUIPrefab, inventory.transform);
          itemUI.GetComponent<ItemUI>().Initialize(inventoryData.items[i], null, i, null);
      }
    }
  }

  private void UpdateUI(VirtualCitizen citizen)
  {
    // clear ui first
    // make this a static method somewhere 
    // set citizen name
    citizenName.text = citizen.citizenData.inventory.items.Count.ToString();
    UpdateInventory(citizen.citizenData.inventory);
  }

  
  private void OnDestroy()
  {
    citizen.citizenData.inventory.UnregisterChangeListener(UpdateInventory);
    // unregister listeners
  }
}