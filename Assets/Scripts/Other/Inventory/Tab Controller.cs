using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{

    public Image[] tabImages;
    public GameObject[] pages;



  
    void Start()
    {
        ActivateTab(0);
    }



    public void ActivateTab(int tabNo)
    {
       
        for (int i = 0; i < pages.Length; i++)
        {
            
            pages[i].SetActive(true);
      
            pages[tabNo].transform.SetAsLastSibling();

        }

        pages[tabNo].SetActive(true);

       
    }

   
}
