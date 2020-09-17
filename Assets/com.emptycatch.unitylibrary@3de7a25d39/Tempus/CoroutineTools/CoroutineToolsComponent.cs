using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tempus.CoroutineTools
{
    public class CoroutineToolsComponent : MonoBehaviour
    {
        private Dictionary<IEnumerator, Coroutine> yields = new Dictionary<IEnumerator, Coroutine>();

        public void AddCoroutine(IEnumerator coroutine, Coroutine yield)
        {
            yields.Add(coroutine, yield);
        }

        public void RemoveCoroutine(IEnumerator coroutine)
        {
            yields.Remove(coroutine);
        }

        public bool HasStarted(IEnumerator coroutine)
        {
            return yields.ContainsKey(coroutine);
        }

        public Coroutine GetYield(IEnumerator coroutine)
        {
            return yields[coroutine];
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
