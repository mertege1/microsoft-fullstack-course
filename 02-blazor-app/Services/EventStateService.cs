using _02_blazor_app.Models;

namespace _02_blazor_app.Services
{
    public class EventStateService
    {
        public List<EventModel> Events { get; private set; } = new()
        {
            new EventModel { Id = 1, Title = "Blazor WebAssembly Summit", Date = DateTime.Now.AddDays(10), Location = "Istanbul (Hybrid)", Capacity = 100, Description = "Discover modern web development techniques with .NET." },
            new EventModel { Id = 2, Title = "AI & The Future", Date = DateTime.Now.AddDays(25), Location = "Ankara", Capacity = 50, Description = "Explore the sectoral impacts of artificial intelligence." },
            new EventModel { Id = 3, Title = "Startup Networking", Date = DateTime.Now.AddDays(5), Location = "Izmir", Capacity = 200, Description = "An exclusive meetup for entrepreneurs and investors." }
        };

        public List<RegistrationModel> Attendances { get; private set; } = new();

        public event Action? OnStateChanged;

        public void RegisterUser(RegistrationModel registration)
        {
            var targetEvent = Events.FirstOrDefault(e => e.Id == registration.EventId);
            if (targetEvent != null && targetEvent.RegisteredCount < targetEvent.Capacity)
            {
                targetEvent.RegisteredCount++;
                Attendances.Add(registration);
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnStateChanged?.Invoke();
    }
}