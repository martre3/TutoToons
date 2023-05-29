using System;
using UnityEngine;

namespace TutoToons
{
    public interface ISpriteExtender
    {
        void ExtendTo(Vector2 to, Action<bool> extendedCallback);
    }
}
