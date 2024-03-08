using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StabbingBuff : BaseBuff
{
    private BuffDBEntity _stabbingBuffData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("광폭화", out BuffDBEntity stabbingBuffData))
        {
            _stabbingBuffData = stabbingBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        if (Main.GameManager.InGameObj.TryGetValue("Berserk", out Object obj)) { buffData.buffSprite = obj as Sprite; }
        else { buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/Berserk"); }
        
        buffData.type = typeof(StabbingBuff);
        buffData.duration = _stabbingBuffData.durationTime;
        buffData.curTime = 0;
        buffData.damage = _stabbingBuffData.damage + myCharacter.stat.Attack;
        buffData.effectTime = _stabbingBuffData.effectTime;
        buffData.isSustainBuff = _stabbingBuffData.isSustain;
        buffData.buffName = "광폭화";
        buffData.info = $"자신의 이동속도가 {_stabbingBuffData.effectTime}초 동안 {_stabbingBuffData.moveSpd}증가하고" +
            $"{_stabbingBuffData.durationTime}초 동안 {_stabbingBuffData.damage + Main.GameManager.SpawnedCharacter.stat.Attack}데미지만큼 평타 강화가 된다";
    }
    public override IEnumerator ApplyEffect(GameObject target, GameObject shooter = null)
    {
        myCharacter.transform.Find("BerserkEffect").gameObject.SetActive(true);
        myCharacter.GetComponent<PhotonView>().RPC("SyncActive", RpcTarget.All, true);
        yield return new WaitForSeconds(buffData.effectTime);
        target.GetComponent<Stat>().MoveSpeed -= _stabbingBuffData.moveSpd;
        yield return new WaitForSeconds(buffData.duration - buffData.effectTime);
        myCharacter.transform.Find("BerserkEffect").gameObject.SetActive(false);
        myCharacter.GetComponent<PhotonView>().RPC("SyncActive", RpcTarget.All, false);
        myCharacter.SkillController.CallSkillExecute(myCharacter.CharacterSkill.FirstSkill);
    }

    public override void Activation()
    {
        myCharacter.stat.MoveSpeed += _stabbingBuffData.moveSpd;
    }

    public override void Deactivation() { }
    public override void RenewalInfo()
    {
        buffData.info = $"자신의 이동속도가 {_stabbingBuffData.effectTime}초 동안 {_stabbingBuffData.moveSpd}증가하고" +
            $"{_stabbingBuffData.durationTime}초 동안 {_stabbingBuffData.damage + Main.GameManager.SpawnedCharacter.stat.Attack}데미지만큼 평타 강화가 된다";
    }
}