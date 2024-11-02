using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private bool overwriteCurrentFile = false;
    public static SaveSystem instance;
    [SerializeField] private GameObject player;
    [SerializeField] private List<RootSaveable> rootSaveables = new List<RootSaveable>();
    public string virtualGameObjectDirectoryPath;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Load();
    }

    private void Save()
    {
        SaveData dataToSave = new SaveData();
        ObjectSpawner.instance.Save(dataToSave);
        ZoneSystem.instance.Save(dataToSave);
        dataToSave.Write(ZoneSystem.instance.GameToWorldPosition(player.transform.position));
        dataToSave.Write(player.transform.rotation);
        foreach (RootSaveable saveable in rootSaveables)
        {
            saveable.Save(dataToSave);
        }
        dataToSave.WriteToDisk($"/{fileName}");
    }

    private void Load()
    {
        SaveData dataToLoad = SaveData.ReadFromDisk($"/{fileName}");
        if (dataToLoad == null || overwriteCurrentFile)
        {
            Debug.LogWarning("No Save File Found");
            foreach (RootSaveable saveable in rootSaveables)
            {
                saveable.Initialize();
            }
            return;
        }
        ObjectSpawner.instance.Load(dataToLoad);
        ZoneSystem.instance.Load(dataToLoad);
        player.transform.position = ZoneSystem.instance.WorldToGamePosition(dataToLoad.ReadVector3());
        player.transform.rotation = dataToLoad.ReadQuaternion();
        foreach (RootSaveable saveable in rootSaveables)
        {
            saveable.Load(dataToLoad);
        }
    }

    public void OnApplicationQuit()
    {
        Debug.Log("Saving");
        Save();
    }
}
