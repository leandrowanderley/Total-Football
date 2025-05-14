using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform bola;      // arraste a bola aqui pelo Inspector
    public Vector3 offset;   // offset da câmera em relação à bola

    void LateUpdate()
    {
        // Atualiza a posição da câmera com base na bola
        transform.position = new Vector3(
            bola.position.x + offset.x,
            bola.position.y + offset.y,
            bola.position.z + offset.z +4.304f // ajuste a posição Z da câmera
        );

        // Garante que a câmera olhe sempre para a bola
        transform.LookAt(bola.position);
    }
}
