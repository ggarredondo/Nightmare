using UnityEngine;
using UnityEngine.UI;

public class EgoHUD : MonoBehaviour
{
    [SerializeField] private Player player;
    private Color defaultColor;
    [SerializeField] private Color belowMinColor;
    private EgoHandler egoHandler;
    private Image wheel;

    private void Awake()
    {
        wheel = GetComponent<Image>();
        defaultColor = wheel.color;
    }

    private void Start() => egoHandler = player.EgoHandler;

    private void Update()
    {
        float fill = egoHandler.CurrentEgo / egoHandler.MaxEgo;
        wheel.fillAmount = fill;
        wheel.color = egoHandler.CurrentEgo < egoHandler.MinUsableEgo ? belowMinColor : defaultColor;
    }
}
