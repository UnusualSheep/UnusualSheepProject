using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{

    [SerializeField] GameObject fightStage;
    [SerializeField] UnitData[] units;



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            RandomEncounter.Instance.encounterEnabled = true;
            RandomEncounter.Instance.encounterRate = 100;
        }

        if(!fightStage.activeSelf)
        {
            ManageHealth();
        }
    }


    void ManageHealth()
    {
        foreach(UnitData unit in units)
        {
            if(unit.curHp <= 0)
            {
                unit.curHp = 1;
            }
        }
    }
}
