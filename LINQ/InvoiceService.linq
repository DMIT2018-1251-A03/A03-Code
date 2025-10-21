<Query Kind="Program">
  <Connection>
    <ID>813ec320-8be0-4b91-8ec8-c1549d53aaea</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>.</Server>
    <Database>OLTP-DMIT2018</Database>
    <DisplayName>OLTP-DMIT2018-Entity</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
  <NuGetReference>BYSResults</NuGetReference>
</Query>

// 	Lightweight result types for explicit success/failure 
//	 handling in .NET applications.
using BYSResults;

// —————— PART 1: Main → UI ——————
//	Driver is responsible for orchestrating the flow by calling 
//	various methods and classes that contain the actual business logic 
//	or data processing operations.
void Main()
{
	CodeBehind codeBehind = new CodeBehind(this); // “this” is LINQPad’s auto Context

	#region GetParts
	//	//	create a place holder for existing parts
	//	List<int> existingPartsIDs = new();
	//
	//	//	Fail
	//	//	Rule: CategoryID & descripotion must be provided
	//	codeBehind.GetParts(0, string.Empty, existingPartsIDs);
	//	codeBehind.ErrorDetails.Dump("Category ID & description must be provided");
	//
	//	//	Rule: No parts found
	//	codeBehind.GetParts(0, "zzz", existingPartsIDs);
	//	codeBehind.ErrorDetails.Dump("No parts were found that contain description 'zzz'");
	//
	//	//	Pass:	valid part category ID (23 -> "Parts")
	//	codeBehind.GetParts(23, string.Empty, existingPartsIDs);
	//	codeBehind.Parts.Dump("Pass - Valid parts category ID");
	//
	//	//	Pass:	valid partial description ('ra');
	//	codeBehind.GetParts(0, "ra", existingPartsIDs);
	//	codeBehind.Parts.Dump("Pass - Valid partial description");
	//
	//	//	Pass: Updated existing parts ids
	//	existingPartsIDs.Add(27); //	Brake Oil, pint
	//	existingPartsIDs.Add(33); //	Transmission fuild, quart
	//	codeBehind.GetParts(0, "ra", existingPartsIDs);
	//	codeBehind.Parts.Dump("Pass - Valid partial description with existing parts ids");
	#endregion

	#region GetPart
	//	//	Fail
	//	//	Rule:  part ID must be greater than zero
	//	codeBehind.GetPart(0);
	//	codeBehind.ErrorDetails.Dump("Part ID must be greater than zero");
	//
	//	// Rule:  part ID must valid 
	//	codeBehind.GetPart(1000000);
	//	codeBehind.ErrorDetails.Dump("No part was found for ID 1000000");
	//
	//	// Pass:  valid part ID
	//	codeBehind.GetPart(52);
	//	codeBehind.Part.Dump("Pass - Valid part ID");
	#endregion

	#region GetInvoice
	//	Fail:
	//	Rule:	Customer and Invoice ID must be provided
	codeBehind.GetInvoice(0, 0, 1);
	codeBehind.ErrorDetails.Dump("Customer and InvoiceID must be greater than zero");

	//	Rule:	Employee ID must be provided
	codeBehind.GetInvoice(0, 1, 0);
	codeBehind.ErrorDetails.Dump("EmployeeID must be greater than zero");

	//	Pass:	New Invoice
	codeBehind.GetInvoice(0, 1, 1);
	codeBehind.Invoice.Dump("Pass - New Invoice");

	//	Pass:	Existing Invoice
	codeBehind.GetInvoice(1, 1, 1);
	codeBehind.Invoice.Dump("Pass - Existing Invoice");
	#endregion

	#region GetCustomerInvoice
	//	Fail
	//	Rule:  customer ID must be greater than zero
	codeBehind.GetCustomerInvoices(0);
	codeBehind.ErrorDetails.Dump("Customer ID must be greater than zero");

	// Rule:  customer ID must valid 
	codeBehind.GetCustomerInvoices(1000000);
	codeBehind.ErrorDetails.Dump("No customer was found for ID 1000000");

	// Pass:  valid customer ID
	codeBehind.GetCustomerInvoices(1);
	codeBehind.CustomerInvoices.Dump("Pass - Valid customer ID");
	#endregion

}

// ———— PART 2: Code Behind → Code Behind Method ————
// This region contains methods used to test the functionality
// of the application's business logic and ensure correctness.
// NOTE: This class functions as the code-behind for your Blazor pages
#region Code Behind Methods
public class CodeBehind(TypedDataContext context)
{
	#region Supporting Members (Do not modify)
	// exposes the collected error details
	public List<string> ErrorDetails => errorDetails;

	// Mock injection of the service into our code-behind.
	// You will need to refactor this for proper dependency injection.
	// NOTE: The TypedDataContext must be passed in.
	private readonly Library YourService = new Library(context);
	#endregion

