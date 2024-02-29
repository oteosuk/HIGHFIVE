using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPortal : MonoBehaviour
{
    [SerializeField] Transform _arrivalPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Character myChracter = Main.GameManager.SpawnedCharacter;
            PhotonView pv = myChracter.GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                myChracter.NavMeshAgent.enabled = false;
                collision.gameObject.transform.position = _arrivalPoint.position;
                myChracter.NavMeshAgent.enabled = true;
                myChracter._playerStateMachine.moveInput = _arrivalPoint.position;

                Camera.main.transform.position = new Vector3(_arrivalPoint.position.x, _arrivalPoint.position.y, Camera.main.transform.position.z);
            }
        }
    }
}
