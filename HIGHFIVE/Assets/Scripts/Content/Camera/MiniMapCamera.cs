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
            MonveCharacterToMiniMapPos();
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

        if ((1515 <= mousePoint.x && mousePoint.x <= 1900) && (20 <= mousePoint.y && mousePoint.y <= 280)) // 1515 미니맵왼쪽x 1900 미니맵오른쪽x, 20 미니맵아래, 280 미니맵위
        {
            xRatio = (mousePoint.x - 1515f) / 385f;
            yRatio = (mousePoint.y - 20f) / 260f;
            raymousePoint.x = -52 + xRatio * 102; // 맵 실제좌표의 맨 왼쪽부분(-52)   *100은 맵 가로길이
            raymousePoint.y = -20 + yRatio * 50; // 맵 실제좌표의 맨 아래쪽부분(-20)   *50은 맵 세로길이
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

    private void MonveCharacterToMiniMapPos()
    {
        PlayerStateMachine playerStateMachine = Main.GameManager.SpawnedCharacter._playerStateMachine;
        Vector2 mousePoint = playerStateMachine._player.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        //Debug.Log(mousePoint + " " + raymousePoint);
        float xRatio;
        float yRatio;
        //minimap관련
        if ((1515 <= mousePoint.x && mousePoint.x <= 1900) && (20 <= mousePoint.y && mousePoint.y <= 280)) // 1515 미니맵왼쪽x 1900 미니맵오른쪽x, 20 미니맵아래, 280 미니맵위
        {
            xRatio = (mousePoint.x - 1515f) / 385f;
            yRatio = (mousePoint.y - 20f) / 260f;
            raymousePoint.x = -52 + xRatio * 102; // 맵 실제좌표의 맨 왼쪽부분(-52)   *100은 맵 가로길이
            raymousePoint.y = -20 + yRatio * 50; // 맵 실제좌표의 맨 아래쪽부분(-20)   *50은 맵 세로길이
            Debug.Log("미니맵쪽 클릭" + raymousePoint);
            playerStateMachine.moveInput = raymousePoint;
        }
    }
}
