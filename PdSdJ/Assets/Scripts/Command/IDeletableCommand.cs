namespace Command
{
    public interface IDeletableCommand : ICommand
    {
        void Undo();
    }
}