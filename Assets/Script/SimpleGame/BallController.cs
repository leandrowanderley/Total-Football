using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{

    public float speed;

    Rigidbody rb;
    float xInput;
    float yInput;

    int score = 0;
    public int winScore;

    public GameObject winText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5f)
        {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        rb.AddForce(xInput * speed, 0, yInput * speed);


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);

            score++;

            if(score >= winScore)
            {
                winText.SetActive(true);
            }
        }


    }

}
