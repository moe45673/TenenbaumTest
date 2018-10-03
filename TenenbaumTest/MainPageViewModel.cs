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


        private bool _isCropping;

        public bool IsCropping
        {
            get { return this._isCropping; }
            set { SetProperty(ref _isCropping, value); }
        }

        private WriteableBitmap _img;

        public WriteableBitmap Img
        {
            get { return _img ?? new WriteableBitmap(60,60); }
            set { SetProperty(ref _img , value); }
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

        }




    }
}
