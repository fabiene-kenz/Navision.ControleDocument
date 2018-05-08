using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Navision.ControleDocuments.Services.Services
{
    [ContentProperty("RessourceId")]
    public class EmbeddedImage : IMarkupExtension
    {
        public string RessourceId { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrEmpty(RessourceId))
            {
                return null;
            }
            else
            {
                var assembly = typeof(EmbeddedImage).Assembly;
                return ImageSource.FromStream(() => assembly.GetManifestResourceStream(RessourceId));
            }
        }
    }
}
