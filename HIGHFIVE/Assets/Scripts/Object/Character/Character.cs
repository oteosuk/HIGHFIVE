using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Stat stat;
    protected PlayerStateMachine _playerStateMachine;
    private PhotonView _photonView;
    public Rigidbody2D _rigidbody { get; protected set; }
    public PlayerInput _input { get; protected set; }
    public Collider2D _controller { get; set; }

    protected virtual void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
        _controller = GetComponent<Collider2D>();

        if (_input == null)
        {
            Debug.Log("InputNull");
            Debug.Log(_input);
        }
    }
    protected virtual void Start()
    {
        _playerStateMachine = new PlayerStateMachine(this);
        _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
    }
    protected virtual void Update()
    {
        if (_photonView.IsMine)
        {
            _playerStateMachine.HandleInput();
            _playerStateMachine.StateUpdate();
        }
    }

    protected virtual void FixedUpdate()
    {
        if(_photonView.IsMine)
        {
            _playerStateMachine.PhysicsUpdate();
        }
    }
}
