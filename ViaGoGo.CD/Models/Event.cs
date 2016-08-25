using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaGoGo.CD.Models
{
    class Event
    {
        public int? eventId { get; set; }
        public string eventName { get; set; }
        public DateTimeOffset? startDate { get; set; }
        public decimal? priceAmount { get; set; }
        public string shownPrice { get; set; }
        public bool lowestPrice { get; set; }
        public GogoKit.Models.Response.Country country { get; set; }

        public List<Event> setEventList(IReadOnlyList<GogoKit.Models.Response.Event> results)
        {
            List<Event> eventList = new List<Event>();
            foreach(var result in results)
            {
                Event tmpEvent = new Event();
                tmpEvent.eventId = result.Id;
                tmpEvent.country = result.Venue.Country;
                tmpEvent.startDate = result.StartDate;
                tmpEvent.eventName = result.Name;
                tmpEvent.lowestPrice = false;
                tmpEvent.priceAmount = result.MinTicketPrice.Amount != null ? result.MinTicketPrice.Amount : 0;
                tmpEvent.shownPrice = result.MinTicketPrice.Display != null ? result.MinTicketPrice.Display : "N/A";

                eventList.Add(tmpEvent);
            }

            return eventList;
        }

        public List<Event> setLowestCountryPrice(List<Event> evntList)
        {
            // sort then loop through list setting the lowest prices per country
            var sortedEvntList = evntList.OrderBy(e => e.country.Name).ThenByDescending(e => e.priceAmount).ToList();
            for (int i = 0; i < sortedEvntList.Count-1; i++)
            {
                if(sortedEvntList[i].country.Name != sortedEvntList[i+1].country.Name)
                {
                    sortedEvntList[i].lowestPrice = true;
                }

            }
            // final object always has the lowest price
            sortedEvntList[sortedEvntList.Count - 1].lowestPrice = true;
            
            return evntList;
        }
    }
}
