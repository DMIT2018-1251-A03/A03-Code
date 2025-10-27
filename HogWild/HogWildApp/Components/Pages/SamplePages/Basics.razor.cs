namespace HogWildApp.Components.Pages.SamplePages
{
    public partial class Basics
    {
        #region Fields
        private const string MY_NAME = "James";
        private int oddEvenValue = 0;
        private string emailText = string.Empty;
        private string passwordText = string.Empty;
        private DateTime dateText = DateTime.Now;
        #endregion

        #region Properties
        private bool IsEven => oddEvenValue % 2 == 0;

        #endregion

        #region Methods
        // This method is automatically called when the component is initialized
        // This method should ALWAYS be the first method in your partial class if used
        // For best organization, put all override methods at the top before other methods
        // Example: When the page is opened
        protected override void OnInitialized()
        {
            RandomValue();
            // Calls the base class OnInitialized method (if any)
            // Note: You do not need to include this unless you have
            // specifically created a new BaseComponent
            // The default OnInitialized methods in the default base component
            // are EMPTY
            // For our class this is NOT needed
            base.OnInitialized();
        }


        /// <summary>
        /// Generates a random number between 0 and 25 using the Random class
        /// </summary>
        private void RandomValue()
        {
            // Create an instance of the Random class to generate random numbers.
            Random rnd = new();

            // Generate a random integer between 0 (inclusive) and 25 (exclusive)
            // Note: Inclusive means that 0 in included as a possibility while
            // exclusive means that 25 is not a possible value that will be generated
            oddEvenValue = rnd.Next(0, 25);
        }

        #endregion

    }
}
