using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private int testInt;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject item = ObjectPool.instance.Dequeue(0);
                item.SetActive(true);
                float positionX = Random.Range(-4f, 4f);
                float positionZ = Random.Range(-4f, 4f);
                item.gameObject.transform.position = new Vector3(positionX, 5, positionZ);
                item.transform.SetParent(transform);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject item = ObjectPool.instance.Dequeue(1);
                item.SetActive(true);
                float positionX = Random.Range(-4f, 4f);
                float positionZ = Random.Range(-4f, 4f);
                item.gameObject.transform.position = new Vector3(positionX, 5, positionZ);
                item.transform.SetParent(transform);
            }
        }

        public static void ObjReturn(GameObject _obj, int _count)
        {
            ObjectPool.instance.ReturnPool(_obj, _count);
        }
    }
}


