using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffEncounters : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RandomEncounter.Instance.encounterEnabled = false;
        }
    }
}
