using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Creature
{
    protected MonsterStateMachine _monsterStateMachine;
    public Vector2 _spawnPoint;
    public MonsterAnimationData MonsterAnimationData { get; set; }
    public Animator Animator { get; set; }

    protected override void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        Animator = transform.GetComponentInChildren<Animator>();
        SetSpawnPoint(transform.position);
        stat = GetComponent<Stat>();
        MonsterAnimationData = new MonsterAnimationData();
    }

    protected override void Start()
    {
        _monsterStateMachine = new MonsterStateMachine(this);
        MonsterAnimationData.GetParameterHash();
        _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
        Main.UIManager.CreateWorldUI<HealthBar>("HealthCanvas", transform);
    }

    protected override void Update()
    {
        _monsterStateMachine.HandleInput();
        _monsterStateMachine.StateUpdate();
    }

    protected override void FixedUpdate()
    {
        _monsterStateMachine.PhysicsUpdate();
    }

    private void SetSpawnPoint(Vector2 spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    [PunRPC]
    public void RPCDetroy()
    {
        Main.ResourceManager.Destroy(gameObject);
    }
}
