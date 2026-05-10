using UnityEngine;

public class Point : MonoBehaviour
{
    public bool isLeftGoal;
    public GameManager gameManager;
    public Ball ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (isLeftGoal)
            {
                gameManager.Player2Point();
            }
            else
            {
                gameManager.Player1Point();
            }

            ball.ResetBall();
        }
    }
}