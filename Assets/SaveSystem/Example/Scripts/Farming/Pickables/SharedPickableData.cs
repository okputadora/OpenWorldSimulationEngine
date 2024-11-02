using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SharedPickableData", order = 1)]
public class SharedPickableData : ScriptableObject
{
    public string pickableName;
    public string id;
    [SerializeField]
    private DropTable itemDrops;

    bool canHandPick = true;
    bool canMachinePick = false;
    public Sprite icon;
    public string description;
    public bool isRenawable;
    public int timeToRenew;
    public int aiTimeToPick;

    public List<SharedItemData> GetItemsFromDrop()
    {
        return itemDrops.dropData.Select(dropData => dropData.itemData).ToList();
    }

    public Dictionary<SharedItemData, int> CreateDrop()
    {
        return itemDrops.CreateDrop();
    }
}
