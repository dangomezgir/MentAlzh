using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation_Test : MonoBehaviour
{

    public static int dayPoints, monthPoints, totalPoints;

    // Start is called before the first frame update
    void Start()
    {
        dayPoints = 0;
        monthPoints = 0;
        totalPoints = 0;
    }

    public static int CalculatePoints(string selectedValue, string correctValue, Color selectedBtnColor, string exercise){

        if(selectedBtnColor != Color.green){
            if (exercise == "Day"){
                if (selectedValue == correctValue && dayPoints < 1){
                    dayPoints++;
                    totalPoints++;
                }
                else if (selectedValue != correctValue && dayPoints > 0){
                    dayPoints--;
                    totalPoints--;
                }
            }
            else if (exercise == "Month"){
                if (selectedValue == correctValue && monthPoints < 1){
                    monthPoints++;
                    totalPoints++;
                }
                else if (selectedValue != correctValue && monthPoints > 0){
                    monthPoints--;
                    totalPoints--;
                }
            }
        }


        return totalPoints;
    }
}
