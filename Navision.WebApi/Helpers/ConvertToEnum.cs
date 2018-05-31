using Navision.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navision.WebApi.Helpers
{
    public static class ConvertToEnum
    {
        public static bool? GetBool(this EnumStatut.Values enumstatut)
        {
            switch (enumstatut)
            {
                case (EnumStatut.Values.Null):
                    return null;
                case (EnumStatut.Values.True):
                    return true;
                case (EnumStatut.Values.False):
                    return false;
                default:
                    return null;
            }
        }
    }
}