using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEncounter : MonoBehaviour
{
    [SerializeField] private RandomEncounter.EnemyArray enemyArray;
    [SerializeField] MovementStateMachine playerMSM;
    [SerializeField] GameObject smokeBomb;
    [SerializeField] GameObject[] enemies;
    [SerializeField] Sprite banditImage;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(FightStart());
        }
    }

    
    IEnumerator FightStart()
    {
        playerMSM.canMove = false;
        yield return new WaitForSeconds(0.1f);
        smokeBomb.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        smokeBomb.SetActive(false);
        PlayerInteractUI.Instance.ShowDialoguePanel("Hand over your gold!", "Bandit", banditImage);
        yield return new WaitForSeconds(1.5f);
        PlayerInteractUI.Instance.HideDialoguePanel();
        StartCoroutine(RandomEncounter.Instance.StartFight(enemyArray.enemies));

    }

}
