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
                float positionX = Random.Range(-4f, 4f);
                float positionZ = Random.Range(-4f, 4f);
                item.transform.SetParent(transform);
                item.gameObject.transform.position = new Vector3(positionX, 5, positionZ);
                item.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject item = ObjectPool.instance.Dequeue(1);
                float positionX = Random.Range(-4f, 4f);
                float positionZ = Random.Range(-4f, 4f);
                item.transform.SetParent(transform);
                item.gameObject.transform.position = new Vector3(positionX, 5, positionZ);
                item.SetActive(true);
            }
        }

        public static void ObjReturn(GameObject _obj, int _count)
        {
            ObjectPool.instance.ReturnPool(_obj, _count);
        }
    }
}


