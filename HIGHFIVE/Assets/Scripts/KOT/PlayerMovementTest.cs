using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTest : MonoBehaviour
{
    public float moveSpeed = 2f; // 캐릭터 이동 속도

    private Vector2 movementInput;

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Input System을 사용하여 WASD 키로부터 입력을 받음
        Vector3 moveDirection = new Vector3(movementInput.x, movementInput.y, 0f).normalized;

        // 이동 방향에 따라 캐릭터 이동
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    // Input System에서 호출되는 이벤트 핸들러
    public void OnMove(InputAction.CallbackContext context)
    {
        // WASD 키 입력을 받아 저장
        movementInput = context.ReadValue<Vector2>();
    }
}
