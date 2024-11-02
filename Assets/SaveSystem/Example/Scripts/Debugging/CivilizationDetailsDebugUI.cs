using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CivilizationDetailsDebugUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject settlementListItemPrefab;
    [SerializeField] private GameObject settlmentList;
    [SerializeField] private SettlementDebugUI settlmentDetails;
    public void SetActive(CivilizationData civ)
    {
        gameObject.SetActive(true);
        UpdateUI(civ);
    }

    private void UpdateUI(CivilizationData civ)
    {
        foreach (SettlementData settlement in civ.settlements)
        {
            GameObject go = Instantiate(settlementListItemPrefab, settlmentList.transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = settlement.settlementName;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                settlmentDetails.SetActive(settlement);
                gameObject.SetActive(false);
            });

        }
    }
}
