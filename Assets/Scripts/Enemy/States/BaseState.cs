public abstract class BaseState
{
    public Enemy enemy;
    public StateMachine stateMachine;
    public StateSoldier stateSoldier;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
