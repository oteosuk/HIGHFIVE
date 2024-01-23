using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Stat stat;
    protected PlayerStateMachine _playerStateMachine;
    private PhotonView _photonView;
    public Rigidbody2D Rigidbody { get; protected set; }
    public PlayerInput Input { get; protected set; }
    public Collider2D Controller { get; set; }
    public Animator PlayerAnim { get; set; }
    public PlayerAnimationData PlayerAnimationData { get; set; }

    protected virtual void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<Collider2D>();
        PlayerAnim = GetComponent<Animator>();
        PlayerAnimationData = new PlayerAnimationData();
        

        if (Input == null)
        {
            Debug.Log("InputNull");
            Debug.Log(Input);
        }
    }
    protected virtual void Start()
    {
        PlayerAnimationData.Initialize();
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
