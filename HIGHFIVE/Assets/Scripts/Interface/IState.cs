public interface IState
{
    // 상태에 진입할 때
    public void Enter();
    // 상태를 해제
    public void Exit();
    // State 도중에 입력에 따라 동작 처리
    public void HandleInput();
    // 현재 State에 Update를 호출
    public void StateUpdate();
    // 물리 업데이트
    public void PhysicsUpdate();
}