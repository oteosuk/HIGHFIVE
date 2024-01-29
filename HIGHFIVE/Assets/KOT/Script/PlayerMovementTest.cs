using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTest : MonoBehaviour
{
    private Vector2 movementInput;
    public float moveSpeed = 3f;

    void Update()
    {
        Move();
    }

    //wasd입력 감지 메서드
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void Move()
    {
        Vector2 movement = movementInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
