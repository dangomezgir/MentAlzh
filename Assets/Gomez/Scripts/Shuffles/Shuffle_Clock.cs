using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle_Clock : MonoBehaviour
{
    private GameObject[] numbers;
    
    private List<Vector2> numbersTargetPositions = new List<Vector2>();
    
    void Start()
    {
        numbers = GameObject.FindGameObjectsWithTag("ClockNumber");

        for (int i = 0; i < numbers.Length; i++){
            numbersTargetPositions.Add(numbers[i].transform.position);
        }
        for (int i = 0; i < numbersTargetPositions.Count; i++) {
            Vector3 temp = numbersTargetPositions[i];
            int randomIndex = Random.Range(i, numbersTargetPositions.Count);
            numbersTargetPositions[i] = numbersTargetPositions[randomIndex];
            numbersTargetPositions[randomIndex] = temp;
        }
        MoveObjects();
    }

    private void MoveObjects(){
        for (int i = 0; i < numbersTargetPositions.Count; i++){
            numbers[i].transform.position = numbersTargetPositions[i];
        }
    }
}
