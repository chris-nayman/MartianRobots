namespace MartianRobots.model.contracts
{
    public interface IInputProvider
    {
        (InputModel? Model, string? Error) Get();
    }
}
