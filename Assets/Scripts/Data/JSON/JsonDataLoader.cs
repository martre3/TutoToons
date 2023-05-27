using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TutoToons
{
    public class JsonDataLoader : MonoBehaviour, IDataLoader
    {
        public T Load<T>(string path)
        {
            string jsonString = Resources.Load<TextAsset>(path)?.text
                                ?? throw new FileNotFoundException($"Levels data file cannot be located at {path}");

            return JsonUtility.FromJson<T>(jsonString);
        }
    }
}
