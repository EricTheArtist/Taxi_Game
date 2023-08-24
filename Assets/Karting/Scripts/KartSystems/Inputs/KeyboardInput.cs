using UnityEngine;
using SimpleInputNamespace;

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

        public override InputData GenerateInput() {

            if (SWOW.Angle!=0)
            {
                Steering = Mathf.Sign(SWOW.Angle);
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

                //TurnInput = Input.GetAxis("Horizontal")
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
