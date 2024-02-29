using TMPro;
using UnityEngine;

public class HealKit : Item
{
    private HealKitSpawner spawner;
    private readonly int _healAmount = 100;

    protected override void Start()
    {
        base.Start();
        spawner = transform.parent.GetComponent<HealKitSpawner>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Stat>()?.Heal(_healAmount);
            if (spawner != null) spawner.healRespawn(gameObject);
        }
    }
}