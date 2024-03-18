using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDraw : MonoBehaviour
{
    [SerializeField] GameObject mainWeaponToDraw;
    [SerializeField] GameObject mainWeaponToActivate;
    [SerializeField] GameObject secondaryWeaponToDraw;
    [SerializeField] GameObject secondaryWeaponToActivate;

    public void DrawMain()
    {
        mainWeaponToActivate.SetActive(true);
        if (mainWeaponToDraw != null)
        {
            mainWeaponToDraw.SetActive(false);
        }
    }

    public void SheathMain()
    {
        mainWeaponToActivate.SetActive(false);
        mainWeaponToDraw.SetActive(true);
    }

    public void DrawSecondary()
    {
        secondaryWeaponToActivate.SetActive(true);
        secondaryWeaponToDraw.SetActive(false);
    }

    public void SheathSecondary()
    {
        secondaryWeaponToActivate.SetActive(false);
        secondaryWeaponToDraw.SetActive(true);
    }
}
