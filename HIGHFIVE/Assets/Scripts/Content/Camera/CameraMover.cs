using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    private float cameraSpeed = 10f;
    public PlayerInput Input { get; protected set; }
    private float zoomSpeed = 1.0f;
    private float minZoom = 3.0f;
    private float maxZoom = 8.0f;


    private void Start()
    {
        Input = GetComponent<PlayerInput>();
        Input._playerActions.CallCamera.started += ReturnCameraToCharacter;
    }

    private void Update()
    {
        UpdateMoveCamera();
        UpdateZoomCamera();
    }

    private void UpdateMoveCamera()
    {
        Vector3 mousePos = Input._playerActions.Move.ReadValue<Vector2>();

        if (mousePos.x <= 0 || mousePos.x >= Screen.width ||
            mousePos.y <= 0 || mousePos.y >= Screen.height)
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
        else if (mousePos.x >= Screen.width)
        {
            currentPosition.x += cameraSpeed * Time.deltaTime;
        }

        if (mousePos.y <= 0)
        {
            currentPosition.y -= cameraSpeed * Time.deltaTime;
        }
        else if (mousePos.y >= Screen.height)
        {
            currentPosition.y += cameraSpeed * Time.deltaTime;
        }

        transform.position = currentPosition;
    }
    private void ZoomCamera(float zoomDelta)
    {
        float currentZoom = Camera.main.orthographicSize;

        float newZoom = Mathf.Clamp(currentZoom - zoomDelta * zoomSpeed * Time.deltaTime, minZoom, maxZoom);

        Camera.main.orthographicSize = newZoom;
    }

    private void ReturnCameraToCharacter(InputAction.CallbackContext context)
    {
        Vector2 characterPos = Main.GameManager.SpawnedCharacter.gameObject.transform.position;
        transform.position = new Vector3(characterPos.x, characterPos.y, transform.position.z);
    }
}
