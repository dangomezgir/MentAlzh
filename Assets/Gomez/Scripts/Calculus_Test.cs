using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculus_Test : MonoBehaviour
{
    GameObject[] _fieldsObj;
    public static List<InputField> answerFieldsList;
    public static int calculusPoints;

    private void Start() {
        calculusPoints = 0;
        _fieldsObj = GameObject.FindGameObjectsWithTag("CalcAnswer");
        answerFieldsList = new List<InputField>();
        for (int i = 0; i < _fieldsObj.Length; i++){
            answerFieldsList.Add(_fieldsObj[i].GetComponent<InputField>());
        }
        Debug.Log(answerFieldsList.Count);
    }

    public static bool AreAllFieldsFull(){
        int counter = 0;
        for (int i = 0; i < answerFieldsList.Count; i++){
            if (answerFieldsList[i].text != ""){
                counter++;
            }
        }
        if (counter == 5){
            return true;
        }
        else return false;
    }

    public static int CalculatePoints(){
        if (answerFieldsList[0].text == "27") calculusPoints++;
        if (answerFieldsList[1].text == "24") calculusPoints++;
        if (answerFieldsList[2].text == "21") calculusPoints++;
        if (answerFieldsList[3].text == "18") calculusPoints++;
        if (answerFieldsList[4].text == "15") calculusPoints++;
        return calculusPoints;
    }
}
