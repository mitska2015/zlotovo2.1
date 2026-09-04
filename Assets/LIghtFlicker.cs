using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;
    [Range(0, 1)]
    public float flickerChance = 0.05f; // Шанс мерцания (от 0 до 1)

    public float normalIntensity = 1.0f; // Обычная яркость
    public float dipIntensity = 0.2f;    // Яркость при «провале»

    void Update()
    {
        // Если случайное число меньше шанса, лампа «мигает»
        if (Random.value < flickerChance)
        {
            lightSource.intensity = dipIntensity;
        }
        else
        {
            // В остальное время горит ровно
            lightSource.intensity = normalIntensity;
        }
    }
}