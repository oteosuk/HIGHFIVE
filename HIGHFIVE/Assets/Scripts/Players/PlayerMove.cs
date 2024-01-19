using DG.Tweening;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public event Action<Collision2D> _damageTest;

    public float speed = 5f; // 이동 속도
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        // 마우스 우클릭 감지
        if (gameObject.GetComponent<PhotonView>().IsMine)
        {
            if (Input.GetMouseButtonDown(1))
            {
                // 마우스 위치를 월드 좌표로 변환
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = 0f; // 2D 게임에서는 주로 z 값이 0이 됩니다.
                
            }
            // 오브젝트 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("충돌");
            _damageTest?.Invoke(collision);
        }
    }
}
