using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemUI : MonoBehaviour, IPointerDownHandler
{
  private int hotkey;
  [SerializeField] private Image background;
  [SerializeField] private TextMeshProUGUI hotKeytext;
  [HideInInspector] public ItemData itemData;
  [SerializeField] private Image icon;
  [SerializeField] private TextMeshProUGUI amount;
  [SerializeField] private RectTransform durability;
  [SerializeField] private Color equipColor;
  private bool isPreview = false;
  public delegate void HandleClick(PointerEventData data, ItemUI slot);
  private HandleClick clickHandler;

  public int index { get; private set; }

  public void Initialize(ItemData item, int? hotKey, int index, HandleClick clickHandler)
  {
    itemData = item;
    this.index = index;
    this.clickHandler = clickHandler;
    if (hotKey != null)
    {
      hotkey = (int)hotKey;
      hotKeytext.text = hotkey.ToString();
    }
    else
    {
      hotKeytext.text = "";
    }
    RenderItem(item);

  }

  public void InitializeDragPreviw(ItemData item)
  {
    hotKeytext.text = "";
    background.raycastTarget = false;
    isPreview = true;
    RenderItemStats(item);
  }
  public void UpdateItem(ItemData item)
  {
    itemData = item;
    RenderItem(item);
  }

  private void RenderItem(ItemData item)
  {
    if (item.isEmpty)
    {
      background.color = Color.white;
      icon.gameObject.SetActive(false);
      amount.transform.parent.gameObject.SetActive(false);
      durability.gameObject.SetActive(false);
      return;
    }
    RenderItemStats(item);
  }

  private void RenderItemStats(ItemData item)
  {
    icon.sprite = item.sharedData.icon;
    icon.gameObject.SetActive(true);
    background.color = item.isEquipped ? equipColor : Color.white;
    if (item.sharedData.maxStackSize > 1)
    {
      // amount.gameObject.SetActive(true);
      amount.transform.parent.gameObject.SetActive(true);
      amount.text = $"{item.amount}/{item.sharedData.maxStackSize}";
      amount.transform.parent.gameObject.SetActive(true);
      durability.gameObject.SetActive(false);
    }
    else
    {
      // amount.gameObject.SetActive(false);
      amount.transform.parent.gameObject.SetActive(false);
      durability.gameObject.SetActive(true);
      // durability.GetChild(0). // set fill with based on current durability...health? whats the correct term for this?
    }
  }
  public void OnPointerDown(PointerEventData data)
  {
    clickHandler(data, this);
  }


}
