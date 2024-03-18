using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapTest : MonoBehaviour
{
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject rifle;
    [SerializeField] GameObject backRifle;


    public void PistolOut()
    {
        pistol.SetActive(true);
    }
    public void PistolIn()
    {
        pistol.SetActive(false);
    }

    public void RifleOut()
    {
        backRifle.SetActive(false);
        rifle.SetActive(true);
    }
    public void RifleIn()
    {
        backRifle.SetActive(true);
        rifle.SetActive(false);
    }
}
