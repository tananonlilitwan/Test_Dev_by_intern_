using UnityEngine;

public class ConditionNode : BehaviorTreeNode
{
    private System.Func<bool> condition;

    public ConditionNode(System.Func<bool> condition)
    {
        this.condition = condition;
    }

    public override State Tick()
    {
        if (condition())
        {
            return State.Success;
        }
        return State.Failure;
    }
}
