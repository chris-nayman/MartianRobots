using MartianRobots.model.contracts;
using MartianRobots.ui;

namespace MartianRobots.model.impl
{

    class ConsoleInputProvider : IInputProvider
    {
        public (InputModel? Model, string? Error) Get()
        {
            // TODO: Pull the maxCoord (50) into AppSettings - Time restricted atm
            return InputScreens.BuildModelFromConsole(50);
        }
    }
}
