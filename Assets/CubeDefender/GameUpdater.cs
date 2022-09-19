using System;
using UnityEngine;

namespace CubeDefender
{
    /// <summary>
    /// Класс по обновлению подключенных объектов
    /// </summary>
    public sealed class GameUpdater : MonoBehaviour
    {
        public event Action OnUpdate;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            OnUpdate?.Invoke();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameProcessor.Instance.Restart();
            }
        }
    }
}
