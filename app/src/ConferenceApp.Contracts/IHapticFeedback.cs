using System;
namespace ConferenceApp.Contracts
{
    public interface IHapticFeedback
    {
        void Success();
        void Error();
    }
}
