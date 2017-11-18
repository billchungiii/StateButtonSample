using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StateButtonSample.CustomRenderers
{
    /// <summary>
    /// 當 IsSelected 為 false 時, TextColor 屬性值為 文字與邊框的顏色, 
    /// 當 IsSelected 為 true 時,  BackgroundColor 為文字顏色, 背景為 BackgroundColor, 此時無邊框
    /// </summary>
    public class StateButton : Button
    {
        public static readonly BindableProperty RoundedCornersProperty =
  BindableProperty.Create("RoundedCorners", typeof(RoundedCorner), typeof(StateButton), (RoundedCorner.TopLeft | RoundedCorner.BottomLeft));
        /// <summary>
        /// 哪一邊的兩個角要圓形,可選左右上下
        /// </summary>
        public RoundedCorner RoundedCorners
        {
            get { return (RoundedCorner)GetValue(RoundedCornersProperty); }
            set { SetValue(RoundedCornersProperty, value); }
        }

        /// <summary>
        /// 是否被選擇, 
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty =
  BindableProperty.Create("IsSelected", typeof(bool), typeof(StateButton), true);
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public StateButton()
        {
            
        }
    }

    [Flags]
    public enum RoundedCorner
    {
        None = 0,
        TopLeft = 1,
        BottomLeft = 2,
        TopRight = 4,
        BottomRight = 8,
        All = 15
    }
}
