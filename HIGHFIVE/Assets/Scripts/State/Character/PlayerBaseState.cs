using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _playerStateMachine;
    private CameraMover cameraMover;
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
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void StateUpdate()
    {
        if (_playerStateMachine._player.stat.CurHp <= 0)
        {
            Debug.Log("gd");
            OnDie();
        }
    }

    protected virtual void AddInputActionsCallbacks()
    {
        if (_playerStateMachine != null && _playerStateMachine._player != null && _playerStateMachine._player.Input != null)
        {
            PlayerInput input = _playerStateMachine._player.Input;
            input._playerActions.Move.canceled += OnMoveCanceled;
            input._playerActions.NormalAttack.started += OnReadyAttackStart;
        }
        else
        {
            Debug.LogError("PlayerStateMachine 또는 Player 또는 PlayerInput이 null입니다.");
        }
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

    protected virtual void OnDie()
    {
        _playerStateMachine.ChangeState(_playerStateMachine._playerDieState);
    }

    protected virtual void StartAnimation(int hashValue)
    {
        _playerStateMachine._player.Animator.SetBool(hashValue, true);
    }

    protected virtual void StopAnimation(int hashValue)
    {
        _playerStateMachine._player.Animator.SetBool(hashValue, false);
    }
    private void OnReadyAttackStart(InputAction.CallbackContext context) { _playerStateMachine.isAttackReady = true; }

    private void MinimapCameraMove()
    {
        cameraMover = GameObject.FindObjectOfType<CameraMover>();

        if (cameraMover == null)
        {
            Debug.LogError("CameraMover 스크립트를 찾을 수 없습니다.");
        }

        Vector2 mousePoint = _playerStateMachine._player.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        float xRatio;
        float yRatio;

        if ((1515 <= mousePoint.x && mousePoint.x <= 1900) && (20 <= mousePoint.y && mousePoint.y <= 280)) // 1515 미니맵왼쪽x 1900 미니맵오른쪽x, 20 미니맵아래, 280 미니맵위
        {
            xRatio = (mousePoint.x - 1515f) / 385f;
            yRatio = (mousePoint.y - 20f) / 260f;
            raymousePoint.x = -52 + xRatio * 102; // 맵 실제좌표의 맨 왼쪽부분(-52)   *100은 맵 가로길이
            raymousePoint.y = -20 + yRatio * 50; // 맵 실제좌표의 맨 아래쪽부분(-20)   *50은 맵 세로길이
            cameraMover.MinimapCamera(raymousePoint);
        }
    }

    private void RayToObjectAndSetTarget()
    {
        Vector2 mousePoint = _playerStateMachine._player.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        Debug.Log(mousePoint + " " + raymousePoint);
        float xRatio;
        float yRatio;

        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red ));

        //minimap관련
        if ((1515 <= mousePoint.x && mousePoint.x <= 1900) && (20 <= mousePoint.y && mousePoint.y <= 280)) // 1515 미니맵왼쪽x 1900 미니맵오른쪽x, 20 미니맵아래, 280 미니맵위
        {
            xRatio = (mousePoint.x - 1515f) / 385f;
            yRatio = (mousePoint.y - 20f) / 260f;
            raymousePoint.x = -52 + xRatio * 102; // 맵 실제좌표의 맨 왼쪽부분(-52)   *100은 맵 가로길이
            raymousePoint.y = -20 + yRatio * 50; // 맵 실제좌표의 맨 아래쪽부분(-20)   *50은 맵 세로길이
            //Debug.Log("미니맵쪽 클릭" + raymousePoint);
            _playerStateMachine.moveInput = raymousePoint;
        }
        else
        {
            //Debug.Log("일반땅 클릭" + Camera.main.ScreenToWorldPoint(mousePoint));
            _playerStateMachine.moveInput = Camera.main.ScreenToWorldPoint(mousePoint);
        }

        RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 10.0f, mask);

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
        //우클릭
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            RayToObjectAndSetTarget();
        }

        //좌클릭
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            MinimapCameraMove();
        }

        //A누르고 좌클릭
        if (Mouse.current.leftButton.wasPressedThisFrame && _playerStateMachine.isAttackReady)
        {
            RayToObjectAndSetTarget();
        }
    }
}
