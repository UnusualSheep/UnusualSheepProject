using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextFloat : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    private void OnEnable()
    {
        StartCoroutine(playAnimation());
    }

    private void OnDisable()
    {
        StopCoroutine(playAnimation());
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    
    
    IEnumerator playAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
