using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset;
    private Collider2D col;
    private UI_Controller _uiController;


    private void Start()
    {
        _uiController = GameObject.Find("UI_Controller").GetComponent<UI_Controller>();
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
        float rotX;
        rotX = Random.Range(0, 360);
        if ((rotX >= 12 && rotX <= 47) || (rotX >= 288 && rotX <= 313)){
            // Debug.Log(rotX +" - Rotacion corregida");
            rotX = Random.Range(50, 281);
        }
        // Debug.Log(rotX);
        transform.eulerAngles = new Vector3(0, 0, rotX);
    }

    private void Update()
    {
        if(!_uiController.errorPanel.activeInHierarchy && !Drag_Objects.isObjectBeingDragged){
            // Debug.Log("Can rotate");
            Vector3 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if(col == Physics2D.OverlapPoint(mousePos))
                {
                    screenPos = myCam.WorldToScreenPoint(transform.position);
                    Vector3 vec3 = Input.mousePosition - screenPos;
                    angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
                }
            }
            if (Input.GetMouseButton(0))
            {
                if(col == Physics2D.OverlapPoint(mousePos))
                {
                    Vector3 vec3 = Input.mousePosition - screenPos;
                    float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
                }
            }
        }
        // else Debug.Log("Cannot rotate");
    }
}
