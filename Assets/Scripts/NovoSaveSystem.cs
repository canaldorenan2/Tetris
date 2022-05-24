using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class NovoSaveSystem : MonoBehaviour
{

    public Text score1, score2, score3;

    // Start is called before the first frame update
    void Start()
    {
        int first = PlayerPrefs.GetInt("Score1");
        score1.text = first.ToString();

        int second = PlayerPrefs.GetInt("Score2");
        score2.text = second.ToString();

        int third = PlayerPrefs.GetInt("Score3");
        score3.text = third.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
