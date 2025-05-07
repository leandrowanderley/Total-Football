using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PressEscExit : MonoBehaviour
{
    public TextMeshProUGUI pressText; // "Pressione ESC para iniciar"
    public float blinkSpeed = 1f;

    private bool escPressed = false;

    void Update()
    {
        if (!escPressed)
        {
            BlinkText();

            // Verifica se a tecla Escape foi pressionada
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                escPressed = true;
                pressText.gameObject.SetActive(false);
                QuitGame();
            }
        }
    }

    void BlinkText()
    {
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
        Color color = pressText.color;
        color.a = alpha;
        pressText.color = color;
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Para sair no Editor
        #else
            Application.Quit(); // Para builds em Windows e macOS
        #endif
    }
}
