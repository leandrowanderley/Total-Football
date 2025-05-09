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



    public GameObject ball;
    public float pickupDistance = 1.0f;
    private GameObject currentBallHolder = null;
    public Vector3 ballOffset = new Vector3(0, 0.251f, 1f);

    public float passForce = 5f;
    public float shootForce = 15f;
    private Rigidbody ballRb;

    void Start()
    {
        for (int i = 0; i < playersFather.transform.childCount; i++)
        {
            GameObject player = playersFather.transform.GetChild(i).gameObject;

            if (i < 11) Team1.Add(player);
            else Team2.Add(player);
        }

        if (Team1.Count == 0) Team1.Add(Instantiate(player1Character, playersFather.transform));
        if (Team2.Count == 0) Team2.Add(Instantiate(player2Character, playersFather.transform));

        ballRb = ball.GetComponent<Rigidbody>();
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

        // Player 1 - passe e chute (C / V)
        if (Input.GetKey(KeyCode.C)) PassBall(player1Character);
        if (Input.GetKey(KeyCode.V)) ShootBall(player1Character);
        // Player 2 - passe e chute (N / M)
        if (Input.GetKey(KeyCode.N)) PassBall(player2Character);
        if (Input.GetKey(KeyCode.M)) ShootBall(player2Character);

        // Player 1 - troca de jogador usando Q
        if (Input.GetKeyDown(KeyCode.Q)) ChangePlayer1();
        // Player 2 - troca de jogador usando /
        if (Input.GetKeyDown(KeyCode.Slash)) ChangePlayer2();

        RotatePlayers();

        updatePlayerIndicator();
        updateNextPlayerIndicator();

        CheckBallPossession();
        UpdateBallPosition();
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

    void RotatePlayers()
    {
        // Rotaciona automaticamente os jogadores de Team1 (exceto o controlado)
        foreach (GameObject player in Team1)
        {
            if (player == player1Character) continue;

            Vector3 direction = ball.transform.position - player.transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, toRotation, 10f * Time.deltaTime);
            }
        }

        // Rotaciona automaticamente os jogadores de Team2 (exceto o controlado)
        foreach (GameObject player in Team2)
        {
            if (player == player2Character) continue;

            Vector3 direction = ball.transform.position - player.transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, toRotation, 10f * Time.deltaTime);
            }
        }

        // Rotaciona Player 1 com base nas teclas WASD
        Vector3 inputDir1 = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) inputDir1 += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) inputDir1 += Vector3.back;
        if (Input.GetKey(KeyCode.A)) inputDir1 += Vector3.left;
        if (Input.GetKey(KeyCode.D)) inputDir1 += Vector3.right;
        RotateControlledPlayer(player1Character, inputDir1);

        // Rotaciona Player 2 com base nas setas
        Vector3 inputDir2 = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow)) inputDir2 += Vector3.forward;
        if (Input.GetKey(KeyCode.DownArrow)) inputDir2 += Vector3.back;
        if (Input.GetKey(KeyCode.LeftArrow)) inputDir2 += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) inputDir2 += Vector3.right;
        RotateControlledPlayer(player2Character, inputDir2);
    }

    void RotateControlledPlayer(GameObject player, Vector3 inputDirection)
    {
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            Quaternion toRotation = Quaternion.LookRotation(inputDirection);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, toRotation, 10f * Time.deltaTime);
        }
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

    void CheckBallPossession()
    {
        // Jogador 1 pega a bola
        if (Vector3.Distance(player1Character.transform.position, ball.transform.position) < pickupDistance)
        {
            currentBallHolder = player1Character;
        }

        // Jogador 2 pega a bola
        else if (Vector3.Distance(player2Character.transform.position, ball.transform.position) < pickupDistance)
        {
            currentBallHolder = player2Character;
        }

        // Soltar a bola (Exemplo: pressionar espaço)
        if (Input.GetKeyDown(KeyCode.Space) && currentBallHolder != null)
        {
            currentBallHolder = null;
        }
    }

    void UpdateBallPosition()
    {
        if (currentBallHolder != null)
        {
            ball.transform.position = currentBallHolder.transform.position + currentBallHolder.transform.rotation * ballOffset;
            ball.transform.rotation = currentBallHolder.transform.rotation;
        }
    }


    void PassBall(GameObject player)
    {
        if (currentBallHolder == player)
        {
            currentBallHolder = null;
            ballRb.isKinematic = false;

            Vector3 direction = player.transform.forward;
            ballRb.linearVelocity = direction * passForce;
        }
    }

    void ShootBall(GameObject player)
    {
        if (currentBallHolder == player)
        {
            currentBallHolder = null;
            ballRb.isKinematic = false;

            Vector3 direction = player.transform.forward;
            ballRb.linearVelocity = direction * shootForce;
        }
    }


}
