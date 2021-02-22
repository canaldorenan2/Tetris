using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetro : MonoBehaviour
{

    public bool naoRoda;

    //float velocidadeQueda = 0.1f;
    float queda = 0;

    //int tempoMudarNivel = 0;

    public float tempoParaMoverPeca;
    public float timer;

    SpawnTetro spawnTetro;
    GameMananger gameManager;


    // Start is called before the first frame update
    void Start()
    {
        timer = tempoParaMoverPeca;

        spawnTetro = FindObjectOfType<SpawnTetro>();
        gameManager = FindObjectOfType<GameMananger>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp("s"))
        {
            timer = tempoParaMoverPeca;
        }

        if (Input.GetKey("a"))
        {
            timer += Time.deltaTime;

            if (timer > tempoParaMoverPeca)
            {
                MoveEsquerda();
                timer = 0;
            }
        }

        if (Input.GetKey("d"))
        {
            timer += Time.deltaTime;

            if (timer > tempoParaMoverPeca)
            {
                MoveDireita();
                timer = 0;
            }

        }
        
        if (Input.GetKey("s"))
        {
            timer += Time.deltaTime;

            if (timer > tempoParaMoverPeca)
            {
                MoveBaixo();
                timer = 0;
                queda = Time.time;
            }

        }

        if (Input.GetKeyDown("w"))
        {
            ChecaSeRoda();
        }

        
        
        // Controla a queda dos blocos
        if ((Time.time - queda) > (1.1f - gameManager.velocidadeQueda))
        {
            MoveBaixo();
            queda = Time.time;
        }

        // Pausa
        if (Input.GetKeyDown("p"))
        {
            Pausa();
        }
    }

    public void MoveDireita()
    {
        transform.position += new Vector3(1, 0, 0);

        if (PosicaoValida())
        {
            gameManager.atualizaGrade(this);

        }
        else
        {
            MoveEsquerda();
        }
    }

    public void MoveEsquerda()
    {
        transform.position += new Vector3(-1, 0, 0);

        if (PosicaoValida())
        {
            gameManager.atualizaGrade(this);
        }
        else
        {
            MoveDireita();
        }
    }

    public void MoveBaixo()
    {
        transform.position += new Vector3(0, -1, 0);

        if (PosicaoValida())
        {
            gameManager.atualizaGrade(this);
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);

            // Apaga a linha se ela estiver cheia
            gameManager.ApagaLinha();

            if (gameManager.AcimaGrade(this))
            {
                gameManager.GameOver();
            }

            gameManager.AcimaGrade(this);



            this.enabled = false;
            spawnTetro.ProximaPeca();
        }
    }

    public void Rotaciona()
    {
        transform.Rotate(0, 0, -90, 0);

        if (PosicaoValida())
        {
            gameManager.atualizaGrade(this);
        }
        else
        {
            transform.Rotate(0, 0, 90, 0);
        }
    }

    void ChecaSeRoda()
    {
        if (!naoRoda)
        {
            Rotaciona();
        }
    }

    bool PosicaoValida()
    {
        foreach (Transform child in transform)
        {
            Vector2 posBloco = gameManager.Arredonda(child.position);

            if (!gameManager.DentroGrade(posBloco))
            {
                return false;
            }

            /*
            
            Essa parte abaixo ira verificar se a posição do bloquinho ja está sendo ocupada por outro bloco( verifica também se não é o própio bloco). 
            Caso seja verdade, ele ira retornar false.

             */


            // Verifica se não ´-e negativo e se não é a peça que esta sendo mexida no momento
            if (gameManager.posicaoTransformGrade(posBloco) != null && gameManager.posicaoTransformGrade(posBloco).parent != transform)
            {
                return false;
            }

        }

        return true;
    }

    public void Pausa()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
