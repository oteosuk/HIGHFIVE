using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    private ShooterInfoController _shooterInfoController;
    private Animator _animator;
    private GameObject _shooter;
    private Vector3 startingPosition;
    private float maxDistance;
    private float currentDistance;
    private void Awake()
    {
        _shooterInfoController = GetComponent<ShooterInfoController>();
        _animator = transform.Find("FireBall").GetComponent<Animator>();
        _shooterInfoController.shooterInfoEvent += GetShooterInfo;
    }
    private void Start()
    {
        startingPosition = transform.position;
        maxDistance = Main.GameManager.SpawnedCharacter.CharacterSkill.FirstSkill.skillData.skillRange;
    }
    private void Update()
    {
        currentDistance = Vector3.Distance(startingPosition, transform.position);

        if (currentDistance >= maxDistance)
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int enemyCamp = Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red;
        if (collision.gameObject.layer == (int)Define.Layer.Monster || collision.gameObject.layer == enemyCamp)
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                //shooter의 정보
                collision.gameObject.GetComponent<Stat>()?.TakeDamage(Main.GameManager.SpawnedCharacter.CharacterSkill.FirstSkill.skillData.damage, _shooter);
                CapsuleCollider2D collider = gameObject.GetComponent<CapsuleCollider2D>();
                Destroy(collider);
                _animator.SetBool("isTrigger", true);
                GetComponent<PhotonView>().RPC("SyncParameter", RpcTarget.All, true);
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero * 0;
            }
        }
    }

    private void GetShooterInfo(GameObject shooter)
    {
        _shooter = shooter;
    }

    [PunRPC]
    private void SyncParameter(bool isTrigger)
    {
        _animator.SetBool("isTrigger", isTrigger);
    }
}
