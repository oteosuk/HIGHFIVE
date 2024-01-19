using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private PlayerStateMachine _playerStateMachine;

    public Rigidbody2D _rigidbody { get; private set; }
    public PlayerInput _input { get; private set; }
    public Collider2D _controller { get; set; }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
        _controller = GetComponent<Collider2D>();

        if(_input == null)
        {
            Debug.LogError("에러!");
        }

        _playerStateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
    }
    private void Update()
    {
        _playerStateMachine.HandleInput();
        _playerStateMachine.StateUpdate();
    }

    private void FixedUpdate()
    {
        _playerStateMachine.PhysicsUpdate();
    }
}
