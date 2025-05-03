using UnityEngine;
using UnityEngine.UI;

public class LockableButton : MonoBehaviour
{
    public bool isLocked = false;

    public GameObject lockOverlay; // overlay cinza do cadeado
    public GameObject lockIcon;     // ícone do cadeado
    public Button button;           // referência ao componente Button

    void Start()
    {
        UpdateLockVisual();
    }

    public void SetLocked(bool locked)
    {
        isLocked = locked;
        UpdateLockVisual();
    }

    void UpdateLockVisual()
    {
        lockOverlay.SetActive(isLocked);
        lockIcon.SetActive(isLocked);
        button.interactable = !isLocked;
    }
}
