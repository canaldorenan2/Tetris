using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    Transform container, template;

    Text pos, pts, nic;

    int [] scoreLoad;
    int [] aux;
    int [] highscore;

    public ControlSave cs;


    float posy;

    private void Awake()
    {
        container = transform.Find("HighScoreContainer");
        template = container.Find("Template");

        //cs = transform.Find("LoadGO").GetComponent<ControlSave>();

        template.gameObject.SetActive(false);

        scoreLoad = cs.Load();

        posy = 0;

        /*
        // Sort by the higgest score
        for (int i = 0; i < 10; i++)
        {
            if (scoreLoad[i] > highscore[i])
            {

            }
        }*/


            // Display Score
            for (int i = 0; i < 11; i++)
        {
            Transform instancia = Instantiate(template, container);

            // React pega as informações de posição como ponto de pivo, etc.
            RectTransform instanciaRect = instancia.GetComponent<RectTransform>();

            posy -= 0.5f ;

            instanciaRect.anchoredPosition = new Vector2(0, posy);
            instancia.gameObject.SetActive(true);

            int posicao = i + 1;
           

            instancia.Find("Pos").GetComponent<Text>().text = "" + posicao;
            instancia.Find("Pts").GetComponent<Text>().text = "" + scoreLoad[i];
            instancia.Find("Nic").GetComponent<Text>().text = "ABC";
        }

        /*
        for(int i = 0; i < 7; i++)
        {
            Transform instancia = Instantiate(template, container);
            RectTransform instanciaRect = instancia.GetComponent<RectTransform>();
            instanciaRect.anchoredPosition = new Vector2(0, -i);
            instancia.gameObject.SetActive(true);

            int rank = i + 1;

            int score = Random.Range(0, 1000);

            string nickName = "ABC";

            instancia.Find("Pos").GetComponent<Text>().text = "" + rank;
            instancia.Find("Pts").GetComponent<Text>().text = "" + score;
            instancia.Find("Nic").GetComponent<Text>().text = "" + nickName;
        }*/

    }
}
