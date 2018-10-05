/****************************** Module Header ******************************\
 * Module Name:  SelectedRegion.cs
 * Project:      CSWindowsStoreAppCropBitmap
 * Copyright (c) Microsoft Corporation.
 *
 * This class represents the selected region. It implements the INotifyPropertyChanged
 * interface and can be bound to the Xaml element
 *
 *
 * This source is subject to the Microsoft Public License.
 * See http://www.microsoft.com/en-us/openness/licenses.aspx#MPL
 * All other rights reserved.
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

namespace XamlBrewer.Uwp.Controls.Helpers
{
    using System;
    using TenenbaumTest.BoilerPlate;
    using Windows.Foundation;

    public class SelectedRegion : BindableBase
    {
        private const string TopLeftCornerCanvasLeftPropertyName = "TopLeftCornerCanvasLeft";
        private const string TopLeftCornerCanvasTopPropertyName = "TopLeftCornerCanvasTop";
        private const string BottomRightCornerCanvasLeftPropertyName = "BottomRightCornerCanvasLeft";
        private const string BottomRightCornerCanvasTopPropertyName = "BottomRightCornerCanvasTop";
        private const string OutterRectPropertyName = "OuterRect";
        private const string SelectedRectPropertyName = "SelectedRect";

        public const string TopLeftCornerName = "TopLeftCorner";
        public const string TopRightCornerName = "TopRightCorner";
        public const string BottomLeftCornerName = "BottomLeftCorner";
        public const string BottomRightCornerName = "BottomRightCorner";

        ///// <summary>
        ///// The minimum size of the selected region
        ///// </summary>

        protected const double minWidthSize = 2;
        protected const double minHeightSize = 2;




       



        private double topLeftCornerCanvasLeft;

        /// <summary>
        /// The Canvas.Left property of the TopLeft corner.
        /// </summary>
        public double TopLeftCornerCanvasLeft
        {
            get => topLeftCornerCanvasLeft;
            protected set => SetProperty(ref topLeftCornerCanvasLeft, value);
        }

        private double topLeftCornerCanvasTop;

        /// <summary>
        /// The Canvas.Top property of the TopLeft corner.
        /// </summary>
        public double TopLeftCornerCanvasTop
        {
            get => topLeftCornerCanvasTop;
            protected set => SetProperty(ref topLeftCornerCanvasTop, value);
        }

        private double bottomRightCornerCanvasLeft;

        /// <summary>
        /// The Canvas.Left property of the BottomRight corner.
        /// </summary>
        public double BottomRightCornerCanvasLeft
        {
            get => bottomRightCornerCanvasLeft;
            protected set => SetProperty(ref bottomRightCornerCanvasLeft, value);
        }

        private double bottomRightCornerCanvasTop;

        /// <summary>
        /// The Canvas.Top property of the BottomRight corner.
        /// </summary>
        public double BottomRightCornerCanvasTop
        {
            get => bottomRightCornerCanvasTop;
            protected set => SetProperty(ref bottomRightCornerCanvasTop, value);
        }

        private Rect outerRect;

        /// <summary>
        /// The outer rect. The non-selected region can be represented by the
        /// OuterRect and the SelectedRect.
        /// </summary>
        public Rect OuterRect
        {
            get => outerRect;
             set => SetProperty(ref outerRect, value);
        }

        private Rect selectedRect;

        /// <summary>
        /// The selected region, which is represented by the four corners.
        /// </summary>
        public Rect SelectedRect
        {
            get => selectedRect;
            set => SetProperty(ref selectedRect, value);
        }

        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);

            // When the corner is moved, update the SelectedRect.
            if (propertyName == TopLeftCornerCanvasLeftPropertyName ||
                propertyName == TopLeftCornerCanvasTopPropertyName ||
                propertyName == BottomRightCornerCanvasLeftPropertyName ||
                propertyName == BottomRightCornerCanvasTopPropertyName)
            {
                SelectedRect = new Rect(
                    TopLeftCornerCanvasLeft,
                    TopLeftCornerCanvasTop,
                    BottomRightCornerCanvasLeft - TopLeftCornerCanvasLeft,
                    BottomRightCornerCanvasTop - topLeftCornerCanvasTop);
            }
        }

        public void ResetCorner(double topLeftCornerCanvasLeft, double topLeftCornerCanvasTop,
            double bottomRightCornerCanvasLeft, double bottomRightCornerCanvasTop)
        {
            this.TopLeftCornerCanvasLeft = topLeftCornerCanvasLeft;
            this.TopLeftCornerCanvasTop = topLeftCornerCanvasTop;
            this.BottomRightCornerCanvasLeft = bottomRightCornerCanvasLeft;
            this.BottomRightCornerCanvasTop = bottomRightCornerCanvasTop;
        }

        ///// <summary>
        ///// Update the Canvas.Top and Canvas.Left of the corner.
        ///// </summary>
        //public void UpdateCorner(string cornerName, double leftUpdate, double topUpdate)
        //{
        //    UpdateCorner(cornerName, leftUpdate, topUpdate);
        //}

        /// <summary>
        /// Update the Canvas.Top and Canvas.Left of the corner.
        /// </summary>
        public void UpdateCorner(string cornerName, double leftUpdate, double topUpdate)
        {
            switch (cornerName)
            {
                case SelectedRegion.TopLeftCornerName:
                    TopLeftCornerCanvasLeft = ValidateValue(topLeftCornerCanvasLeft + leftUpdate,
                        0, bottomRightCornerCanvasLeft - minWidthSize);
                    TopLeftCornerCanvasTop = ValidateValue(topLeftCornerCanvasTop + topUpdate,
                        0, bottomRightCornerCanvasTop - minHeightSize);
                    break;

                case SelectedRegion.TopRightCornerName:
                    BottomRightCornerCanvasLeft = ValidateValue(bottomRightCornerCanvasLeft + leftUpdate,
                        topLeftCornerCanvasLeft + minWidthSize, outerRect.Width);
                    TopLeftCornerCanvasTop = ValidateValue(topLeftCornerCanvasTop + topUpdate,
                        0, bottomRightCornerCanvasTop - minHeightSize);
                    break;

                case SelectedRegion.BottomLeftCornerName:
                    TopLeftCornerCanvasLeft = ValidateValue(topLeftCornerCanvasLeft + leftUpdate,
                        0, bottomRightCornerCanvasLeft - minWidthSize);
                    BottomRightCornerCanvasTop = ValidateValue(bottomRightCornerCanvasTop + topUpdate,
                        topLeftCornerCanvasTop + minHeightSize, outerRect.Height);
                    break;

                case SelectedRegion.BottomRightCornerName:
                    BottomRightCornerCanvasLeft = ValidateValue(bottomRightCornerCanvasLeft + leftUpdate,
                        topLeftCornerCanvasLeft + minWidthSize, outerRect.Width);
                    BottomRightCornerCanvasTop = ValidateValue(bottomRightCornerCanvasTop + topUpdate,
                        topLeftCornerCanvasTop + minHeightSize, outerRect.Height);
                    break;

                default:
                    throw new ArgumentException("cornerName: " + cornerName + "  is not recognized.");
            }
        }

        private double ValidateValue(double tempValue, double from, double to)
        {
            if (tempValue < from)
            {
                tempValue = from;
            }

            if (tempValue > to)
            {
                tempValue = to;
            }

            return tempValue;
        }

        /// <summary>
        /// Update the SelectedRect when it is moved or scaled.
        /// </summary>
        public void UpdateSelectedRect(double scale, double leftUpdate, double topUpdate)
        {
            double width = bottomRightCornerCanvasLeft - topLeftCornerCanvasLeft;
            double height = bottomRightCornerCanvasTop - topLeftCornerCanvasTop;

            if (scale != 1)
            {
                double scaledLeftUpdate = width * (scale - 1) / 2;
                double scaledTopUpdate = height * (scale - 1) / 2;

                if (scale > 1)
                {
                    UpdateCorner(SelectedRegion.BottomRightCornerName, scaledLeftUpdate, scaledTopUpdate);
                    UpdateCorner(SelectedRegion.TopLeftCornerName, -scaledLeftUpdate, -scaledTopUpdate);
                }
                else
                {
                    UpdateCorner(SelectedRegion.TopLeftCornerName, -scaledLeftUpdate, -scaledTopUpdate);
                    UpdateCorner(SelectedRegion.BottomRightCornerName, scaledLeftUpdate, scaledTopUpdate);
                }

                return;
            }

            double minWidth = Math.Max(minWidthSize, width * scale);
            double minHeight = Math.Max(minHeightSize, height * scale);

            // Move towards BottomRight: Move BottomRightCorner first, and then move TopLeftCorner.
            if (leftUpdate >= 0 && topUpdate >= 0)
            {
                this.UpdateCorner(SelectedRegion.BottomRightCornerName, leftUpdate, topUpdate);
                this.UpdateCorner(SelectedRegion.TopLeftCornerName, leftUpdate, topUpdate);
            }

            // Move towards TopRight: Move TopRightCorner first, and then move BottomLeftCorner.
            else if (leftUpdate >= 0 && topUpdate < 0)
            {
                this.UpdateCorner(SelectedRegion.TopRightCornerName, leftUpdate, topUpdate);
                this.UpdateCorner(SelectedRegion.BottomLeftCornerName, leftUpdate, topUpdate);
            }

            // Move towards BottomLeft: Move BottomLeftCorner first, and then move TopRightCorner.
            else if (leftUpdate < 0 && topUpdate >= 0)
            {
                this.UpdateCorner(SelectedRegion.BottomLeftCornerName, leftUpdate, topUpdate);
                this.UpdateCorner(SelectedRegion.TopRightCornerName, leftUpdate, topUpdate);
            }

            // Move towards TopLeft: Move TopLeftCorner first, and then move BottomRightCorner.
            else if (leftUpdate < 0 && topUpdate < 0)
            {
                this.UpdateCorner(SelectedRegion.TopLeftCornerName, leftUpdate, topUpdate);
                this.UpdateCorner(SelectedRegion.BottomRightCornerName, leftUpdate, topUpdate);
            }
        }
    }
}