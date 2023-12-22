using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float timeScale = 1f;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnValidate() => Time.timeScale = timeScale;
}
