using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject canvasGameStart; // หน้าหลัก
    public GameObject canvasNavigation; // Canva นำทาง
    public GameObject canvasGamePlay; // เกม Play
    public GameObject canvasText; 
    
    void Start()
    {
        // เริ่มต้น หยุดเกมไว้
        Time.timeScale = 0f;

        // เปิดเฉพาะหน้าเริ่มเกม
        canvasGameStart.SetActive(true);
        canvasNavigation.SetActive(false);
        canvasGamePlay.SetActive(false);
        canvasText.SetActive(false);
    }

    public void OnStartButtonPressed()
    {
        // เริ่มเกม
        Time.timeScale = 1f;

        // ปิดหน้าเริ่มเกม
        canvasGameStart.SetActive(false);

        // เปิด UI สำหรับเล่นเกม
        canvasNavigation.SetActive(true);
        canvasGamePlay.SetActive(true);
        canvasText.SetActive(true);
        
        // หยุดเวลาเกม
        Time.timeScale = 0f;

        // หน่วงเวลา
        StartCoroutine(ResumeGameAfterDelay(1.5f)); // 1.5 วินาที
    }
    
    public void OnExitButtonPressed()
    { 
        Application.Quit();
    }
    
    IEnumerator ResumeGameAfterDelay(float delaySeconds)
    {
        yield return new WaitForSecondsRealtime(delaySeconds);
        Time.timeScale = 1f; // กลับมาเปิดเวลา
    }
    
    public void OnResetButtonPressed() // LoadScene ใหม่
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}