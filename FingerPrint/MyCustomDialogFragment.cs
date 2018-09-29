﻿using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Fingerprint.Dialog;

namespace FingerPrint
{
    public class MyCustomDialogFragment : FingerprintDialogFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            view.Background = new ColorDrawable(Color.Magenta);
            return view;
        }
    }
}
