using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViaGoGo.CD.Models
{
    public class Listing
    {
        public string ticketType {get; set;}
        public string totalPriceShown {get; set;}
        public decimal? totalPrice { get; set; }
        public int? qty {get; set;}
        public string ticketURL { get; set; }


     /*   public List<Listing> setListingList(GogoKit.Models.Response.PagedResource<GogoKit.Models.Response.Listing> results)
        {
            List<Listing> listingList = new List<Listing>();
            foreach (var result in results)
            {
                Listing tmpListing = new Listing();
                tmpListing.totalPriceShown = result.TicketPrice.Display;
                tmpListing.totalPrice = result.TicketPrice.Amount;
                tmpListing.ticketType = result.TicketType.Name;
                tmpListing.qty = result.NumberOfTickets;
                tmpListing.ticketURL = result.LocalWebPageLink.HRef;
                listingList.Add(tmpListing);
            }

            return listingList;
        } */
    }
}