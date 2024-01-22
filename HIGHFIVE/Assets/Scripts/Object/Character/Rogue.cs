using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Character
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {

        base.Start();
        stat = Util.GetOrAddComponent<RogueStat>(gameObject);
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
