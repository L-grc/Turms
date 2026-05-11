using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Item : MonoBehaviour
{

    public int ID;

    public string Name;
    public int quantity = 1;

    private TMP_Text quantityText;


    private void Awake()
    {
        quantityText = GetComponentInChildren<TMP_Text>();
        UpdateQuantityDislay();

    }

    public void AddToStack(int amount = 1)
    {
        quantity += amount;
        UpdateQuantityDislay();
    }
    public int removeFromStack(int amount = 1)
    {
        int removed = Mathf.Min(amount, quantity);
        quantity -= removed;
        UpdateQuantityDislay();
        return removed;
    }

    public GameObject CloneItem(int newQuantity)
    {
        GameObject clone = Instantiate(gameObject);
        Item cloneItem = clone.GetComponent<Item>();
        cloneItem.quantity = newQuantity;
        cloneItem.UpdateQuantityDislay();
        return clone;
    }

    public void UpdateQuantityDislay()
    {
        quantityText.text = quantity > 1 ? quantity.ToString() : "";
        

    }


}
