using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScore : MonoBehaviour
{
    public float temps;
    public int score;
    public int manche;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    
}
