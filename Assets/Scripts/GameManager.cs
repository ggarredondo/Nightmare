using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float timeScale = 1f;
    private void OnValidate() => Time.timeScale = timeScale;
}
