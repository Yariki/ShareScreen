namespace ShareScreen.Core.Interfaces.Core
{
    public interface ISSLogger
    {
        void Error(string message);

        void Debug(string message);

        void Info(string message);

        void Warning(string message);
    }
}