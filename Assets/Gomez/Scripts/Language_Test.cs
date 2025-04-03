using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language_Test : MonoBehaviour
{

    private Ejer_Controller _exercisesController;

    private void Start() {
        _exercisesController = GameObject.Find("Ejer_Controller").GetComponent<Ejer_Controller>();
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        string objName = gameObject.name;
        string collisionName = collision.gameObject.name;
        string collisionTag = collision.gameObject.tag;

        if(collisionTag == "Word"){

            switch (objName){
                case "img_Casa":
                    if (collisionName == "txt_Casa") {
                        _exercisesController.CheckLanguageExercise(true);
                        Debug.Log("Score point");
                    }
                    else _exercisesController.CheckLanguageExercise(false);
                break;

                case "img_Lapiz":
                    if (collisionName == "txt_Lapiz") {
                        _exercisesController.CheckLanguageExercise(true);
                        Debug.Log("Score point");
                    }
                    else _exercisesController.CheckLanguageExercise(false);
                break;

                case "img_Arbol":
                    if (collisionName == "txt_Arbol") {
                        _exercisesController.CheckLanguageExercise(true);
                        Debug.Log("Score point");
                    }
                    else _exercisesController.CheckLanguageExercise(false);
                break;

                case "img_Perro":
                    if (collisionName == "txt_Perro") {
                        _exercisesController.CheckLanguageExercise(true);
                        Debug.Log("Score point");
                    }
                    else _exercisesController.CheckLanguageExercise(false);
                break;
            }

            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            
        }
    }
}
