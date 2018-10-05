using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenenbaumTest.BoilerPlate;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace TenenbaumTest
{
    public class MainPageViewModel : BindableBase
    {
        private const int maxCropHeight = 100;

        private bool _isCropping;

        public bool IsCropping
        {
            get { return _isCropping; }
            set { SetProperty(ref _isCropping, value); }
        }

        private WriteableBitmap _img;

        public WriteableBitmap Img
        {
            get { return _img ?? new WriteableBitmap(60,60); }
            set { SetProperty(ref _img , value); }
        }

        private WriteableBitmap _croppedImg;

        public WriteableBitmap CroppedImg
        {
            get { return _croppedImg ?? new WriteableBitmap(2, 2); }
            set { SetProperty(ref _croppedImg, value); }
        }

        private int _maxCropHeight;
        public int MaxCropHeight
        {
            get { return this._maxCropHeight; }
            set { SetProperty(ref _maxCropHeight, value); }
        }

        public MainPageViewModel() : this(default(WriteableBitmap))
        {
            
        }

        public MainPageViewModel(WriteableBitmap model = default(WriteableBitmap))
        {
            Img = model; //Should use Dependency Injection
            IsCropping = true;
            MaxCropHeight = maxCropHeight;
        }




    }
}
