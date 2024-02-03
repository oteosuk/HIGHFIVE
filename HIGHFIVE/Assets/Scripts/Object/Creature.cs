using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviourPunCallbacks
{
    public Stat stat;
    public Rigidbody2D Rigidbody { get; protected set; }
    public Collider2D Controller { get; set; }
    public GameObject targetObject;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Controller = GetComponent<Collider2D>();
        stat = GetComponent<Stat>();
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
}
