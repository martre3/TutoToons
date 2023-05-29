using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public interface IActivatable
    {
        bool IsDisabled();
        void Disable();
        void Activate();
    }
}
