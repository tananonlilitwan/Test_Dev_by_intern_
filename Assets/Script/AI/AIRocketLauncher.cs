using UnityEngine;

public class AIRocketLauncher : MonoBehaviour
{
    public GameObject aiRocketPrefab; 
    public Transform launchPoint;

    public GameObject currentAIRocket; 

    public void LaunchRocket(Transform target)
    {
        if (currentAIRocket != null) return; // เช็คว่า AIRocket ถูกยิงไปยัง ถ้ายิงไปห้ามยิงใหม่

        currentAIRocket = Instantiate(aiRocketPrefab, launchPoint.position, Quaternion.identity); // สร้าง AIRocket ที่ ตำแหน่งยิง

        AIRocket rocket = currentAIRocket.GetComponent<AIRocket>();

        rocket.launcher = this;  

        rocket.SetTarget(target);  // เป้าหมายAIRocket
        rocket.SetPathTo(target);  // เส้นทางAIRocket

        
        rocket.basePoint = launchPoint;  // ตำแหน่งฐาน AI
    }
    public void ClearAIRocket() // ล้าง ObjAIRocket
    {
        currentAIRocket = null;
    }
}
