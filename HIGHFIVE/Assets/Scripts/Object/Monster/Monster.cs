using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Stat stat;
    protected MonsterStateMachine _monsterStateMachine;
    public Vector2 _spawnPoint;

    public Rigidbody2D Rigidbody { get; protected set; }
    public PlayerInput Input { get; protected set; }
    public Collider2D Controller { get; set; }
    public Animator Animator { get; set; }
    public MonsterAnimationData MonsterAnimationData { get; set; }


    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<Collider2D>();
        Animator = transform.GetComponentInChildren<Animator>();

        stat = GetComponent<Stat>();
        SetSpawnPoint(transform.position);

        MonsterAnimationData = new MonsterAnimationData();
    }

    protected virtual void Start()
    {
        _monsterStateMachine = new MonsterStateMachine(this);
        MonsterAnimationData.GetParameterHash();
        _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
        Main.UIManager.CreateWorldUI<HealthBar>("HealthCanvas", transform);
    }

    protected virtual void Update()
    {
        _monsterStateMachine.HandleInput();
        _monsterStateMachine.StateUpdate();
    }

    protected virtual void FixedUpdate()
    {
        _monsterStateMachine.PhysicsUpdate();
    }

    private void SetSpawnPoint(Vector2 spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }
}
