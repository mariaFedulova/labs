using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObjectPage : MonoBehaviour, IUI
{

    [SerializeField] private GameObject page;

    public void OpenPage()
    {
        page.SetActive(true);
    }

    public void ClosePage() 
    {
        page.SetActive(false);   
    }

}
