using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{    public float moveSpeed;

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
    }
}
