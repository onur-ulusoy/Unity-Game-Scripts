using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buffTimerAnimation : MonoBehaviour
{
    public RectTransform FxHolder;
    public Image CircleImg;
    public float timer;
    public float progressAngle;
    public float fillAmount;
    public float consumptionDuration;

    private void OnEnable()
    {
        timer = Time.time;

        progressAngle = 0;
        FxHolder.rotation = Quaternion.identity;
        CircleImg.fillAmount = 1;
    }

    void Update()
    {
        progressAngle = Mathf.Abs((Time.time - timer) * 360 / consumptionDuration);
        fillAmount = (360 - progressAngle) / 360;

        FxHolder.rotation = Quaternion.Euler(new Vector3(0f, 0f, progressAngle));
        CircleImg.fillAmount = fillAmount;

        if (progressAngle >= 360)
        {
            enabled = false;
        }
        
    }
}
