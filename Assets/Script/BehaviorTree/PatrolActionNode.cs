using UnityEngine;

public class PatrolActionNode : BehaviorTreeNode
{
    private AIRocket aiRocket;
    private float patrolRadius = 2f; // รอบวงการเดินวน 
    private float patrolSpeed = 1f;  // ความเร็วในการเดิน
    private float angle;  
    
    public PatrolActionNode(AIRocket rocket)
    {
        this.aiRocket = rocket;
    }

    public override State Tick()
    {
        angle += patrolSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * patrolRadius;
        Vector3 targetPos = aiRocket.basePoint.position + offset;
        aiRocket.MoveTowards(targetPos);
        return State.Running; 
    }
}
