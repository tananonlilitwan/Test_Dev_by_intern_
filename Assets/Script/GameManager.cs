using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Image[] playerHPImages;    // Array ของ UI Hp ของ Player
    public Image[] aiHPImages;        // Array ของ UI Hp ของ AI
    public Color grayColor = Color.gray; 

    private int playerHP = 3;   // HP ของ Player
    private int aiHP = 3;       // HP ของ AI

    public Canvas winCanvas;    // Canvas Player ชนะ
    public Canvas loseCanvas;   // Canvas Player แพ้
    
    public CameraFollow cameraFollow; // กล้องตาม Player

    public bool IsGameResetting = false;
    
    
    void Start()
    {
        // ซ่อน Canvas ชนะและแพ้
        winCanvas.gameObject.SetActive(false);
        loseCanvas.gameObject.SetActive(false);
    }

    public void ReduceAIHP() // เปลี่ยนสี Hp AI
    {
        if (aiHP > 0)
        {
            aiHP--;
            aiHPImages[aiHP].color = grayColor; // เปลี่ยนสีของ HP ที่ลดลงเป็นสีเทา
            CheckGameOver();
        }
    }

    public void ReducePlayerHP() // เปลี่ยนสี Hp Player
    {
        if (playerHP > 0)
        {
            playerHP--;
            playerHPImages[playerHP].color = grayColor; // เปลี่ยนสีของ HP ที่ลดลงเป็นสีเทา
            CheckGameOver();
        }
    }

    void CheckGameOver()
    {
        if (aiHP == 0)
        {
            // เปิด Canvas Win
            winCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;  // หยุดเวลาเกมจบ
        }
        else if (playerHP == 0)
        {
            // เปิด Canvas Lose
            loseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;  // หยุดเวลาเกมจบ
        }
    }
    
    public void ResetAfterCollision()
    {
        if (IsGameResetting) return;

        // ไม่ต้องรีเซ็ตถ้า HP ของฝ่ายใดฝ่ายหนึ่งหมดแล้ว
        if (playerHP <= 0 || aiHP <= 0) return;

        StartCoroutine(ResetAfterDelay());
    }
    IEnumerator ResetAfterDelay()
    {
        IsGameResetting = true;

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);

        // รีเซ็ตกล้อง
        cameraFollow.SetTarget(null);
        Camera.main.transform.position = new Vector3(0, 0, -10);

        // ลบ Obj PlayerRocket
        var playerLauncher = GameObject.FindObjectOfType<PlayerRocketLauncher>();
        if (playerLauncher.currentRocket != null)
        {
            Destroy(playerLauncher.currentRocket);
            playerLauncher.ClearPlayerRocket();
        }

        var aiLauncher = GameObject.FindObjectOfType<AIRocketLauncher>();
        if (aiLauncher.currentAIRocket != null)
        {
            Destroy(aiLauncher.currentAIRocket);
            aiLauncher.ClearAIRocket();
        }

        Time.timeScale = 1f;
        
        cameraFollow.ShowHint("Press Space to shoot.");

        IsGameResetting = false;
    }
    
}