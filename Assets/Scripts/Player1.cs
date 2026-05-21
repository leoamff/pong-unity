using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 10f;

    // limite da tela
    public float limit = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool isUp = Keyboard.current.wKey.isPressed;
        bool isDown = Keyboard.current.sKey.isPressed;

        if (isUp)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }

        if (isDown)
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }

        // trava a raquete dentro da tela
        float clampedY = Mathf.Clamp(transform.position.y, -limit, limit);

        transform.position = new Vector3(
            transform.position.x,
            clampedY,
            transform.position.z
        );
    }
}