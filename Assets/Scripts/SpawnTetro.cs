using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class SpawnTetro : MonoBehaviour
{

    public int proxPeca;

    public Transform[] criapecas;
    public List<GameObject> mostraPecas;

    public Text tetrisText;
    int random;

    float aux;

    Transform instanciaPeca;
    Tetro tetroScript;

    void Start()
    {
        proxPeca = Random.Range(0, 7);
        ProximaPeca();

    }

    public void ProximaPeca()
    {
        
        instanciaPeca = Instantiate(criapecas[proxPeca], this.transform.position, Quaternion.identity);
        tetroScript = instanciaPeca.GetComponentInParent<Tetro>(); 

        proxPeca = Random.Range(0, 7);

        for (int i = 0; i < mostraPecas.Count; i++)
        {
            mostraPecas[i].SetActive(false);
        }

        mostraPecas[proxPeca].SetActive(true);
    }

    public void Roda()
    {
        tetroScript.Rotaciona();
    }

    public void Desce()
    {
        tetroScript.MoveBaixo();
    }

    public void MoveParaDireita()
    {
        tetroScript.MoveDireita();
    }

    public void MoveParaEsquerda()
    {
        tetroScript.MoveEsquerda();
    }
}
