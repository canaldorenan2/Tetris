using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    static bool arquivoCriado = false;

    public static void SaveScore(GameMananger gm)
    {

        int[] loadScore = LoadScore();
        int[] aux = new int[1] { 0 };

        PlayerData data = new PlayerData(gm);

        aux[0] = data.score[0];
        Debug.Log("valor aux: " + aux[0]);
        Debug.Log("data.score[0]: " + data.score[0]);
        {

            for (int i = 0; i < data.score.Length; i++)
            {
                data.score[i] = loadScore[i];
                //Debug.Log("score posição " + i + ": " + data.score[i]);

            }

            data.score[10] = aux[0];

            // Sort
            
            Array.Sort(data.score);

            for (int i = 0; i < data.score.Length; i++)
            {
                Debug.Log("Depois do sorte, score posição " + i + ": " + data.score[i]);
            }
        }

        BinaryFormatter formatoBinario = new BinaryFormatter();

        // Local onde vai ser salvo os dados
        string path = Application.persistentDataPath + "/player.ane";

        // Puxar as infos que serão salvas
        FileStream fileStream = new FileStream(path, FileMode.Create);

        // Formata e grava
        formatoBinario.Serialize(fileStream, data);
        //Debug.Log("Criado " + data.score);

        fileStream.Close();
    }

    public static int[] LoadScore()
    {
        //Debug.Log("Load Chamado");

        // Local onde vai ser carregado os dados
        string path = Application.persistentDataPath + "/player.ane";

        // Verifica se o arquivo com os dados existe
        if (File.Exists(path))
        {
            BinaryFormatter formatoBinario = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            // transforma em um formato que pode ser lido
            // o as no final formata como PlayerData
            //Debug.Log("Load Chamado4");

            PlayerData data = formatoBinario.Deserialize(fileStream) as PlayerData;
            //Debug.Log("Load Chamado5");
            fileStream.Close();

            //Debug.Log("posicao 0: " + data.score[0] + "..... Posicao 11: " + data.score[10]);
            Array.Reverse(data.score);
            //Debug.Log("posicao 0: " + data.score[0] + "..... Posicao 11: " + data.score[10]);
            return data.score;
        }
        else
        {
            int[] i = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Debug.Log("Valor Bool " + arquivoCriado);

            Debug.Log("Arquivo com save não encontrado");

            return i;
        }
    }
}
