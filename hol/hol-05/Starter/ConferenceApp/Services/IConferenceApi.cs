using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceApp.Contracts.Models;
using Refit;

namespace ConferenceApp.Services
{
	[Headers("Accept: application/json")]
	public interface IConferenceApi
	{
		[Get("/sessions")]
		Task<IEnumerable<Session>> GetSessions();
		[Get("/sessions/{sessionId}")]
		Task<Session> GetSession(string sessionId);
		[Get("/speakers")]
		Task<IEnumerable<Speaker>> GetSpeakers();
		[Get("/speakers/{speakerId}")]
		Task<Speaker> GetSpeaker(string speakerId);
	}
}
