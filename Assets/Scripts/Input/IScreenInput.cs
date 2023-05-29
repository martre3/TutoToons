using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public interface IScreenInput
    {
        bool IsTriggered();
        Vector2 GetPositionOnScreen();
    }
}
