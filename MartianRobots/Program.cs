using MartianRobots.model.impl;
using MartianRobots.ui;
using System.Text;

namespace MartianRobots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            LandingChoice choice;
            do
            {
                choice = LandingScreen.Show();

                switch (choice)
                {
                    case LandingChoice.RunSampleCases:
                    {
                        var (fileViewModel, fileInputError) = RobotController.Execute(new FileInputProvider("input.txt"));
                        if (fileInputError is not null)
                            SampleResultsScreen.ShowError(fileInputError);
                        else
                            SampleResultsScreen.Show(fileViewModel!);
                        break;
                    }

                    case LandingChoice.RunInteractiveMode:
                    {
                        var (consoleViewModel, consoleError) = RobotController.Execute(new ConsoleInputProvider());
                        if (consoleError is not null)
                            ConsoleResultsScreen.ShowError(consoleError);
                        else
                            ConsoleResultsScreen.Show(consoleViewModel!);
                        break;
                    }

                    case LandingChoice.Exit:
                        // fall through to end the loop
                        break;
                }
            }
            while (choice != LandingChoice.Exit);
        }
    }
}
