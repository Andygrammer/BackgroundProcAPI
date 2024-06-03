using Domain.Handlers;

namespace Domain.Interfaces
{
    public interface IBackgroundProcService
    {
        public void AddProc(BackgroundProcHandler backgroundProcHandler);

        public bool GetProc(string id, out BackgroundProcHandler backgroundProcHandler);
    }
}
