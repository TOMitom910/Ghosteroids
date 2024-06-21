using UnityEngine;

public class GoTo : MonoBehaviour
{
    public Vector3 target; // Position cible vers laquelle se déplacer
    public float speed = 5f; // Vitesse de déplacement

    void Update()
    {
        // Vérifier si la cible est définie
        if (target != null)
        {
            // Calculer la direction vers la cible
            Vector3 direction = (target - transform.position).normalized;

            // Calculer la distance jusqu'à la cible
            float distance = Vector3.Distance(transform.position, target);

            // Si on n'est pas encore arrivé à la cible, continuer à avancer
            if (distance > 0.1f)
            {
                // Déplacer l'objet vers la cible
                transform.position += direction * speed * Time.deltaTime;
            }
            else
            {
                // Arrivé à la cible, on peut exécuter une action ou simplement arrêter le mouvement
                Debug.Log("Arrivé à la position cible !");
                // Par exemple, désactiver ce script pour arrêter le mouvement
                enabled = false;
            }
        }
    }
}