using System;
using System.ComponentModel;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using XamlBrewer.Uwp.Controls;
using XamlBrewer.Uwp.Controls.Helpers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TenenbaumTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                ViewModel = DataContext as MainPageViewModel;
                ViewModel.PropertyChanged -= OnPropertyChanged;
                ViewModel.PropertyChanged += OnPropertyChanged;

                VisibilityColor = SetVisibilityColor(ViewModel.IsCropping, Colors.Blue, Colors.Transparent);
            };
            
        }



        private SolidColorBrush VisibilityColor;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.IsCropping) :
                    VisibilityColor = SetVisibilityColor(ViewModel.IsCropping, Colors.Blue, Colors.Transparent);
                    break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ImageCropper.SelectedRegion.PropertyChanged += SelectedRegion_PropertyChanged;
        }

        //void Target_PointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    // Prevent most handlers along the event route from handling the same event again.
            

        //    PointerPoint ptrPt = e.GetCurrentPoint(this.ImageCropper.);

            

        //    // Lock the pointer to the target.
        //    Target.CapturePointer(e.Pointer);

        //    // Update event log.
        //    UpdateEventLog("Pointer captured: " + ptrPt.PointerId);

        //    // Check if pointer exists in dictionary (ie, enter occurred prior to press).
        //    if (!pointers.ContainsKey(ptrPt.PointerId))
        //    {
        //        // Add contact to dictionary.
        //        pointers[ptrPt.PointerId] = e.Pointer;
        //    }

        //    // Change background color of target when pointer contact detected.
        //    Target.Fill = new SolidColorBrush(Windows.UI.Colors.Green);

        //    // Display pointer details.
        //    CreateInfoPop(ptrPt);
        //}

        private void SelectedRegion_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedRegion.SelectedRect):
                case nameof(SelectedRegion.BottomRightCornerCanvasTop):
                case nameof(SelectedRegion.TopLeftCornerCanvasTop):

                    //var tempRect = (sender as SelectedRegion).SelectedRect;
                    //if (ValidateSelectedRectangle(ref tempRect, maxHeight: ViewModel.MaxCropHeight))
                    //{
                    //    var selectedRegion = sender as SelectedRegion;
                    //    selectedRegion.ResetCorner(tempRect.Left, tempRect.Top, tempRect.Right, tempRect.Bottom);
                    //}
                    break;
            }
        }

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

        private static bool ValidateSelectedRectangle(ref Rect selectedRect, double minWidth = 2, double minHeight = 2, double maxWidth = double.MaxValue, double maxHeight = double.MaxValue)
        {
            var croppedHeight = Math.Max(minHeight, Math.Min(selectedRect.Height, maxHeight));
            var croppedWidth = Math.Max(minWidth, Math.Min(selectedRect.Width, maxWidth));

            if (selectedRect.Width != croppedWidth || selectedRect.Height != croppedHeight) //if given Rect is invalid
            {
                
                selectedRect = new Rect(selectedRect.X, selectedRect.Y, croppedWidth, croppedHeight);
                return true;
            }
            return false;
        }

        private static SolidColorBrush SetVisibilityColor(bool condition, Color trueColor, Color falseColor)
        {
            var color = condition ? trueColor : falseColor;

            return new SolidColorBrush(color);
        }
    }
}