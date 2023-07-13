using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    Image LoadImage;
    // Start is called before the first frame update
    void Start()
    {
        LoadImage = gameObject.GetComponent<Image>();
        LoadImage.color = Color.black;
        Invoke("EndLoading", 2);
    }

    void EndLoading()
    {
        Destroy(gameObject);
    }

}
