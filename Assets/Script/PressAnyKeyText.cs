using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PressAnyKey : MonoBehaviour
{
    public TextMeshProUGUI pressText; // "Pressione qualquer tecla para iniciar"
    public float blinkSpeed = 1f;

    private bool keyPressed = false;

    void Update()
    {
        if (!keyPressed)
        {
            BlinkText();

            if (Input.anyKeyDown)
            {
                keyPressed = true;
                pressText.gameObject.SetActive(false);
                SceneManager.LoadScene("OptionsMenu");
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
}
