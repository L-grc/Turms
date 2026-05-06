using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Chest : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }
    public string ChestID { get; private set; }
    public GameObject itemPrefab;
    public Sprite OpenedSprit;


    
    void Start()
    {
        ChestID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    
    void Update()
    {
        
    }
    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        OpenChest();
    }


    private void OpenChest()
    {
        SetOpened(true);

        if (itemPrefab)
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity);
            Debug.Log("droppedItem");
        }
        
    }


    public void SetOpened(bool opened)
    {
        IsOpened = opened;
        if (IsOpened == opened)
        {
            GetComponent<SpriteRenderer>().sprite = OpenedSprit;

        }
    }
}
