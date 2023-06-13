using UnityEngine;
using UnityEngine.UI;

public class ThrottleController : MonoBehaviour
{
    public Slider slider;

    public float maxThrottle = 100f;
    public float currentThrottle;

    void Start()
    {
        SetMaxThrottle(maxThrottle);
        currentThrottle = 0f;
    }

    void Update()
    {
        // Here you would typically update the currentThrottle based on player input
        // For testing purposes, we'll just continuously increase the throttle over time
        currentThrottle += Time.deltaTime * 10;

        // Clamp the current throttle between 0 and maxThrottle
        currentThrottle = Mathf.Clamp(currentThrottle, 0, maxThrottle);

        // Update the throttle bar
        SetThrottle(currentThrottle);
    }

    public void SetMaxThrottle(float throttle)
    {
        slider.maxValue = throttle;
        slider.value = throttle;
    }

    public void SetThrottle(float throttle)
    {
        slider.value = throttle;
    }
}
