using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fubix.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeReference] GameObject persistantObjectPrefab;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistantObjuct();
            hasSpawned = true;
        }

        private void SpawnPersistantObjuct()
        {
            GameObject persistantObject = Instantiate(persistantObjectPrefab);
            DontDestroyOnLoad(persistantObject);
        }
    }
}
