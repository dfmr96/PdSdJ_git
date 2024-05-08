namespace Inventory.Controllers
{
    public interface IPickUpProvider
    {
        public PickUpController PickUpController { get; }
    }
}