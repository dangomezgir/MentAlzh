using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_Objects : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    public static bool isObjectBeingDragged = false;

    private UI_Controller _uiController;
    
    private void Start() {
        isObjectBeingDragged = false;
        _uiController = GameObject.Find("UI_Controller").GetComponent<UI_Controller>();
    }

    public void DragHandler(BaseEventData data){
        if (!_uiController.errorPanel.activeInHierarchy){
            PointerEventData pointerData = (PointerEventData)data;

            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pointerData.position,
                canvas.worldCamera,
                out position
            );

            transform.position = canvas.transform.TransformPoint(position);
            isObjectBeingDragged = true;
        }
    }

    public void DropHandler(){
        isObjectBeingDragged = false;
    }

    
}
