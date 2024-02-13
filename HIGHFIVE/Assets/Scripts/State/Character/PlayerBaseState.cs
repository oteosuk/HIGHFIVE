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
            RayToObjectAndSetTarget();
        }

        //A누르고 좌클릭
        if (Mouse.current.leftButton.wasPressedThisFrame && _playerStateMachine.isAttackReady)
        {
            RayToObjectAndSetTarget();
        }
    }
    private void ReadSkillInput()
    {
        //나중에 키세팅 기능 추가
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Character myCharacter = _playerStateMachine._player;
            if (myCharacter.CharacterSkill.FirstSkill.skillData.isUse)
            {
                _playerStateMachine.ChangeState(_playerStateMachine.PlayerFirstSkillState);
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
        if (dir.x > 0) { _playerStateMachine._player.transform.localScale = new Vector3(-1.5f, localScale.y, 1); }
        else { _playerStateMachine._player.transform.localScale = new Vector3(1.5f, localScale.y, 1); }
    }
}
