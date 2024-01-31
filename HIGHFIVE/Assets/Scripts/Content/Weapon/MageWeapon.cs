using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageWeapon : MonoBehaviourPunCallbacks
{
    private GameObject _targetObject;
    private Rigidbody2D _rigidbody;

    //private void OnEnable()
    //{
    //    projectileRb = GetComponent<Rigidbody2D>();
    //    Vector2 dir = _targetObject.transform.position - transform.position;
    //    projectileRb.velocity = dir.normalized * 10.0f;
    //}
    //private void Start()
    //{
    //    projectileRb = GetComponent<Rigidbody2D>();
    //    Vector2 dir = _targetObject.transform.position - transform.position;
    //    projectileRb.velocity = dir.normalized * 10.0f;
    //}

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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red));
        if (((1 << collision.gameObject.layer) & mask) != 0)
        {
            //Main.GameManager.SpawnedCharacter.stat.Attack 나중에 교체
            collision.transform.GetComponentInChildren<Receiver>()?.TakeDamage(15);
            //풀링
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            //GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
        }
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

    [PunRPC]
    public void Destroy()
    {
        Main.ResourceManager.Destroy(gameObject);
    }
}
