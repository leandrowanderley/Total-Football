using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public float rotateSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }

}
