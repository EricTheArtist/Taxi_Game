using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    public GameObject[] Characters;
    // Start is called before the first frame update
    void Start()
    {
        int CharacterIndex = Random.Range(0, Characters.Length);
        Characters[CharacterIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
