using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : Monster
{
    protected override void Awake()
    {
        base.Awake();
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

    [PunRPC]
    public void SyncHpRatio(float ratio)
    {
        Debug.Log("se");
        transform.GetComponentInChildren<Slider>().value = ratio;
    }
}
