using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniMapCamera : MonoBehaviour
{
    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            MoveCharacterToMiniMapPos();
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            MinimapCameraMove();
        }
    }
    private void MinimapCameraMove()
    {
        Vector2 mousePoint = Main.GameManager.SpawnedCharacter._playerStateMachine._player.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        float xRatio;
        float yRatio;

        if ((1505 <= mousePoint.x && mousePoint.x <= 1913) && (25 <= mousePoint.y && mousePoint.y <= 320))
        {
            xRatio = (mousePoint.x - 1505f) / 408f;
            yRatio = (mousePoint.y - 25f) / 295f;
            raymousePoint.x = -54 + xRatio * 106; // 맵 실제좌표의 맨 왼쪽부분(-52)   *100은 맵 가로길이
            raymousePoint.y = -20 + yRatio * 59; // 맵 실제좌표의 맨 아래쪽부분(-20)   *50은 맵 세로길이
            MinimapCamera(raymousePoint);
        }
    }

    private void MinimapCamera(Vector2 minimapPos)
    {
        Vector3 minimapPos3;
        minimapPos3.x = minimapPos.x;
        minimapPos3.y = minimapPos.y;
        minimapPos3.z = -10;
        Camera.main.transform.position = minimapPos3;
    }

    private void MoveCharacterToMiniMapPos()
    {
        PlayerStateMachine playerStateMachine = Main.GameManager.SpawnedCharacter._playerStateMachine;
        Vector2 mousePoint = playerStateMachine._player.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        Debug.Log(mousePoint + " " + raymousePoint);
        float xRatio;
        float yRatio;
        //minimap관련
        if ((1505 <= mousePoint.x && mousePoint.x <= 1913) && (25 <= mousePoint.y && mousePoint.y <= 320))
        {
            xRatio = (mousePoint.x - 1505f) / 408f;
            yRatio = (mousePoint.y - 25f) / 295f;
            raymousePoint.x = -54 + xRatio * 106; // 맵 실제좌표의 맨 왼쪽부분(-52)   *100은 맵 가로길이
            raymousePoint.y = -20 + yRatio * 59; // 맵 실제좌표의 맨 아래쪽부분(-20)   *50은 맵 세로길이
            //Debug.Log("미니맵쪽 클릭" + raymousePoint);
            playerStateMachine.moveInput = raymousePoint;
        }
    }
}