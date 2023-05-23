using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.Extensions.Markup
{
    public class FluentRegularIconSource : FontImageSource
    {
        public FluentSystemIconsRegularKeys IconName
        {
            get { return (FluentSystemIconsRegularKeys)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Glyph.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty IconNameProperty =
            BindableProperty.Create(nameof(IconName), typeof(FluentSystemIconsRegularKeys), typeof(FluentRegularIconSource),
                propertyChanged:
                (bindable, oldValue, newValue) =>
                {
                    ((FluentRegularIconSource)bindable).Glyph = ((FluentSystemIconsRegularKeys)newValue).ToGlyphString();
                });


        public FluentRegularIconSource()
        {
            FontFamily = "FluentSystemIconsRegular";
            Color = Colors.Black;
        }
    }
    public class FluentFilledIconSource : FontImageSource
    {
        public FluentSystemIconsFilledKeys IconName
        {
            get { return (FluentSystemIconsFilledKeys)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Glyph.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty IconNameProperty =
            BindableProperty.Create(nameof(IconName), typeof(FluentSystemIconsFilledKeys), typeof(FluentFilledIconSource),
                propertyChanged:
                (bindable, oldValue, newValue) =>
                {
                    ((FluentFilledIconSource)bindable).Glyph = ((FluentSystemIconsFilledKeys)newValue).ToGlyphString();
                });

        public FluentFilledIconSource()
        {
            FontFamily = "FluentSystemIconsFilled";
            Color = Colors.Black;

        }
    }
}
