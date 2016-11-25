using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IPlayer.Contracts
{
    public sealed partial class SettingsFlyLayout : UserControl
    {
        public  StorageFile StorageFile;
        public SettingsFlyLayout()
        {
            

            this.InitializeComponent();
            this.Loaded+=SettingsFlyLayout_Loaded;
        }

        async void SettingsFlyLayout_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                if (StorageFile != null)
                {
                    info.Text =  StorageFile.DisplayName;
                    var bt = new BitmapImage
                    {
                        UriSource = new Uri("ms-appdata:///roaming/" + StorageFile.DisplayName + ".png"),
                    };
                    Image.Source = bt;
                    info.Text += "\n" + StorageFile.DateCreated.ToString();
                  
                    var formats = new List<string>
                    {
                        ".mp3",
                        ".aac",
                        ".adt",
                        ".adts",
                        ".wav",
                        ".ac3",
                        ".ec3"
                    };
                    var ff = from x in formats where x.Equals(StorageFile.FileType.ToLower()) select x;
                    int count = ff.Count();
                    if (count >= 1)
                    {
                        var properties = await StorageFile.Properties.GetMusicPropertiesAsync();
                        info.Text += "\n" + properties.Duration.ToString(@"hh\:mm\:ss");
                        info.Text += properties.Album == "" ? "\nUnknown Album" : "\n " + properties.Album;
                        var p = await StorageFile.GetBasicPropertiesAsync();
                        info.Text += "\n " + (p.Size/(1024*1024)).ToString() + " MB";
                    }
                    else
                    {
                        var proper = await StorageFile.Properties.GetVideoPropertiesAsync();
                        info.Text += "\n" + proper.Duration.ToString(@"hh\:mm\:ss");
                        var p = await StorageFile.GetBasicPropertiesAsync();
                        info.Text += "\n" + (p.Size/(1024*1024)).ToString() + " MB";
                    }
                    info.Text += "\n" + StorageFile.Path;
                }
            }
            catch
            {
            }
          
          
        }
       
        //private void Huering_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    //var cor = ColorExtensions.FromHsl(huering.Value,1,0.5);
        //    //huering.Background = cor;
        //}
    }
}
