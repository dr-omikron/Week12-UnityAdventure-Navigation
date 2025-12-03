using UnityEngine;

namespace PlayerInputs
{
    public class PlayerInput
    {
        private const int LeftMouseButtonID = 0;

        public bool IsRayCastingStart => Input.GetMouseButtonDown(LeftMouseButtonID);
        public Vector3 MousePosition => Input.mousePosition;
    }
}
