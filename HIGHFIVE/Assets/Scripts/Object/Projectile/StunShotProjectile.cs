using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunShotProjectile : MonoBehaviour
{
    private ShooterInfoController _shooterInfoController;
    private GameObject _shooter;
    private GameObject _targetObject;
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _shooterInfoController = GetComponent<ShooterInfoController>();
        //_shooterInfoController.shooterInfoEvent += GetShooterInfo;
    }
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
                collision.gameObject.GetComponent<Stat>().TakeDamage(Main.GameManager.SpawnedCharacter.CharacterSkill.SecondSkill.skillData.damage, _shooter.gameObject);
                if (collision.gameObject.GetComponent<Character>())
                {
                    PhotonView targetPv = collision.gameObject.GetComponent<PhotonView>();
                    int viewId = _shooter.GetComponent<PhotonView>().ViewID;
                    _shooter.GetComponent<PhotonView>().RPC("ReceiveBuff", RpcTarget.Others, targetPv.ViewID, Define.Buff.StunShot, viewId);
                }
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
