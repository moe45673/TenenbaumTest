using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenenbaumTest.BoilerPlate;

namespace TenenbaumTest
{
    public class MainPageViewModel : BindableBase
    {
        

        private ImageModel _img;

        public ImageModel Img
        {
            get { return _img; }
            set { SetProperty(ref _img , value); }
        }

        public MainPageViewModel()
        {
            Img = new ImageModel();
        }

        public MainPageViewModel(ImageModel model)
        {
            Img = model;
        }




    }
}
