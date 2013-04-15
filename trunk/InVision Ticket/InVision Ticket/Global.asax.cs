using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using InVision_Ticket.Models;
using InVision_Ticket.ViewModels;
using InVision_Ticket.Utilities;
using System.Net;
namespace InVision_Ticket
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
            
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
            CreateMaps();
            
		}

        private static void CreateMaps()
        {
            Mapper.CreateMap<Login, LoginViewModel>()
                .ForMember(d => d.UserType, o => o.MapFrom(s => s.UserType.UserType1))
                .ForMember(d => d.StoreLocation, o => o.MapFrom(s =>s.Location.StoreLocation));
            Mapper.CreateMap<Ticket, TicketViewModel>()
                .ForMember(d => d.CreatedByLogin, o => o.MapFrom(s => s.CreatedByLogin.DisplayName))
                .ForMember(d => d.CreatedByCustomer, o => o.ResolveUsing<TicketCreatedByCustomer>())
                .ForMember(d => d.CustomerContactID, o => o.MapFrom(s => s.Customer.CustomerContacts.FirstOrDefault().CustomerContactID))
                .ForMember(d => d.CustomerContactName, o => o.ResolveUsing<CustomerContactResolver>())
                .ForMember(d => d.BusinessName, o => o.ResolveUsing<BusinessNameResolver>())
                .ForMember(d => d.Phone, o => o.MapFrom(s=>s.Customer.CustomerContacts.FirstOrDefault().Phone.ToString().Insert(0, "(").Insert(4, ") ").Insert(9, "-")));
            Mapper.CreateMap<Ticket, TicketListViewModel>()
                .ForMember(d => d.BillRate, o => o.MapFrom(s => s.BillRate.TicketBillRate))
                .ForMember(d => d.BillRateDescription, o => o.MapFrom(s => s.BillRate.BillRateDescription))
                .ForMember(d => d.CustomerPhone, o => o.MapFrom(s => s.Customer.CustomerContacts.First().Phone))
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.CustomerName))
                .ForMember(d => d.TicketType, o => o.MapFrom(s => s.TicketType.TicketType1))
                .ForMember(d => d.CreatedBy, o => o.ResolveUsing<TicketCreatedByTicketListResolver>())
                .ForMember(d => d.Technician, o => o.MapFrom(s => s.Technician.DisplayName))
                .ForMember(d => d.Salesman, o => o.MapFrom(s => s.Salesman.DisplayName))
                .ForMember(d => d.LastModifiedDateTime, o=> o.MapFrom(s => s.LastModifiedDateTime))
                .ForMember(d => d.TicketStatus, o => o.MapFrom(s => s.TicketStatus.Status));
            Mapper.CreateMap<TicketViewModel, Ticket>()
                .ForMember(d => d.CreatedByCustomer, o => o.Ignore());
            //.ForMember(d => d.CreatedByLoginID.Value, o => o.MapFrom(s => s.CreatedByLoginID));
            Mapper.CreateMap<LoginViewModel, Login>()
                .ForMember(d => d.UserType, o => o.MapFrom(s => new UserType { UserType1 = s.UserType, UserTypeID = s.UserTypeID}))
                .ForMember(d => d.Location, o => o.MapFrom(s => new Location {LocationID = s.LocationID.Value, StoreLocation = s.StoreLocation}));
                


        }
	}
    public class TicketCreatedByCustomer : ValueResolver<Ticket, bool>
    {
        protected override bool ResolveCore(Ticket source)
        {
            if (source.CreatedByCustomer == null)
                return false;
            return true;

        }
    
    }
    public class CustomerContactResolver : ValueResolver<Ticket, string>
    {
        protected override string ResolveCore(Ticket source)
        {

            if (source.Customer.BusinessCustomer.Value)
                return source.Customer.CustomerContacts.FirstOrDefault().LastName + ", " + source.Customer.CustomerContacts.FirstOrDefault().FirstName;
            return source.Customer.CustomerName;
        
        }
    }
    public class TicketCreatedByTicketListResolver : ValueResolver<Ticket, string>
    {
        protected override string ResolveCore(Ticket source)
        {
            
            if (source.CreatedByCustomer == null)
            {
                return source.CreatedByLogin.DisplayName;
            }
            return source.CreatedByCustomer.CustomerName;
        }
        
    }
    public class BusinessNameResolver : ValueResolver<Ticket, string>
    {
        protected override string ResolveCore(Ticket source)
        {
            if (source.Customer.BusinessCustomer.Value)
                return source.Customer.CustomerName;
            return null;
        }
    
    }
}
//    if (tquery.BusinessCustomer.Value)
//    {
//        vm.CustomerContactName = tquery.LastName + ", " + tquery.FirstName;
//        vm.BusinessName = tquery.CustomerName;
//    }
//    else
//    {
//        vm.CustomerContactName = tquery.CustomerName;
//    }