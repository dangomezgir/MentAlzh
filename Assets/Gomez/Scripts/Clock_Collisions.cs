using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_Collisions : MonoBehaviour
{

    string objName, colName;
    bool isInRange;
    GameObject reference;

    // Start is called before the first frame update
    void Start()
    {
        objName = gameObject.name;
        reference = GameObject.Find("img_Referencia");
        isInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(objName[0] == 'N'){
            if (!isInRange){
                if (CheckDistance() <= 280){
                    Clock_Test.totalCounter++;
                    isInRange = true;
                    Debug.Log("In range");
                }
            }
            else{
                if (CheckDistance() > 280){
                    Clock_Test.totalCounter--;
                    isInRange = false;
                    Debug.Log("Out of range");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        colName = other.gameObject.name;
        // if (objName[0] == 'N' && colName == "img_Reloj")Clock_Test.totalCounter++;
        if (objName == colName.Remove(0, 4))Clock_Test.rightCounter++;

    }

    private void OnTriggerExit2D(Collider2D other) {
        colName = other.gameObject.name;
        // if (objName[0] == 'N' && colName == "img_Reloj")Clock_Test.totalCounter--;
        if (objName == colName.Remove(0, 4))Clock_Test.rightCounter--;
    }

    private double CheckDistance(){
        RectTransform rect1 = GetComponent<RectTransform>();
        RectTransform rect2 = reference.GetComponent<RectTransform>();

        float x1, x2, y1, y2;
        (x1, y1) = (rect1.anchoredPosition.x, rect1.anchoredPosition.y);
        (x2, y2) = (rect2.anchoredPosition.x, rect2.anchoredPosition.y);

        return System.Math.Sqrt((System.Math.Pow((x2-x1), 2) + System.Math.Pow((y2-y1), 2)));
    }
}
