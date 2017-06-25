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
using System.Globalization;

namespace Cleared.Droid
{
    public static class StringExtensions
    {
        public static int ToColorInt(this string str)
        {
            int value = 0;
            if (string.IsNullOrWhiteSpace(str))
                return 0;

            str = str.Replace("#", "");
            str = str.Replace("0x", "");

            if (Int32.TryParse(str, NumberStyles.AllowHexSpecifier | NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }

            return 0;
        }
    }
}