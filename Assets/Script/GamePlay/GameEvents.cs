using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;


public class GameEventsManager : MonoBehaviour
{

    public GameObject ball;
    public TMP_Text scoreBoard1;
    public TMP_Text scoreBoard2;

    private int scoreTeam1 = 0;
    private int scoreTeam2 = 0;

    public InitialPositions initialPositions;

    public List<GameObject> Team1;
    public List<GameObject> Team2;

    private void Start()
    {
        // Opcional: Garantir que as listas Team1 e Team2 estejam preenchidas no Inspector
        if (Team1.Count == 0 || Team2.Count == 0)
        {
            Debug.LogError("As listas Team1 e Team2 não estão preenchidas corretamente!");
        }
        else
        {
            Debug.Log("Team1 e Team2 preenchidos corretamente.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter chamado.");
        
        if (other.CompareTag("Ball"))
        {
            Debug.Log("A bola entrou no trigger.");
            string tag = gameObject.tag;
            Debug.Log("Tag do objeto que colidiu: " + tag);

            if (tag == "TouchLine")
            {
                Debug.Log("Bola saiu pela lateral.");
                HandleOutOfBounds();
            }
            else if (tag == "Gol1")
            {
                Debug.Log("Gol na equipe 2!");
                scoreTeam2++;
                PlayGoalSound();
                UpdateScoreboard2();
                HandleGoal();
            }
            else if (tag == "Gol2")
            {
                Debug.Log("Gol na equipe 1!");
                scoreTeam1++;
                PlayGoalSound();
                UpdateScoreboard1();
                HandleGoal();
            }
            else
            {
                Debug.Log("Tag desconhecida.");
            }
        }
        else
        {
            Debug.Log("Objeto que entrou no trigger não é a bola.");
        }
    }

    void HandleOutOfBounds()
    {
        Debug.Log("Chamando ResetGamePositions para a bola sair pela lateral.");
        if (initialPositions != null)
        {
            ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            initialPositions.ResetGamePositions(Team1, Team2, ball);  // Passando as listas corretas
            Debug.Log("Posições de jogo resetadas.");
        }
        else
        {
            Debug.LogError("initialPositions é null.");
        }
    }

    void HandleGoal()
    {
        Debug.Log("Chamando ResetGamePositions para o gol.");
        if (initialPositions != null)
        {
            ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
            initialPositions.ResetGamePositions(Team1, Team2, ball);  // Passando as listas corretas
            Debug.Log("Posições de jogo resetadas após o gol.");
        }
        else
        {
            Debug.LogError("initialPositions é null.");
        }
    }

    void UpdateScoreboard1()
    {
        Debug.Log($"Atualizando placar: {scoreTeam1} - {scoreTeam2}");
        scoreBoard1.text = $"{scoreTeam1}";
    }

    void UpdateScoreboard2()
    {
        Debug.Log($"Atualizando placar: {scoreTeam1} - {scoreTeam2}");
        scoreBoard2.text = $"{scoreTeam2}";
    }

    public AudioSource goalSound;

    public void PlayGoalSound()
    {
        if (goalSound != null)
        {
            goalSound.Stop(); // Garante que o som reinicie mesmo se estiver tocando
            goalSound.Play();
            Debug.Log("Som do gol tocado.");

            if (audioManager != null)
            {
                StartCoroutine(ReduceBackgroundVolumeTemporarily());
            }
        }
        else
        {
            Debug.LogError("goalSound é null.");
        }
    }

    public AudioManager audioManager;

    private IEnumerator ReduceBackgroundVolumeTemporarily()
    {
        float originalVolume = audioManager.audioSource.volume;
        audioManager.audioSource.volume = originalVolume * 0.3f; // Diminui para 30%

        yield return new WaitForSeconds(goalSound.clip.length);

        audioManager.audioSource.volume = originalVolume;
    }

}
