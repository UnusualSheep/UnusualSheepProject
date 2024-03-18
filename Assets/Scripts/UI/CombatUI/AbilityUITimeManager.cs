using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUITimeManager : MonoBehaviour
{

    private void OnEnable()
    {
        GlobalTimeManager.Instance.globalTimeScale = 0f;
    }
    private void Update()
    {
        GlobalTimeManager.Instance.globalTimeScale = 0f;
    }
    private void OnDisable()
    {
        GlobalTimeManager.Instance.globalTimeScale = 1f;
    }
}
