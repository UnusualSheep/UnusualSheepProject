using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public GameObject[] cameras;
    public GameObject allyCamera;
    public GameObject enemyCamera;
    public float cameraSwitchSpeed;
    public int cameraIndex;
    [SerializeField] bool switcher = true;


    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
        StartCoroutine(cameraSwitch());
    }

    IEnumerator cameraSwitch()
    {
        while (switcher)
        {
            yield return new WaitForSeconds(cameraSwitchSpeed);
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i].activeSelf)
                {
                    cameraIndex = i + 1;
                    if (cameraIndex >= cameras.Length)
                    {
                        cameraIndex = 0;
                    }
                }
            }
            if (GlobalTimeManager.Instance.globalTimeScale > 0)
            {
                foreach (var cam in cameras)
                {
                    cam.SetActive(false);
                }
                cameras[cameraIndex].SetActive(true);
            }
        }
    }
}
