using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variable pour stocker la position du joueur
    [SerializeField]
    private Transform player;

    // Variable pour d�finir la distance entre le joueur et la cam�ra
    [SerializeField]
    private float distance;
        
    // Variable pour d�finir la hauteur de la cam�ra par rapport au joueur
    [SerializeField]
    private float height;

    void Update()
    {
        // Calcul de la nouvelle position de la cam�ra en fonction de la position du joueur
        Vector3 pos = player.position - player.forward * distance;
        pos.y += height;

        // Mise � jour de la position de la cam�ra
        transform.position = pos;

        // Mise � jour de la rotation de la cam�ra pour qu'elle regarde toujours vers le joueur
        transform.LookAt(player.position);
    }
}
