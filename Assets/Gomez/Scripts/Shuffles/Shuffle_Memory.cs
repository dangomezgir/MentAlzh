using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle_Memory : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] memory1, memory2, memory3;
    private List<GameObject> words = new List<GameObject>();

    private List<Vector2> wordTargetPositions = new List<Vector2>();

    private Shuffle_Controller shuffle;

    [SerializeField] float delay, speed;
    private float step;

    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        shuffle = GameObject.Find("Shuffle_Controller").GetComponent<Shuffle_Controller>();
        memory1 = GameObject.FindGameObjectsWithTag("Memory1");
        memory2 = GameObject.FindGameObjectsWithTag("Memory2");
        memory3 = GameObject.FindGameObjectsWithTag("Memory3");
        words.AddRange(memory1);
        words.AddRange(memory2);
        words.AddRange(memory3);

        for (int i = 0; i < words.Count; i++){
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
