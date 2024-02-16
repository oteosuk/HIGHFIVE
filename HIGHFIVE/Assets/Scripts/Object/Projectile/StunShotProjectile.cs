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
        _shooterInfoController.shooterInfoEvent += GetShooterInfo;
    }
    private void Update()
    {
        if (_targetObject != null)
        {
            Vector2 dir = _targetObject.transform.position - transform.position;
            _rigidbody.velocity = dir.normalized * 5.0f;
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
                _targetObject.GetComponent<Stat>().TakeDamage(Main.GameManager.SpawnedCharacter.CharacterSkill.SecondSkill.skillData.damage, _shooter.gameObject);
                BaseBuff stunShotBuff = new StunShotBuff();
                if (_targetObject.GetComponent<Character>())
                {
                    PhotonView targetPv = _targetObject.GetComponent<PhotonView>();
                    if (Main.NetworkManager.photonPlayer.TryGetValue(targetPv.ViewID, out Player targetPlayer))
                    {
                        _shooter.GetComponent<PhotonView>().RPC("ReceiveBuff", RpcTarget.Others, targetPv.ViewID, Define.Buff.StunShot);
                    }
                }

                else { _targetObject.GetComponent<Creature>().BuffController?.AddBuff(stunShotBuff); }
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    private void GetShooterInfo(GameObject shooter)
    {
        _shooter = shooter;
    }

    [PunRPC]
    public void SetTarget(int viewId)
    {
        PhotonView targetPhotonView = PhotonView.Find(viewId);
        if (targetPhotonView != null)
        {
            _targetObject = targetPhotonView.gameObject;
        }
    }


    [PunRPC]
    public void ToTarget(float speed, float posX, float posY)
    {
        Vector2 dir = new Vector2(posX, posY);
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = dir.normalized * speed;
    }
}
