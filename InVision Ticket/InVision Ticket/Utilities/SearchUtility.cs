using System;
using System.Collections.Generic;
using System.Linq;
using InVision_Ticket.Models;
using System.Data;
using System.Data.Entity;
using InVision_Ticket.Utilities;
using InVision_Ticket.ViewModels;
using LinqKit;
using AutoMapper;
namespace InVision_Ticket.Utilities
{
   class SearchUtility
    {
        public static List<CustomerViewModel> CustomerSearch(string SearchString, int? LocationID, DateTime? StartDate, DateTime? EndDate)
        {
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                var predicate = PredicateBuilder.False<Customer>();
                if (LocationID.HasValue)
                {
                    predicate = predicate.And(c => c.CustomerTickets.Any(t => t.LocationID == LocationID.Value));
                }
                if (StartDate.HasValue)
                {
                    predicate = predicate.And(c => c.CustomerTickets.Any(t => t.CreatedDateTime >= StartDate.Value));
                }
                if (EndDate.HasValue)
                {
                    predicate = predicate.And(c => c.CustomerTickets.Any(t => t.CreatedDateTime <= EndDate.Value));
                }
                var StringPredicate = PredicateBuilder.False<Customer>();
                if (!String.IsNullOrWhiteSpace(SearchString))
                {
                    StringPredicate = StringPredicate.Or(c => c.CustomerName.Contains(SearchString));
                    StringPredicate = StringPredicate.Or(c => c.CustomerNotes.Contains(SearchString));
                    StringPredicate = StringPredicate.Or(c => c.CustomerContacts.FirstOrDefault().Email.Contains(SearchString));
                    StringPredicate = StringPredicate.Or(c => c.CustomerContacts.FirstOrDefault().FirstName.Contains(SearchString));
                    StringPredicate = StringPredicate.Or(c => c.CustomerContacts.FirstOrDefault().LastName.Contains(SearchString));
                    predicate = predicate.Or(StringPredicate.Expand());
                }

                var Customers = db.Customers.AsExpandable().Include(c => c.CustomerContacts).Where(predicate);
                List<CustomerViewModel> CustomerList = new List<CustomerViewModel>();
                foreach (var c in Customers)
                {
                    CustomerViewModel CVM = CustomerCustomerView.ConvertToCustomerViewModel(c, c.CustomerContacts.FirstOrDefault());
                    CustomerList.Add(CVM);
                }
                return CustomerList.ToList();
            }
        }
        public static List<TicketListViewModel> TicketSearch(string SearchString, int? LocationID, DateTime? StartDate, DateTime? EndDate, bool? OpenStatusOnly, long? CustomerID, int? LoginID)
        {
 
            using (InVisionTicketContext db = new InVisionTicketContext())
            {
                var predicate = PredicateBuilder.True<Ticket>();
                if (LocationID.HasValue)
                {
                    predicate = predicate.And(t => t.LocationID == LocationID.Value);
                }
                if (StartDate.HasValue)
                {
                    predicate = predicate.And(t => t.CreatedDateTime >= StartDate.Value);
                }
                if (EndDate.HasValue)
                {
                    predicate = predicate.And(t => t.CreatedDateTime <= EndDate.Value);
                }
                if (OpenStatusOnly.HasValue)
                {
                    if (OpenStatusOnly.Value)
                        predicate = predicate.And(t => t.TicketStatus.Open == true);
                    else
                        predicate = predicate.And(t => t.TicketStatus.Open == false);
                }
                if (CustomerID.HasValue)
                {
                    predicate = predicate.And(t => t.CustomerID == CustomerID);
                }
                var LoginPredicate = PredicateBuilder.False<Ticket>();
                if (LoginID.HasValue)
                {
                    LoginPredicate = LoginPredicate.Or(t => t.SalesmanLoginID == LoginID);
                    LoginPredicate = LoginPredicate.Or(t => t.TechnicianLoginID == LoginID);
                    predicate = predicate.And(LoginPredicate.Expand());
                }
                var StringPredicate = PredicateBuilder.False<Ticket>();
                if (!String.IsNullOrWhiteSpace(SearchString))
                {
                    string[] terms = null;
                    string[] Delimeters = new string[] { " ", "," };
                    terms = SearchString.Split(Delimeters, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string term in terms)
                    {
                        string temp = term;
                        StringPredicate = StringPredicate.Or(t => t.Details.Contains(temp));
                        StringPredicate = StringPredicate.Or(t => t.Customer.CustomerName.Contains(temp));
                        StringPredicate = StringPredicate.Or(t => t.Summary.Contains(temp));
                    }
                    predicate = predicate.And(StringPredicate.Expand());
                }
                var tickets = db.Tickets.AsExpandable().Include(t => t.Updates).Include(t => t.Customer).Include(t => t.Uploads).Where(predicate);

                return Mapper.Map<List<Ticket>, List<TicketListViewModel>>(tickets.ToList());
            }
        }
    }
}