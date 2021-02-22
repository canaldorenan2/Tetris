using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData 
{
    public int [] score = new int [11];

    public int i = 0;

    public PlayerData(GameMananger gm)
    {
        i = gm.pontos;
        score[0] = i;

        Debug.Log("Pontos - player Data" + score[0]);

        //Array.Reverse(score);
    }
    
}
