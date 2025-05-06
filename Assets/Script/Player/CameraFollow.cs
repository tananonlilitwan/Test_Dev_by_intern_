using UnityEngine;
using System.Collections;
using TMPro;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    private bool isIntro = true; // ช่วง intro 
    public TextMeshProUGUI hintText; 

    void Start()
    {
        StartCoroutine(IntroSequence());
    }

    void LateUpdate()
    {
        if (target == null || isIntro) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothed.x, smoothed.y, transform.position.z); // lock Z
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Intro ของกล้อง
    IEnumerator IntroSequence()
    {
        Transform targetObj = GameObject.FindWithTag("AIRocketLauncher")?.transform;
        Transform playerObj = GameObject.FindWithTag("Player")?.transform;

        if (targetObj == null || playerObj == null)
        {
            Debug.LogWarning("Target or Player not found. Skipping intro.");
            isIntro = false;
            yield break;
        }

        // กล้องไปยังเป้าหมาย
        yield return MoveCameraTo(targetObj.position + offset, 1.5f);

        // หน่วงเวลา 1วิ
        yield return new WaitForSeconds(1f);

        // กลับมาที่ Player
        yield return MoveCameraTo(playerObj.position + offset, 1.5f);
        
        SetTarget(playerObj);
        isIntro = false;
        
        // แสดงข้อความ
        if (hintText != null)
        {
            hintText.text = "Press Space to shoot.";
        }
    }

    IEnumerator MoveCameraTo(Vector3 destination, float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, destination, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
    }
    
    public bool IsIntroFinished()
    {
        return !isIntro;
    }
    
    public void ShowHint(string message)
    {
        if (hintText != null)
            hintText.text = message;
    }

    public void HideHint()
    {
        if (hintText != null)
            hintText.text = "";
    }

}
