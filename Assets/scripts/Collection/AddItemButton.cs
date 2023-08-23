using UnityEngine;

public class AddItemButton : MonoBehaviour
{
    public Item item;

    public void AddItem()
    { 
        Collection.instance.AddItem(item);        
    }
}
