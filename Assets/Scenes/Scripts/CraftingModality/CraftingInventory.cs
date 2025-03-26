using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CraftingInventory : MonoBehaviour
{
    private List<string> inventoryItems = new List<string>();
    public List<Image> uiSlots;
    public Sprite defaultSprite; //empty slot default sprite
    
    private void Start()
    {
        foreach (Image slot in uiSlots) 
        {
            slot.sprite = defaultSprite;
            slot.enabled = false;
        }
    }
    public void AddItemToInventory(string itemID, Sprite itemSprite)
    {
        if (!inventoryItems.Contains(itemID))//prevents duplicates
        {
            inventoryItems.Add(itemID);

            //find empty slot and update UI
            for (int i = 0; i < uiSlots.Count; i++)
            {
                if (!uiSlots[i].enabled) //if slot is empty
                {
                    uiSlots[i].sprite = itemSprite;
                    uiSlots[i].enabled = true; // Show the image
                    
                    return;
                }
            }
        }
    }


}
