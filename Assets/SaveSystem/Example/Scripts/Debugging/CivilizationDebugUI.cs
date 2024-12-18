using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CivilizationDebugUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject civilizationListItemPrefab;
    [SerializeField] private GameObject civilizationList;
    [SerializeField] private CivilizationDetailsDebugUI civilizationDetails;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetActive()
    {
        gameObject.SetActive(true);
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIUtils.RemoveChildren(civilizationList.transform);
        foreach (CivilizationData civ in CivilziationManager.instance.civilizations)
        {
            GameObject go = Instantiate(civilizationListItemPrefab, civilizationList.transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = civ.civilizationName;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Civilization Clicked: " + civ.civilizationName);
                civilizationDetails.SetActive(civ);
                gameObject.SetActive(false);
            });

        }
    }
}
