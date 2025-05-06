using UnityEngine;
using System.Collections.Generic;

public class SelectorNode : BehaviorTreeNode
{
    private List<BehaviorTreeNode> nodes;

    public SelectorNode(List<BehaviorTreeNode> nodes)
    {
        this.nodes = nodes;
    }

    public override State Tick()
    {
        foreach (var node in nodes)
        {
            var state = node.Tick();
            if (state != State.Failure)
                return state;
        }

        return State.Failure;
    }
}
