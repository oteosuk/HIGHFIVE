using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Character
{
    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<Stat>();
        CharacterSkill = GetComponent<RogueSkill>();
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
}
