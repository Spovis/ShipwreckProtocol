public abstract class EnemyBaseBehavior
{
    protected enemy enemy;

    public EnemyBaseBehavior(enemy enemy)
    {
        this.enemy = enemy;
    }

    public virtual void OnEnterBehavior() { }
    public virtual void OnExitBehavior() { }
    public virtual void OnBehaviorUpdate() { }
    public virtual void OnBehaviorCollisionEnter2D() { }
    public virtual void OnBehaviorTriggerEnter2D() { }
}