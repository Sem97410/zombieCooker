using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variable pour stocker la position du joueur
    [SerializeField]
    private Transform player;

    // Variable pour définir la distance entre le joueur et la caméra
    [SerializeField]
    private float distance;
        
    // Variable pour définir la hauteur de la caméra par rapport au joueur
    [SerializeField]
    private float height;

    void Update()
    {
        // Calcul de la nouvelle position de la caméra en fonction de la position du joueur
        Vector3 pos = player.position - player.forward * distance;
        pos.y += height;

        // Mise à jour de la position de la caméra
        transform.position = pos;

        // Mise à jour de la rotation de la caméra pour qu'elle regarde toujours vers le joueur
        transform.LookAt(player.position);
    }
}
