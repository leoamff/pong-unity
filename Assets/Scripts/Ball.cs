using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    // impede a bola de andar reta demais
    public float minYVelocity = 0.5f;

    // tempo de espera após um ponto (em segundos)
    public float resetDelay = 1.5f;

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
        StartCoroutine(ResetBallCoroutine());
    }

    private System.Collections.IEnumerator ResetBallCoroutine()
    {
        // Posiciona a bola no centro e zera a velocidade imediatamente
        transform.position = Vector2.zero;
        transform.rotation = Quaternion.identity;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Espera o tempo de delay configurado
        yield return new WaitForSeconds(resetDelay);

        // Lança a bola novamente
        LaunchBall();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (AudioManager.instance == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayHitPaddle();
        }
        else
        {
            AudioManager.instance.PlayHitWall();
        }
    }
}