using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MageWeapon : MonoBehaviourPunCallbacks
{
    private GameObject _targetObject;
    private Rigidbody2D _rigidbody;
    private GameObject _shooter;

    private void Start()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        _rigidbody = GetComponent<Rigidbody2D>();
        _targetObject = myCharacter.targetObject;
        _shooter = myCharacter.gameObject;
    }

    private void Update()
    {
        if (_targetObject != null)
        {
            Vector2 dir = _targetObject.transform.position - transform.position;
            _rigidbody.velocity = dir.normalized * 10.0f;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
        else
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            } 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _targetObject)
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                collision.gameObject.GetComponent<Stat>()?.TakeDamage(Main.GameManager.SpawnedCharacter.stat.Attack, _shooter);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
