using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle_Language : MonoBehaviour
{
    private GameObject[] words;
    
    private List<Vector2> wordTargetPositions = new List<Vector2>();

    private Shuffle_Controller shuffle;

    [SerializeField] float delay, speed;
    private float step;

    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        shuffle = GameObject.Find("Shuffle_Controller").GetComponent<Shuffle_Controller>();
        words = GameObject.FindGameObjectsWithTag("Word");

        for (int i = 0; i < words.Length; i++){
            wordTargetPositions.Add(words[i].transform.position);
        }
        canMove = true;
        StartCoroutine(shuffle.Shuffle(wordTargetPositions, delay));
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;
        if (canMove){
            MoveObjects();
        }
    }

    public void StopShuffle(){
        StopCoroutine(shuffle.Shuffle(wordTargetPositions, delay));
        canMove = false;
    }  

    private void MoveObjects(){
        for (int i = 0; i < wordTargetPositions.Count; i++){
            words[i].transform.position = Vector3.Lerp(words[i].transform.position, wordTargetPositions[i], step);
        }
    }
}
