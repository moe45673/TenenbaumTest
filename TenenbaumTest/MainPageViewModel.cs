using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenenbaumTest.BoilerPlate;
using Windows.UI.Xaml.Media.Imaging;

namespace TenenbaumTest
{
    public class MainPageViewModel : BindableBase
    {
        

        private WriteableBitmap _img;

        public WriteableBitmap Img
        {
            get { return _img; }
            set { SetProperty(ref _img , value); }
        }

        public MainPageViewModel() : this(default(WriteableBitmap))
        {
            
        }

        public MainPageViewModel(WriteableBitmap model)
        {
            Img = model ?? new WriteableBitmap(1, 1); //Should use Dependency Injection
        }




    }
}
