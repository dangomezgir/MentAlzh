using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Ejer_Controller : MonoBehaviour
{

    public int totalPoints, orientationPoints, clockPoints, fixationPoints, languagePoints, languageCounter, calculusPoints, memoryPoints;
    private string _currentDay, _currentMonth;
    private UI_Controller _uiController;
    private GameObject _errorPanel;
    private bool _isDaySelected, _isMonthSelected, _isWordsOptionSelected, _wasAudioPlayed;
    public static AudioSource audioSrc;
    private int memoryCounterAll, memory1, memory2, memory3;

    private AudioClip _audioExercise;

    private void Awake() {
        (orientationPoints, clockPoints, fixationPoints, languagePoints, languageCounter, calculusPoints, memoryPoints) = (0, 0, 0, 0, 0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        totalPoints = 0;
        _currentDay = System.DateTime.Now.ToString("dddd").ToUpper();
        _currentMonth = System.DateTime.Now.ToString("MMMM").ToUpper();
        _uiController = GameObject.Find("UI_Controller").GetComponent<UI_Controller>();
        _errorPanel = _uiController.errorPanel;
        _isDaySelected = false;
        _isMonthSelected = false;
        _isWordsOptionSelected = false;
        _wasAudioPlayed = false;
        audioSrc = GetComponent<AudioSource>();
        _audioExercise = Resources.Load<AudioClip>("Audio/ejercicio");

        
    }
     
    public void SelectDayButton(){
        GameObject[] dayBtnArray = GameObject.FindGameObjectsWithTag("DayBtn"); //Arreglo de objetos con todos los botones de los días de la semana
        GameObject selectedBtn = EventSystem.current.currentSelectedGameObject;
        string selectedDay = selectedBtn.name.ToUpper();

        if (!_errorPanel.activeInHierarchy){

            orientationPoints = Orientation_Test.CalculatePoints(selectedDay, _currentDay, selectedBtn.GetComponent<Image>().color, "Day");

            for (int i = 0; i < dayBtnArray.Length; i++){
                dayBtnArray[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

            selectedBtn.GetComponent<Image>().color = Color.green;

            _isDaySelected = true;
        }
    }

    public void SelectMonthButton(){
        GameObject[] monthBtnArray = GameObject.FindGameObjectsWithTag("MonthBtn"); //Arreglo de objetos con todos los botones de los meses
        GameObject selectedBtn = EventSystem.current.currentSelectedGameObject;
        string selectedMonth = selectedBtn.name.ToUpper();

        if (!_errorPanel.activeInHierarchy){

            orientationPoints = Orientation_Test.CalculatePoints(selectedMonth, _currentMonth, selectedBtn.GetComponent<Image>().color , "Month");

            for (int i = 0; i < monthBtnArray.Length; i++){
                monthBtnArray[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

            selectedBtn.GetComponent<Image>().color = Color.green;

            _isMonthSelected = true;
        }

    }

    public void PlayFixationAudio(){
        audioSrc.Play();
        _wasAudioPlayed = true;
    }

    public void SelectWordsOption(){
        GameObject[] optionBtnArray = GameObject.FindGameObjectsWithTag("WordsBtn"); //Arreglo de objetos con todos los botones de las palabras
        GameObject selectedBtn = EventSystem.current.currentSelectedGameObject;
        string selectedOption = selectedBtn.name;

        if (!_errorPanel.activeInHierarchy){

            if (!_wasAudioPlayed){
                _uiController.ShowErrorAudio();
            }
            else{
                fixationPoints = Fixation_Exercise.CalculatePoints(selectedOption, selectedBtn.GetComponent<Image>().color);
                for (int i = 0; i < optionBtnArray.Length; i++){
                    optionBtnArray[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                selectedBtn.GetComponent<Image>().color = Color.green;
                if (_wasAudioPlayed)_isWordsOptionSelected = true;
            }

        }
    }

    public void CheckLanguageExercise(bool isCorrect){
        if (isCorrect){
            languagePoints++;
        }
        languageCounter++;
    }

    public void SelectMemoryButton(){

        GameObject selectedBtn = EventSystem.current.currentSelectedGameObject;

        string searchTag = selectedBtn.tag;

        GameObject[] memoryBtnArray = GameObject.FindGameObjectsWithTag(searchTag); //Arreglo de objetos con todos los botones de los meses

        string selectedOption = selectedBtn.name.ToUpper();


        if (!_errorPanel.activeInHierarchy){

            string correctValue = "";

            switch (searchTag){
                case "Memory1":
                    if (memory1 < 1){
                        memory1 = 1;
                        if (memoryCounterAll < 3)memoryCounterAll++;
                    }
                    correctValue = "PESETA";
                break;

                case "Memory2":
                    if (memory2 < 1){
                        memory2 = 1;
                        if (memoryCounterAll < 3)memoryCounterAll++;
                    }
                    correctValue = "CABALLO";
                break;

                case "Memory3":
                    if (memory3 < 1){
                        memory3 = 1;
                        if (memoryCounterAll < 3)memoryCounterAll++;
                    }
                    correctValue = "MANZANA";
                break;
            }
            memoryPoints = Memory_Test.CalculatePoints(selectedOption, correctValue, selectedBtn.GetComponent<Image>().color, searchTag);

            for (int i = 0; i < memoryBtnArray.Length; i++){
                memoryBtnArray[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

            selectedBtn.GetComponent<Image>().color = Color.green;
        }

    }

    public bool IsExerciseDone(string exercise){
        switch (exercise){
            case "orientation":
                if (_isDaySelected && _isMonthSelected) return true;
            return false;

            case "clock":
                return Clock_Test.IsDone();

            case "language":
                if (languageCounter == 4) return true;
            return false;

            case "fixation":
                if (_isWordsOptionSelected){
                    audioSrc.Stop();
                    return true;
                }
            return false;

            case "calculus":
                if(Calculus_Test.AreAllFieldsFull()){
                    calculusPoints = Calculus_Test.CalculatePoints();
                    return true;
                }
            return false;

            case "memory":
                if (memoryCounterAll == 3)return true;
            return false;
        }
        return false;
        
    }

    public int CalculateTotalPoints(){
        totalPoints = orientationPoints + Clock_Test.clockPoints + fixationPoints + languagePoints + calculusPoints + memoryPoints;

        return totalPoints;
    }

    
}
