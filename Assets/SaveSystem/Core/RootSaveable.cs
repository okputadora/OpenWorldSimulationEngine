using UnityEngine;
public abstract class RootSaveable : MonoBehaviour
{

  public abstract void Save(SaveData dataToSave);

  public abstract void Load(SaveData dataToLoad);
}
