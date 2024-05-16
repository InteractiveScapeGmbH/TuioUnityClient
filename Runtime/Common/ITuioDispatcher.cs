using TuioNet.Common;

namespace TuioUnity.Common
{
    public interface ITuioDispatcher
    {
        public void SetupProcessor(TuioClient tuioClient);
        public void RegisterCallbacks();
        public void UnregisterCallbacks();
        
    }
}