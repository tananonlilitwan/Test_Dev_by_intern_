using UnityEngine;

public class RocketTrail : MonoBehaviour
{
    public TrailRenderer trailRenderer;

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        
        trailRenderer.time = 0.5f; // ความยาวของเส้น
        trailRenderer.startWidth = 0.1f; // ความกว้างเริ่มต้น
        trailRenderer.endWidth = 0.05f; // ความกว้างปลาย
        trailRenderer.material = new Material(Shader.Find("Sprites/Default"));
        trailRenderer.startColor = Color.magenta; // สีเริ่มต้น
        trailRenderer.endColor = new Color(1f, 1f, 1f, 0); // สีตรงปลายที่โปร่งแสง
    }
}