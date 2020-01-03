using System;
using System.Collections.Generic;
using System.ComponentModel;
using BindingBugExample.ApiViewModels;
using BindingBugExample.MVVM;
using BindingBugExample.ViewModels;
using Xamarin.Forms;

namespace BindingBugExample
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly StackLayout _layout;

        private readonly PageViewModel _pageViewModel;

        public MainPage()
        {
            InitializeComponent();

            _layout = new StackLayout();
            Content = _layout;
            _pageViewModel = new PageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var fakeRegionId = 1;
            var fakeCategories = new CategoriesListApiViewModel
            {
                //It's ditionary of IDs and nameOf
                Category = new Dictionary<long, string>
                {
                    {0, "test1" },
                    {1, "test2" },
                    {32, "test3" },
                    {14, "test4" }
                }
            };

            _pageViewModel.AddCategoriesOption(fakeCategories, fakeRegionId);

            //adding switches
            foreach (var category in fakeCategories.Category)
            {
                var checkbox = new Switch { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };

                checkbox.SetBinding(Switch.IsToggledProperty, $"{nameof(_pageViewModel.CategoriesList)}[{fakeRegionId}].Value[{category.Value}].{nameof(UniversalSwitchViewModel.Value)}", BindingMode.TwoWay);
                
                //to test purpose only:
                checkbox.Toggled += (s, eventHanderArgumets) =>
                {
                    Console.WriteLine($"Swtich name {category.Value} with value: {_pageViewModel.CategoriesList[fakeRegionId][category.Value].Value}");
                };
                //end

                _layout.Children.Add(checkbox);
            }

            //test binding
            var testData = new List<string>
            {
                "test1",
                "test3"
            };
            
            _pageViewModel.SetCategoriesOption(testData, fakeRegionId);
        }
    }
}
