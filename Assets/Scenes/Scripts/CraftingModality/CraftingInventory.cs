using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class CraftingInventory : MonoBehaviour
{
    private List<string> inventoryItems = new List<string>();
    public List<Image> inventorySlots;
    public Sprite defaultSprite; //empty slot default sprite
    public Image craftingOutputImage; //image for combined puzzle item

    private Dictionary<(string, string), Sprite> craftingRecipes = new Dictionary<(string, string), Sprite>();

    private void Start()
    {
       ;
        //initialize inventory slots
        foreach (Image slot in inventorySlots) 
        {
            slot.sprite = defaultSprite;
            slot.enabled = true;
        }
        craftingOutputImage.enabled = true;

        //Define crafting recipes
        craftingRecipes.Add(("key1", "key2"), Resources.Load<Sprite>("Sprites/Piano"));

    }
    public void AddItemToInventory(string itemID, Sprite itemSprite)
    {
        if (inventoryItems.Count >= 3)
        {
            return;
        }
        if (!inventoryItems.Contains(itemID))//prevents duplicates
        {
            inventoryItems.Add(itemID);

            //find empty slot and update UI
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (!inventorySlots[i].enabled) //if slot is empty
                {
                    inventorySlots[i].sprite = itemSprite;
                    inventorySlots[i].enabled = true; // Show the image
                    CheckForCombination();
                    return;
                }
            }
        }
    }
    private void CheckForCombination()
    {
        foreach (var recipe in craftingRecipes)
        {
            if (inventoryItems.Contains(recipe.Key.Item1) && inventoryItems.Contains(recipe.Key.Item2))
            {
                // Show the new combined item in the crafting output UI
                craftingOutputImage.sprite = recipe.Value;
                craftingOutputImage.enabled = true;
                return;

            }
        }

    }


}
