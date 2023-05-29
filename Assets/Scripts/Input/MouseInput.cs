using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class MouseInput : MonoBehaviour, IScreenInput
    {
        public bool IsTriggered()
        {
            return Input.GetMouseButtonDown(0);
        }

        public Vector2 GetPositionOnScreen()
        {
            return Input.mousePosition;
        }
    }
}
