using UnityEngine;

public abstract class BehaviorTreeNode
{
    // State ของ Node 
    public enum State 
    {
        Running, // กำลังทำ
        Success, // เสร็จ
        Failure // ไม่เสร็จ
    }

    public abstract State Tick();
}
