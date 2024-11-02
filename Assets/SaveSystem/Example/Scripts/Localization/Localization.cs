using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;
public class Localization
{
    private List<string> languages;
    private string currentLanguage;
    private Dictionary<string, string> translations = new Dictionary<string, string>();
    private static Localization instance;
    public static Localization Instance
    {
        get
        {
            if (instance == null)
            {
                Initialize();
            }
            return instance;
        }
        private set { }
    }

    private static void Initialize()
    {
        instance = new Localization();
    }

    private Localization()
    {
        languages = LoadLanguages();
        string language = PlayerPrefs.GetString("playerLanguage");
        if (!languages.Contains(language))
        {
            Debug.LogWarning("Could not find player language");
            language = "english";
        }
        LoadTranslations(language);
    }


    public string Translate(string key)
    {
        return translations.TryGetValue(key, out string translation) ? translation : "[" + key + "]";
    }

    private List<string> LoadLanguages()
    {
        TextAsset languagesText = Resources.Load("languages", typeof(TextAsset)) as TextAsset;
        Languages languagesJson = JsonConvert.DeserializeObject<Languages>(languagesText.text);
        return languagesJson.languages;
    }

    private void LoadTranslations(string language)
    {
        currentLanguage = language;
        TextAsset languagesText = Resources.Load(currentLanguage, typeof(TextAsset)) as TextAsset;
        translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(languagesText.text);
    }
    public class Languages
    {
        public List<string> languages;
    }

}
