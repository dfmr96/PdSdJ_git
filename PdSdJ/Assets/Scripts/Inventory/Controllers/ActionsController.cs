using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Controllers
{
    public class ActionsController : MonoBehaviour
    {
        [SerializeField] private GameObject actionPanel;
        [SerializeField] private List<Button> actionButtons;
    }
}