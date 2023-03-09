using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleInputNamespace
{
    public class MenuCharacterController : MonoBehaviour
    {
        public float movementSpeed = 10f;
        //public SpawnManager spawnManager;

        public SteeringWheel SW;

        private float hMovement;

        void Update()
        {
            if (transform.position.x > 4.2f && SW.Angle > 0) //ther 4.2 is the cordinqate position of the edge of the road
            {
                hMovement = 0 * movementSpeed / 100;
            }
            else if (transform.position.x < -4.2f && SW.Angle < 0)
            {
                hMovement = 0 * movementSpeed / 100;
            }
            else
            {
                hMovement = SW.Angle * movementSpeed / 100;
            } 
            //float vMovement = 1 * movementSpeed;

            transform.Translate(new Vector3(hMovement, 0f, 0f) * Time.deltaTime);

        }

        private void OnTriggerEnter(Collider other)
        {
           if (other.tag == "Play")
            {
                print("play");
                SceneManager.LoadScene("Game_Scene");
            }
            if (other.tag == "Credits")
            {
                print("Credits");
            }
            if (other.tag == "Quit")
            {
                print("Quit");
                Application.Quit();
            }
        }

    }
}
