using GogoKit;
using GogoKit.Models.Request;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ViaGoGo.CD.Services
{
    public class Actions
    {
        public async Task<IReadOnlyList<Event>> getSearchResults(ViagogoClient client, string searchTerm)
        {
            // Get a list of events for the search term 
            SearchResultRequest srr = new SearchResultRequest();
            List<SearchResultTypeFilter> srrList = new List<SearchResultTypeFilter>();
            srrList.Add(SearchResultTypeFilter.Category);
            srr.Type = srrList;

            // get category list
            var searchResults = await client.Search.GetAsync(searchTerm, srr);

            // category ID
            StringHelper strHelp = new StringHelper();
            string catId = strHelp.substringLastChar(searchResults.Items[0].CategoryLink.HRef, "/");

            // Search events with catId
            var events = await client.Events.GetAllByCategoryAsync(Int32.Parse(catId));
            
            return events;
        }

        public async Task<PagedResource<Listing>> getListings(ViagogoClient client, int eventId)
        {
            ListingRequest listReq = new ListingRequest();
            listReq.PageSize = 10;
            PagedResource<Listing> listing = await client.Listings.GetByEventAsync(eventId, listReq);

            return listing;
        }

        public async Task<Event> eventPagination(ViagogoClient client, string paginationURL)
        {
            HalKit.Models.Response.Link link = new HalKit.Models.Response.Link();
            link.HRef = paginationURL;
            Event events = await client.Hypermedia.GetAsync<Event>(link);

            return events;
        }

        public async Task<Listing> listingPagination(ViagogoClient client, string paginationURL)
        {
            HalKit.Models.Response.Link link = new HalKit.Models.Response.Link();
            link.HRef = paginationURL;
            Listing listing = await client.Hypermedia.GetAsync<Listing>(link);

            return listing;
        }

    }
}

