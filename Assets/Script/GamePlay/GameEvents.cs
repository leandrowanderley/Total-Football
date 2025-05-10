using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
                UpdateScoreboard2();
                HandleGoal();
            }
            else if (tag == "Gol2")
            {
                Debug.Log("Gol na equipe 1!");
                scoreTeam1++;
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



}
