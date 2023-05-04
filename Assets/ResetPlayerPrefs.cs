using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
