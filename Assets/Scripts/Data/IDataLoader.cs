using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public interface IDataLoader
    {
        T Load<T>(string path);
    }
}
