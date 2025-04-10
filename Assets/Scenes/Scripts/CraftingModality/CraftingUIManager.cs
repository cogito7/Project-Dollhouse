using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingUIManager : MonoBehaviour
{
    public GameObject craftingPanel;
    private bool isOpen = false;
    public static bool IsCraftingOpen { get; private set; }

    void Start()
    {
        craftingPanel.SetActive(false); // Hide the panel at start
        Cursor.lockState = CursorLockMode.None; // Make sure cursor is always free
        Cursor.visible = true; // Ensure the cursor is visible from the start
    }

    public void ToggleCraftingUI()
    {
        isOpen = !isOpen;
        IsCraftingOpen = isOpen;
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
