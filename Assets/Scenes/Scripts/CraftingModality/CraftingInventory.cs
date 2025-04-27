using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingInventory : MonoBehaviour
{
    public static CraftingInventory Instance; // Singleton instance

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private List<string> inventoryItems = new List<string>();
    public List<Image> inventorySlots;
    private List<string> craftingItems = new List<string>();
    public List<Image> craftingSlots;
    public Sprite defaultSprite; //empty slot default sprite
    public Image craftingOutputImage; //image for combined puzzle item

    // Inputs are three crafted items. Output are an item with a corresponding name
    private Dictionary<(string, string, string), (string, Sprite)> craftingRecipes = new Dictionary<(string, string, string), (string, Sprite)>();

    private void Start()
    {

        //initialize inventory slots
        foreach (Image slot in inventorySlots)
        {
            slot.sprite = defaultSprite;
            slot.enabled = true;
        }
        craftingOutputImage.enabled = true;

        //Define crafting recipes
        craftingRecipes.Add(("key1", "key2", "key3"), ("complete_key", Resources.Load<Sprite>("Sprites/Piano/Piano Key")));

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
                if (inventorySlots[i].sprite == defaultSprite) //if slot is empty
                {
                    inventorySlots[i].sprite = itemSprite;
                    inventorySlots[i].enabled = true; // Show the image
                    CheckForCombination();
                    return;
                }
            }
        }
    }

    public void AddItemToCrafting(string itemID, Sprite itemSprite)
    {
        if (craftingItems.Count >= 3)
        {
            return;
        }
        if (!craftingItems.Contains(itemID))//prevents duplicates
        {
            craftingItems.Add(itemID);
            //find empty slot and update UI
            for (int i = 0; i < craftingSlots.Count; i++)
            {
                if (craftingSlots[i].sprite == defaultSprite) //if slot is empty
                {
                    craftingSlots[i].sprite = itemSprite;
                    craftingSlots[i].enabled = true; // Show the image
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
            if (inventoryItems.Contains(recipe.Key.Item1) && inventoryItems.Contains(recipe.Key.Item2) && inventoryItems.Contains(recipe.Key.Item3))
            {
                // Show the new combined item in the crafting output UI
                craftingOutputImage.sprite = recipe.Value.Item2;
                craftingOutputImage.enabled = true;
                return;
            }
        }

    }

    public void Craft()
    {
        foreach (var recipe in craftingRecipes)
        {
            if (inventoryItems.Contains(recipe.Key.Item1) && inventoryItems.Contains(recipe.Key.Item2) && inventoryItems.Contains(recipe.Key.Item3))
            {
                inventoryItems = new List<string>();
                craftingItems = new List<string>();
                foreach (Image inventory in inventorySlots)
                {
                    inventory.sprite = defaultSprite;
                }
                foreach (Image crafting in craftingSlots)
                {
                    crafting.sprite = defaultSprite;
                }
                craftingOutputImage.sprite = defaultSprite;
                AddItemToInventory(recipe.Value.Item1, recipe.Value.Item2);
                AddItemToCrafting(recipe.Value.Item1, recipe.Value.Item2);
                return;
            }
        }
    }
}