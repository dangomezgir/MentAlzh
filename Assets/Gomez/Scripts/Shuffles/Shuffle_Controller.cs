using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Shuffle(List<Vector2> list, float delay){
        while (true)
        {
            yield return new WaitForSeconds(delay);
            for (int i = 0; i < list.Count; i++) {
                Vector3 temp = list[i];
                int randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
    }
}
