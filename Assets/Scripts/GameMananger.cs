using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMananger : MonoBehaviour
{
    public static int altura = 20;
    public static int largura = 10;

    public static Transform[,] grade = new Transform[largura, altura];

    public int pontos, pontosAux, auxLinhas, level;

    public float tempoMudaLevel, resetTime, timer, tempoParaMoverPeca;

    public  float velocidadeQueda = 0.15f;

    public bool pontuou, primeiraContagem;

    int tempoQueda = 36;

    int i = 1;

    Tetro tetro;

    public Text txtLevel;
    public Text txtScore;


    private void Start()
    {
        pontos = pontosAux = 0;

        timer = 0;

        tetro = GetComponent<Tetro>();

        txtScore.text = "" + pontos;
        txtLevel.text = "" + velocidadeQueda * 10;
    }

    private void Update()
    {
        tempoMudaLevel += Time.deltaTime;
        //Debug.Log("" + tempoMudaLevel);

        if (tempoMudaLevel > tempoQueda)
        {
            velocidadeQueda += 0.075f;

            i++;
            txtLevel.text = "" + i ;

            tempoMudaLevel = 0;
            tempoQueda += 1;
        }

        if (Input.GetKeyDown("m"))
        {
            pontos += 100;
            pontosAux = 0;
            txtScore.text = "" + pontos;
        }


    }

    // Verifica se o bloco está dentro dos limites da grade
    public bool DentroGrade(Vector2 posicao)
    {
        // Se a posicao em x for maior ou igual a zero, e, x menor que largura, e y maior ou igual a zero
        return ((int)posicao.x >= 0 && (int)posicao.x < largura && (int)posicao.y >= 0);
    }

    // Arredonda valores 
    public Vector2 Arredonda(Vector2 NumeroArredondado)
    {
        return new Vector2(Mathf.Round(NumeroArredondado.x), Mathf.Round(NumeroArredondado.y));
    }

    public void atualizaGrade(Tetro pecaTetris)
    {

        /*
         
         A lógica aqui é a seguinte, toda vez que é realizado um movimento na peça, esse metodo é chamado quando o movimento é válido

        Caso o movimento não seja válido, persevera os valores da última chamada
         
         */

        // limpa a grade referente ao bloco que está caindo
        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                if (grade[x, y] != null)
                {
                    // Verifica se não é a peça que está sendo usada agora
                    if (grade[x, y].parent == pecaTetris.transform)
                    {
                        grade[x, y] = null;

                    }
                }
            }
        }


        // Esse loop passa por cada quadradinho dentro da peça de tetris
        foreach (Transform quadradinhoPeca in pecaTetris.transform)
        {
            Vector2 posicao = Arredonda(quadradinhoPeca.position);


            if ((int)posicao.y < altura)
            {
                grade[(int)posicao.x, (int)posicao.y] = quadradinhoPeca;
            }

        }

    }

    public Transform posicaoTransformGrade(Vector2 posicao)
    {
        if (posicao.y > altura - 1)
        {
            return null;
        }
        else
        {
            return grade[(int)posicao.x, (int)posicao.y];
        }

    }

    public void ApagaLinha()
    {
        primeiraContagem = true;
        pontuou = false;

        for (int y = 0; y < altura; y++)
        {
            if (LinhaCheia(y))
            {
                DeletaQuadrado(y);
                MoveTodasLinhasBaixo(y + 1);
                y--;
                auxLinhas++;

                pontuou = true;
                if (pontuou && primeiraContagem)
                {
                    pontosAux = 100;
                    primeiraContagem = false;
                }
                else
                {

                    
                    if (auxLinhas >= 4)
                    {
                        pontosAux *= 2;
                    }
                    else
                    {
                        pontosAux += 300;
                    }

                }
            }
        }
        auxLinhas = 0;
        pontos += pontosAux;
        pontosAux = 0;
        txtScore.text = ""+ pontos;

    }

    



    public bool LinhaCheia(int y)
    {
        // Faz a verificação em x referente ao y informado. 
        // Se em algum lugar encontrar um valor null, retorna false. 
        // Caso contrario passa pelo loop e retorna true

        for (int x = 0; x < largura; x++)
        {
            if (grade[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    public void DeletaQuadrado(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            // destroi o game object
            Destroy(grade[x, y].gameObject);

            // deixa null o local dele na matrix
            grade[x, y] = null;
        }
    }

    public void MoveLinhaParaBaixo(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            // verifica se tem alguma coisa pra mover para baixo
            if (grade[x, y] != null)
            {
                // valor do array (estrutural)
                grade[x, y - 1] = grade[x, y];
                grade[x, y] = null;

                // valor do gameobject (visual)
                grade[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveTodasLinhasBaixo(int y)
    {
        for (int i = y; i < altura; i++)
        {
            MoveLinhaParaBaixo(i);
        }

    }
    public bool AcimaGrade(Tetro pecaTetro)
    {
        for (int x = 0; x < largura; x++)
        {
            foreach (Transform quadrado in pecaTetro.transform)
            {
                Vector2 posicao = Arredonda(quadrado.position);

                if (posicao.y > altura - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void GameOver()
    {
        int score1, score2, score3, aux1, aux2;
        
        score1 = PlayerPrefs.GetInt("Score1");
        score2 = PlayerPrefs.GetInt("Score2");
        score3 = PlayerPrefs.GetInt("Score3");




        // Verifica se o score obtido é o score mais alto de todos os tempos
        if (pontos > score1)
        {

            // Adiciona o valor obtido na partida atual ao maior high score
            PlayerPrefs.SetInt("Score1", pontos);


            // Movimenta o score maior anterior para essa posição
            PlayerPrefs.SetInt("Score2", score1);


            // Movimenta o score maior anterior para essa posição
            PlayerPrefs.SetInt("Score3", score2);


        }
        else
        {
            // Verifica se o score obtido é o segundo mais alto de todos os tempos
            if (pontos > score2)
            {
                PlayerPrefs.SetInt("Score2", pontos);

                // Movimenta o score maior anterior para essa posição
                PlayerPrefs.SetInt("Score3", score2);

            }
            else
            {
                // Verifica se o score obtido é o terceiro mais alto de todos os tempos
                if (pontos > score3)
                {
                    PlayerPrefs.SetInt("Score3", pontos);
                }
            }
        }





        SceneManager.LoadScene("GameOver");
    }

    public void Save()
    {



        SaveSystem.SaveScore(this);
    }

}
