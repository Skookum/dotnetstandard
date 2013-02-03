using DotNetStandard.Vent;

namespace DotNetStandard.Tests.Models
{
    public class EventTest : Event 
    {
        public EventTest(string name) : base(name) { }
    }
}