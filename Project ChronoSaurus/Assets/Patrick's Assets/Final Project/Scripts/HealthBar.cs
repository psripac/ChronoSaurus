using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image foreground;
    public float updateSpeedSeconds = 0.5f;

    public void ChangeHealthBar(float pct)
    {
        foreground.fillAmount = pct;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
