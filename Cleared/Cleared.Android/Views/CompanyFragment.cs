using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.OS;
using Android.Views;

namespace Cleared.Droid.Views
{
    public class CompanyFragment : Android.Support.V4.App.Fragment
    {

        public static CompanyFragment CreateInstance()
        {
            return new CompanyFragment();

            //var fragment = new CompanyFragment();
            //var args = new Bundle();
            //args.PutString("ARGName1", "Value");
            //fragment.Arguments = args;
            //return fragment;
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_company, container, false);
            return view;
        }
    }
}