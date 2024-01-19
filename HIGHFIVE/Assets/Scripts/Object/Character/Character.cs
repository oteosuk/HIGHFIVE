using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    public Rigidbody2D _rigidbody { get; private set; }
    public PlayerInput _input { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();

        _stateMachine = new PlayerStateMachine(this);
    }
}
