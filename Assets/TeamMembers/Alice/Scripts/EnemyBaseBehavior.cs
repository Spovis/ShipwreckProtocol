public abstract class EnemyBaseBehavior
{
    /*makes a field called enemy. This is assigned an enemy object below
    allows my subclasses to use the enemy object  and make it do stuff*/
    protected enemy enemy;

    /* this assigns an enemy object to that field i just created- This is talking about the instance of the class*/
    public EnemyBaseBehavior(enemy enemy){
        this.enemy = enemy;
    }

//these are my virtual static functions that I can override in each specific behavior
    public virtual void OnEnterBehavior() { }
    public virtual void OnExitBehavior() { }
    public virtual void OnBehaviorUpdate() { }
    public virtual void OnBehaviorCollisionEnter2D() { }
    public virtual void OnBehaviorTriggerEnter2D() { }
}