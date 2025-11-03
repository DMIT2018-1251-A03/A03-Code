using HogWildSystem.BLL;
using HogWildSystem.ViewModels;
using Microsoft.AspNetCore.Components;

namespace HogWildApp.Components.Pages.SamplePages
{
    public partial class CustomerList
    {
        #region Fields
        private string lastName = string.Empty;
        private string phoneNumber = string.Empty;
        //  this will tell us if the search has been performed
        private bool noRecords;
        private string feedbackMesssage = string.Empty;   
        private string errorMessage = string.Empty;
        private bool hasFeedback => !string.IsNullOrWhiteSpace(feedbackMesssage);
        private bool hasError => !string.IsNullOrWhiteSpace(errorMessage);
        //error list
        private List<string> errorDetails = new List<string>();

        #endregion

        #region Properties
        //  inject the CustomerService dependency
        [Inject]
        protected CustomerService CustomerService { get; set; } = default!;
        //  inject the NavigationManager dependency
        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        //  list of customer search views
        protected List<CustomerSearchView> CustomerSearchViews { get; set; } = default!;
        #endregion

        #region Methods
        public void GetCustomers(string lastName, string phone)
        {
            //	clear the previous error details and messages
            errorDetails.Clear();
            errorMessage = string.Empty;
            feedbackMesssage = string.Empty;

            //	wrap the service call in a try/catch to handle unexpected exceptions
            try
            {
                var result = CustomerService.GetCustomers(lastName, phone);
                if (result.IsSuccess)
                {
                    CustomerSearchViews = result.Value;
                }
                else
                {
                    errorDetails = HogWildHelperClass.GetErrorMessages(result.Errors.ToList());
                }

            }
            catch (Exception ex)
            {
                //	capture any exception message for display
                errorMessage = ex.Message;
            }
        }
        #endregion
    }
}