	#region Fields from Blazor Page Code-Behind
	// feedback message to display to the user.
	private string feedbackMessage = string.Empty;
	// collected error details.
	private List<string> errorDetails = new();
	// general error message.
	private string errorMessage = string.Empty;
	#endregion

	//	part return from the service
	//	using GetParts()
	public List<PartView> Parts = default!;

	//	using GetPart
	public PartView Part = default!;

	//	invoice view returned by the service
	//	using both the GetInvoice() & AddEditInvoice()
	public InvoiceView Invoice = default!;
	
	//	using GetCustomerInvoices
	public List<InvoiceView> CustomerInvoices = new();

	public void GetParts(int partCategoryID, string description, List<int> existingPartIDs)
	{
		//	clear previous error details and message
		errorDetails.Clear();
		errorMessage = string.Empty;
		feedbackMessage = string.Empty;

		//wrap the service cal in a try/catch to handle unexpected exceptions
		try
		{
			var result = YourService.GetParts(partCategoryID, description, existingPartIDs);
			if (result.IsSuccess)
			{
				Parts = result.Value;
			}
			else
			{
				errorDetails = GetErrorMessages(result.Errors.ToList());
			}
		}
		catch (Exception ex)
		{
			// capture any exceptions message for display
			errorMessage = ex.Message;
		}
	}

	public void GetPart(int partID)
	{
		// clear previous error details and messages
		errorDetails.Clear();
		errorMessage = string.Empty;
		feedbackMessage = String.Empty;

		// wrap the service call in a try/catch to handle unexpected exceptions
		try
		{
			var result = YourService.GetPart(partID);
			if (result.IsSuccess)
			{
				Part = result.Value;
			}
			else
			{
				errorDetails = GetErrorMessages(result.Errors.ToList());
			}
		}
		catch (Exception ex)
		{
			// capture any exception message for display
			errorMessage = ex.Message;
		}
	}

	public void GetInvoice(int invoiceID, int customerID, int employeeID)
	{
		// clear previous error details and messages
		errorDetails.Clear();
		errorMessage = string.Empty;
		feedbackMessage = String.Empty;

		// wrap the service call in a try/catch to handle unexpected exceptions
		try
		{
			var result = YourService.GetInvoice(invoiceID, customerID, employeeID);
			if (result.IsSuccess)
			{
				Invoice = result.Value;
			}
			else
			{
				errorDetails = GetErrorMessages(result.Errors.ToList());
			}
		}
		catch (Exception ex)
		{
			// capture any exception message for display
			errorMessage = ex.Message;
		}
	}

	public void GetCustomerInvoices(int customerID)
	{
		// clear previous error details and messages
		errorDetails.Clear();
		errorMessage = string.Empty;
		feedbackMessage = String.Empty;

		// wrap the service call in a try/catch to handle unexpected exceptions
		try
		{
			var result = YourService.GetCustomerInvoices(customerID);
			if (result.IsSuccess)
			{
				CustomerInvoices = result.Value;
			}
			else
			{
				errorDetails = GetErrorMessages(result.Errors.ToList());
			}
		}
		catch (Exception ex)
		{
			// capture any exception message for display
			errorMessage = ex.Message;
		}
	}



}
#endregion

// ———— PART 3: Database Interaction Method → Service Library Method ————
//	This region contains support methods for testing
#region Methods
public class Library
{
	#region Data Context Setup
	// The LINQPad auto-generated TypedDataContext instance used to query and manipulate data.
	private readonly TypedDataContext _hogWildContext;

	// The TypedDataContext provided by LINQPad for database access.
	// Store the injected context for use in library methods
	// NOTE:  This constructor is simular to the constuctor in your service
	public Library(TypedDataContext context)
	{
		_hogWildContext = context
					?? throw new ArgumentNullException(nameof(context));
	}
	#endregion

