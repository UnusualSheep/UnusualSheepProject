using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterControl : MonoBehaviour
{   
    public Transform selectedGrpahicPosition;
    public Transform meleeRange;
    public Transform rangedPosition;
    public GameObject meleeCamera;
    public CharacterTeam characterTeam;
    [Space(10)]
    public float moveSpeed = 5;
    [Space(10)]
    public AbiltySO basicAttack;
    public AbiltySO burstAttack;
    public AbiltySO selectedAttack;
    public AbiltySO[] abilities;
    public ItemSO selectedItem;

    public CharacterControl _target;
    [HideInInspector]
    public Vector3 startPos;
    [SerializeField] Transform hitSpot;
    private void Awake()
    {
        startPos = transform.position;
    }
    
    public void SpawnParticleOnTarget() //This needs reworkding
    {
        if (selectedAttack.attackParticle != null)
        {
            GameObject tempParticle = Instantiate(selectedAttack.attackParticle);
            tempParticle.transform.position = _target.hitSpot.position;

            ParticleSystem particleComponent = tempParticle.GetComponent<ParticleSystem>();
            _target.GetComponent<CombatStateMachine>().animator.SetTrigger("hit");

            Destroy(tempParticle, particleComponent.main.duration);
        }
    }
    
    public void SwitchCameras()
    {
        if(!meleeCamera.activeInHierarchy)
        {
            foreach(GameObject camera in CameraManager.Instance.cameras)
            {
                camera.SetActive(false);
            }
            meleeCamera.SetActive(true);
        }
        else if (meleeCamera.activeInHierarchy)
        {
            int index = CameraManager.Instance.cameraIndex;
            GameObject camera = CameraManager.Instance.cameras[index];
            foreach (GameObject cameras in CameraManager.Instance.cameras)
            {
                cameras.SetActive(false);
            }
            camera.SetActive(true);
            meleeCamera.SetActive(false);
        }
    }

    public void PerformAttack(AbiltySO ability)
    {
        Animator animator = GetComponent<Animator>();
        UIManager.Instance.SetAbilityText(ability.abilityName, characterTeam);
        if (characterTeam == CharacterTeam.Friendly)
        {
            UIManager.Instance.DisplayAbilityWindow();
        }
        else if (characterTeam == CharacterTeam.Enemy)
        {
            UIManager.Instance.DisplayEnemyAbilityWindow();
        }

        if (ability == basicAttack)
        {
            animator.SetFloat("skill_Index", 0);
            animator.SetTrigger("InAttackPosition");
        }
        else if (ability == burstAttack)
        {
            animator.SetFloat("skill_Index", 1);
            animator.SetTrigger("InAttackPosition");
        }
        else
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                if (ability == abilities[i])
                {
                    animator.SetFloat("skill_Index", (float)(i + 1) / 10);
                    animator.SetTrigger("InAttackPosition");
                }
            }
        }
    }







    /// <summary>
    /// Animation section
    /// </summary>
    /// <returns></returns>
    /// 
    public bool animationStopped { get; private set; }
    public void SetAnimationStopped()
    { animationStopped = true; }
    public void SetAnimationStoppedFalse()
    { animationStopped = false; }

}

public enum CharacterTeam
{
    Friendly,
    Enemy
}
