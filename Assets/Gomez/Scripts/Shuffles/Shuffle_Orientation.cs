using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle_Orientation : MonoBehaviour
{

    private GameObject[] days, months;
    
    private List<Vector2> dayTargetPositions = new List<Vector2>();

    private List<Vector2> monthTargetPositions = new List<Vector2>();

    private Shuffle_Controller shuffle;

    [SerializeField] float delay, speed;
    private float step;

    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        shuffle = GameObject.Find("Shuffle_Controller").GetComponent<Shuffle_Controller>();
        days = GameObject.FindGameObjectsWithTag("DayBtn");
        months = GameObject.FindGameObjectsWithTag("MonthBtn");

        for (int i = 0; i < days.Length; i++){
            dayTargetPositions.Add(days[i].transform.position);
        }
        for (int i = 0; i < months.Length; i++){
            monthTargetPositions.Add(months[i].transform.position);
        }
        canMove = true;
        StartCoroutine(shuffle.Shuffle(dayTargetPositions, delay));
        StartCoroutine(shuffle.Shuffle(monthTargetPositions, delay));
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;
        if (canMove){
            MoveObjects();
        }
    }

    public void StopShuffle(){
        StopCoroutine(shuffle.Shuffle(dayTargetPositions, delay));
        StopCoroutine(shuffle.Shuffle(monthTargetPositions, delay));
        canMove = false;
    }  

    private void MoveObjects(){
        for (int i = 0; i < dayTargetPositions.Count; i++){
            days[i].transform.position = Vector3.Lerp(days[i].transform.position, dayTargetPositions[i], step);
        }
        for (int i = 0; i < monthTargetPositions.Count; i++){
            months[i].transform.position = Vector3.Lerp(months[i].transform.position, monthTargetPositions[i], step);
        }
    }
}
