using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HealKit : Item
{
    private HealKitSpawner spawner;
    private int _healAmount;

    protected override void Start()
    {
        base.Start();
        spawner = transform.parent.GetComponent<HealKitSpawner>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Character collisionCharact = collision.gameObject.GetComponent<Character>();
            PhotonView pv = collisionCharact.GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                if (Main.GameManager.InGameObj.TryGetValue("HealPack", out Object obj)) { collisionCharact.AudioSource.clip = obj as AudioClip; }
                else { collisionCharact.AudioSource.clip = Main.ResourceManager.Load<AudioClip>("Sounds/SFX/InGame/HealPack"); }
                Main.SoundManager.PlayEffect(collisionCharact.AudioSource);
                collisionCharact.GetComponent<PhotonView>().RPC("ShareEffectSound", RpcTarget.Others, "HealPack");

                _healAmount = collisionCharact.stat.MaxHp / 2;
                collision.gameObject.GetComponent<Stat>()?.Heal(_healAmount);
            }
            if (spawner != null) spawner.healRespawn(gameObject);
        }
    }
}