	public Result<List<PartView>> GetParts(int partCategoryID, string description, List<int> existingPartIDs)
	{
		//	Create a Result container that will hold either a 
		//		list of PartView on success or any accumulated errors on failure
		var result = new Result<List<PartView>>();

		#region Business Rules
		//	These are processing rules that need to be satisfied
		//		for valid data
		//		rule:	Both part id must be valid and/or description cannot be empty
		//		rule: 	Part IDs in existing part IDs will be ignored
		//		rule:	RemoveFromViewFlag must be false


		//	Both part id must be valid and/or description cannot be empty
		if (partCategoryID == 0 && string.IsNullOrWhiteSpace(description))
		{
			return result.AddError(new Error("Missing Information",
								"Please provide either a category and/or description"));
		}
		#endregion

		//	need to update description parametrs so we are not searching on
		//	 an empty string value.  Otherwise, this would return all records
		Guid tempGuid = Guid.NewGuid();
		if (string.IsNullOrWhiteSpace(description))
		{
			description = tempGuid.ToString();
		}

		//	igrore any parts that are in the "existing part IDs" list.
		//	ensure that we are compairing uppercase value for description
		var parts = _hogWildContext.Parts
						.Where(p => !existingPartIDs.Contains(p.PartID)
							&& (description.Length > 0
							&& description != tempGuid.ToString()
							&& partCategoryID > 0
								? (p.Description.ToUpper().Contains(description.ToUpper())
									&& p.PartCategoryID == partCategoryID)
								: (p.Description.ToUpper().Contains(description.ToUpper())
									|| p.PartCategoryID == partCategoryID)
							&& !p.RemoveFromViewFlag))
					.Select(p => new PartView
					{
						PartID = p.PartID,
						PartCategoryID = p.PartCategoryID,
						CategoryName = p.PartCategory.Name,
						Description = p.Description,
						Cost = p.Cost,
						Price = p.Price,
						ROL = p.ROL,
						QOH = p.QOH,
						Taxable = p.Taxable,
						RemoveFromViewFlag = p.RemoveFromViewFlag
					})
					.OrderBy(p => p.Description)
					.ToList();

		//  if no parts were found
		if (parts == null || parts.Count() == 0)
		{
			//need to exit because we did not find any parts
			return result.AddError(new Error("No Parts", "No parts were found"));
		}

		//	return the result
		return result.WithValue(parts);
	}

	//	Get the part
	public Result<PartView> GetPart(int partID)
	{
		// Create a Result container that will hold either a
		//	PartView objects on success or any accumulated errors on failure
		var result = new Result<PartView>();
		#region Business Rules
		//	These are processing rules that need to be satisfied
		//		rule:	partID must be valid
		//		rule: 	RemoveFromViewFlag must be false
		if (partID == 0)
		{
			//  need to exit because we have no part information
			return result.AddError(new Error("Missing Information",
							"Please provide a valid part id"));
		}
		#endregion

		var part = _hogWildContext.Parts
						.Where(p => (p.PartID == partID
									 && !p.RemoveFromViewFlag))
						.Select(p => new PartView
						{
							PartID = p.PartID,
							PartCategoryID = p.PartCategoryID,
							//  PartCategory is an alias for Lookup
							CategoryName = p.PartCategory.Name,
							Description = p.Description,
							Cost = p.Cost,
							Price = p.Price,
							ROL = p.ROL,
							QOH = p.QOH,
							Taxable = (bool)p.Taxable,
							RemoveFromViewFlag = p.RemoveFromViewFlag
						}).FirstOrDefault();

		//  if no part were found
		if (part == null)
		{
			//  need to exit because we did not find any part
			return result.AddError(new Error("No part", "No part were found"));
		}

		//  return the result
		return result.WithValue(part);
	}

	public Result<InvoiceView> GetInvoice(int invoiceID, int customerID, int employeeID)
	{
		//	Create a Result container that will hold either a
		//	  InvoiceView object on success or any accumulated errors on failure
		var result = new Result<InvoiceView>();

		#region Business Rules
		//	These are processing rules that need to be satisfied
		//		for valid data
		//		rule:  cusomerID must be provided if invoiceID == 0
		//		Rule:  employeeID must be provided
		if (customerID == 0 && invoiceID == 0)
		{
			result.AddError(new Error("Missing Information", "Please provide a customer ID"));
		}

		if (employeeID == 0)
		{
			result.AddError(new Error("Missing Information", "Please provide a employee ID"));
		}

		// need to exit because we are missing key data
		if (result.IsFailure)
		{
			return result;
		}
		#endregion
		//	Handles both new and existing invoice
		//	For a new invoice, the following information is needed
		//		Customer & Employee IDs
		//	For a existing invoice, the following information is needed
		//	Invoice & employeeID (We maybe updating an invoice at a later date
		//		and we need the current employee who is handling the transaction

		InvoiceView invoice = null;
		//	new invoice for a customer
		if (invoiceID == 0)
		{
			invoice = new InvoiceView()
			{
				CustomerID = customerID,
				EmployeeID = employeeID,
				InvoiceDate = DateOnly.FromDateTime(DateTime.Now)
			};
		}
		else
		{
			invoice = _hogWildContext.Invoices
						.Where(x => x.InvoiceID == invoiceID
									&& !x.RemoveFromViewFlag)
						.Select(x => new InvoiceView
						{
							InvoiceID = x.InvoiceID,
							InvoiceDate = x.InvoiceDate,
							CustomerID = x.CustomerID,
							EmployeeID = x.EmployeeID,
							SubTotal = x.SubTotal,
							Tax = x.Tax,
							RemoveFromViewFlag = x.RemoveFromViewFlag, //  this will always be false
							InvoiceLines = x.InvoiceLines
											.Select(il => new InvoiceLineView
											{
												InvoiceLineID = il.InvoiceLineID,
												InvoiceID = il.InvoiceID,
												PartID = il.PartID,
												Quantity = il.Quantity,
												Description = il.Part.Description,
												Price = il.Part.Price,
												Taxable = (bool)il.Part.Taxable,
												RemoveFromViewFlag = il.RemoveFromViewFlag
											}).ToList()
						}).FirstOrDefault();
			customerID = invoice.CustomerID;
		}
		invoice.CustomerName = GetCustomerFullName(customerID);
		invoice.EmployeeName = GetEmployeeFullName(employeeID);

		//	only happen if the invoice was mark as remove
		if (invoice == null)
		{
			//	need to exit because we did not find any invoice
			return result.AddError(new Error("No Invoice", "No invoice was found"));
		}
		return result.WithValue(invoice);

	}

