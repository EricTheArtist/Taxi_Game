using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeShoot : MonoBehaviour
{

    public GameObject projectile;
    public float launchVelocity = 700f;
    public Transform ShootPos;
    public GameObject Tank;
    public GameObject Turret;
    public ParticleSystem MuzleFlash;
    bool AimL = false;
    bool AimR = false;
    bool AimUP = false;
    bool AimDOWN = false;

    public void ShootBullet()
    {

            GameObject ball = Instantiate(projectile, ShootPos.position,ShootPos.rotation);
            MuzleFlash.Play();
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity));
        
    }

    private void FixedUpdate()
    {
        if (AimL == true && (Tank.transform.eulerAngles.y < 45 || Tank.transform.eulerAngles.y > 320))
        {
            Tank.transform.Rotate(0, -0.5f, 0, Space.Self);
           
        }
        if (AimR == true && (Tank.transform.eulerAngles.y < 40 || Tank.transform.eulerAngles.y > 318))
        {
            Tank.transform.Rotate(0, 0.5f, 0, Space.Self);
            
        }
        if (AimUP == true && (Turret.transform.eulerAngles.x > 345 || Turret.transform.eulerAngles.x < 8))
        {
            Turret.transform.Rotate(-0.5f, 0, 0, Space.Self);
            Debug.Log(Tank.transform.eulerAngles.x);
        }
        if (AimDOWN == true && (Turret.transform.eulerAngles.x < 5 || Turret.transform.eulerAngles.x > 340))
        {
            Turret.transform.Rotate(0.5f, 0, 0, Space.Self);
            Debug.Log(Tank.transform.eulerAngles.x);
        }
    }

    public void StartAimL()
    {
        AimL = true;
    }
    public void StopAimL()
    {
        AimL = false;
    }

    public void StartAimR()
    {
        AimR = true;
    }
    public void StopAimR()
    {
        AimR = false;
    }

    public void StartAimUP()
    {
        AimUP = true;
    }
    public void StopAimUP()
    {
        AimUP = false;
    }

    public void StartAimDOWN()
    {
        AimDOWN = true;
    }
    public void StopAimDOWN()
    {
        AimDOWN = false;
    }






}
