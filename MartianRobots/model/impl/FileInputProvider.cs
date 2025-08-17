using MartianRobots.infrastructure;
using MartianRobots.model.contracts;

namespace MartianRobots.model.impl
{
    class FileInputProvider: IInputProvider
    {
        private readonly string _path;
        public FileInputProvider(string path) => _path = path;
        public (InputModel?, string?) Get()
        {
            try
            {
                return (InputFileHandler.ParseFile(_path), null);
            }
            catch (Exception ex)
            {
                return (null, $"Failed to parse input: {ex.Message}");
            }
        }
    }
}
