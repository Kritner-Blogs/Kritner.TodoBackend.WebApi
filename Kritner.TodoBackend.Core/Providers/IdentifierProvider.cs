using System.Threading;

namespace Kritner.TodoBackend.Core.Providers
{
    public class IdentifierProvider : IIdentifierProvider
    {
        private int id = 0;
        
        public int GetId()
        {
            Interlocked.Increment(ref id);

            return id;
        }
    }
}