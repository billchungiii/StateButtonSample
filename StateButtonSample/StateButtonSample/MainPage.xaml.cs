using StateButtonSample.CustomRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StateButtonSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void StateButton_Clicked(object sender, EventArgs e)
        {
            ((StateButton)sender).IsSelected = !((StateButton)sender).IsSelected;
            // System.Diagnostics.Debug.WriteLine(((StateButton)sender).IsSelected);
            ((StateButton)sender).BorderRadius = 5;
        }
    }
}
