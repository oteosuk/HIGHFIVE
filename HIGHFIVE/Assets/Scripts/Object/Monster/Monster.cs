using Photon.Pun;
using UnityEngine;

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
        MonsterAnimationData = new MonsterAnimationData();
        BuffController = GetComponent<BuffController>();
    }

    protected override void Start()
    {
        base.Start();
        _monsterStateMachine = new MonsterStateMachine(this);
        MonsterAnimationData.GetParameterHash();
        _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
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

    public virtual void SetAnimation(float angle)
    {

    }

    public virtual void StopAnimationAll()
    {

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