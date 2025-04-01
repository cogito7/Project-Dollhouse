using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingUIManager : MonoBehaviour
{
    public GameObject craftingPanel;
    private bool isOpen = false;

    void Start()
    {
        craftingPanel.SetActive(false); // Hide the panel at start
    }

    public void ToggleCraftingUI()
    {
        isOpen = !isOpen;
        craftingPanel.SetActive(isOpen);
    }

    void Update()
    {

    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