	//	Get the customer invoices
	public Result<List<InvoiceView>> GetCustomerInvoices(int customerId)
	{
		// Create a Result container that will hold either a
		//	PartView objects on success or any accumulated errors on failure
		var result = new Result<List<InvoiceView>>();
		#region Business Rules
		//	These are processing rules that need to be satisfied
		//		rule:	customerID must be valid
		//		rule: 	RemoveFromViewFlag must be false
		if (customerId == 0)
		{
			result.AddError(new Error("Missing Information",
							"Please provide a valid customer id"));
			//  need to exit because we have no customer information
			return result;
		}
		#endregion

		var customerInvoices = _hogWildContext.Invoices
				.Where(x => x.CustomerID == customerId
							&& !x.RemoveFromViewFlag)
				.Select(x => new InvoiceView
				{
					InvoiceID = x.InvoiceID,
					InvoiceDate = x.InvoiceDate,
					CustomerID = x.CustomerID,
					SubTotal = x.SubTotal,
					Tax = x.Tax
				}).ToList();

		//  if no invoices were found
		if (customerInvoices == null || customerInvoices.Count() == 0)
		{
			result.AddError(new Error("No customer invoices", "No invoices were found"));
			//  need to exit because we did not find any invoices
			return result;
		}
		//  return the result
		return result.WithValue(customerInvoices);
	}

	//	get the customer full name
	public string GetCustomerFullName(int customerID)
	{
		return _hogWildContext.Customers
					.Where(c => c.CustomerID == customerID)
					.Select(c => $"{c.FirstName} {c.LastName}").FirstOrDefault() ?? string.Empty;
	}

	//	get the employee full name
	public string GetEmployeeFullName(int employeeID)
	{
		return _hogWildContext.Employees
					.Where(e => e.EmployeeID == employeeID)
					.Select(e => $"{e.FirstName} {e.LastName}").FirstOrDefault() ?? string.Empty;
	}
}
#endregion

// ———— PART 4: View Models → Service Library View Model ————
//	This region includes the view models used to 
//	represent and structure data for the UI.
#region View Models
public class PartView
{
	public int PartID { get; set; }
	public int PartCategoryID { get; set; }
	public string CategoryName { get; set; }
	public string Description { get; set; }
	public decimal Cost { get; set; }
	public decimal Price { get; set; }
	public int ROL { get; set; }
	public int QOH { get; set; }
	public bool Taxable { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}

public class InvoiceView
{
	public int InvoiceID { get; set; }
	public DateOnly InvoiceDate { get; set; }
	public int CustomerID { get; set; }
	public string CustomerName { get; set; }
	public int EmployeeID { get; set; }
	public string EmployeeName { get; set; }
	public decimal SubTotal { get; set; }
	public decimal Tax { get; set; }
	public decimal Total => SubTotal + Tax;
	public List<InvoiceLineView> InvoiceLines { get; set; } = new List<InvoiceLineView>();
	public bool RemoveFromViewFlag { get; set; }
}

public class InvoiceLineView
{
	public int InvoiceLineID { get; set; }
	public int InvoiceID { get; set; }
	public int PartID { get; set; }
	public string Description { get; set; }
	public int Quantity { get; set; }
	public decimal Price { get; set; }
	public bool Taxable { get; set; }
	public decimal ExtentPrice => Price * Quantity;
	public bool RemoveFromViewFlag { get; set; }
}















#endregion

//	This region includes support methods
#region Support Method
// Converts a list of error objects into their string representations.
public static List<string> GetErrorMessages(List<Error> errorMessage)
{
	// Initialize a new list to hold the extracted error messages
	List<string> errorList = new();

	// Iterate over each Error object in the incoming list
	foreach (var error in errorMessage)
	{
		// Convert the current Error to its string form and add it to errorList
		errorList.Add(error.ToString());
	}

	// Return the populated list of error message strings
	return errorList;
}
#endregion