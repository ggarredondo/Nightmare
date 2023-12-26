using UnityEngine;

[System.Serializable]
public class EgoHandler
{
    [SerializeField] [ReadOnlyField] private float ego;
    [SerializeField] private float maxEgo, regenRate, minUsableEgo;

    public void Initialize() => ego = maxEgo;

    private void AddToEgo(float addend) => ego = Mathf.Clamp(ego + addend, 0f, maxEgo);

    public System.Action OnEgoDepletion;
    public void DepleteEgo(float depletionRate) 
    { 
        AddToEgo(-depletionRate * Time.deltaTime);
        if (ego <= 0f) OnEgoDepletion?.Invoke();
    }
    public void RegenEgo() => AddToEgo(regenRate * Time.deltaTime);

    public ref readonly float CurrentEgo => ref ego;
    public ref readonly float MaxEgo => ref maxEgo;
    public ref readonly float MinUsableEgo => ref minUsableEgo;
}
