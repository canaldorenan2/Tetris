using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSave : MonoBehaviour
{
    public int[] Load()
    {
        int[] i = SaveSystem.LoadScore();
        return i;
    }
}
