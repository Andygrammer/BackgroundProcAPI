using Domain.Handlers;
using Domain.Interfaces;
using System.Collections.Concurrent;

namespace Services.Services
{
    public class BackgroundProcService : IBackgroundProcService
    {
        private readonly ConcurrentDictionary<string, BackgroundProcHandler> _processing;

        public BackgroundProcService()
        {
            _processing = new ConcurrentDictionary<string, BackgroundProcHandler>();
        }

        public void AddProc(BackgroundProcHandler backgroundProcHandler)
        {
            _processing.TryAdd(backgroundProcHandler.Id, backgroundProcHandler);
        }

        public bool GetProc(string id, out BackgroundProcHandler backgroundProcHandler)
        {
            return _processing.TryGetValue(id, out backgroundProcHandler);
        }
    }
}
