using UnityEngine;

public class Item : MonoBehaviour
{
    protected virtual void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("힐킷 충돌");
            collision.gameObject.GetComponent<Stat>()?.Heal(100);
            Destroy(gameObject);
        }
    }
}