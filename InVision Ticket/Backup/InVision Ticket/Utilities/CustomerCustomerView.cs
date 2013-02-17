using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InVision_Ticket.Models;

namespace InVision_Ticket.Utilities
{
	public class CustomerCustomerView
	{
		internal static CustomerViewModel ConvertToCustomerViewModel(Customer C, CustomerContact CC)
		{
			
			CustomerViewModel CVM = new CustomerViewModel();
			CVM.FirstName = CC.FirstName;
			CVM.LastName = CC.LastName;
			CVM.CustomerName = C.CustomerName;
			CVM.PhoneString = CC.Phone.ToString();
			CVM.Address1 = CC.AddressLine1;
			CVM.Address2 = CC.AddressLine2;
			if (C.BusinessCustomer.HasValue)
			{
				CVM.BusinessCustomer = C.BusinessCustomer.Value;
			}
			CVM.City = CC.City;
			CVM.CustomerID = C.CustomerID;
			CVM.CustomerContactID = CC.CustomerContactID;
			CVM.CustomerNotes = C.CustomerNotes;
			CVM.Email = CC.Email;
			CVM.PromotionalEmails = CC.PromotionalEmails;
			CVM.State = CC.State;
			if (CC.Zip.HasValue)
			{
				CVM.Zip = CC.Zip.Value;
			}
		
			return(CVM);
		
		}


	}
}