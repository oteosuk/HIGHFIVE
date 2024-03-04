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
            Character myCharacter = collision.gameObject.GetComponent<Character>();
            PhotonView pv = myCharacter.GetComponent<PhotonView>();
            myCharacter.NavMeshAgent.enabled = false;
            collision.gameObject.transform.position = _arrivalPoint.position;
            myCharacter._playerStateMachine.moveInput = _arrivalPoint.position;
            if (pv.IsMine)
            {
                Camera.main.transform.position = new Vector3(_arrivalPoint.position.x, _arrivalPoint.position.y, Camera.main.transform.position.z);
            }
            myCharacter.NavMeshAgent.enabled = true;
        }
    }
}
