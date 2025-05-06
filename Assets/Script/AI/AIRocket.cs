using UnityEngine;
using System.Collections.Generic;

public class AIRocket : MonoBehaviour
{
    public Transform target;  // เป้าหมายไล่ล่า
    public Transform basePoint; // ฐาน AI
    [SerializeField] public float chaseRange; // ระยะห่างที่จะไล่ล่า
    [SerializeField] public float speed;  // ความเร็วAI
    [SerializeField] public float turnSpeed; // ความเร็วการหมุน

    public AIRocketLauncher launcher;  // AIRocketLauncher

    private BehaviorTreeNode root;  //  Behavior Node

    private GameManager gameManager;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        if (basePoint == null)
        {
            // basePoint เป็นตำแหน่งตัวละคร AI
            basePoint = transform;
        }        
        root = new SelectorNode(new List<BehaviorTreeNode>
        {
            // ระยะไล่ล่าPlayer
            new SequenceNode(new List<BehaviorTreeNode>
            {
                new ConditionNode(() => Vector3.Distance(transform.position, target.position) <= chaseRange),
                new ActionNode(() => ChaseTarget(target.position))
            }),
            // พ้นระยะการไล่ล่า กลับไปยัง basePoint
            new SequenceNode(new List<BehaviorTreeNode>
            {
                new ConditionNode(() => Vector3.Distance(transform.position, target.position) > chaseRange && Vector3.Distance(transform.position, basePoint.position) > 0.5f),
                new ActionNode(() => ReturnToBase(basePoint.position))
            }),
            // เดินวน ที่ basePoint
            new SequenceNode(new List<BehaviorTreeNode>
            {
                new ConditionNode(() => Vector3.Distance(transform.position, basePoint.position) <= 0.5f),
                new PatrolActionNode(this)
            })
        });
    }

    void Update()
    {
        root.Tick(); 
    }

    public bool ChaseTarget(Vector3 position)
    {
        MoveTowards(position);
        return true;
    }

    public bool ReturnToBase(Vector3 position)
    {
        MoveTowards(position);
        return true;
    }

    public void MoveTowards(Vector3 position)  
    {
        Vector3 dir = position - transform.position; // ทิศทางที่จะไป
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        transform.position += transform.up * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other) // เช็คการชน Obj Tag "PlayerRocket"
    {
        if (other.CompareTag("PlayerRocket"))
        {
            Debug.Log("AIRocket hit PlayerRocket!");
            gameManager.ReducePlayerHP();

            // reset game 
            gameManager.ResetAfterCollision();
        }
    }
    
    // SetTarget เป้าหมายไล่ล่า
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    // SetPathTo เส้นทาง
    public void SetPathTo(Transform targetTransform)
    {
        Debug.Log("Setting path to " + targetTransform.position);
    }
}
