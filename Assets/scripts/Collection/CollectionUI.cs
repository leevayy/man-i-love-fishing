using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{
	[SerializeField] private Item defaultItem;

	public Transform itemsPanel;

    Collection collection;

    private void Start()
    {
        collection = Collection.instance;
		collection.onItemChangedCallBack += UpdateUI;
		UpdateUI();
	}
		
	public void UpdateUI()
	{
		ItemSlot[] slots = GetComponentsInChildren<ItemSlot>();

		for (int i = 0; i < slots.Length; i++)
		{
			if (i < collection.items.Count)
			{
				slots[i].AddItem(collection.items[i]);
			}
			else
			{
				slots[i].ClearSlot();
				slots[i].AddItem(defaultItem);
			}
		}

		collection.balanceText.text = Convert.ToString(collection.balance);
	}
}
