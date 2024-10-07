using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeComponent : MonoBehaviour
{
    public TextMeshProUGUI TimeLabel;
    public TextMeshProUGUI CountDownLabel;

    private DateTime finalTime;
    private bool isCount = false;

    void Start()
    {   
        Show30Seconds();
    }

    private void Show30Seconds(bool forceRefresh = false)
    {
        DateTime currTime = DateTime.UtcNow;
        if (forceRefresh) currTime = currTime.AddSeconds(30);
        int seconds = currTime.Second < 30 ? 30 : 0;
        finalTime = seconds == 0 ? currTime.AddMinutes(1).AddSeconds(-currTime.Second) : currTime.AddSeconds(seconds - currTime.Second);

        TimeLabel.text = finalTime.ToString();
    }

    public void ShowStartTime()
    {
        TimeSpan timeDiff = finalTime - DateTime.UtcNow;
        if (timeDiff.TotalMilliseconds <= 2000f) Show30Seconds(true);
        StartCoroutine(HandleCountDown());
    }

    IEnumerator HandleCountDown()
    {
        isCount = true;

        while (isCount)
        {
            TimeSpan timeDiff = finalTime - DateTime.UtcNow;
            if (timeDiff.TotalMilliseconds <= 0)
            {
                timeDiff = TimeSpan.Zero;
                isCount = false;
            }
            
            CountDownLabel.text = string.Format("{0:D2}:{1:D2}:{2:D3}",
                timeDiff.Minutes,
                timeDiff.Seconds,
                timeDiff.Milliseconds);

                yield return new WaitForSeconds(0.001f);
        }
    }
    
    
    
}
