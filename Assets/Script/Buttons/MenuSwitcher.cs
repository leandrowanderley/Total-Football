using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject settings;

    public void ShowSettings()
    {
        menuPrincipal.SetActive(false);
        settings.SetActive(true);
    }

    public void ReturnToMenu()
    {
        settings.SetActive(false);
        menuPrincipal.SetActive(true);
    }
}
