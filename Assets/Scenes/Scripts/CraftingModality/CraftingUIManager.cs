using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingUIManager : MonoBehaviour
{
    public GameObject craftingPanel;
    public GameObject pausemenu;
    private bool isOpen = false;
    public static bool IsCraftingOpen { get; private set; }
    public static bool IsPauseOpen { get; private set; }

    void Start()
    {
        craftingPanel.SetActive(false); // Hide the panel at start
        pausemenu.SetActive(false); // Hide the panel at start
        Cursor.lockState = CursorLockMode.None; // Make sure cursor is always free
        Cursor.visible = true; // Ensure the cursor is visible from the start
    }

    public void ToggleCraftingUI()
    {
        isOpen = !isOpen;
        IsCraftingOpen = isOpen;
        craftingPanel.SetActive(isOpen);
    }

    public void CloseCraftingUI()
    {
        isOpen = false;
        IsCraftingOpen = false;
        craftingPanel.SetActive(false);
    }

    public void TogglePauseUI()
    {
        isOpen = !isOpen;
        IsPauseOpen = isOpen;
        pausemenu.SetActive(isOpen);
    }

    public void ClosePauseUI()
    {
        isOpen = false;
        IsPauseOpen = false;
        pausemenu.SetActive(false);
    }

    void Update()
    {

    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
