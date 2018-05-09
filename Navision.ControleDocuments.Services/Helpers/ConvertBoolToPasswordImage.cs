using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Navision.ControleDocuments.Services.Helpers
{
    public class ConvertBoolToPasswordImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                string ResourceId;
                if ((bool)value)
                {
                    ResourceId = "Navision.ControleDocuments.Services.Images.PasswordOk.png";
                    if (String.IsNullOrEmpty(ResourceId))
                        return null;
                    else
                    {
                        var assembly = typeof(ConvertBoolToPasswordImage).Assembly;
                        return ImageSource.FromStream(() => assembly.GetManifestResourceStream(ResourceId));
                    }
                }
                else if (!(bool)value)
                {
                    ResourceId = "Navision.ControleDocuments.Services.Images.PasswordNotOk.png";
                    if (String.IsNullOrEmpty(ResourceId))
                        return null;
                    else
                    {
                        var assembly = typeof(ConvertBoolToPasswordImage).Assembly;
                        return ImageSource.FromStream(() => assembly.GetManifestResourceStream(ResourceId));
                    }
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(value, targetType, parameter, culture);
        }
    }
}
