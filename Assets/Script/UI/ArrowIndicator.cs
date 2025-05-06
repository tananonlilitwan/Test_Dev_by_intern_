using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicator : MonoBehaviour
{
    public Transform target;         // เป้าหมายที่ต้องการชี้
    public Transform player;         // ตัวของ Player
    public RectTransform arrowUI;    // ลูกศรใน UI
    public Camera mainCamera;

    [Header("Screen Offset")]
    public float edgeBuffer = 50f; // ระยะห่างจากขอบจอ

    void Update()
    {
        if (target == null || player == null || mainCamera == null) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        bool isOffScreen = screenPos.z < 0 || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;

        Vector3 cappedScreenPos = screenPos;

        if (isOffScreen)
        {
            cappedScreenPos = ClampToScreenEdge(screenPos);
        }

        arrowUI.position = cappedScreenPos;

        // หมุนให้ชี้ไปยังเป้าหมาย
        Vector3 dir = (screenPos - new Vector3(Screen.width / 2f, Screen.height / 2f, 0)).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    Vector3 ClampToScreenEdge(Vector3 pos)
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Vector3 fromCenter = (pos - screenCenter).normalized;

        float x = screenCenter.x + fromCenter.x * ((Screen.width / 2f) - edgeBuffer);
        float y = screenCenter.y + fromCenter.y * ((Screen.height / 2f) - edgeBuffer);

        return new Vector3(x, y, 0);
    }
}