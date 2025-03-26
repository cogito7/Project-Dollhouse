using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    public static ItemTracker Instance;
    private Dictionary<string, bool> pickedUpItems = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void MarkItemAsPicked(string itemID)
    {
        pickedUpItems[itemID] = true;
        
    }
    public bool HasItemBeenPicked(string itemID)
    {
        bool isPicked = pickedUpItems.ContainsKey(itemID) && pickedUpItems[itemID];
        
        return isPicked;
    }
  
}
