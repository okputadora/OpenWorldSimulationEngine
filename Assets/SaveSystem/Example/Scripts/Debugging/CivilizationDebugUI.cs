using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilizationDebugUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpateUI()
    {
        foreach (CivilizationData civ in CivilziationManager.instance.civilizations)
        {
            Debug.Log("Civ: " + civ);
            foreach (SettlementData settlement in civ.settlements)
            {
                foreach (WorkforceData workforce in settlement.workforces)
                {
                    Debug.Log("Workforce: " + workforce);
                }
            }
        }
    }
}
