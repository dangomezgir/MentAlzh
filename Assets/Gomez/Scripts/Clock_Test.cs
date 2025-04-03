using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clock_Test : MonoBehaviour
{

    public static int rightCounter, totalCounter, clockPoints;
    // public Text yes, no;
    [SerializeField] Collider2D hourCollider, minuteCollider;


    // Start is called before the first frame update
    void Start()
    {
        (rightCounter, totalCounter, clockPoints) = (0, 0, 0);
        hourCollider.enabled = false;
        minuteCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // yes.text = rightCounter.ToString();
        // no.text = totalCounter.ToString();
        if(rightCounter == 14) clockPoints = 1;
        else clockPoints = 0;
    }

    public static bool IsDone(){
        if (totalCounter == 12) return true;
        return false;
    }

    public void SelectClockHand(GameObject hand){
        GameObject[] btnArray = GameObject.FindGameObjectsWithTag("ClockHandBtn"); 
        GameObject selectedBtn = EventSystem.current.currentSelectedGameObject;
        Collider2D selectedCollider;

        selectedCollider = hand.GetComponent<CircleCollider2D>();
        for (int i = 0; i < btnArray.Length; i++){
            btnArray[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        selectedBtn.GetComponent<Image>().color = Color.green;

        // if(selectedCollider.enabled == false) 
        selectedCollider.enabled = true;

        switch(hand.name){
            case "Horario":
                minuteCollider.enabled = false;
            break;

            case "Minutero":
                hourCollider.enabled = false;
            break;
        }
    }
}
