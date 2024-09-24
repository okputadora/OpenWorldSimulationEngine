using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SharedPickableData", order = 1)]
public class SharedPickableData : ScriptableObject
{
    public string pickableName;
    public string id;
    public DropTable itemDrops;

    bool canHandPick;
    bool canMachinePick;
    public Sprite icon;
    public string description;
}
