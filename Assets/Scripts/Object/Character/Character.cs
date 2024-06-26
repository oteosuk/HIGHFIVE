using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Character : Creature
{
    private PhotonView _photonView;
    public PlayerStateMachine _playerStateMachine;

    public PlayerInput Input { get; protected set; }
    public PlayerAnimationData PlayerAnimationData { get; set; }
    public Animator Animator { get; set; }
    public SkillController SkillController { get; private set; }
    public CharacterSkill CharacterSkill { get; protected set; }
    public NavMeshAgent NavMeshAgent { get; protected set; }
    public AudioSource AudioSource { get; private set; }
    public Vector2 MousePoint { get; private set; } = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();
        _photonView = GetComponent<PhotonView>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Input = GetComponent<PlayerInput>();
        Collider = GetComponent<Collider2D>();
        Animator = GetComponentInChildren<Animator>();
        PlayerAnimationData = new PlayerAnimationData();
        SkillController = GetComponent<SkillController>();
        BuffController = GetComponent<BuffController>();
        AudioSource = GetComponent<AudioSource>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        if (_photonView.IsMine) { NavMeshAgent.enabled = true; }
        NavMeshAgent.updateRotation = false;
        NavMeshAgent.updateUpAxis = false;
    }
    protected override void Start()
    {
        base.Start();
        PlayerAnimationData.Initialize();
        _playerStateMachine = new PlayerStateMachine(this);
        _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
        GetComponent<StatController>().dieEvent += OnDie;
        GetComponent<StatController>().moveSpeedChangeEvent += OnChangeSpeed;
        NavMeshAgent.speed = Main.GameManager.SpawnedCharacter.stat.AttackSpeed;
    }
    protected override void Update()
    {
        if (_photonView.IsMine)
        {
            _playerStateMachine.HandleInput();
            _playerStateMachine.StateUpdate();
            UpdateMousePoint();
        }
    }

    protected override void FixedUpdate()
    {
        if(_photonView.IsMine)
        {
            _playerStateMachine.PhysicsUpdate();
        }
    }

    private void UpdateMousePoint()
    {
        MousePoint = Input._playerActions.Move.ReadValue<Vector2>();
    }

    public void Revival(float respawnTime)
    {
        StartCoroutine(RespawnDelay(respawnTime));
    }

    IEnumerator RespawnDelay(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        Respawn();
    }

    private void Respawn()
    {
        if (Main.GameManager.page == Define.Page.Battle) return;
        _playerStateMachine._player.NavMeshAgent.enabled = false;
        _playerStateMachine._player.transform.position = Main.GameManager.CharacterSpawnPos;
        _playerStateMachine.moveInput = Main.GameManager.CharacterSpawnPos;
        _photonView.RPC("SetHpRPC", RpcTarget.All, _playerStateMachine._player.stat.MaxHp, _playerStateMachine._player.stat.MaxHp);
        int layer = Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Red : (int)Define.Layer.Blue;
        _photonView.RPC("SetLayer", RpcTarget.All, layer);
        _playerStateMachine._player.Animator.SetBool(_playerStateMachine._player.PlayerAnimationData.DieParameterHash, false);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        _playerStateMachine._player.NavMeshAgent.enabled = true;
    }

    private void OnChangeSpeed(float speed)
    {
        //Debug.Log(speed);
        NavMeshAgent.speed = speed;
    }
    private void OnDie()
    {
        BuffController.CancelUnSustainBuff();
        _playerStateMachine.ChangeState(_playerStateMachine._playerDieState);
    }

    ////////////////////////////////////
    ///////// Synchronizer /////////////
    ////////////////////////////////////

    [PunRPC]
    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

    [PunRPC]
    public void SetHpBarColor()
    {
        Image fillImage = null;
        foreach (Image component in gameObject.GetComponentsInChildren<Image>())
        {
            if (component.name == "Fill")
            {
                fillImage = component;
            }
        }
        if (fillImage != null)
        {
            if (gameObject.layer == (int)Define.Camp.Red)
            {
                fillImage.color = Define.GreenColor;
            }
            else
            {
                fillImage.color = Define.BlueColor;
            }
        }
    }

    [PunRPC]
    public void AddPlayer(int viewId)
    {
        if (!Main.NetworkManager.photonPlayerObject.ContainsKey(viewId) && !Main.NetworkManager.photonPlayer.ContainsKey(viewId))
        {
            Main.NetworkManager.photonPlayerObject.Add(viewId, gameObject);
            PhotonView photonView = PhotonView.Find(viewId);
            if (photonView != null)
            {
                Main.NetworkManager.photonPlayer.Add(viewId, photonView.Owner);
            }
        }
    }

    [PunRPC]
    public void ReceiveBuff(int viewId, int buffNum, int shooterId)//shooter추가
    {
        if (Main.NetworkManager.photonPlayerObject.TryGetValue(shooterId, out GameObject shooterObj))
        {
            if (Main.NetworkManager.photonPlayerObject.TryGetValue(viewId, out GameObject targetObject))
            {
                if (targetObject == Main.GameManager.SpawnedCharacter.gameObject)
                {
                    switch (buffNum)
                    {
                        case (int)Define.Buff.StunShot:
                            BaseBuff stunShotBuff = new StunShotBuff();
                            targetObject.GetComponent<Character>().BuffController.AddBuff(stunShotBuff, shooterObj);
                            break;
                        case (int)Define.Buff.Assassination:
                            BaseBuff assassinationBuff = new AssassinationBuff();
                            targetObject.GetComponent<Character>().BuffController.AddBuff(assassinationBuff, shooterObj);
                            break;
                    }
                }
            }
        }
    }
    [PunRPC]
    public void SyncActive(bool isActive)
    {
        gameObject.transform.Find("BerserkEffect")?.gameObject.SetActive(isActive);
    }

    [PunRPC]
    public void SyncLevel(int level, int viewId)
    {
        if (Main.NetworkManager.photonPlayerObject.TryGetValue(viewId, out GameObject obj))
        {
            obj.GetComponent<CharacterStat>().Level = level;
            Transform characterInfoObj = obj.transform?.Find("HealthCanvas")?.Find("CharacterInfo");
            if (characterInfoObj)
            {
                characterInfoObj.Find("Level").GetComponent<TMP_Text>().text = $"{level}Lv";
            }
        }
    }
    [PunRPC]
    public void SyncNickname(int viewId)
    {
        if (Main.NetworkManager.photonPlayer.TryGetValue(viewId, out Player player))
        {
            Transform characterInfoObj = transform?.Find("HealthCanvas")?.Find("CharacterInfo");
            if (characterInfoObj)
            {
                characterInfoObj.Find("Nickname").GetComponent<TMP_Text>().text = player.NickName;
            }
        }
    }

    [PunRPC]
    public void ShareEffectSound(string clipName)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (Main.GameManager.InGameObj.TryGetValue(clipName, out Object obj))
        {
            audioSource.clip = obj as AudioClip;
            Main.SoundManager.PlayEffect(audioSource);
        }
    }

    [PunRPC]
    public void SyncMiniMapColor()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if ((int)Define.Layer.Red == gameObject.layer)
        {
            spriteRenderer.color = Define.RedColor;
        }
        else { spriteRenderer.color = Color.blue; }
    }
}
