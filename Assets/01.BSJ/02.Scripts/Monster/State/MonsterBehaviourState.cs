using System;

public class MonsterBehaviourState : State
{
    protected Monster monster;
    protected MonsterBehaviour behaviour;

    public MonsterBehaviourState(Monster monster, MonsterBehaviour behaviour)
    {
        this.monster = monster;
        this.behaviour = behaviour;
    }

    public override void Enter()
    {
        behaviour.OnBehaviourStart(monster);
    }

    public override void Tick(float deltaTime)
    {
        behaviour.OnBehaviourUpdate(monster);
    }

    public override void Exit()
    {
        behaviour.OnBehaviourEnd(monster);
    }
}