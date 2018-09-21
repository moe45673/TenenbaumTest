﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TenenbaumTest.BoilerPlate;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace TenenbaumTest
{
    public class ImageModel : BindableBase
    {
        public ImageModel() : this(default(ImageProperties), default(StorageFile), string.Empty, string.Empty)
        {

        }

        public ImageModel(ImageProperties properties, StorageFile imageFile, string name, string type)
        {
            ImageProperties = properties;
            ImageName = name;
            ImageFileType = type;
            ImageFile = imageFile;
            var rating = ((int?)properties?.Rating).GetValueOrDefault();
            var random = new Random();
            //ImageRating = rating == 0 ? random.Next(1, 5) : rating;
        }

        public StorageFile ImageFile { get; }

        public ImageProperties ImageProperties { get; }

        private BitmapImage _imageSource = null;
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public string ImageName { get; }

        public string ImageFileType { get; }

        public string ImageDimensions => $"{ImageSource.PixelWidth} x {ImageSource.PixelHeight}";

        public string ImageTitle
        {
            get => string.IsNullOrEmpty(ImageProperties.Title) ? ImageName : ImageProperties.Title;
            set
            {
                if (ImageProperties.Title != value)
                {
                    ImageProperties.Title = value;
                    var ignoreResult = ImageProperties.SavePropertiesAsync();
                    OnPropertyChanged();
                }
            }
        }

        //public int ImageRating
        //{
        //    get => (int)ImageProperties.Rating;
        //    set
        //    {
        //        if (ImageProperties.Rating != value)
        //        {
        //            ImageProperties.Rating = (uint)value;
        //            var ignoreResult = ImageProperties.SavePropertiesAsync();
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        private float _exposure = 0;
        public float Exposure
        {
            get => _exposure;
            set => SetEditingProperty(ref _exposure, value);
        }

        private float _temperature = 0;
        public float Temperature
        {
            get => _temperature;
            set => SetEditingProperty(ref _temperature, value);
        }

        private float _tint = 0;
        public float Tint
        {
            get => _tint;
            set => SetEditingProperty(ref _tint, value);
        }

        private float _contrast = 0;
        public float Contrast
        {
            get => _contrast;
            set => SetEditingProperty(ref _contrast, value);
        }

        private float _saturation = 1;
        public float Saturation
        {
            get => _saturation;
            set => SetEditingProperty(ref _saturation, value);
        }

        private float _blur = 0;
        public float Blur
        {
            get => _blur;
            set => SetEditingProperty(ref _blur, value);
        }

        private bool _needsSaved = false;
        public bool NeedsSaved
        {
            get => _needsSaved;
            set => SetProperty(ref _needsSaved, value);
        }

        

        protected bool SetEditingProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (SetProperty(ref storage, value, propertyName))
            {
                if (Exposure != 0 || Temperature != 0 || Tint != 0 || Contrast != 0 || Saturation != 1 || Blur != 0)
                {
                    NeedsSaved = true;
                }
                else
                {
                    NeedsSaved = false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
