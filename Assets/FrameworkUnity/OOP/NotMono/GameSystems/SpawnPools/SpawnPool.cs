using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FrameworkUnity.OOP.NotMono.GameSystems.SpawnPools
{
    [Serializable]
    public class SpawnPool
    {
        [Header("Spawn")]
        [SerializeField] private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _prefab;

        private readonly Queue<GameObject> _objectPool = new();


        public void InstallPool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var obj = Object.Instantiate(_prefab, _container);
                obj.SetActive(false);
                _objectPool.Enqueue(obj);
            }
        }

        public bool TrySpawn(out GameObject obj)
        {
            obj = null;

            if (!_objectPool.TryDequeue(out var spawnedObject))
            {
                return false;
            }
            else
            {
                spawnedObject.transform.SetParent(_worldTransform);
                spawnedObject.SetActive(true);
                obj = spawnedObject;
                return true;
            }
        }

        public void Spawn(out GameObject obj)
        {
            obj = null;

            if (_objectPool.TryDequeue(out var spawnedObject))
            {
                spawnedObject.transform.SetParent(_worldTransform);
                spawnedObject.SetActive(true);
                obj = spawnedObject;
            }
            else
            {
                obj = Object.Instantiate(obj, _worldTransform);
            }
        }

        public void Unspawn(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_container);
            _objectPool.Enqueue(obj);
        }
    }
}