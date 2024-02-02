using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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
        transform.GetComponentInChildren<Slider>().value = ratio;
    }

    [PunRPC]
    public void RPCDetroy()
    {
        Main.ResourceManager.Destroy(gameObject);
    }
}
