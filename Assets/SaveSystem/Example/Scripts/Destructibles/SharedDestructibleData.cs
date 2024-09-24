using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SharedDestructibleData", order = 1)]
public class SharedDestructibleData : ScriptableObject
{
    public DropTable itemDrops;
    public enum DestructibleType
    {
        tree = 1,
        animal = 20,
        stone = 30,

    }
    public int initialHealth = 100;
    // hasPieces? like rock pieces or tree trunk or is the tree trunk going to have its own SharedDestructibleData
}
