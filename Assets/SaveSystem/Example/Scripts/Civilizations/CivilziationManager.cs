using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilziationManager : RootSaveable
{
    private List<CivilizationData> civilizations = new List<CivilizationData>();


    public override void Load(SaveData dataToLoad)
    {
        int civilizationCount = dataToLoad.ReadInt();
        for (int i = 0; i < civilizationCount; i++)
        {
            CivilizationData civ = new CivilizationData();
            civ.Load(dataToLoad);
        }
    }

    public override void Save(SaveData dataToSave)
    {
        dataToSave.Write(civilizations.Count);
        foreach (CivilizationData civ in civilizations)
        {
            civ.Save(dataToSave);
        }
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void Simulate(float deltaTime)
    {

    }


}
