using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviourPunCallbacks
{
    public Stat stat;
    public Rigidbody2D Rigidbody { get; protected set; }
    public BuffController BuffController { get; protected set; }
    public Collider2D Collider { get; set; }

    [HideInInspector] public GameObject targetObject;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
    }
    protected virtual void Start()
    {
        Main.UIManager.CreateWorldUI<HealthBar>("HealthCanvas", transform);
    }

    protected virtual void Update()
    {
        
    }
    protected virtual void FixedUpdate()
    {

    }
    public virtual void OnNormalAttack() { }

    [PunRPC]
    public void SetHpRPC(int curHp, int maxHp)
    {
        gameObject.GetComponent<Stat>().MaxHp = maxHp;
        gameObject.GetComponent<Stat>().CurHp = curHp;
    }
}