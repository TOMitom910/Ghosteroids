using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI tempsText;
    [SerializeField]
    private TextMeshProUGUI roundText;
    
    [SerializeField]
    private SaveScore saveScore;
    
    // Start is called before the first frame update
    void Start()
    {
        saveScore = FindObjectOfType<SaveScore>();
        roundText.text = "Manche validé :  "+ saveScore.manche;
        scoreText.text = "Score :  "+ saveScore.score;
        tempsText.text = "Temps survécu :  "+saveScore.temps.ToString("F2")+"s";
    }
}
