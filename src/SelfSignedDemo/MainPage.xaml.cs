using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfSignedDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NavigateButtonClicked(object sender, EventArgs e)
        {
            LoadingLabel.IsVisible = true;
            MyWebView.Source = UrlEntry.Text;
        }

        // Issue: This event is not fired if the url refers to a page requested
        //        by https in a server with self signed certificate
        private void OnUrlNavigating(object sender, WebNavigatingEventArgs e)
        {
            Debug.WriteLine($"++++ Navigated to {e.Url}");
        }

        // Issue: This event is fired with succes result even if the url refers to a page requested
        //        by https in a server with self signed certificate.
        private void OnUrlNavigated(object sender, WebNavigatedEventArgs e)
        {
            LoadingLabel.IsVisible = false;
            Debug.WriteLine($"++++ Navigated to {e.Url}, result is: {e.Result}");
        }
    }
}
