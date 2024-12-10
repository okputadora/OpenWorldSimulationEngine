using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CivilziationManager : RootSimulatable
{
    public static CivilziationManager instance;
    [SerializeField] private int civilizationCount;
    public List<CivilizationData> civilizations = new List<CivilizationData>();
    [SerializeField] private List<CivilizationStrategy> strategies = new List<CivilizationStrategy>();
    [SerializeField] private List<SharedBuildingData> buildingTemplates = new List<SharedBuildingData>();
    [SerializeField] private int startingCivilizationRange = 100;
    [SerializeField] private int startingCitizenCount = 10;
    [Header("Gizmo Settings")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private bool showSettlementGizmos = true;
    [SerializeField] private bool showCitizenGizmos = true;

    public void Awake()
    {
        instance = this;
    }
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
        // Simulate(Time.deltaTime);
    }

    private void CreateInitialCivilizations()
    {
        // civilizationSettings for getting number of civilizations
        for (int i = 0; i < civilizationCount; i++)
        {
            CreateAICivilization(i + 1);
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


    private void CreateAICivilization(int index)
    {
        CivilizationStrategy strategy = PickRandomStrategy();
        int startRange = startingCivilizationRange / 2;
        Vector3 worldLocation = new Vector3(Random.Range(-startRange, startRange), 0, Random.Range(-startRange, startRange));
        CivilizationAIData civilizationAIData = new CivilizationAIData(strategy, worldLocation, startingCitizenCount, "Test AI Civilization");
        civilizationAIData.civilizationName = "AI Civilization" + index.ToString();
        civilizations.Add(civilizationAIData);

    }



    public override void Simulate(float deltaTime)
    {
        foreach (CivilizationData civ in civilizations)
        {
            civ.Simulate(deltaTime);
        }
    }

    public SharedBuildingData GetHouse()
    {
        foreach (SharedBuildingData building in buildingTemplates)
        {
            if (building.buildingType == SharedBuildingData.BuildingType.House)
            {
                return building;
            }
        }
        return null;
    }


    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        foreach (CivilizationData civ in civilizations)
        {
            if (showSettlementGizmos)
            {
                foreach (SettlementData settlement in civ.settlements)
                {
                    settlement.DrawSettlement(showCitizenGizmos);
                }
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