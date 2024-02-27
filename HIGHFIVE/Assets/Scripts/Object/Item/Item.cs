using Photon.Pun;
using UnityEngine;

public class Item : MonoBehaviour
{
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //shooter의 정보
            collision.gameObject.GetComponent<Stat>()?.Heal(100);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}