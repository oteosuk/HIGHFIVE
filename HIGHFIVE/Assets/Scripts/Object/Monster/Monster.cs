using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected MonsterStateMachine _monsterStateMachine;
    public Rigidbody2D Rigidbody { get; protected set; }
    public PlayerInput Input { get; protected set; }
    public Collider2D Controller { get; set; }
    public Animator Animator { get; set; }
    public MonsterAnimationData _monsterAnimationData { get; set; }


    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();

        _monsterAnimationData = new MonsterAnimationData();
    }

    protected virtual void Start()
    {
        _monsterStateMachine = new MonsterStateMachine(this);
        _monsterAnimationData.Initialize();
        _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
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
}
