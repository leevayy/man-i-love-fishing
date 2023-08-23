using System;
using UnityEngine;
using UnityEngine.UI;


public class InfoMenu : MonoBehaviour
{
    public Text amountText;
    public Text sellCostText;
    public Text itemNameText;
    public Image itemIcon;
    public GameObject infoMenu;

    public Item item;

    Collection collection;

    void Start()
    {
        collection = Collection.instance;
        infoMenu.SetActive(false);
    }
        
    public void OpenInfoMenu()
    {
        UpdateItem();
        infoMenu.SetActive(true);
    }
    public void CloseButton()
    {
        infoMenu.SetActive(false);
    }
    private void UpdateItem() 
    {
        amountText.text = Convert.ToString(item.amount);
        sellCostText.text = Convert.ToString(item.price);
        itemNameText.text = item.name;
        itemIcon.sprite = item.icon;
    }
}
