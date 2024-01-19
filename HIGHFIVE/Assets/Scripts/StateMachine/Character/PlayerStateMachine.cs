using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // Player에 상태들을 관리해주기 위한 클래스
    public Character _player { get; }

    public PlayerIdleState _idleState { get; }

    public Vector2 _moveInput { get; set; }
    public float _speed { get; private set; }

    public Transform _mainCameraTransform { get; set; }

    public PlayerStateMachine(Character character)
    {
        this._player = character;
        
        _idleState = new PlayerIdleState(this);

        _mainCameraTransform = Camera.main.transform;
    }

}
