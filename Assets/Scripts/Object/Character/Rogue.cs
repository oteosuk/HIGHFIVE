using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Character
{
    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<Stat>();
        CharacterSkill = GetComponent<RogueSkill>();
    }
    protected override void Start()
    {

        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
            base.FixedUpdate();
    }
    public override void OnNormalAttack()
    {
        base.OnNormalAttack();
        if (_playerStateMachine._player.targetObject != null && _playerStateMachine._player.targetObject.layer != (int)Define.Layer.Default)
        {
            _playerStateMachine._player.targetObject.GetComponent<Stat>()?.TakeDamage(Main.GameManager.SpawnedCharacter.stat.Attack, gameObject);
        }
        if (Main.GameManager.InGameObj.TryGetValue("RogueAttack01", out Object obj)) { AudioSource.clip = obj as AudioClip; }
        else { AudioSource.clip = Main.ResourceManager.Load<AudioClip>("Sounds/SFX/InGame/RogueAttack01"); }
        GetComponent<PhotonView>().RPC("ShareEffectSound", RpcTarget.Others, "RogueAttack01");
        Main.SoundManager.PlayEffect(AudioSource);
    }
}
