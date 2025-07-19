using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngletChoice : MonoBehaviour
{
    public GameObject onglet1;
    public GameObject onglet2;
    public GameObject onglet3;

    public void SwapOnglet1()
    {
        onglet1.SetActive(true);
        onglet2.SetActive(false);
        onglet3.SetActive(false);
    }
    
    public void SwapOnglet2()
    {
        onglet1.SetActive(false);
        onglet2.SetActive(true);
        onglet3.SetActive(false);
    }
    
    public void SwapOnglet3()
    {
        onglet1.SetActive(false);
        onglet2.SetActive(false);
        onglet3.SetActive(true);
    }
}
