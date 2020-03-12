using System;
using Windows.UI.Xaml.Controls;

namespace Capstone.Common
{
    static class UIUtils
    {
        /// <summary>
        /// Navigates from the passed CurrentPage to the main screen. Useful for something like a back button
        /// </summary>
        /// <param name="CurrentPage">the instance of the page the user is currently on. must not be null</param>
        public static void GoToMainPage(Page CurrentPage)
        {
            if (CurrentPage is null)
            {
                throw new ArgumentNullException(nameof(CurrentPage));
            }
            CurrentPage.Frame.Navigate(typeof(MainPage));
        }
    }
}
