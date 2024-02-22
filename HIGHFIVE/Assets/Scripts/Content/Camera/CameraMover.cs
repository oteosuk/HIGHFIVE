using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    private float cameraSpeed = 15f;
    public PlayerInput Input { get; protected set; }
    public Vector2 size;
    public Vector2 center;
    private float _zoomSpeed = 1.3f;
    private float _minZoom = 3.0f;
    private float _maxZoom = 8.0f;
    private float _cameraHeight;
    private float _cameraWidth;
    private bool _isSpacebarPressed = false; // Spacebar가 눌렸는지 여부를 저장


    private void Start()
    {
        Input = GetComponent<PlayerInput>();
        Input._playerActions.CallCamera.performed += ReturnCameraToCharacter;
        Input._playerActions.CallCamera.canceled += CancelReturnCameraToCharacter; // 취소 시 처리할 콜백 추가
        _cameraHeight = Camera.main.orthographicSize;
        _cameraWidth = _cameraHeight * Screen.width / Screen.height;
    }
    private void Update()
    {
        if (!_isSpacebarPressed)
        {
            UpdateMoveCamera();
            UpdateZoomCamera();
        }
    }

    private void LateUpdate()
    {
        LimitCameraMoveRange();
    }

    private void UpdateMoveCamera()
    {
        Vector3 mousePos = Input._playerActions.Move.ReadValue<Vector2>();

        if (mousePos.x <= 0 || mousePos.x >= Screen.width -5 ||
            mousePos.y <= 0 || mousePos.y >= Screen.height -5)
        {
            MoveCamera(mousePos);
        }
    }
    private void UpdateZoomCamera()
    {
        float scrollDelta = Mouse.current.scroll.ReadValue().y;
        ZoomCamera(scrollDelta);
    }

    private void MoveCamera(Vector2 mousePos)
    {
        Vector3 currentPosition = transform.position;

        if (mousePos.x <= 0)
        {
            currentPosition.x -= cameraSpeed * Time.deltaTime;
        }
        else if (mousePos.x >= Screen.width - 5)
        {
            currentPosition.x += cameraSpeed * Time.deltaTime;
        }

        if (mousePos.y <= 0)
        {
            currentPosition.y -= cameraSpeed * Time.deltaTime;
        }
        else if (mousePos.y >= Screen.height - 5)
        {
            currentPosition.y += cameraSpeed * Time.deltaTime;
        }

        transform.position = currentPosition;
    }

    private void ZoomCamera(float zoomDelta)
    {
        float currentZoom = Camera.main.orthographicSize;

        float newZoom = Mathf.Clamp(currentZoom - zoomDelta * _zoomSpeed * Time.deltaTime, _minZoom, _maxZoom);
        
        Camera.main.orthographicSize = Mathf.Lerp(currentZoom, newZoom, 0.5f);
    }

    /*private void ReturnCameraToCharacter(InputAction.CallbackContext context)
    {
        Vector2 characterPos = Main.GameManager.SpawnedCharacter.gameObject.transform.position;
        transform.position = new Vector3(characterPos.x, characterPos.y, transform.position.z);
    }*/

    private void ReturnCameraToCharacter(InputAction.CallbackContext context)
    {
        _isSpacebarPressed = true; // Spacebar가 눌렸음을 표시
    }

    private void CancelReturnCameraToCharacter(InputAction.CallbackContext context)
    {
        _isSpacebarPressed = false; // Spacebar가 눌리지 않았음을 표시
    }

    private void FixedUpdate()
    {
        if (_isSpacebarPressed)
        {
            Vector2 characterPos = Main.GameManager.SpawnedCharacter.gameObject.transform.position;
            transform.position = new Vector3(characterPos.x, characterPos.y, transform.position.z);
        }
    }


    private void LimitCameraMoveRange()
    {
        float limitX = size.x * 0.5f - _cameraWidth;
        float ClampX = Mathf.Clamp(transform.position.x, -limitX + center.x, limitX + center.x);
        float limitY = size.y * 0.5f - _cameraHeight;
        float ClampY = Mathf.Clamp(transform.position.y, -limitY + center.y, limitY + center.y);
        transform.position = new Vector3(ClampX, ClampY, transform.position.z);
    }
}
