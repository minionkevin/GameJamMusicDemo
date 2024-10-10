using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeComponent : MonoBehaviour
{
    public TextMeshProUGUI TimeLabel;
    public TextMeshProUGUI CountDownLabel;
    public GameStartComponent GameStart;

    private DateTime finalTime;
    private bool isCount = false;
    private TimeSpan timeDiffA;
    
    private DateTime startTime;
    private TimeSpan countdownDuration;

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

    public void ShowStartTime(bool isA)
    {
        TimeSpan timeDiff = finalTime - DateTime.UtcNow;
        if (timeDiff.TotalMilliseconds <= 2000f) Show30Seconds(true);
        
        timeDiffA = finalTime - DateTime.UtcNow;
        StartCoroutine(HandleCountDown(isA));
    }

    IEnumerator HandleCountDown(bool isA)
    {
        startTime = DateTime.UtcNow;
        // countdownDuration = TimeSpan.FromSeconds(5); // 30秒倒计时

        while (DateTime.UtcNow < finalTime)
        {
            TimeSpan timeDiff = finalTime - DateTime.UtcNow;

            CountDownLabel.text = string.Format("{0:D2}:{1:D2}:{2:D3}",
                timeDiff.Minutes,
                timeDiff.Seconds,
                timeDiff.Milliseconds);

            yield return null;
        }

        CountDownLabel.text = "00:00:000"; 
        GameStart.HandleLoadScene(isA);
    }
}
