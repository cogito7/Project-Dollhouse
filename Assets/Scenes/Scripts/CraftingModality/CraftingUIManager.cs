using UnityEngine;

public class CraftingUIManager : MonoBehaviour
{
    public GameObject craftingPanel;

    void Start()
    {
        craftingPanel.SetActive(false); // Hide the panel at start
    }
    public void ShowCraftingUI()
    {
        craftingPanel.SetActive(true);
    }

    public void HideCraftingUI()
    {
        craftingPanel.SetActive(false);
    }
}
