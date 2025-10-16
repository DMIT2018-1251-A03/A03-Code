<Query Kind="Program">
  <Connection>
    <ID>37a64ce9-5c5f-4d4d-afc7-7324799c8fda</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>.</Server>
    <Database>OLTP-DMIT2018</Database>
    <DisplayName>OLTP-DMIT2018-ENtity</DisplayName>
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
	//	create a place holder for existing parts
	List<int> existingPartsIDs = new();
	
	//	Fail
	//	Rule: CategoryID & descripotion must be provided
	codeBehind.GetParts(0, string.Empty, existingPartsIDs);
	codeBehind.ErrorDetails.Dump("Category ID & description must be provided");
	
	//	Rule: No parts found
	codeBehind.GetParts(0, "zzz", existingPartsIDs);
	codeBehind.ErrorDetails.Dump("No parts were found that contain description 'zzz'");
	
	//	Pass:	valid part category ID (23 -> "Parts")
	codeBehind.GetParts(23, string.Empty, existingPartsIDs);
	codeBehind.Parts.Dump("Pass - Valid parts category ID");
	
	//	Pass:	valid partial description ('ra');
	codeBehind.GetParts(0, "ra", existingPartsIDs);
	codeBehind.Parts.Dump("Pass - Valid partial description");
	
	//	Pass: Updated existing parts ids
	existingPartsIDs.Add(27); //	Brake Oil, pint
	existingPartsIDs.Add(33); //	Transmission fuild, quart
	codeBehind.GetParts(0, "ra", existingPartsIDs);
	codeBehind.Parts.Dump("Pass - Valid partial description with existing parts ids");
	#endregion

	#region GetPart
		//	Fail
		//	Rule:  part ID must be greater than zero
		codeBehind.GetPart(0);
		codeBehind.ErrorDetails.Dump("Part ID must be greater than zero");
	
		// Rule:  part ID must valid 
		codeBehind.GetPart(1000000);
		codeBehind.ErrorDetails.Dump("No part was found for ID 1000000");
	
		// Pass:  valid part ID
		codeBehind.GetPart(52);
		codeBehind.Part.Dump("Pass - Valid part ID");
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
		if(string.IsNullOrWhiteSpace(description))
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
	if(parts ==null || parts.Count() == 0)
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