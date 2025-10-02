﻿using BYSResults;
namespace LinqToWebApp.Components
{
    public static class LinqToWebHelperClass
    {
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
    }
}
