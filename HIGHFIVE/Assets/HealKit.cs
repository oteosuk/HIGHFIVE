using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
            Character myCharacter = collision.gameObject.GetComponent<Character>();
            PhotonView pv = myCharacter.GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                if (Main.GameManager.InGameObj.TryGetValue("HealPack", out Object obj)) { myCharacter.AudioSource.clip = obj as AudioClip; }
                else { myCharacter.AudioSource.clip = Main.ResourceManager.Load<AudioClip>("Sounds/SFX/InGame/HealPack"); }
                Main.SoundManager.PlayEffect(myCharacter.AudioSource);
                myCharacter.GetComponent<PhotonView>().RPC("ShareEffectSound", RpcTarget.Others, "HealPack");

                collision.gameObject.GetComponent<Stat>()?.Heal(_healAmount);
            }
            if (spawner != null) spawner.healRespawn(gameObject);
        }
    }
}