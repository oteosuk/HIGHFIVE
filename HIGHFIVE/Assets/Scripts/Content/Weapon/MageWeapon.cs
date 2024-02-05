using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageWeapon : MonoBehaviourPunCallbacks
{
    private GameObject _targetObject;
    private Rigidbody2D _rigidbody;
    private ShooterInfoController _shooterInfoController;
    private GameObject _shooter;

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
                //나중에 교체
                Debug.Log(Main.GameManager.SpawnedCharacter.stat.Attack);
                collision.gameObject.GetComponent<Stat>()?.TakeDamage(Main.GameManager.SpawnedCharacter.stat.Attack, _shooter);
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
