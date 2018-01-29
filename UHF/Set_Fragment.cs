
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace UHF
{
	public class Set_Fragment : Fragment
	{
		Button btnGetFre;
		Button btnSetFre;
		Button btnGetPower;
		Button btnSetPower;
		Button btnGetTime;
		Button btnSetTime;
		EditText edtTxtWorkTime;
		EditText edtTxtWaitTime;
		Spinner spnWorkMode;
		Spinner spnPower;
		MainActivity mContext;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container,Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.Set_Fragment,container,false);
			return view;
		}
		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated (savedInstanceState);
		
			mContext = (MainActivity)Activity;
			btnGetFre = View.FindViewById<Button> (Resource.Id.btnGetFre);
			btnSetFre = View.FindViewById<Button> (Resource.Id.btnSetFre);
			btnGetPower = View.FindViewById<Button> (Resource.Id.btnGetPower);
			btnSetPower = View.FindViewById<Button> (Resource.Id.btnSetPower);
			btnGetTime = View.FindViewById<Button> (Resource.Id.btnGetTime);
			btnSetTime = View.FindViewById<Button> (Resource.Id.btnSetTime);
			edtTxtWorkTime = View.FindViewById<EditText> (Resource.Id.edtTxtTime);
			edtTxtWaitTime = View.FindViewById<EditText> (Resource.Id.edtTxtWait);
			spnWorkMode = View.FindViewById<Spinner> (Resource.Id.spnWorkMode);
			spnPower = View.FindViewById<Spinner> (Resource.Id.spnPower);
		    
			btnGetFre.Click += delegate {
				GetFre();
			};
			btnSetFre.Click += delegate {
				SetFre();
			};
			btnGetPower.Click += delegate {
				GetPower();
			};
			btnSetPower.Click += delegate {
				SetPower();
			};
			btnGetTime.Click += delegate {
				GetTime();
			};
			btnSetTime.Click += delegate {
				SetTime();
			};
		}
		 
		public override void OnResume() {
			base.OnResume();
			GetFre();
			GetPower();
			//GetTime();
		}
		private void SetFre()
		{
			sbyte iFre = (sbyte)spnWorkMode.SelectedItemPosition;
			if (mContext.uhfAPI.SetFrequencyMode(iFre)) 
			{
				Toast.MakeText (mContext,"success!",ToastLength.Short).Show();
			} 
			else 
			{
				Toast.MakeText (mContext,"failuer!",ToastLength.Short).Show();
			}

		}
		private void GetFre()
		{
			int idx = mContext.uhfAPI.FrequencyMode;

			if (idx != -1)
			{
				int count = spnWorkMode.Count;
				spnWorkMode.SetSelection(idx > count - 1 ? count - 1 : idx);
			} 
			else
			{
				Toast.MakeText (mContext,"failuer!",ToastLength.Short).Show();
			}
		}
			
		public void GetPower() {
			int iPower = mContext.uhfAPI.Power;
			if (iPower > -1) {
				int position = iPower - 5;
				int count = spnPower.Count;
				spnPower.SetSelection(position > count - 1 ? count - 1 : position);
			}
			else 
			{
				Toast.MakeText (mContext,"failuer!",ToastLength.Short).Show();
			}

		}
		public void SetPower() {
			int iPower = spnPower.SelectedItemPosition + 5;
		
			if (mContext.uhfAPI.SetPower(iPower)) 
			{
				Toast.MakeText (mContext,"success!",ToastLength.Short).Show();
			} 
			else 
			{
				Toast.MakeText (mContext,"failuer!",ToastLength.Short).Show();
			}

		}


		public void GetTime() {
			int[] pwm = mContext.uhfAPI.GetPwm();
			if (pwm == null||pwm.Length<2)
			{
				Toast.MakeText (mContext,"failuer!",ToastLength.Short).Show();
				return;
			}
			edtTxtWorkTime.SetText(pwm[0]);
			edtTxtWaitTime.SetText(pwm[1]);

		}

		public void SetTime() 
		{
			try
			{
				int workTime = int.Parse (edtTxtWorkTime.Text);
				int waitTime = int.Parse (edtTxtWaitTime.Text);
				if(mContext.uhfAPI.SetPwm(workTime,waitTime)) 
				{
					Toast.MakeText (mContext,"success!",ToastLength.Short).Show();
				} 
				else 
				{
					Toast.MakeText (mContext,"failuer!",ToastLength.Short).Show();
				}
			}
			catch
			{

			}

		}
	}
}

