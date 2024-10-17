public enum EnemyBehaviors
{
    Idle,
    Attack,
    Patrol
}

public class EnemyBehaviorFactory
{
    public static EnemyBaseBehavior GetBehavior(EnemyBehaviors behaviorType, enemy enemy)
    {
        switch (behaviorType)
        {
            case EnemyBehaviors.Idle:
                return new IdleBehavior(enemy);
            case EnemyBehaviors.Attack:
                return new AttackBehavior(enemy);
            /*case EnemyBehaviors.Patrol:
                return new PatrolBehavior(enemy);*/
            default:
                return new IdleBehavior(enemy);
        }
    }
}
