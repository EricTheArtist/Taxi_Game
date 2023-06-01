using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    public Animator CharacterAnimEric;
    public Transform LeftStartPos;
    public Transform RightStartPos;

    
    public void PlayCharacterJump()
    {
        //CharacterAnimEric.SetTrigger("isPassengerBoarding");
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {

        //Vector3 leftLane = new Vector3(-1.27f, transform.localPosition.y, transform.localPosition.z);
        //Vector3 rightLane = new Vector3(1.27f, transform.localPosition.y, transform.localPosition.z);

        if (transform.parent.position.x == -4f)
        {
            //transform.localPosition = leftLane;
            transform.localPosition = LeftStartPos.localPosition;
            transform.localRotation = LeftStartPos.localRotation;

        }


        if (transform.parent.position.x == 4f)
        {
            //transform.localPosition = rightLane;
            transform.localPosition = RightStartPos.localPosition;
            transform.localRotation = RightStartPos.localRotation;

        }
    }
}
