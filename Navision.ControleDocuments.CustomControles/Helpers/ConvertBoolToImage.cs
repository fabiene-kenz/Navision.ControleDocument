using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Navision.ControleDocuments.CustomControles.Helpers
{
    public class ConvertBoolToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value)
                {
                    string RessourceId = "Navision.ControleDocuments.CustomControles.Images.Check.png";
                    if (String.IsNullOrEmpty(RessourceId))
                    {
                        return null;
                    }
                    else
                    {
                        var assembly = typeof(ConvertBoolToImage).Assembly;
                        return ImageSource.FromStream(() => assembly.GetManifestResourceStream(RessourceId));
                    }
                }
                else if (!(bool)value)
                {
                    string RessourceId = "Navision.ControleDocuments.CustomControles.Images.Cancel.png";
                    if (String.IsNullOrEmpty(RessourceId))
                    {
                        return null;
                    }
                    else
                    {
                        var assembly = typeof(ConvertBoolToImage).Assembly;
                        return ImageSource.FromStream(() => assembly.GetManifestResourceStream(RessourceId));
                    }
                }
                
            }
            else
            {
                string RessourceId = "Navision.ControleDocuments.CustomControles.Images.Edit.png";
                if (String.IsNullOrEmpty(RessourceId))
                {
                    return null;
                }
                else
                {
                    var assembly = typeof(ConvertBoolToImage).Assembly;
                    return ImageSource.FromStream(() => assembly.GetManifestResourceStream(RessourceId));
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(value, targetType, parameter, culture);
        }
    }
}
