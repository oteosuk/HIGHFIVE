using UnityEngine;

public class Orc : Monster
{
    private double _curDelay;
    Vector3 originalScale;

    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<Stat>();
        originalScale = transform.localScale;
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

    public override void SetAnimation(float angle)
    {
        if (angle >= -90 && angle <= 90)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // x 스케일을 -1를 곱하여 좌우 반전
            animSet("isMove");
        }
        else if (angle > 90 && angle <= 180 || angle >= -180 && angle < -90)
        {
            transform.localScale = originalScale; // 원래 스케일
            animSet("isMove");
        }
        else
        {
            Debug.Log("예외 앵글" + angle);
        }
    }

    // 해당 bool 파라미터가 true가 아닐때만 true가 되게끔
    private void animSet(string animName)
    {
        if (!Animator.GetBool(animName))
        {
            StopAnimationAll();
            Animator.SetBool(animName, true);
        }
    }

    // anim bool파라미터 초기화
    public override void StopAnimationAll()
    {
        Animator.SetBool(MonsterAnimationData.IdleParameterHash, false);
        Animator.SetBool(MonsterAnimationData.MoveParameterHash, false);
    }
}
