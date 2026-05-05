using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Healing : MonoBehaviour
{
    [SerializeField] 
    private int healthRecovered = 2;

    public static Action<float> OnFruitCollected;



    public void Collect()
    {
        OnFruitCollected.Invoke(healthRecovered);
    }






}
