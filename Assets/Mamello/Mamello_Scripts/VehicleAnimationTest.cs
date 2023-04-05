using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAnimationTest : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //animator.SetBool("isSwervingRight", true);
            animator.SetTrigger("isSwervingRight 0");
        }
        /*else
        {
            animator.SetBool("isSwervingRight", false);
        }*/

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //animator.SetBool("isSwervingLeft", true);
            animator.SetTrigger("isSwervingLeft 0");
        }
        /*else
        {
            animator.SetBool("isSwervingLeft", false);
        }*/
    }

    public void SwerveAnimations()
    {
     
    }

}
