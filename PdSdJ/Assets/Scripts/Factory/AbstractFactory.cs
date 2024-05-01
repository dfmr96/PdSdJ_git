using UnityEngine;

namespace Factory
{
    public abstract class AbstractFactory<T> : MonoBehaviour where T : IProduct
    {
        public abstract T CreateProduct(string id);
    }
}