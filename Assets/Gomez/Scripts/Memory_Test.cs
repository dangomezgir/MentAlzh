using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory_Test : MonoBehaviour
{

    public static int totalPoints, pointsCol1, pointsCol2, pointsCol3;
    // Start is called before the first frame update
    void Start()
    {
        totalPoints = 0;
        pointsCol1 = 0;
        pointsCol2 = 0;
        pointsCol3 = 0;
    }

    public static int CalculatePoints(string selectedValue, string correctValue, Color selectedBtnColor, string tag){
        if(selectedBtnColor != Color.green){
            switch (tag){
                case "Memory1":
                    if (selectedValue == correctValue && pointsCol1 < 1){
                        totalPoints++;
                        pointsCol1++;
                    }
                    else if (selectedValue != correctValue && pointsCol1 > 0){
                        totalPoints--;
                        pointsCol1--;
                    }
                break;

                case "Memory2":
                    if (selectedValue == correctValue && pointsCol2 < 1){
                        totalPoints++;
                        pointsCol2++;
                    }
                    else if (selectedValue != correctValue && pointsCol2 > 0){
                        totalPoints--;
                        pointsCol2--;
                    }
                break;

                case "Memory3":
                    if (selectedValue == correctValue && pointsCol3 < 1){
                        totalPoints++;
                        pointsCol3++;
                    }
                    else if (selectedValue != correctValue && pointsCol3 > 0){
                        totalPoints--;
                        pointsCol3--;
                    }
                break;
            }
        }
        return totalPoints;
    }


}
