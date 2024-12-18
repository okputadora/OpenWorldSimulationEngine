using UnityEngine;

public class UIUtils {
    public static void RemoveChildren(Transform t)
    {
        foreach (Transform child in t)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}