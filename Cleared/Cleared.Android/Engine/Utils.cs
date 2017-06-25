using Android.Views;
using Android.Animation;
using Cleared.XamNative.Droid;

namespace Cleared.Droid
{
    internal static class Utils
    {
        public static Java.Lang.ICharSequence ToJavaString(this string str)
        {
            if (str == null) return null;
            return new Java.Lang.String(str);
        }
    }

    public class JavaLangObject<T> : Java.Lang.Object
    {
        public T Value { get; set; }
        public JavaLangObject(T value) { Value = value; }
        public static implicit operator T(JavaLangObject<T> wrapper) { return wrapper.Value; }
    }




}