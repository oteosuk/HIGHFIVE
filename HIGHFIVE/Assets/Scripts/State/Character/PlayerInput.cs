using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // InputSystem으로 생성한 클래스
    public PlayerController _playerController { get; private set; }

    // InputSystem으로 생성하면서 만들어진 PlayerAction 
    public PlayerController.PlayerActions _playerActions { get; private set; }

    private void Awake()
    {
        _playerController = new PlayerController();
        _playerActions = _playerController.Player;
    }

    private void OnEnable()
    {
        _playerController.Enable();
    }

    private void OnDisable()
    {
        _playerController.Disable();
    }
}

