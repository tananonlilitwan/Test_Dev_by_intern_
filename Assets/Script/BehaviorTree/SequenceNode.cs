using UnityEngine;
using System.Collections.Generic;

public class SequenceNode : BehaviorTreeNode
{
    private List<BehaviorTreeNode> nodes;

    public SequenceNode(List<BehaviorTreeNode> nodes)
    {
        this.nodes = nodes;
    }

    public override State Tick()
    {
        foreach (var node in nodes)
        {
            var state = node.Tick();
            if (state == State.Failure)
                return State.Failure;
        }

        return State.Success;
    }
}
