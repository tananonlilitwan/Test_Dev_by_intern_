using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    public float speed = 5f; // ความเร็ว Player
    public float turnSpeed = 200f; // ความเร็วการหมุน
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); 
    }
    void Update()
    {
        // พุ่งไปข้างหน้า
        transform.position += transform.up * speed * Time.deltaTime;

        // ควบคุมด้วยเมาส์ โดยการลาก
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float step = turnSpeed * Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AIRocketLauncher"))
        {
            if (!gameManager.IsGameResetting)  // เพิ่มการเช็คเพื่อป้องกันการลด HP ซ้ำ
            {
                Debug.Log("PlayerRocket hit AIRocketLauncher!");
                gameManager.ReduceAIHP();

                // เรียก reset game
                gameManager.ResetAfterCollision();
            }
        }
    }
}
