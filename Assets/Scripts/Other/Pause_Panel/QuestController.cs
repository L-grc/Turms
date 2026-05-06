using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestController : MonoBehaviour
{
  
    public static QuestController Instance { get; private set; }
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

}
