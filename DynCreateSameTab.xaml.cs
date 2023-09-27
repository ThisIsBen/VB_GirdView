using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Inline_Runtime_System
{
    /// <summary>
    /// Tab.xaml の相互作用ロジック
    /// </summary>

    //INotifyPropertyChangedの継承が必要
    public partial class Tab : Window
    {
        


        //各Tabが有する各Itemの値を格納するList
        private ObservableCollection<CameraItems_Runtime> _TabCameraItems_Runtime = new ObservableCollection<CameraItems_Runtime>();
        private ObservableCollection<CameraItems_Runtime> TabCameraItems_Runtime
        {
            get { return _TabCameraItems_Runtime; }
            set { _TabCameraItems_Runtime = value; }
        }


        //xamlのTabControl内の１つのTabが有する各Itemの値を定義
        public class CameraItems_Runtime: INotifyPropertyChanged
        {
            //Tabが有する各Itemの値が変わるときに、cs側の値も更新するためのイベント
            public event PropertyChangedEventHandler PropertyChanged;

            private void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
            {
                field = value;
                PropertyChangedEventHandler h = PropertyChanged;
                if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
            }

            private int _ID;
            public int ID
            { get { return _ID; }
                set { SetProperty(ref _ID, value); }
            }

            private string _FirstName;
            public string FirstName
            {
                get { return _FirstName; }
                set { SetProperty(ref _FirstName, value); }
            }

            private string _LastName;
            public string LastName
            {
                get { return _LastName; }
                set { SetProperty(ref _LastName, value); }
            }
        }



        public Tab()
        {



            InitializeComponent();

           
            //Tabを１つ追加
            TabCameraItems_Runtime.Add(new CameraItems_Runtime()
            {
                ID = 11,
                FirstName = "Kapil",
                LastName = "Malhotra"
            });

            //Tabを１つ追加
            TabCameraItems_Runtime.Add(new CameraItems_Runtime()
            {
                ID = 22,
                FirstName = "Joe",
                LastName = "Yarbrough"
            });


            //各Tabが有する各Itemの値をUIに適用
            TabControl1.ItemsSource = TabCameraItems_Runtime;
        }

        

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string kd= TabCameraItems_Runtime[0].FirstName;
            
            TabCameraItems_Runtime[0].FirstName = "lamblast";
            
            
        }
    }
}
