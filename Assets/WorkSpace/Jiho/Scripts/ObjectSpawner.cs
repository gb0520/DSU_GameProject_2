using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace JH
{
    [System.Serializable]
    public class ObjectTransform
    {
        public string objectName;
        public Transform[] transforms;
        [Header("°Çµé ¤¤¤¤")]
        public GameObject[] tempObject;
    }

    public class ObjectSpawner : MonoBehaviour
    {

        public ObjectTransform[] objTransforms;

        public void SpawnerInit()
        {
            for(int i = 0; i < objTransforms.Length; i++)
            {
                MobSpawn(objTransforms[i].transforms.Length, i);
            }
        }

        public void MobSpawn(int count, int index)
        {
            objTransforms[index].tempObject = new GameObject[count];
            for(int i = 0; i < count; i++)
            {
                GameObject item = ObjectPool.instance.Dequeue(index);
                objTransforms[index].tempObject[i] = item;
                item.transform.SetParent(transform);
                item.gameObject.transform.position = objTransforms[index].transforms[i].position;
                item.SetActive(true);
            }

            
        }

        public void ReturnObject()
        {
            for(int i = 0; i < objTransforms.Length; i++)
            {
                for(int j = 0; j < objTransforms[i].tempObject.Length; j++)
                {
                    if (objTransforms[i].tempObject[j] != null)
                        ObjReturn(objTransforms[i].tempObject[j], i);
                }
            }
        }


        public static void ObjReturn(GameObject _obj, int _count)
        {
            ObjectPool.instance.ReturnPool(_obj, _count);
        }

    }
}


