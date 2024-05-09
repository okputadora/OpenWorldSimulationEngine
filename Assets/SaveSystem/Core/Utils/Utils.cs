using UnityEngine;
public static class Utils
{
  public static string GetPrefabName(GameObject gameObject)
  {
    string name = gameObject.name;
    // remove (Clone) text
    char[] anyOf = new char[2] { '(', ' ' };
    int startIndex = name.IndexOfAny(anyOf);
    return startIndex != -1 ? name.Remove(startIndex) : name;
  }

}
