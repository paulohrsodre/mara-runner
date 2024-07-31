using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Velocidade de movimento no eixo Z
    public float zMoveSpeed = 5f; // Velocidade de transição no eixo X
    public float zSmoothFactor = 0.1f; // Fator de suavização para o movimento no eixo X
    public float jumpForce = 5f; // Força do salto
    public float maxJumpHeight = 2f; // Altura máxima do salto
    public LayerMask groundLayer; // Layer do chão para detectar quando o personagem está no chão

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float[] xPositions = new float[] { -1f, 0f, 1f }; // Posições permitidas no eixo X
    private int currentXIndex = 1; // Índice inicial no eixo X (0, que é o meio)
    private Quaternion initialRotation; // Rotação inicial do personagem
    private Rigidbody rb;
    private bool isGrounded;
    private float initialYPosition;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition;
        initialRotation = Quaternion.Euler(0, 90, 0); // Define a rotação inicial como 90 graus no eixo Y
        transform.rotation = initialRotation;
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        initialYPosition = transform.position.y;
    }

    void Update()
    {
        MoveCharacter();
        SmoothTransition();
        CheckGrounded();
        Jump();
    }

    void MoveCharacter()
    {
        // Movimento livre no eixo Z (eixo X do mundo devido à rotação)
        float moveZ = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        targetPosition.z -= moveZ; // Inverte o movimento devido à rotação

        // Movimento limitado no eixo X (eixo Z do mundo devido à rotação)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentXIndex = Mathf.Clamp(currentXIndex + 1, 0, xPositions.Length - 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentXIndex = Mathf.Clamp(currentXIndex - 1, 0, xPositions.Length - 1);
        }

        targetPosition.x = startPosition.x + xPositions[currentXIndex]; // Ajusta targetPosition.x
    }

    void SmoothTransition()
    {
        // Suaviza a transição
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, zSmoothFactor);
        smoothedPosition.x = Mathf.Lerp(transform.position.x, targetPosition.x, zSmoothFactor); // Suaviza no eixo X do mundo
        smoothedPosition.z = targetPosition.z; // Move diretamente no eixo Z do mundo

        transform.position = smoothedPosition;

        // Mantém a rotação inicial
        transform.rotation = initialRotation;
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (transform.position.y > initialYPosition + maxJumpHeight)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }
}