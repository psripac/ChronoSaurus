using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image foreground;
    public Image background;
    public Text label1;
    public Text label2;
    public float updateSpeedSeconds = 0.5f;
    private float center_x = Screen.width / 2;
    private float center_y = Screen.height / 2;

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
