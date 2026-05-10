using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    // impede a bola de andar reta demais
    public float minYVelocity = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LaunchBall();
    }

    void LaunchBall()
    {
        bool isRight = UnityEngine.Random.value > 0.5f;
        float xVelocity = -1f;

        if (isRight == true)
        {
            xVelocity = 1f;
        }

        float yVelocity = UnityEngine.Random.Range(-1f, 1f);

        // força uma inclinação mínima
        if (yVelocity > 0)
        {
            yVelocity = Mathf.Max(yVelocity, minYVelocity);
        }
        else
        {
            yVelocity = Mathf.Min(yVelocity, -minYVelocity);
        }

        Vector2 direction = new Vector2(xVelocity, yVelocity).normalized;

        rb.linearVelocity = direction * speed;
    }

    public void ResetBall()
    {
        transform.position = Vector2.zero;
        transform.rotation = Quaternion.identity;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        LaunchBall();
    }

    // Update is called once per frame
    void Update()
    {

    }
}