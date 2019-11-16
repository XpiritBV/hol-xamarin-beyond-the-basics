using System;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;

namespace ConferenceApp.Contracts
{
    public interface ISetReminder
    {
        Task<bool> AddAppointment(MyAppointmentType appointment);
    }
}
