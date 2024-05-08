using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Pool
{
    public class BulletCasing : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField] private float impulse;
        private Vector3 dir = Vector3.zero;

        [SerializeField] private float timeoutDelay = 3f;

        private IObjectPool<BulletCasing> _objectPool;
        public IObjectPool<BulletCasing> ObjectPool
        {
            set => _objectPool = value;
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            dir = (transform.right + transform.up).normalized;
        }

        private void OnEnable()
        {
            rb.AddForce(dir * impulse, ForceMode.Impulse);
        }

        public void Deactivate()
        {
            StartCoroutine(DeactivateRoutine(timeoutDelay));
        }

        private IEnumerator DeactivateRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
            
            

            _objectPool.Release(this);
        }
    }
}