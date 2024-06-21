using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    
    private Vector2 vector;
    
    private SaveScore saveScore;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        saveScore = FindObjectOfType<SaveScore>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("Collision with projectile");
            //détruire le projectile
            Destroy(collision.gameObject);

            switch (gameObject.name)
            {
                case "SmallEnemy(Clone)":
                    gameManager.playerPoints += 60;
                    gameManager.scoreText.text = gameManager.playerPoints.ToString();
                    break;
                case "MediumEnemy(Clone)":
                    gameManager.CreateEnnemy("SmallEnemy", transform.position,vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
                    gameManager.CreateEnnemy("SmallEnemy", transform.position,vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
                    gameManager.playerPoints += 30;
                    gameManager.scoreText.text = gameManager.playerPoints.ToString();

                    break;
                case "LargeEnemy(Clone)":
                    gameManager.CreateEnnemy("MediumEnemy", transform.position,vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
                    gameManager.CreateEnnemy("MediumEnemy", transform.position,vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
                    gameManager.playerPoints += 10;
                    gameManager.scoreText.text = gameManager.playerPoints.ToString();
                    break;
                default:
                    Debug.Log("Enemy type not found");
                    break;
            }
            //detruire l'objet touché
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
            if (gameManager.numberOfLives > 0)
            {
                gameManager.numberOfLives--;
                Destroy
                (
                    gameManager.playerLife.transform.GetChild
                    (
                        gameManager.numberOfLives
                    ).gameObject
                );

                if (gameManager.numberOfLives == 0)
                {
                    saveScore.score = gameManager.playerPoints;
                    saveScore.temps = gameManager.timeSurvived;
                    saveScore.manche = gameManager.actualRound -1;
                    SceneManager.LoadScene("Dead");
                }
            }
            //faire perde 1 points de vie au joueur
        }

    }
    

}
