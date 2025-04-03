using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] GameObject startPanel, instructionsPanel, exitPanel, exercisesPanel, dataPanel, resultsPanel, emailPanel, emailWarningPanel, backPanel;
    [SerializeField] GameObject orientationPanel, clockPanel, languagePanel, fixationPanel, calculusPanel, memoryPanel;
    [SerializeField] string totalPoints;
    public GameObject errorPanel, errorTextObj, exerciseTitle, resultsObj, interpretationObj;
    private Ejer_Controller _exercisesController;
    
    private Text _txtPoints, _txtError, _txtExerciseTitle, _txtResults;

    private Shuffle_Orientation shuffleOrientation;
    private Shuffle_Fixation shuffleFixation;
    
    private Scene _scene;
    private string _sceneName;

    public Timer timer;
    public string resultsTotal, resultsDetail;
    private void Awake() {
        _scene = SceneManager.GetActiveScene();
        _sceneName = _scene.name;
        if(_sceneName == "Inicio"){
            startPanel.SetActive(true);
            instructionsPanel.SetActive(false);
        }
        else if(_sceneName == "Ejercicios"){

            exercisesPanel.SetActive(true);
            dataPanel.SetActive(false);
            errorPanel.SetActive(false);
            resultsPanel.SetActive(false);
            emailPanel.SetActive(false);
            orientationPanel.SetActive(true);
            clockPanel.SetActive(false);
            languagePanel.SetActive(false);
            fixationPanel.SetActive(false);
            calculusPanel.SetActive(false);
            memoryPanel.SetActive(false);

            _exercisesController = GameObject.Find("Ejer_Controller").GetComponent<Ejer_Controller>();
            _txtError = errorTextObj.GetComponent<Text>();
            _txtExerciseTitle = exerciseTitle.GetComponent<Text>();
            _txtResults = resultsObj.GetComponent<Text>();
            _txtExerciseTitle.text = "Ejercicio 1 - Orientación \nSeleccione el día y el mes para continuar";

            timer = GameObject.Find("Timer_Controller").GetComponent<Timer>();

            shuffleOrientation = orientationPanel.GetComponent<Shuffle_Orientation>();
            shuffleFixation = fixationPanel.GetComponent<Shuffle_Fixation>();

        }
        exitPanel.SetActive(false);
    }

    private void Update() {
        if(_sceneName == "Ejercicios"){
            if (!backPanel.activeInHierarchy){
                if (Input.GetKeyDown(KeyCode.Escape))backPanel.SetActive(true);
            }
            else {
                if (Input.GetKeyDown(KeyCode.Escape))backPanel.SetActive(false);
            }
        }
        else{
            if (!exitPanel.activeInHierarchy){
                if (Input.GetKeyDown(KeyCode.Escape))exitPanel.SetActive(true);
            }
            else {
                if (Input.GetKeyDown(KeyCode.Escape))exitPanel.SetActive(false);
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    public void ChangeScene(){
        switch (_sceneName){
            case "Inicio":
                SceneManager.LoadScene("Ejercicios");
            break;

            case "Ejercicios":
                SceneManager.LoadScene("Inicio");
            break;
        }
    }

    public void NextExercise(){

        if (orientationPanel.activeInHierarchy ){

            if(_exercisesController.IsExerciseDone("orientation")){

                ChangePanels(orientationPanel, clockPanel);
                timer.Next();
                shuffleOrientation.StopShuffle();
                
            }
            else{
                _txtError.text = "Por favor seleccione el día y el mes antes de continuar";
                errorPanel.SetActive(true);
            }

        }
        else if (clockPanel.activeInHierarchy ){

            if(_exercisesController.IsExerciseDone("clock")){

                ChangePanels(clockPanel, fixationPanel);
                timer.Next();

            }
            else{
                _txtError.text = "Por favor arrastre todos los números dentro del círculo antes de continuar";
                errorPanel.SetActive(true);
            }

        }
        else if (fixationPanel.activeInHierarchy){

            if(_exercisesController.IsExerciseDone("fixation")){

                ChangePanels(fixationPanel, languagePanel);
                timer.Next();
                shuffleFixation.StopShuffle();

            }
            else{
                _txtError.text = "Por favor escuche el audio y seleccione una opción antes de continuar";
                errorPanel.SetActive(true);
            }

        }
        else if (languagePanel.activeInHierarchy){

            if(_exercisesController.IsExerciseDone("language")){

                ChangePanels(languagePanel, calculusPanel);
                timer.Next();

            }
            else{
                _txtError.text = "Por favor arrastre todas las imágenes antes de continuar";
                errorPanel.SetActive(true);
            }
            
        }
        else if (calculusPanel.activeInHierarchy){

            if(_exercisesController.IsExerciseDone("calculus")){
                
                ChangePanels(calculusPanel, memoryPanel);
                timer.Next();
            }
            else{
                _txtError.text = "Por favor llene todos los campos antes de continuar";
                errorPanel.SetActive(true);
            }
            
        }
        else if (memoryPanel.activeInHierarchy){

            if(_exercisesController.IsExerciseDone("memory")){
                
                timer.StopTimer();
                ShowResults();
            }
            else{
                _txtError.text = "Por favor seleccione 3 palabras antes de continuar";
                errorPanel.SetActive(true);
            }
            
        }

    }

    private void ChangePanels(GameObject currentPanel, GameObject nextPanel){
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
        switch (currentPanel.name){
            
            case "panel_Orientacion":
                _txtExerciseTitle.text = "Ejercicio 1 - Orientación 2 (reloj) " + 
                                         "\n 1. Arrastre los números de la parte superior dentro del círculo formando un reloj.\n"+
                                         "2. Mueva las manecillas hasta las 11:10";
            break;

            case "panel_Reloj":
                _txtExerciseTitle.text = "Ejercicio 2 - Fijación " + 
                                         "\nPresione el botón para reproducir el audio y seleccione las palabras que escucha";
            break;

            case "panel_Fijacion":
                _txtExerciseTitle.text = "Ejercicio 3 - Lenguaje " + 
                                         "\nArrastre cada imagen a la palabra que le corresponde";
            break;

            case "panel_Lenguaje":
                _txtExerciseTitle.text = "Ejercicio 4 - Cálculo" + 
                                         "\nLea atentamente el problema y escriba las respuestas a las preguntas";
                Debug.Log("Cálculo: " + _exercisesController.calculusPoints);
            break;

            case "panel_Calculo":
                _txtExerciseTitle.text = "Ejercicio 5 - Memoria" + 
                                         "\nSeleccione las palabras que escuchó en el ejercicio de fijación";
            break;

        }
    }

    public void ShowErrorAudio(){
        _txtError.text = "Escuche el audio antes de seleccionar una opción";
        errorPanel.SetActive(true);
    }

    public void ShowResults(){
        exercisesPanel.SetActive(false);
        resultsPanel.SetActive(true);
        emailWarningPanel.SetActive(false);

        List<string> timeToDisplay = new List<string>();
        
        timeToDisplay = timer.GetDisplayTimes();

        _txtPoints = GameObject.Find("txt_Puntos").GetComponent<Text>();
        string pointsObtained = _exercisesController.CalculateTotalPoints().ToString();
        resultsTotal = "Puntuación total: " + pointsObtained + " / " + totalPoints + " - Tiempo: " + timeToDisplay[0];
        resultsDetail = "Puntos por prueba:   \nOrientación 1: " + _exercisesController.orientationPoints + " / 2 " + "- Tiempo: " + timeToDisplay[1] +
                                                "\nOrientación 2 (reloj): " + Clock_Test.clockPoints + " / 1 " + "- Tiempo: " + timeToDisplay[2] +
                                                "\nFijación: " + _exercisesController.fixationPoints + " / 1 " + "- Tiempo: " + timeToDisplay[3] +
                                                "\nLenguaje: " + _exercisesController.languagePoints + " / 4 " + "- Tiempo: " + timeToDisplay[4] +
                                                "\nCálculo: " + _exercisesController.calculusPoints + " / 5 " + "- Tiempo: " + timeToDisplay[5] +
                                                "\nMemoria: " + _exercisesController.memoryPoints + " / 3 " + "- Tiempo: " + timeToDisplay[6];
        _txtPoints.text = resultsTotal;
        _txtResults.text = resultsDetail;
    }

    public void Skip(){
        timer.StopTimer();
        ShowResults();
    }

    public void CloseApp(){
        Application.Quit();
        Debug.Log("Aquí cerraría la aplicación");
    }
}
