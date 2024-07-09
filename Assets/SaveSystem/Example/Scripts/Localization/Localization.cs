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
        Debug.Log("new localization");
        languages = LoadLanguages();
        // get player language from player prefs
        string language = "english";
        LoadTranslations(language);

        // string language =
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
        // return languagesJson;
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
