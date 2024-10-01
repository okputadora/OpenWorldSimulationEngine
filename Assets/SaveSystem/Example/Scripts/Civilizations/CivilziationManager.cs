using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CivilziationManager : RootSaveable
{
    [SerializeField] private int civilizationCount;
    private List<CivilizationData> civilizations = new List<CivilizationData>();
    [SerializeField] private List<CivilizationStrategy> strategies = new List<CivilizationStrategy>();


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

    public override void Initialize()
    {
        CreateInitialCivilizations();
        CreatePlayerCivilization();
    }

    private void Update()
    {
        // simulation loop
        Simulate(Time.deltaTime);
    }

    private void CreateInitialCivilizations()
    {
        // civilizationSettings for getting number of civilizations
        for (int i = 0; i < civilizationCount; i++)
        {
            CreateAICivilization();
        }
    }

    private void CreatePlayerCivilization()
    {

    }

    private CivilizationStrategy PickRandomStrategy()
    {
        // ensure a random but even distribution of strategies (based on strategy frequency number)
        return CivilizationStrategy.CreateInstance<CivilizationStrategy>();
    }


    private void CreateAICivilization()
    {
        CivilizationStrategy strategy = PickRandomStrategy();
        Vector3 worldLocation = new Vector3(Random.Range(-500, 500), 0, Random.Range(-500, 500));
        int initialCitizenCount = Random.Range(6, 12);
        CivilizationAIData civilizationAIData = new CivilizationAIData(strategy, worldLocation, initialCitizenCount);
        civilizations.Add(civilizationAIData);

    }



    private void Simulate(float deltaTime)
    {
        foreach (CivilizationData civ in civilizations)
        {
            civ.Simulate(deltaTime);
        }
    }


    private void OnDrawGizmos()
    {
        foreach (CivilizationData civ in civilizations)
        {
            foreach (SettlementData settlement in civ.settlements)
            {
                Vector3 gamePosition = ZoneSystem.instance.WorldToGamePosition(settlement.worldPosition);
                Gizmos.DrawCube(gamePosition, Vector3.one * 5);
                Vector2Int zone = ZoneSystem.instance.GetZoneFromPosition(settlement.worldPosition);
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.yellow;
                Handles.Label(gamePosition, zone.ToString(), style);

                foreach (VirtualCitizen citizen in settlement.citizens)
                {
                    Gizmos.DrawSphere(ZoneSystem.instance.WorldToGamePosition(citizen.worldPosition), 1);
                }
                return;
            }
        }
    }


}

public class StartingConditions
{
    public List<IDestructible> mineRocks;
    public List<IDestructible> trees;
    public List<IDestructible> animals;
    public List<ItemData> resources;
    public int averageFertility; // maybe
    public int shortestDistanceToCoast;
    public int biome; // replace with Biome class


}