using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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

    private void Start()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        _targetObject = myCharacter.targetObject;
        _rigidbody = GetComponent<Rigidbody2D>();
        //Vector2 dir = myCharacter.targetObject.transform.position - transform.position;
        //PhotonView targetPhotonView = Util.GetOrAddComponent<PhotonView>(myCharacter.targetObject);

        //GetComponent<ShooterInfoController>().CallShooterInfoEvent(myCharacter.gameObject);
        //GetComponent<PhotonView>().RPC("SetTarget", RpcTarget.All, targetPhotonView.ViewID);
        //GetComponent<PhotonView>().RPC("ToTarget", RpcTarget.All, 5.0f, dir.x, dir.y);
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
                collision.gameObject.GetComponent<Stat>()?.TakeDamage(Main.GameManager.SpawnedCharacter.stat.Attack, Main.GameManager.SpawnedCharacter.gameObject);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    private void GetShooterInfo(GameObject shooter)
    {
        _shooter = shooter;
    }

    //[PunRPC]
    //public void SetTarget(int viewId)
    //{
    //    PhotonView targetPhotonView = PhotonView.Find(viewId);
    //    if (targetPhotonView != null)
    //    {
    //        _targetObject = targetPhotonView.gameObject;
    //    }
    //}


    //[PunRPC]
    //public void ToTarget(float speed, float posX, float posY)
    //{
    //    Vector2 dir = new Vector2(posX, posY);
    //    _rigidbody = GetComponent<Rigidbody2D>();
    //    _rigidbody.velocity = dir.normalized * speed;
    //}
}
