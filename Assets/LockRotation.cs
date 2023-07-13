using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{

    public GameObject Player;
    [SerializeField] private AudioClip policesirenClip;


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z -2.5f); 
    }

    private void Awake()
    {
        SoundManager.Instance.PlaySound(policesirenClip);
    }

    private void OnDisable()
    {
        //SoundManager.Instance.
    }
}
