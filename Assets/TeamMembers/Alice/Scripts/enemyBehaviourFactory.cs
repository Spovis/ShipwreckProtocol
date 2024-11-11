
using UnityEngine;
public enum EnemyBehaviors //different types of behaviors
{
    Idle,
    Attack,
    Patrol
}

public class EnemyBehaviorFactory
{
    //get the behavior type and the enemy instance
    public static EnemyBaseBehavior GetBehavior(EnemyBehaviors behaviorType, enemy enemy){
        Vector2 minBoundary = enemy.minBoundary; //Get boundaries from enemy
        Vector2 maxBoundary = enemy.maxBoundary;
        switch (behaviorType){
            case EnemyBehaviors.Idle:
                return new IdleBehavior(enemy, minBoundary, maxBoundary); //I use those in IdleBehavior
            case EnemyBehaviors.Attack:
                return new AttackBehavior(enemy);
            case EnemyBehaviors.Patrol:
                return new PatrolBehavior(enemy);
            default:
                return new IdleBehavior(enemy,minBoundary, maxBoundary);
        }
    }
}
