using System.Collections.Generic;
using UnityEngine;

public class InitialPositions : MonoBehaviour
{
    public List<Vector3> Players1Position = new List<Vector3>
    {
        new Vector3(-7.02f, 0.00f, -4.71f),
        new Vector3(-5.17f, 0.00f, 4.59f),
        new Vector3(-5.17f, 0.00f, -15.34f),
        new Vector3(-11.95f, 0.00f, -9.63f),
        new Vector3(-11.95f, 0.00f, 1.35f),
        new Vector3(-14.37f, 0.00f, -4.26f),
        new Vector3(-16.52f, 0.00f, -16.74f),
        new Vector3(-16.52f, 0.00f, 7.16f),
        new Vector3(-19.47f, 0.00f, 1.97f),
        new Vector3(-19.47f, 0.00f, -10.48f),
        new Vector3(-29.26f, 0.00f, -4.27f)
    };

    public List<Vector3> Players2Position = new List<Vector3>
    {
        new Vector3(4.93f, 0.00f, -8.71f),
        new Vector3(4.93f, 0.00f, -0.12f),
        new Vector3(8.55f, 0.00f, 5.75f),
        new Vector3(8.55f, 0.00f, -16.30f),
        new Vector3(12.10f, 0.00f, -9.12f),
        new Vector3(12.10f, 0.00f, -0.07f),
        new Vector3(18.31f, 0.00f, 0.96f),
        new Vector3(16.71f, 0.00f, 7.47f),
        new Vector3(16.71f, 0.00f, -15.75f),
        new Vector3(18.31f, 0.00f, -9.52f),
        new Vector3(28.72f, 0.00f, -4.42f)
    };

    public Vector3 BallInitialPosition = new Vector3(-0.1887279f, 0.251f, -4.304f);

    public void ResetGamePositions(List<GameObject> team1, List<GameObject> team2, GameObject ball)
    {
        // Resetando jogadores para as posições iniciais
        for (int i = 0; i < team1.Count; i++)
        {
            team1[i].transform.position = Players1Position[i];
        }

        for (int i = 0; i < team2.Count; i++)
        {
            team2[i].transform.position = Players2Position[i];
        }

        // Resetando bola para a posição inicial
        ball.transform.position = BallInitialPosition;
    }
}
