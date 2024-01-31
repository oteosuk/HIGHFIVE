using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviourPunCallbacks
{
    private enum CursorType
    {
        None,
        Nomal,
        Attack
    }

    public Stat stat;
    public GameObject targetObject;
    protected bool isFistTime = true;
    protected PlayerStateMachine _playerStateMachine;
    private PhotonView _photonView;
    private Texture2D _attackTexture;
    private Texture2D _normalTexture;
    private CursorType _cursorType = CursorType.None;
    public Rigidbody2D Rigidbody { get; protected set; }
    public PlayerInput Input { get; protected set; }
    public Collider2D Controller { get; set; }
    public Animator PlayerAnim { get; set; }
    public PlayerAnimationData PlayerAnimationData { get; set; }

    public int resolution = 30;

    protected virtual void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<Collider2D>();
        PlayerAnim = GetComponent<Animator>();
        PlayerAnimationData = new PlayerAnimationData();
        
        stat = GetComponent<Stat>();

        if (Input == null)
        {
            Debug.Log("InputNull");
            Debug.Log(Input);
        }
    }
    protected virtual void Start()
    {
        _attackTexture = Main.ResourceManager.Load<Texture2D>("Sprites/Cursor/Attack");
        _normalTexture = Main.ResourceManager.Load<Texture2D>("Sprites/Cursor/Normal");
        PlayerAnimationData.Initialize();
        _playerStateMachine = new PlayerStateMachine(this);
        _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
        Main.UIManager.CreateWorldUI<HealthBar>("HealthCanvas", transform);
    }
    protected virtual void Update()
    {
        if (_photonView.IsMine)
        {
            _playerStateMachine.HandleInput();
            _playerStateMachine.StateUpdate();
            UpdateMouseCursor();
        }
    }

    protected virtual void FixedUpdate()
    {
        if(_photonView.IsMine)
        {
            _playerStateMachine.PhysicsUpdate();
        }
    }

    private void UpdateMouseCursor()
    {
        Vector2 mousePoint = _playerStateMachine._player.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red));

        RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 100.0f, mask);

        if (hit.collider?.gameObject != null || _playerStateMachine.isAttackReady)
        {
            if (_cursorType != CursorType.Attack)
            {
                Cursor.SetCursor(_attackTexture, new Vector2(_attackTexture.width / 5, _attackTexture.height / 10), CursorMode.Auto);
                _cursorType = CursorType.Attack;
            }
        }
        else
        {
            if (_cursorType != CursorType.Nomal)
            {
                Cursor.SetCursor(_normalTexture, new Vector2(_normalTexture.width / 5, _normalTexture.height / 10), CursorMode.Auto);
                _cursorType = CursorType.Nomal;
            }
        }
    }

    public virtual void OnNormalAttack() { }

    public void CheckTargetInRange()
    {
        if (_playerStateMachine._player.targetObject != null)
        {
            Debug.Log(isFistTime);
            if (isFistTime) return;
            float distance = (_playerStateMachine._player.targetObject.transform.position - _playerStateMachine._player.transform.position).magnitude;

            if (distance > _playerStateMachine._player.stat.AttackRange)
            {
                OnMove();
            }
            else
            {
                return;
            }
        }
        OnMove();
    }
    private  void OnMove()
    {
        isFistTime = true;
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }
}
