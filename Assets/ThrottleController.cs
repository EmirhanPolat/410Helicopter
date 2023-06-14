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
