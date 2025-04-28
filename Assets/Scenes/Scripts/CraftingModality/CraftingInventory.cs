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

    private List<(string, Sprite)> inventoryItems = new List<(string, Sprite)>();
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
        if (!inventoryItems.Contains((itemID, itemSprite))) //prevents duplicates
        {
            inventoryItems.Add((itemID, itemSprite));
            UpdateInventorySlots();
        }
    }
    private void UpdateInventorySlots()
    {
        for (int i = 0;i < inventorySlots.Count; i++)
        {
            if (i < inventoryItems.Count)
            {
                inventorySlots[i].sprite = inventoryItems[i].Item2;
            }
            else
            {
                inventorySlots[i].sprite = defaultSprite;
            }
        }
        CheckForCombination();
    }

    Sprite GetSprite(string itemID)
    {
        foreach ((string, Sprite) item in inventoryItems)
        {
            if (item.Item1 == itemID)
            {
                return item.Item2;
            }
        }
        return null;
    }

    bool Remove(string itemID)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].Item1 == itemID)
            {
                inventoryItems.RemoveAt(i);
                return true;
            }
        }
        return false;
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
        CheckForCombination();
    }

    private void CheckForCombination()
    {
        foreach (var recipe in craftingRecipes)
        {
            Sprite sprite1 = GetSprite(recipe.Key.Item1);
            Sprite sprite2 = GetSprite(recipe.Key.Item2);
            Sprite sprite3 = GetSprite(recipe.Key.Item3);
            Debug.Log("here1");
            if (sprite1 != null && sprite2 != null && sprite3 != null)
            {
                Debug.Log("here2");
                // Show the new combined item in the crafting output UI
                craftingSlots[0].sprite = sprite1;
                craftingSlots[1].sprite = sprite2;
                craftingSlots[2].sprite = sprite3;
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
            if (GetSprite(recipe.Key.Item1) && GetSprite(recipe.Key.Item2) && GetSprite(recipe.Key.Item3))
            {
                Remove(recipe.Key.Item1);
                Remove(recipe.Key.Item2);
                Remove(recipe.Key.Item3);
                AddItemToInventory(recipe.Value.Item1, recipe.Value.Item2);
                craftingSlots[0].sprite = defaultSprite;
                craftingSlots[1].sprite = defaultSprite;
                craftingSlots[2].sprite = defaultSprite;
                craftingOutputImage.sprite = defaultSprite;
                UpdateInventorySlots();

                return;
            }
        }
    }
}