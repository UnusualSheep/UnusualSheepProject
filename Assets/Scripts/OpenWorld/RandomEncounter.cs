using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    public static RandomEncounter Instance;
    [SerializeField] GameObject fightMap;
    [SerializeField] GameObject openWorldMap;
    [SerializeField] Transform[] enemySpawnpoints;
    [SerializeField] MovementStateMachine playerMSM;
    [SerializeField] CharacterController player;
    [SerializeField] GameObject transitionScreen;


    [System.Serializable]
    public class EnemyArray
    {
        public string groupName;
        public GameObject[] enemies;
    }

    [SerializeField]
    private EnemyArray[] enemiesArray;


    public int encounterRate;
    public bool encounterEnabled;


    private void Awake()
    {
        Instance = this;
        StartCoroutine(EncounterCheck());
    }

    private void Update()
    {
    }

    IEnumerator StartFight(GameObject[] enemiesToSpawn)
    {
        transitionScreen.SetActive(true);
        playerMSM.canMove = false;
        yield return new WaitForSeconds(1);
        fightMap.SetActive(true);
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            GameObject tempEnemy = Instantiate(enemiesToSpawn[i].gameObject, enemySpawnpoints[i]);
        }

        FightManager.Instance.PopulateTeams();
        FightManager.Instance.SpawnUI();
        openWorldMap.SetActive(false);
        transitionScreen.SetActive(false);
    }

    IEnumerator EncounterCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (encounterEnabled)
            {
                if (player.velocity.x > 0 || player.velocity.z > 0)
                {
                    //check if moving
                    if (Random.Range(1, 101) < encounterRate)
                    {
                        int enemyGroupIndex = Random.Range(0, enemiesArray.Length);
                        StartCoroutine(StartFight(enemiesArray[enemyGroupIndex].enemies));
                        encounterEnabled = false;
                    }
                }
            }
        }
    } 

    public IEnumerator EndFight()
    {
        transitionScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        fightMap.SetActive(false);
        transitionScreen.SetActive(false);
        openWorldMap.SetActive(true);
        playerMSM.canMove = true;
    }

}
