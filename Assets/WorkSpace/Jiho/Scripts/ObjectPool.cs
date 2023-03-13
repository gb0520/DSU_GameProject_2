using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    [System.Serializable]
    public class ObjectInfo
    {
        public string objectName;
        public GameObject perfab;
        public int count;
    }

    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool instance;

        [SerializeField] private ObjectInfo[] objectInfos = null;

        [Header("오브젝트 풀의 위치")]
        [SerializeField] private Transform tfPoolParent;

        public List<Queue<GameObject>> objectPoolList;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            objectPoolList = new List<Queue<GameObject>>();
            ObjectPoolState();
        }

        private void ObjectPoolState()
        {
            if (objectInfos != null)
            {
                for (int i = 0; i < objectInfos.Length; i++)
                {
                    objectPoolList.Add(InsertQueue(objectInfos[i], i));
                }
            }
        }

        public void ReturnPool(GameObject obj, int _count)
        {
            objectPoolList[_count].Enqueue(obj);
            obj.transform.SetParent(tfPoolParent);
            obj.SetActive(false);
        }

        public GameObject Dequeue(int _count)
        {
            if(objectPoolList[_count].Count > 0)
            {
                return objectPoolList[_count].Dequeue();
            }
            else
            {
                objectPoolList[_count].Enqueue(Enqueue(_count));
                return objectPoolList[_count].Dequeue();
            }
        }

        public GameObject Enqueue(int _count)
        {
            GameObject temp = Instantiate(objectInfos[_count].perfab) as GameObject;
            temp.SetActive(false);
            temp.transform.SetParent(tfPoolParent);
            return temp;
        }

        Queue<GameObject> InsertQueue(ObjectInfo perfab_objectInfo, int _count)
        {
            Queue<GameObject> test_queue = new Queue<GameObject>();

            for (int i = 0; i < perfab_objectInfo.count; i++)
            {
                GameObject objectClone = Instantiate(perfab_objectInfo.perfab) as GameObject;
                objectClone.SetActive(false);
                objectClone.transform.SetParent(tfPoolParent);
                test_queue.Enqueue(objectClone);
                //오브젝트.getcomponent<???>.count = _count;
            }

            return test_queue;
        }
    }
}


