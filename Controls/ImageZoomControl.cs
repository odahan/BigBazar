#nullable disable
namespace BigBazar.Controls
{
    using System;
    using Microsoft.Maui.Controls;


    namespace ImageZoomControlProject.Controls
    {
        /// <summary>
        /// Image control supporting zooming and panning.
        /// usage: <controls:ImageZoomControl ImageSource="{Binding ImageSource}" />
        /// </summary>
        public class ImageZoomControl : ContentView
        {
            public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
                propertyName: "ImageSource",
                returnType: typeof(ImageSource),
                declaringType: typeof(ImageZoomControl),
                defaultValue: default(ImageSource),
                propertyChanged: OnImageSourceChanged);


            /// <summary>
            /// Controls the image to be displayed.
            /// </summary>
            public ImageSource ImageSource
            {
                get => (ImageSource)GetValue(ImageSourceProperty);
                set => SetValue(ImageSourceProperty, value);
            }

            private Image image;
            private double currentScale = 1;
          
            /// <summary>
            /// Constructor
            /// </summary>
            public ImageZoomControl()
            {
                image = new Image { Aspect = Aspect.AspectFit };
                Content = image;
                AddGestureRecognizers(image);
            }

            private static void OnImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
            {
                var control = (ImageZoomControl)bindable;
                control.image.Source = (ImageSource)newValue; // Directly use the new value which is already an ImageSource
            }

            private void AddGestureRecognizers(Image image)
            {
                var pinchGesture = new PinchGestureRecognizer();
                pinchGesture.PinchUpdated += OnPinchUpdated;
                image.GestureRecognizers.Add(pinchGesture);

                var panGesture = new PanGestureRecognizer();
                panGesture.PanUpdated += OnPanUpdated;
                image.GestureRecognizers.Add(panGesture);

                var tapGesture = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
                tapGesture.Tapped += OnDoubleTap;
                image.GestureRecognizers.Add(tapGesture);
            }

            private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
            {
                if (e.Status == GestureStatus.Started)
                {
                    image.Aspect = Aspect.Fill;
                }
                if (e.Status == GestureStatus.Running)
                {
                    currentScale = image.Scale * e.Scale;
                    image.Scale = currentScale;
                }
                if (e.Status == GestureStatus.Completed)
                {
                    image.Aspect = Aspect.Fill;  // Keep Aspect.Fill if zooming has stopped at a non-default scale.
                }
            }

            private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
            {
                image.TranslationX += e.TotalX;
                image.TranslationY += e.TotalY;
            }

            /// <summary>
            /// Reset zoom and pan on double tap.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnDoubleTap(object sender, EventArgs e)
            {
                image.Scale = 1;
                currentScale = 1;
                image.TranslationX = 0;
                image.TranslationY = 0;
                image.Aspect = Aspect.AspectFit;  // Reset to AspectFit on double tap
            }
        }
    }

}