using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVENeT.EVENeTServiceReference;
using System.Collections.ObjectModel;

namespace EVENeT.DataModel
{
    public class EventSearchResults
    {
        public ObservableCollection<Event> events = new ObservableCollection<Event>();

        public void addEvent(Event _event)
        {
            events.Add(_event);
        }

        public async Task getEventsFromName(string name)
        {
            IEnumerable<getEventFromNameResult> list = await DatabaseHelper.Client.GetEventsByNameAsync(name);
            foreach (getEventFromNameResult eventlist in list)
            {
                Event _event = new Event(eventlist.id, eventlist.title, eventlist.thumbnail);
                addEvent(_event);
            }
        }
    }
}
