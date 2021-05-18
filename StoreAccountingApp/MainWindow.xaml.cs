using StoreAccountingApp.DBModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StoreAccountingApp.ViewModels;

namespace StoreAccountingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccountTypeViewModel ViewModel;
        public MainWindow()
        {
            InitializeComponent();
            InitializeDB();
            ViewModel = new AccountTypeViewModel();
            this.DataContext = ViewModel;
        }
        private void InitializeDB()
        {
            Debug.WriteLine("Starting DB...");
            _DBStoreAccountingContext ctx = new _DBStoreAccountingContext();
            ctx.LoadDemoData();
            Debug.WriteLine("Db successfully initialized");
        }

    }
}
