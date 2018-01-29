
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Media;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UHF
{
	[Activity (Label = "Write_Fragment")]			
	public class Write_Fragment : Fragment
	{
		SoundPool soundPool;
		int soundPoolId;
		MainActivity mContext;
		EditText edtTxtAddress_W;
		EditText edtTxtLeng_W;
		EditText edtTxtPassword_W;
		EditText edtTxtData_W;
		Button btnWriteData;
		Spinner spnBank_W;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container,Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.WriteData_Fragment,container,false);
			return view;
		}
		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated (savedInstanceState);

			mContext = (MainActivity)Activity;

			edtTxtAddress_W = (EditText)View.FindViewById (Resource.Id.edtTxtAddress_W);
			edtTxtLeng_W = (EditText)View.FindViewById (Resource.Id.edtTxtLeng_W);
			edtTxtPassword_W= (EditText)View.FindViewById (Resource.Id.edtTxtPassword_W);
			edtTxtData_W = (EditText)View.FindViewById (Resource.Id.edtTxtData_W);
			btnWriteData = (Button)View.FindViewById (Resource.Id.btnWriteData);
			spnBank_W = (Spinner)View.FindViewById (Resource.Id.spnBank_W);
			 
  
			btnWriteData.Click += delegate {
				write() ;
			};
			soundPool = new SoundPool (10, Stream.Music, 0);
			soundPoolId = soundPool.Load(mContext,Resource.Drawable.beep, 1);
		}

			private void write() {
			try
			{
			string ptrStr = edtTxtAddress_W.Text.Trim ();
			if (ptrStr == string.Empty) {
				Toast.MakeText (mContext, "Please input the address!", ToastLength.Short).Show ();
				return;
			} else if (!Comm.IsNumber (ptrStr)) {
				Toast.MakeText (mContext, "Address must be a decimal data!", ToastLength.Short).Show ();
				return;
			}

			string cntStr = edtTxtLeng_W.Text.Trim ();
			if (cntStr == string.Empty) {
				Toast.MakeText (mContext, "Length cannot be empty", ToastLength.Short).Show ();
				return;
			} else if (!Comm.IsNumber (cntStr)) {
				Toast.MakeText (mContext, "Length must be a decimal data!", ToastLength.Short).Show ();
				return;
			}

			string pwdStr = edtTxtPassword_W.Text.Trim ();
			if (pwdStr != string.Empty) {
				if (pwdStr.Length != 8) {
					Toast.MakeText (mContext, "The length of the access password must be 8!", ToastLength.Short).Show ();
					return;
				} else if (!Comm.isHex (pwdStr)) {
					Toast.MakeText (mContext, "Please enter the hexadecimal number content!", ToastLength.Short).Show ();
					return;
				}
			} else {
				pwdStr = "00000000";
			}


			string strData = edtTxtData_W.Text.Trim ();// 要写入的内容

			if (strData == string.Empty) {
				Toast.MakeText (mContext, "Write data can not be empty!", ToastLength.Short).Show ();
				return;
			} else if (!Comm.isHex (strData)) {
				Toast.MakeText (mContext, "Please enter the hexadecimal number content!", ToastLength.Short).Show ();
				return;
			} else if ((strData.Length) % 4 != 0) {
				Toast.MakeText (mContext, "Write data of the length of the string must be in multiples of four!", ToastLength.Short).Show ();
				return;
			}  

			Com.Rscja.Deviceapi.RFIDWithUHF.BankEnum bank = Com.Rscja.Deviceapi.RFIDWithUHF.BankEnum.ValueOf (spnBank_W.SelectedItem.ToString ());

			string strReUII = mContext.uhfAPI.WriteData (pwdStr, bank,int.Parse(ptrStr) ,int.Parse (cntStr), strData);// 返回的UII
			if (strReUII != string.Empty) {
				Toast.MakeText (mContext, "Write data successfully!" + "\nUII: "
				+ strReUII, ToastLength.Short).Show ();
				Sound ();
			} else {
				Toast.MakeText (mContext, "Write data failure!", ToastLength.Short).Show ();
			}
			}catch{
			}
		}


		private void Sound()
		{
			//第一个参数为id
			//第二个和第三个参数为左右声道的音量控制
			//第四个参数为优先级，由于只有这一个声音，因此优先级在这里并不重要

			//第五个参数为是否循环播放，0为不循环，-1为循环
			//
			//最后一个参数为播放比率，从0.5到2，一般为1，表示正常播放。
			soundPool.Play(soundPoolId,1, 1, 0, 0, 1);


		}
	}
}

