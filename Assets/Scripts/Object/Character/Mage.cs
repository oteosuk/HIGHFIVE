using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage : Character
{
    [SerializeField] Transform _tip;
    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<Stat>();
        CharacterSkill = GetComponent<MageSkills>();
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
            Main.ResourceManager.Instantiate("SkillEffect/MageWeapon", _tip.position, syncRequired: true);
            if (Main.GameManager.InGameObj.TryGetValue("MageAttack01", out Object obj)) { AudioSource.clip = obj as AudioClip; }
            else { AudioSource.clip = Main.ResourceManager.Load<AudioClip>("Sounds/SFX/InGame/MageAttack01"); }
            GetComponent<PhotonView>().RPC("ShareEffectSound", RpcTarget.Others, "MageAttack01");
            Main.SoundManager.PlayEffect(AudioSource);
        }
    }
}
