namespace Inventory.Controllers
{
    public interface ISlotProvider
    {
        public SlotsController SlotsController { get; }
    }
}