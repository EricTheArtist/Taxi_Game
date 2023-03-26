using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class Test_Floating_Origin : MonoBehaviour
{
    public float threshold = 500f;
    public RoadSpawner Road_Spawner;

    private void LateUpdate()
    {
        Vector3 cameraPosition = gameObject.transform.position;
        cameraPosition.y = 0f;

        if(cameraPosition.magnitude > threshold)
        {
            for (int z = 0; z < SceneManager.sceneCount; z++)
            {
                foreach(GameObject g in SceneManager.GetSceneAt(z).GetRootGameObjects())
                {
                    g.transform.position -= cameraPosition;
                }
            }

            Vector3 originDelta = Vector3.zero - cameraPosition;
            Road_Spawner.AlignRoads(); // calls funtion that makes sure roads are in the middle of the 5 lanes regardless of the camera position
        }

    }

}
