using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;
using Android.Support.V4.Content.Res;
using Android.Content.PM;

namespace Test
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        private readonly int buttons = 20;
        private readonly string[] letters = new string[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        private Button selected = null;
        private Button selected2 = null;
        private string label1 = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //getting all the buttons in the view
            List<Button> button_list = new List<Button>();
            int index;
            Button btn = null;
            for (index = 1; index < buttons + 1; index++)
            {
                int id = Resources.GetIdentifier("button" + index.ToString(), "id", this.PackageName);
                btn = (Button)FindViewById(id);
                button_list.Add(btn);
            }
            List<int> ramdom_numbers = new List<int>();
            int[] stream = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            Random random = new Random();
            int[] my_array = stream.OrderBy(x => random.Next()).ToArray();
            Button _button;
            int letters_index = 0;
            int random_index = 0;
            for(int i = 0; i<button_list.Count; i+=2)
            {
                _button = button_list[my_array[random_index++]-1];
                _button.Text = letters[letters_index];
                _button.Click += ButtonOnClick;
                _button = null;
                _button = button_list[my_array[random_index++]-1];
                _button.Text = letters[letters_index];
                _button.Click += ButtonOnClick;
                letters_index++;
            }

        }
        public void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            Button btn = (Button)sender;
            if(label1 == "")
            {
                label1 = btn.Text;
                selected = btn;
                btn.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Gold);
                btn.Enabled = false;
                btn.TextSize = 16;
            }
            else
            {
                if(label1 == btn.Text)
                {
                    btn.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Green);
                    btn.TextSize = 16;
                    btn.Enabled = false;
                    selected.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Green);
                    label1 = "";
                    selected = null;
                }
                else
                {
                    if (selected2 == null)
                    {
                        btn.TextSize = 16;
                        btn.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.IndianRed);
                        selected.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.IndianRed);
                        selected2 = btn;
                        btn.Enabled = false;
                        selected.Enabled = false;
                    }
                    else
                    {
                        selected.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Gray);
                        selected2.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Gray);
                        selected.Enabled = true;
                        selected2.Enabled = true;
                        selected.TextSize = 0;
                        selected2.TextSize = 0;
                        selected = null;
                        selected2 = null;
                        label1 = btn.Text;
                        selected = btn;
                        btn.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Gold);
                        btn.Enabled = false;
                        btn.TextSize = 16;
                    }
                }
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

