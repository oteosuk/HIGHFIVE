using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageWeapon : MonoBehaviour
{
    private GameObject _targetObject;
    private Rigidbody2D projectileRb;
    private void OnEnable()
    {
        projectileRb = GetComponent<Rigidbody2D>();
        Vector2 dir = _targetObject.transform.position - transform.position;
        projectileRb.velocity = dir.normalized * 10.0f;
    }

    //private void Start()
    //{
    //    projectileRb = GetComponent<Rigidbody2D>();
    //    Vector2 dir = _targetObject.transform.position - transform.position;
    //    projectileRb.velocity = dir.normalized * 10.0f;
    //}

    private void Update()
    {
        Vector2 dir = _targetObject.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red));
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("Damage");
            //Main.GameManager.SpawnedCharacter.stat.Attack 나중에 교체
            collision.gameObject.GetComponent<DamageReceiver>()?.TakeDamage(15);
            //풀링
            Main.ResourceManager.Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        _targetObject = target;
    }
}
