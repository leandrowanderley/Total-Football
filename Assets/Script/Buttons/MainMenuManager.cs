#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    #if UNITY_EDITOR
    public SceneAsset gameplayScene;
    #endif

    [HideInInspector]
    public string gameplaySceneName;

    void OnValidate()
    {
        #if UNITY_EDITOR
        if (gameplayScene != null)
        {
            gameplaySceneName = gameplayScene.name;
        }
        #endif
    }

    public void QuickPlay()
    {
        if (!string.IsNullOrEmpty(gameplaySceneName))
        {
            SceneManager.LoadScene(gameplaySceneName);
        }
        else
        {
            Debug.LogError("Cena de gameplay não definida!");
        }
    }

    public void RandomSelection()
    {
        Debug.Log("Random Selection pressed");
        // Logic to start a game with random selection
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
