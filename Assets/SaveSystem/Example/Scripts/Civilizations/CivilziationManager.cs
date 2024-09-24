using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilziationManager : RootSaveable
{
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

    private void Start()
    {
        // if no load file exists
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
        for (int i = 0; i < 32; i++)
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
        return new CivilizationStrategy();
    }


    private void CreateAICivilization()
    {
        CivilizationStrategy strategy = PickRandomStrategy();
        // get random point, on land in world bounds
        Vector3 worldLocation = new Vector3(Random.Range(-500, 500), 0, Random.Range(-500, 500));
        int initialCitizenCount = Random.Range(6, 12);
        CivilizationAIData civilizationAIData = new CivilizationAIData(strategy, worldLocation, initialCitizenCount);
        // spawn location?
        // might want to add some randomization to it 
        civilizations.Add(civilizationAIData);

    }



    private void Simulate(float deltaTime)
    {
        foreach (CivilizationData civ in civilizations)
        {
            civ.Simulate(deltaTime);
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