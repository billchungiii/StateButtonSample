using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms.Platform.Android;
using StateButtonSample.CustomRenderers;
using StateButtonSample.Droid.CustomRenderers;
using Android.Graphics.Drawables;
using System.ComponentModel;
using Xamarin.Forms;
[assembly: Xamarin.Forms.ExportRenderer(typeof(StateButton), typeof(StateButtonRenderer))]
namespace StateButtonSample.Droid.CustomRenderers
{
    public class StateButtonRenderer : ButtonRenderer
    {
        /// <summary>
        /// IsSelected  true 時的文字顏色, 此時無邊框
        /// IsSelected  fasle 時的背景顏色
        /// 對應 Button 原來的 BackgroundColor 屬性
        /// </summary>
        private Color SelectedTextColor
        { get; set; }

        /// <summary>
        /// IsSelected false 時的文字與邊框顏色
        /// IsSelected true  時的背景嚴色
        /// 對應 Button 原來的 TextColor 屬性
        /// </summary>
        private Color UnselectedTextColor
        { get; set; }

        private GradientDrawable CurrentDrawable
        { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var button = (StateButton)e.NewElement;
                SelectedTextColor = button.BackgroundColor;
                UnselectedTextColor = button.TextColor;
                ChangeControl(button);
            }

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            base.OnElementPropertyChanged(sender, e);
            var button = (StateButton)sender;
            if (e.PropertyName == "IsSelected")
            {
                ChangeControl(button);
            }
        }


        private void ChangeControl(StateButton button)
        {

            Control.SetBackground(null);
            if (CurrentDrawable != null)
            {
                CurrentDrawable.Dispose();
                CurrentDrawable = null;
            }
            if (button.IsSelected)
            {
                SetSelectedState(button);
                // System.Diagnostics.Debug.WriteLine("Selected");
            }
            else
            {
                SetUnselectedState(button);
                // System.Diagnostics.Debug.WriteLine("Unselected");
            }
        }



        private void SetSelectedState(StateButton button)
        {

            GradientDrawable drawable = CreateDrawable(button);
            drawable.SetColor(UnselectedTextColor.ToAndroid());
            Control.SetBackground(drawable);
            button.TextColor = SelectedTextColor;
            CurrentDrawable = drawable;
        }


        private void SetUnselectedState(StateButton button)
        {
            GradientDrawable drawable = CreateDrawable(button);
            drawable.SetStroke(5, UnselectedTextColor.ToAndroid());
            drawable.SetColor(SelectedTextColor.ToAndroid());
            Control.SetBackground(drawable);
            button.TextColor = UnselectedTextColor;
            CurrentDrawable = drawable;
        }

        private static GradientDrawable CreateDrawable(StateButton button)
        {
            GradientDrawable drawable = new GradientDrawable();
            var radius = Xamarin.Forms.Forms.Context.ToPixels(button.BorderRadius);
            System.Diagnostics.Debug.WriteLine($"radius {radius}");
            float[] radii = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            if (button.RoundedCorners != RoundedCorner.None)
            {
                CreateRadii(button, radius, radii);
                drawable.SetCornerRadii(radii);
            }
            else
            {
                drawable.SetCornerRadius(0);
            }

            return drawable;
        }

        private static void CreateRadii(StateButton button, float radius, float[] radii)
        {
            SetTopLeftRadius(button, radius, radii);
            SetTopRightRadius(button, radius, radii);
            SetBottomRightRadius(button, radius, radii);
            SetBottomLeftRadius(button, radius, radii);

        }

        private static void SetTopLeftRadius(StateButton button, float radius, float[] radii)
        {
            if ((button.RoundedCorners & RoundedCorner.TopLeft) == RoundedCorner.TopLeft)
            {
                radii[0] = radius;
                radii[1] = radius;
            }
        }

        private static void SetTopRightRadius(StateButton button, float radius, float[] radii)
        {
            if ((button.RoundedCorners & RoundedCorner.TopRight) == RoundedCorner.TopRight)
            {
                radii[2] = radius;
                radii[3] = radius;
            }
        }
        private static void SetBottomRightRadius(StateButton button, float radius, float[] radii)
        {
            if ((button.RoundedCorners & RoundedCorner.BottomRight) == RoundedCorner.BottomRight)
            {
                radii[4] = radius;
                radii[5] = radius;
            }
        }

        private static void SetBottomLeftRadius(StateButton button, float radius, float[] radii)
        {
            if ((button.RoundedCorners & RoundedCorner.BottomLeft) == RoundedCorner.BottomLeft)
            {
                radii[6] = radius;
                radii[7] = radius;
            }
        }

    }
}