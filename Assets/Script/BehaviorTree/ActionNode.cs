using UnityEngine;

public class ActionNode : BehaviorTreeNode
{
    private System.Func<bool> action;

    public ActionNode(System.Func<bool> action)
    {
        this.action = action;
    }
    
    public override State Tick()
    {
        if (action())
        {
            return State.Success; // ถ้าสำเร็จ
        }
        return State.Failure; // ถ้าไม่สำเร็จ
    }
}
