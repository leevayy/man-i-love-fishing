using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Collection" +
	"/Item")]
public class Item : ScriptableObject
{
    public new string name = "New Item";
	public string nameId = "";
	public Sprite icon = null;
    public int amount = 0;
	public int price = 0;


	// Called when the item is pressed in the Collection
	public virtual void Use()
	{
		// Use the item
		// Something may happen
	}

	// Call this method to remove the item from Collection
	public void RemoveFromCollection()
	{
		Collection.instance.RemoveItem(this);
	}
}
