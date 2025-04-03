using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private float totalTime, orientationTime, clockTime, fixationTime, languageTime, calculusTime, memoryTime;
    private int exerciseCounter;
    private bool isTimerOn;
    private List<float> timesInSeconds = new List<float>();
    private List<string> timeToDisplay = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        (totalTime, orientationTime, clockTime, fixationTime, languageTime, calculusTime, memoryTime) = (0f, 0f, 0f, 0f, 0f, 0f, 0f);
        exerciseCounter = 1;
        isTimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn){
            totalTime += Time.deltaTime;
            switch (exerciseCounter)
            {
                case 1:
                    orientationTime += Time.deltaTime;
                break;

                case 2:
                    clockTime += Time.deltaTime;
                break;

                case 3:
                    fixationTime += Time.deltaTime;
                break;

                case 4:
                    languageTime += Time.deltaTime;
                break;
                
                case 5:
                    calculusTime += Time.deltaTime;
                break;

                case 6:
                    memoryTime += Time.deltaTime;
                break;
            }
        }
    }

    public void StopTimer(){
        isTimerOn = false;
        timesInSeconds.Add(totalTime);
        timesInSeconds.Add(orientationTime);
        timesInSeconds.Add(clockTime);
        timesInSeconds.Add(fixationTime);
        timesInSeconds.Add(languageTime);
        timesInSeconds.Add(calculusTime);
        timesInSeconds.Add(memoryTime);
    }

    public void Next(){
        exerciseCounter++;
        Debug.Log(exerciseCounter);
    }

    public List<string> GetDisplayTimes(){
        float minutes, seconds;
        for (int i = 0; i < timesInSeconds.Count; i++){
            minutes = Mathf.FloorToInt(timesInSeconds[i] / 60); 
            seconds = Mathf.FloorToInt(timesInSeconds[i] % 60);
            timeToDisplay.Add(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
        return timeToDisplay;
    }

    public void TestTimer(){
        float minutes = Mathf.FloorToInt(orientationTime / 60); 
        float seconds = Mathf.FloorToInt(orientationTime % 60);
        Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
