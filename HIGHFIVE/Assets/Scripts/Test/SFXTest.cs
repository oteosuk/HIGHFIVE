using UnityEngine;

public class SFXTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭된 위치의 좌표를 가져옵니다.
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 클릭 이벤트 처리 메서드 호출
            HandleClick(clickPosition);
        }
    }

    void HandleClick(Vector2 clickPosition)
    {
        SoundManager.instance.PlayEffect("SFX_Click", 1f);
        Debug.Log("마우스가 클릭되었습니다! 위치: " + clickPosition);
    }
}
