using UnityEngine;

public class PlayerRocketLauncher : MonoBehaviour
{
    public GameObject rocketPrefab;
    public Transform launchPoint;

    public AIRocketLauncher aiLauncher;

    public GameObject currentRocket;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentRocket == null)
        {
            // เช็คว่ากล้อง intro จบยัง
            CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
            if (!cam.IsIntroFinished()) return;
            
            currentRocket = Instantiate(rocketPrefab, launchPoint.position, Quaternion.identity);
            currentRocket.AddComponent<PlayerRocket>();
            
            // ซ่อนข้อความ
            cam.HideHint();
            
            Debug.Log("Player ยิง Rocket!");

            // กล้องตาม
            Camera.main.GetComponent<CameraFollow>().SetTarget(currentRocket.transform);

            Debug.Log("ให้ AI ยิง rocket ตาม Player...");

            // AI ยิง พร้อมกับตามเป้า
            aiLauncher.LaunchRocket(currentRocket.transform);
        }
    }
    
    public void ClearPlayerRocket()
    {
        currentRocket = null;
    }


}
