using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotationAnimation : MonoBehaviour
{
    public float coinDelayTime = 10f;
    public float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RotationControl()
    {
        //GameObject[] parentCoinRow = GameObject.FindGameObjectsWithTag("CoinRow");
        GameObject parentCoinRow = GameObject.FindGameObjectWithTag("CoinRow");
        GameObject[] childrenCoin;

        for (int i=0; i<parentCoinRow.transform.childCount; i++)
        {
            childrenCoin = new GameObject[parentCoinRow.transform.childCount];
            childrenCoin[i] = parentCoinRow.transform.GetChild(i).gameObject;
            for(int j=0; j<childrenCoin.Length-1; j++)
            {
                //Invoke(nameof(RotationAnimation(childrenCoin[j].gameObject)), coinDelayTime);
                Invoke("RotationAnimation", coinDelayTime);
            }
        }

      
    }
    void RotationAnimation(GameObject coin)
    {
        coin.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);
    }
}
