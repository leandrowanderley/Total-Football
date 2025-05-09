using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManagement : MonoBehaviour
{

    //   Assets/Images/PlayerIndicator/Player1Indicator.png // Player atual do Jogador 1
    //   Assets/Images/PlayerIndicator/Player1Indicator2.png // Player mais próximo do player atual do Jogador 1
    //   Assets/Images/PlayerIndicator/Player2Indicator.png // Player atual do Jogador 2
    //   Assets/Images/PlayerIndicator/Player2Indicator2.png // Player mais próximo do player atual do Jogador 2

    public GameObject player1Indicator;
    public GameObject player2Indicator;
    public GameObject player1Indicator2;
    public GameObject player2Indicator2;


    public GameObject playersFather;
    public GameObject player1Character;
    public GameObject player2Character;
    List<GameObject> Team1 = new List<GameObject>();
    List<GameObject> Team2 = new List<GameObject>();

    public float playerIndicatorDistance;

    public float moveSpeed;


    void Start()
    {
        for (int i = 0; i < playersFather.transform.childCount; i++)
        {
            GameObject player = playersFather.transform.GetChild(i).gameObject;

            if (i < 11)
                Team1.Add(player);
            else
                Team2.Add(player);
        }

        if (Team1.Count == 0)
        {
            Team1.Add(Instantiate(player1Character, playersFather.transform));
        }
        if (Team2.Count == 0)
        {
            Team2.Add(Instantiate(player2Character, playersFather.transform));
        }
    }

    void Update()
    {
        // Player 1 - usando WASD
        if (Input.GetKey(KeyCode.W)) Player1Movement(Vector3.forward);
        if (Input.GetKey(KeyCode.S)) Player1Movement(Vector3.back);
        if (Input.GetKey(KeyCode.A)) Player1Movement(Vector3.left);
        if (Input.GetKey(KeyCode.D)) Player1Movement(Vector3.right);

        // Player 2 - usando setas
        if (Input.GetKey(KeyCode.UpArrow)) Player2Movement(Vector3.forward);
        if (Input.GetKey(KeyCode.DownArrow)) Player2Movement(Vector3.back);
        if (Input.GetKey(KeyCode.LeftArrow)) Player2Movement(Vector3.left);
        if (Input.GetKey(KeyCode.RightArrow)) Player2Movement(Vector3.right);


        // Player 1 - troca de jogador usando Q
        if (Input.GetKeyDown(KeyCode.Q)) ChangePlayer1();
        // Player 2 - troca de jogador usando /
        if (Input.GetKeyDown(KeyCode.Slash)) ChangePlayer2();

        updatePlayerIndicator();
        updateNextPlayerIndicator();
    }

    void Player1Movement(Vector3 direction)
    {
        Vector3 newPos = player1Character.transform.position + direction * moveSpeed * Time.deltaTime;
        newPos.y = player1Character.transform.position.y;
        player1Character.transform.position = newPos;
    }

    void Player2Movement(Vector3 direction)
    {
        Vector3 newPos = player2Character.transform.position + direction * moveSpeed * Time.deltaTime;
        newPos.y = player2Character.transform.position.y;
        player2Character.transform.position = newPos;
    }

     void updatePlayerIndicator()
    {
        Vector3 worldOffset = new Vector3(0, playerIndicatorDistance, 0);

        Vector2 screenPos1 = RectTransformUtility.WorldToScreenPoint(Camera.main, player1Character.transform.position + worldOffset);
        player1Indicator.GetComponent<RectTransform>().position = screenPos1;

        Vector2 screenPos2 = RectTransformUtility.WorldToScreenPoint(Camera.main, player2Character.transform.position + worldOffset);
        player2Indicator.GetComponent<RectTransform>().position = screenPos2;
    }

    void updateNextPlayerIndicator()
    {
        Vector3 offset = new Vector3(0, playerIndicatorDistance, 0);

        // Jogador mais próximo do Player 1 (busca em team1)
        GameObject closestToPlayer1 = null;
        float minDist1 = float.MaxValue;

        foreach (GameObject teammate in Team1)
        {
            if (teammate == player1Character) continue;

            float dist = Vector3.Distance(player1Character.transform.position, teammate.transform.position);
            if (dist < minDist1)
            {
                minDist1 = dist;
                closestToPlayer1 = teammate;
            }
        }

        if (closestToPlayer1 != null)
        {
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, closestToPlayer1.transform.position + offset);
            player1Indicator2.GetComponent<RectTransform>().position = screenPos;
        }

        // Jogador mais próximo do Player 2 (busca em team2)
        GameObject closestToPlayer2 = null;
        float minDist2 = float.MaxValue;

        foreach (GameObject teammate in Team2)
        {
            if (teammate == player2Character) continue;

            float dist = Vector3.Distance(player2Character.transform.position, teammate.transform.position);
            if (dist < minDist2)
            {
                minDist2 = dist;
                closestToPlayer2 = teammate;
            }
        }

        if (closestToPlayer2 != null)
        {
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, closestToPlayer2.transform.position + offset);
            player2Indicator2.GetComponent<RectTransform>().position = screenPos;
        }
    }

    void ChangePlayer1()
    {
        GameObject closestToPlayer1 = null;
        float minDist = float.MaxValue;

        foreach (GameObject teammate in Team1)
        {
            if (teammate == player1Character) continue;

            float dist = Vector3.Distance(player1Character.transform.position, teammate.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestToPlayer1 = teammate;
            }
        }

        if (closestToPlayer1 != null)
        {
            player1Character = closestToPlayer1;
        }
    }

    void ChangePlayer2()
    {
        GameObject closestToPlayer2 = null;
        float minDist = float.MaxValue;

        foreach (GameObject teammate in Team2)
        {
            if (teammate == player2Character) continue;

            float dist = Vector3.Distance(player2Character.transform.position, teammate.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestToPlayer2 = teammate;
            }
        }

        if (closestToPlayer2 != null)
        {
            player2Character = closestToPlayer2;
        }
    }

}
