using UnityEngine;
using System.Collections.Generic;
interface ISaveableComponent<T>
{

  public void Save(SaveData dataToSave);

  public void Load(SaveData dataToLoad); // dont think we need this

  public void HydrateData(T data);

  public T CreateNewData();

}