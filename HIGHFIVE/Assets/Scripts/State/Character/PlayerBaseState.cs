using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _playerStateMachine;
    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        this._playerStateMachine = playerStateMachine;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        if (_playerStateMachine._player.stat.CurHp <= 0) return;
        if (_playerStateMachine._player.Animator.GetBool(_playerStateMachine._player.PlayerAnimationData.ConfuseParameterHash)) return;
        ReadMoveInput();
        ReadSkillInput();
    }

    public virtual void PhysicsUpdate() { }

    public virtual void StateUpdate() { }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = _playerStateMachine._player.Input;
        input._playerActions.Move.canceled += OnMoveCanceled;
        input._playerActions.NormalAttack.started += OnReadyAttackStart;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = _playerStateMachine._player.Input;
        input._playerActions.Move.canceled -= OnMoveCanceled;
        input._playerActions.NormalAttack.started -= OnReadyAttackStart;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if(_playerStateMachine.moveInput == Vector2.zero)
        {
            return;
        }

        _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
    }

    protected virtual void  OnMove() { }

    protected virtual void StartAnimation(int hashValue)
    {
        _playerStateMachine._player.Animator.SetBool(hashValue, true);
    }

    protected virtual void StopAnimation(int hashValue)
    {
        _playerStateMachine._player.Animator.SetBool(hashValue, false);
    }
    private void OnReadyAttackStart(InputAction.CallbackContext context) { _playerStateMachine.isAttackReady = true; }


    private void RayToObjectAndSetTarget()
    {
        Vector2 mousePoint = _playerStateMachine._player.Input._playerActions.Move.ReadValue<Vector2>();
        if ((1515 <= mousePoint.x && mousePoint.x <= 1900) && (20 <= mousePoint.y && mousePoint.y <= 280)) { return; }
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);


        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red ));

        RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 10.0f, mask);

        _playerStateMachine.moveInput = raymousePoint;

        if (hit.collider?.gameObject != null)
        {
            _playerStateMachine._player.targetObject = hit.collider.gameObject;
            float distance = (hit.collider.transform.position - _playerStateMachine._player.transform.position).magnitude;

            if (_playerStateMachine._player.stat.AttackRange > distance)
            {
                _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
            }
            else
            {
                _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
            }
        }
        else
        {
            _playerStateMachine._player.targetObject = null;
            _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
        }
    }

    private void ReadMoveInput()
    {
        FlipCharacter(GetDir());
        //우클릭
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //Vector2 mousePoint = _playerStateMachine._player.Input._playerActions.Move.ReadValue<Vector2>();
            
            
            RayToObjectAndSetTarget();
            _playerStateMachine.InputKey = Define.InputKey.RightMouse;
        }

        //A누르고 좌클릭
        if (Mouse.current.leftButton.wasPressedThisFrame && _playerStateMachine.isAttackReady)
        {
            RayToObjectAndSetTarget();
            _playerStateMachine.InputKey = Define.InputKey.LeftMouse;
        }
    }
    private void ReadSkillInput()
    {
        //나중에 키세팅 기능 추가
        Character myCharacter = _playerStateMachine._player;
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            _playerStateMachine.InputKey = Define.InputKey.FirstSkill;
            if (myCharacter.CharacterSkill.FirstSkill.CanUseSkill())
            {
                _playerStateMachine.ChangeState(_playerStateMachine.PlayerFirstSkillState);
            }
        }
        else if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            _playerStateMachine.InputKey = Define.InputKey.SecondSkill;
            if (myCharacter.CharacterSkill.SecondSkill.CanUseSkill())
            {
                _playerStateMachine.ChangeState(_playerStateMachine.PlayerSecondSkillState);
            }
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _playerStateMachine.InputKey = Define.InputKey.ThirdSkill;
            if (myCharacter.CharacterSkill.ThirdSkill.CanUseSkill())
            {
                _playerStateMachine.ChangeState(_playerStateMachine.PlayerThirdSkillState);
            }
        }
    }

    private Vector2 GetDir()
    {
        Vector2 dir;
        if (_playerStateMachine._player.targetObject != null) { dir = (_playerStateMachine._player.targetObject.transform.position - _playerStateMachine._player.transform.position); }
        else { dir = (_playerStateMachine.moveInput - (Vector2)_playerStateMachine._player.transform.position); }
        return dir;
    }

    private void FlipCharacter(Vector2 dir)
    {
        Vector3 localScale = _playerStateMachine._player.transform.localScale;
        if (dir.x > 0) { _playerStateMachine._player.transform.localScale = new Vector3(-2f, localScale.y, 1); }
        else { _playerStateMachine._player.transform.localScale = new Vector3(2f, localScale.y, 1); }
    }
}
