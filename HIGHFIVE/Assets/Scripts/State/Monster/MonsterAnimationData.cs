using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationData
{
    private string idleParmeterName = "isIdle";
    private string moveParmeterName = "Move";
    private string AttackParmeterName = "Attack";

    public int IdleParameterHash { get; private set; }
    public int MoveParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParmeterName);
        MoveParameterHash = Animator.StringToHash(moveParmeterName);
        AttackParameterHash = Animator.StringToHash(AttackParmeterName);
    }
}

