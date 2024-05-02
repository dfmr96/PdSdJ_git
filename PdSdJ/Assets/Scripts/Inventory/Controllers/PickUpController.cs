using TMPro;
using UnityEngine;

namespace Inventory.Controllers
{
    public class PickUpController : MonoBehaviour
    {
        [SerializeField] private GameObject pickUpPromptPanel;
        [SerializeField] private GameObject pickUpYES;
        [SerializeField] private GameObject pickUpNO;
        [SerializeField] private TMP_Text pickUpPromptText;
    }
}