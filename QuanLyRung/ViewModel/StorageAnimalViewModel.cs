using QuanLyRung.Model;
using QuanLyRung.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyRung.ViewModel
{

    public class StorageAnimalViewModel : BaseViewModel
    {
        private ObservableCollection<StorageAnimal> _List;
        public ObservableCollection<StorageAnimal> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private StorageAnimal _SelectedItem;
        public StorageAnimal SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {



                    DisplayName = SelectedItem.DisplayName;
                    Adress = SelectedItem.Adress;




                }
            }
        }




        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Adress;
        public string Adress { get => _Adress; set { _Adress = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }





        public StorageAnimalViewModel()
        {
            List = DataServicess.Instancess.StorageAnimalList;

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (DisplayName == null)
                    return false;

                return true;

            }, (p) => {
                {
                    var storageAnimal = new StorageAnimal() { DisplayName = DisplayName, Adress = Adress };

                    DataProvider.Ins.DB.StorageAnimal.Add(storageAnimal);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Add(storageAnimal);
                }
            });



            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.StorageAnimal.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var storageAnimal = DataProvider.Ins.DB.StorageAnimal.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                storageAnimal.DisplayName = DisplayName;
                storageAnimal.Adress = Adress;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                List = DataServicess.Instancess.StorageAnimalList;

            });



            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null;
            }, (p) =>
            {
                var storageAnimal = DataProvider.Ins.DB.StorageAnimal.Where(x => x.Id == SelectedItem.Id)
                    .SingleOrDefault();

                if (storageAnimal != null)
                {

                    var relatedItems = DataProvider.Ins.DB.Animal.Where(x => x.IdStorageAnimal == SelectedItem.Id).ToList();

                    DataProvider.Ins.DB.Animal.RemoveRange(relatedItems);
                    DataProvider.Ins.DB.StorageAnimal.Remove(storageAnimal);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Remove(SelectedItem);
                    DataServicess.Instancess.StorageAnimalList.Remove(SelectedItem);
                }
            });
        }
    }
}
