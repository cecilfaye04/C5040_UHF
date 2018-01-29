
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
using Com.Rscja.Deviceapi.Entity;
namespace UHF
{
	[Activity (Label = "Read_Fragment")]			
	public class Read_Fragment : Fragment
	{
		SoundPool soundPool;
		int soundPoolId;
		MainActivity mContext;
		EditText edtTxtAddress_R;
		EditText edtTxtLeng_R;
		EditText edtTxtPassword_R;
		EditText edtTxtData_R;
		Button btnReadData;
		Spinner spnBank_R;
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container,Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.ReadData_Fragment,container,false);
			return view;
		}
		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated (savedInstanceState);

			mContext = (MainActivity)Activity;
		 
			edtTxtAddress_R=(EditText)View.FindViewById(Resource.Id.edtTxtAddress_R);
			edtTxtLeng_R=(EditText)View.FindViewById(Resource.Id.edtTxtLeng_R);
			edtTxtPassword_R=(EditText)View.FindViewById(Resource.Id.edtTxtPassword_R);
			edtTxtData_R=(EditText)View.FindViewById(Resource.Id.edtTxtData_R);
			btnReadData=(Button)View.FindViewById(Resource.Id.btnReadData);
			spnBank_R=(Spinner)View.FindViewById(Resource.Id.spnBank_R);
			btnReadData.Click += delegate {
				read();
			};
			soundPool = new SoundPool (10, Stream.Music, 0);
			soundPoolId = soundPool.Load(mContext,Resource.Drawable.beep, 1);

		}
		private void read() {
			try
			{
			string ptrStr = edtTxtAddress_R.Text.Trim ();
			if (ptrStr==string.Empty) 
			{
				Toast.MakeText (mContext,"Please input the address!",ToastLength.Short).Show();
				return;
			}
			else if (!Comm.IsNumber(ptrStr)) {
				Toast.MakeText (mContext,"Address must be a decimal data!",ToastLength.Short).Show();
				return;
			}

			string cntStr = edtTxtLeng_R.Text.Trim ();
			if (cntStr == string.Empty)
			{
				Toast.MakeText (mContext,"Length cannot be empty",ToastLength.Short).Show();
				return;
			} 
		    else if (!Comm.IsNumber(cntStr))
			{
			    Toast.MakeText (mContext,"Length must be a decimal data!",ToastLength.Short).Show();
				return;
			}

			string pwdStr = edtTxtPassword_R.Text.Trim ();
			if (pwdStr != string.Empty)
			{
				if (pwdStr.Length!= 8) {
					Toast.MakeText (mContext,"The length of the access password must be 8!",ToastLength.Short).Show();
					return;
				} 
				else if (!Comm.isHex(pwdStr)) 
				{
					Toast.MakeText (mContext,"Please enter the hexadecimal number content!",ToastLength.Short).Show();
					return;
				}
			}
			else
			{
				pwdStr = "00000000";
			}
			//	getSelectedItem
			Com.Rscja.Deviceapi.RFIDWithUHF.BankEnum bank=Com.Rscja.Deviceapi.RFIDWithUHF.BankEnum.ValueOf (spnBank_R.SelectedItem.ToString ());
 
		 	SimpleRFIDEntity  entity = mContext.uhfAPI.ReadData(pwdStr,
				bank, int.Parse(ptrStr), Int32.Parse(cntStr));

				if (entity != null) 
			    {
				    string uid = "UII:"+entity.Id+"\r\n";
			    	string data ="data:"+entity.Data;
				    edtTxtData_R.Text = uid + data;
			    	Sound ();
				}
			    else
			    {
				    edtTxtData_R.Text = "";
				Toast.MakeText (mContext,"Read failure!",ToastLength.Short).Show();
				}

			}
			catch {
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

