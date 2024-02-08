
public class Tree : Monster
{
    private double _curDelay;
    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<Stat>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnNormalAttack()
    {
        if (targetObject != null)
        {
            targetObject.GetComponent<Stat>()?.TakeDamage(stat.Attack);
        }
        _curDelay = 1.0 / stat.AttackSpeed;
    }
}
