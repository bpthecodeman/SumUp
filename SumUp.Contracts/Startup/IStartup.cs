namespace SumUp.Contracts.Startup
{
    public interface IStartup
    {
        public void Start(StartMode mode, string path);
    }
}
