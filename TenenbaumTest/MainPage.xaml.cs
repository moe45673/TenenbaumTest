using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;
using XamlBrewer.Uwp.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TenenbaumTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                ViewModel = DataContext as MainPageViewModel;
            };

            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ImageCropper.SelectedRegion.PropertyChanged += SelectedRegion_PropertyChanged;
        }

        private void SelectedRegion_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //ImageCropper.SelectedRegion.SelectedRect.Height = ImageCropper.SelectedRegion.SelectedRect.Height > ViewModel.MaxCropHeight ? ViewModel.MaxCropHeight : ImageCropper.SelectedRegion.SelectedRect.Height;
        }

        public MainPageViewModel ViewModel { get; set; }
    

    private async void OpenImage(object sender, RoutedEventArgs eventArgs)
    {
        var picker = new Windows.Storage.Pickers.FileOpenPicker();
        picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
        picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
        picker.FileTypeFilter.Add(".jpg");
        picker.FileTypeFilter.Add(".jpeg");
        picker.FileTypeFilter.Add(".png");
        picker.FileTypeFilter.Add(".bmp");

        Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            // Application now has read/write access to the picked file
            var wb = new WriteableBitmap(1, 1);
            await wb.LoadAsync(file);
            ViewModel.Img = wb;



        }

    }

        
    }
}
