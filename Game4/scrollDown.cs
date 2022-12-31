using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollDown : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Scrollbar sb;
    bool oldu = false;
    float süre = 2;

    void Update()
    {
        //Debug.Log(scrollRect.verticalNormalizedPosition);

        if (sb.isActiveAndEnabled && !oldu)
        {
            scrollRect.verticalNormalizedPosition = 0f;
            Debug.Log(scrollRect.verticalNormalizedPosition);
            süre -= Time.deltaTime;

            if (süre < 0)
            {
                oldu = true;

            }
        }
        if (scrollRect.verticalNormalizedPosition < 0.5f)
        {
            scrollRect.verticalNormalizedPosition = 0f;

        }
            

    }
}
