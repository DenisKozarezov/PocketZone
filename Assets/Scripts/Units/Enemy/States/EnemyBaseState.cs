namespace Core.Units.Enemy
{
    public abstract class EnemyBaseState : IState<EnemyController>
    {
        protected readonly IStateMachine<EnemyController> StateMachine;
        protected EnemyController Context => StateMachine.Context;
        protected ITransformable Transformable => Context.Transformable;

        protected EnemyBaseState(IStateMachine<EnemyController> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Exit();
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}