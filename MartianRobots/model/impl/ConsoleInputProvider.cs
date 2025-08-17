using MartianRobots.model.contracts;
using MartianRobots.ui;

namespace MartianRobots.model.impl
{

    class ConsoleInputProvider : IInputProvider
    {
        public (InputModel? Model, string? Error) Get()
        {
            return InputScreens.BuildModelFromConsole(50);
        }
    }
}
