using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Collection : MonoBehaviour
{
    public static Collection instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int balance = 0;
    public int space =  10;
    public Text balanceText;
    public bool isOpen = false;

    public List<Item> items = new List<Item>();
    [SerializeField] private List<Item> itemsCollection;

    private void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Debug.LogWarning("more than one instance of Collection");
            return;
        }

        instance = this;
        #endregion
    }

    private void Start()
    {
        foreach (var item in itemsCollection)
        {
            if (PlayerPrefs.GetInt(item.nameId) == 1)
            {
                AddItem(item);
                item.amount = PlayerPrefs.GetInt(item.nameId + "_amount");
            }
        }
    }

    public void AddItem(Item item)
    {
        if(items.Count >= space)
        {
            return;
        }
        if (items.Contains(item) == false)
        {
            items.Add(item);
            PlayerPrefs.SetInt(item.nameId, 1);
            UpdateBalance(item);
        }
        else
        {
            item.amount += 1;
            PlayerPrefs.SetInt(item.nameId + "_amount", item.amount);
            UpdateBalance(item);
        }
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
    public void RemoveItem(Item item)
    {
        if (items.Contains(item) == false)
        {
            return;
        }
        item.amount = 1;
        items.Remove(item);
        PlayerPrefs.SetInt(item.nameId, 0);
        onItemChangedCallBack.Invoke();
        if (onItemChangedCallBack != null) 
        {            
            onItemChangedCallBack.Invoke(); 
        }
    }
    public void CleanItems()
    {
        foreach (var item in itemsCollection)
        {
            PlayerPrefs.SetInt(item.nameId, 0);
            PlayerPrefs.SetInt(item.nameId + "_amount", 1);
            item.amount = 1;
            items.Remove(item);
        }
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
    private void UpdateBalance(Item item)
    {
        balance += item.price * item.amount;
        PlayerPrefs.SetInt("balance", balance);
    }
}
