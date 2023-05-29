using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseObjectButton : MonoBehaviour
{

    public GameObject ObjectToDisable;


    public void Button_DisableObject()
    {
        ObjectToDisable.SetActive(false);
    }
}
