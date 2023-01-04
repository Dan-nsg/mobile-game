using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plataformer2D.Core.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected void Awake() 
        {
            if(Instance == null)
                Instance = GetComponent<T>();
            else
                Destroy(gameObject);
        }
    }
}