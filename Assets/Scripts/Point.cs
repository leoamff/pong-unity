using UnityEngine;

public class Point : MonoBehaviour
{
    public bool isLeftGoal;
    public GameManager gameManager;
    public Ball ball;

    private void Start()
    {
        // Tenta encontrar o GameManager automaticamente se não estiver atribuído no Inspector
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("Point: Não foi possível encontrar o GameManager na cena!");
            }
        }

        // Tenta encontrar a Bola automaticamente se não estiver atribuída no Inspector
        if (ball == null)
        {
            ball = FindObjectOfType<Ball>();
            if (ball == null)
            {
                Debug.LogError("Point: Não foi possível encontrar a Bola (Ball) na cena!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a colisão foi com a bola (por Tag ou pelo Componente)
        if (collision.CompareTag("Ball") || collision.GetComponent<Ball>() != null)
        {
            if (gameManager != null)
            {
                if (isLeftGoal)
                {
                    gameManager.Player2Point();
                }
                else
                {
                    gameManager.Player1Point();
                }
            }

            // Reseta a bola de forma segura
            if (ball != null)
            {
                ball.ResetBall();
            }
            else
            {
                Ball ballComponent = collision.GetComponent<Ball>();
                if (ballComponent != null)
                {
                    ballComponent.ResetBall();
                }
            }
        }
    }
}