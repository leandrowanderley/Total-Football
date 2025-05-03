using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    void FixedUpdate()
    {
        transform.LookAt(target);
    }

}
