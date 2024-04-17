using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFightStart : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] private RandomEncounter.EnemyArray enemyArray;
    public bool readyToDestroy = false;


    public void StartFight()
    {
        StartCoroutine(RandomEncounter.Instance.StartFight(enemyArray.enemies));
    }

    private void OnDisable()
    {
        DestroyAll();
    }
    void DestroyAll()
    {
        if (readyToDestroy)
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            Destroy(gameObject);
        }
    }
}
