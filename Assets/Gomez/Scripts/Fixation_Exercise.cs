using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixation_Exercise : MonoBehaviour
{

    public static int fixationPoints;

    // Start is called before the first frame update
    void Start()
    {
        fixationPoints = 0;
    }

    public static int CalculatePoints(string selectedValue, Color selectedBtnColor){

        string correctValue = "btn_Opcion1";

        if(selectedBtnColor != Color.green){
            if (selectedValue == correctValue){
                fixationPoints = 1;
            }
            else if (selectedValue != correctValue){
                fixationPoints = 0;
            }
        }


        return fixationPoints;
    }
}
