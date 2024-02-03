using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviourPunCallbacks
{
    public Stat stat;
    public Rigidbody2D Rigidbody { get; protected set; }
    public Collider2D Collider { get; set; }
    public GameObject targetObject;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
    }
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }
    protected virtual void FixedUpdate()
    {
    }
    public virtual void OnNormalAttack() { }


    [PunRPC]
    public void SyncHpRatio(float ratio)
    {
        transform.GetComponentInChildren<Slider>().value = ratio;
    }
}
