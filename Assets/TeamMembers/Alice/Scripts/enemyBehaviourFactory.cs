
using UnityEngine;
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
        Vector2 minBoundary = enemy.minBoundary; //Get boundaries from the enemy instance
        Vector2 maxBoundary = enemy.maxBoundary;
        switch (behaviorType)
        {
            case EnemyBehaviors.Idle:
                return new IdleBehavior(enemy, minBoundary, maxBoundary);
            case EnemyBehaviors.Attack:
                return new AttackBehavior(enemy);
            case EnemyBehaviors.Patrol:
                return new PatrolBehavior(enemy);
            default:
                return new IdleBehavior(enemy,minBoundary, maxBoundary);
        }
    }
}
