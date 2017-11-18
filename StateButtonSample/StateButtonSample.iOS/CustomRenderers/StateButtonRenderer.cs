using CoreAnimation;
using CoreGraphics;
using StateButtonSample.CustomRenderers;
using StateButtonSample.iOS.CustomRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(StateButton), typeof(StateButtonRenderer))]
namespace StateButtonSample.iOS.CustomRenderers
{
    public class StateButtonRenderer : ButtonRenderer
    {
        private CALayer StorkeLayer
        { get; set; }
        private StateButton XButton
        { get; set; }

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

        private int Radius
        { get; set; }


        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                XButton = (StateButton)e.NewElement;
                Radius = XButton.BorderRadius;
                SelectedTextColor = XButton.BackgroundColor;
                UnselectedTextColor = XButton.TextColor;
                e.NewElement.BorderRadius = 0;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "IsSelected")
            {
                LayoutSubviews();
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            // 先把原來的 Mask Dispose
            if (Control.Layer.Mask != null)
            {
                var mask = Control.Layer.Mask;
                mask.RemoveFromSuperLayer();
                mask.Dispose();
                Control.Layer.Mask = null;
            }

            var radius = Radius;
            UIRectCorner corners = 0;
            if (XButton.RoundedCorners == RoundedCorner.None)
            {
                radius = 0;
                corners = UIRectCorner.AllCorners;
            }
            else
            {
                List<UIRectCorner> cornersList = CreateRadiiCorners();
                foreach (var corner in cornersList)
                {
                    corners = corners | corner;
                }
            }

            if (XButton.IsSelected)
            {
                SetSelectedMask(corners, radius);
                Control.SetTitleColor(SelectedTextColor.ToUIColor(), UIControlState.Normal);
                //XButton.TextColor = SelectedTextColor;
            }
            else
            {
                SetUnselectedMask(corners, radius);
                Control.SetTitleColor(UnselectedTextColor.ToUIColor(), UIControlState.Normal);
                //XButton.TextColor = UnselectedTextColor;
            }
        }

        private void SetSelectedMask(UIRectCorner corners, int radius)
        {
            if (StorkeLayer != null)
            {
                StorkeLayer.RemoveFromSuperLayer();
                StorkeLayer.Dispose();
                StorkeLayer = null;
            }
            Control.BackgroundColor = UnselectedTextColor.ToUIColor();
            UIBezierPath path = CreatePath(corners, radius);
            var mask = new CAShapeLayer();
            mask.Path = path.CGPath;
            Control.Layer.Mask = mask;
        }



        private void SetUnselectedMask(UIRectCorner corners, int radius)
        {
            Control.BackgroundColor = UIColor.Clear;
            UIBezierPath path = CreatePath(corners, radius);
            var mask = new CAShapeLayer();
            mask.Bounds = Control.Bounds;
            mask.Position = new CGPoint(Control.Frame.Size.Width / 2, Control.Frame.Size.Height / 2);
            mask.FillColor = UIColor.Clear.CGColor;
            mask.StrokeColor = UnselectedTextColor.ToCGColor();

            mask.LineWidth = 2;
            mask.LineJoin = CAShapeLayer.JoinRound;
            mask.Path = path.CGPath;
            Control.Layer.Mask = mask;
            StorkeLayer = mask;
            Control.Layer.AddSublayer(StorkeLayer);
        }
        private UIBezierPath CreatePath(UIRectCorner corners, int radius)
        {
            return UIBezierPath.FromRoundedRect(Control.Bounds, corners, new CoreGraphics.CGSize(radius, radius));
        }
        private List<UIRectCorner> CreateRadiiCorners()
        {
            List<UIRectCorner> cornersList = new List<UIRectCorner>();
            SetTopLeftRadius(cornersList);
            SetTopRightRadius(cornersList);
            SetBottomRightRadius(cornersList);
            SetBottomLeftRadius(cornersList);
            return cornersList;
        }

        private void SetTopLeftRadius(List<UIRectCorner> cornersList)
        {
            if ((XButton.RoundedCorners & RoundedCorner.TopLeft) == RoundedCorner.TopLeft)
            {
                cornersList.Add(UIRectCorner.TopLeft);
            }
        }

        private void SetTopRightRadius(List<UIRectCorner> cornersList)
        {
            if ((XButton.RoundedCorners & RoundedCorner.TopRight) == RoundedCorner.TopRight)
            {
                cornersList.Add(UIRectCorner.TopRight);
            }
        }

        private void SetBottomRightRadius(List<UIRectCorner> cornersList)
        {
            if ((XButton.RoundedCorners & RoundedCorner.BottomRight) == RoundedCorner.BottomRight)
            {
                cornersList.Add(UIRectCorner.BottomRight);
            }
        }

        private void SetBottomLeftRadius(List<UIRectCorner> cornersList)
        {
            if ((XButton.RoundedCorners & RoundedCorner.BottomLeft) == RoundedCorner.BottomLeft)
            {
                cornersList.Add(UIRectCorner.BottomLeft);
            }
        }

    }


}
