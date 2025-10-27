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
        protected override void OnInitialized()
        {
            RandomValue();
            base.OnInitialized();
        }

       
        private void RandomValue()
        {
            // Create an instance of the Random class to generate random numbers
            Random rnd = new Random();

            //  Gereate a random integer between 0 (inclusive) and 25 (exclusive)
            oddEvenValue = rnd.Next(0, 25);
        }

        #endregion

    }
}
