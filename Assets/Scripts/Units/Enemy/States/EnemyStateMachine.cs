using System.Linq;
using System.Collections.Generic;

namespace Core.Units.Enemy
{
    public class EnemyStateMachine : IStateMachine<EnemyController>
    {
        private List<IState<EnemyController>> _states;
        public EnemyController Context { get; private set; }
        public IState<EnemyController> CurrentState { get; private set; }

        public EnemyStateMachine(EnemyController enemy, EnemyModel model)
        {
            Context = enemy;
            _states = new List<IState<EnemyController>>()
            {
                new EnemyPatrolState(this, model),
                new EnemyAttackState(this, model),
                new EnemyChasingState(this, model)
            };
        }
        public void SwitchState<State>() where State : IState<EnemyController>
        {
            CurrentState?.Exit();
            CurrentState = _states.FirstOrDefault(state => state is State);
            CurrentState.Enter();
        }
    }
}