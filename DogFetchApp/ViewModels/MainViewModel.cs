using ApiHelper;
using ApiHelper.Models;
using DogFetchApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private string selectedDog = string.Empty;
        public string SelectedDog
        {
            get => selectedDog;
            set
            {
                selectedDog = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> imageList = new ObservableCollection<string>();
        public ObservableCollection<string> ImageList
        {
            get
            {
                return imageList;
            }
            set
            {
                imageList = value;
                OnPropertyChanged();
            }
        }
        private string selectedBreed;
        public string SelectedBreed
        {
            get
            {
                return selectedBreed;
            }
            set
            {
                selectedBreed = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> breedList;
        public ObservableCollection<string> BreedList {
            get {
                return breedList;
            }
            set {
                breedList = value;
                OnPropertyChanged();
            }
        }

        private int selectedNumber;
        public int SelectedNumber
        {
            get
            {
                return selectedNumber;
            }
            set
            {
                selectedNumber = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand<string> FetchCommand { get; set; }
        public DelegateCommand<string> SetEnglishCommand { get; set; }
        public DelegateCommand<string> SetFrenchCommand { get; set; }
        public DelegateCommand<string> NextCommand { get; set; }

        private ObservableCollection<int> numberList = new ObservableCollection<int>()
        {
            1,3,5,10
        };
        public ObservableCollection<int> NumberList
        {
            get
            {
                return numberList;
            }
            set
            {
                numberList = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            FetchCommand = new DelegateCommand<string>(ClickOnFetch);
            NextCommand = new DelegateCommand<string>(ClickOnFetch);
            SetEnglishCommand = new DelegateCommand<string>(SetEnglish);
            SetFrenchCommand = new DelegateCommand<string>(SetFrench);
            init();
        }

        public async void init()
        {
            Task<List<string>> result = DogApiProcessor.LoadBreedList();
            await result;

            List<string> l = result.Result;

            BreedList = new ObservableCollection<string>(l);
            SelectedBreed = BreedList[0];
            SelectedNumber = NumberList[0];
        }
        private void SetEnglish(string nothing)
        {
            DogFetchApp.Properties.Settings.Default.Language = "";
            Properties.Settings.Default.Save();
            MessageBoxResult res = MessageBox.Show(Properties.Resources.Reboot, "Reboot App", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Application.Current.Shutdown();
            }
        }
        private void SetFrench(string nothing)
        {
            DogFetchApp.Properties.Settings.Default.Language = "fr";
            Properties.Settings.Default.Save();
            MessageBoxResult res = MessageBox.Show(Properties.Resources.Reboot, "Reboot App", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Application.Current.Shutdown();
            }
        }
        public async void ClickOnFetch(string nothing)
        {
            IList<string> all = await DogApiProcessor.GetImageUrl(SelectedBreed);
            Random random = new Random();
            ImageList.Clear();
            int k = 0;
            if(all.Count > SelectedNumber)
            {
                while (k < SelectedNumber)
                {
                    int idx = random.Next(0, all.Count - 1);
                    if (!imageList.Contains(all[idx]))
                    {
                        ImageList.Add(all[idx]);
                        k++;
                    }
                }
            }
            else
            {
                foreach(string dog in all)
                {
                    ImageList.Add(dog);
                }
            }
            
        }
    }
}
