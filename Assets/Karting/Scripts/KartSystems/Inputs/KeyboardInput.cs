using UnityEngine;
using SimpleInputNamespace;
using TMPro;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";

        public SteeringWheel SWOW;
        float Steering;

        bool Forward;
        bool Reverse;

        

        public Rigidbody ThisRB;

        public override InputData GenerateInput() {

            if (SWOW.Angle!=0)
            {
                if(ThisRB.velocity.magnitude > 0.5)
                {
                    if (SWOW.Angle > 180)
                    {
                        Steering = 1;
                    }
                    else if(SWOW.Angle < -180)
                    {
                        Steering = -1;
                    }
                    else
                    {
                        Steering = (SWOW.Angle / 180);
                    }
                    
                }
                else
                {
                    Steering = 0;
                }
            }
            if (SWOW.Angle == 0)
            {
                Steering = 0;
            }
            return new InputData
            {
                Accelerate = Forward, //Input.GetKey(KeyCode.W),
                Brake = Reverse,//Input.GetKey(KeyCode.S),
                TurnInput = Steering

                
            };
        }


        public void ButtonForward()
        {
            Forward = true;
            
        }

        public void ButtonForwardUp()
        {
            Forward = false;
        }

        public void ButtonBackwards()
        {
            Reverse = true;
        }

        public void ButtonBackwardsUp()
        {
            Reverse = false;
        }
    }
}
