using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class StorageUI : MonoBehaviour
{
  [SerializeField] private GameObject itemsList;
  [SerializeField] private GameObject itemUIPrefab;
  private VirtualStorage storage;
  public void SetActive(VirtualStorage storage)
  {
    Debug.Log("Virtual storage");
    gameObject.SetActive(true);
    this.storage = storage;
    UpdateUI();
  }

  private void UpdateUI()
  {
    UIUtils.RemoveChildren(itemsList.transform);
    Debug.Log("storagte name: " + storage.inventoryData.displayName);
    foreach (ItemData item in storage.inventoryData.items)
    {
        if (item.isEmpty) continue;
        GameObject go = Instantiate(itemUIPrefab, itemsList.transform);
        go.GetComponent<ItemUI>().Initialize(item, null, 0, null);
    }
  }

}