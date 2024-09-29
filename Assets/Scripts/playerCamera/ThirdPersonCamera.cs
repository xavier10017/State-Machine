using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // O jogador ou objeto a ser seguido
    public float distance = 5.0f; // Dist�ncia da c�mera para o jogador
    public float height = 2.0f; // Altura da c�mera em rela��o ao jogador
    public float rotationSpeed = 5.0f; // Velocidade da rota��o da c�mera

    private float currentX = 0.0f; // Armazena a rota��o atual no eixo X
    private float currentY = 0.0f; // Armazena a rota��o atual no eixo Y

    void Update()
    {
        // Atualiza a rota��o com base na entrada do mouse
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Limita a rota��o vertical da c�mera
        currentY = Mathf.Clamp(currentY, -20, 60);
    }

    void LateUpdate()
    {
        // Calcula a rota��o da c�mera
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // Calcula a posi��o desejada da c�mera
        Vector3 position = target.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);

        // Define a posi��o e a rota��o da c�mera
        transform.position = position;
        transform.LookAt(target);
    }
}

