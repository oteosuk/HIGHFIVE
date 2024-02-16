using UnityEngine;

public class Tree : Monster
{
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
    }

    public override void SetAnimation(float angle)
    {
        if (angle >= 45 && angle < 135)
        {
            //Debug.Log("윗방향  :  " + angle);
            animSet("isUp");
        }
        else if (angle >= 135 && angle <= 180 || angle >= -180 && angle < -135)
        {
            //Debug.Log("왼쪽방향  :  " + angle);
            animSet("isLeft");
        }
        else if (angle >= -135 && angle < -45)
        {
            //Debug.Log("아래방향  :  " + angle);
            animSet("isDown");
        }
        else if (angle >= -45 && angle < 0 || angle >= 0 && angle < 45)
        {
            //Debug.Log("오른쪽방향  :  " + angle);
            animSet("isRight");
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
        Animator.SetBool(MonsterAnimationData.LeftParameterHash, false);
        Animator.SetBool(MonsterAnimationData.RightParameterHash, false);
        Animator.SetBool(MonsterAnimationData.UpParameterHash, false);
        Animator.SetBool(MonsterAnimationData.DownParameterHash, false);
    }
}
