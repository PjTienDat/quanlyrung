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

    public class SupplierWoodViewModel : BaseViewModel
    {
        private ObservableCollection<SupplierWood> _List;
        public ObservableCollection<SupplierWood> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private SupplierWood _SelectedItem;
        public SupplierWood SelectedItem
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
                    Typess = SelectedItem.Typess;
                    Form = SelectedItem.Form;




                }
            }
        }




        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Adress;
        public string Adress { get => _Adress; set { _Adress = value; OnPropertyChanged(); } }
        private string _Typess;
        public string Typess { get => _Typess; set { _Typess = value; OnPropertyChanged(); } }
        private string _Form;
        public string Form { get => _Form; set { _Form = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }





        public SupplierWoodViewModel()
        {
            List = DataServices.Instances.SupplierWoodList;

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (DisplayName == null)
                    return false;

                return true;

            }, (p) => {
                {
                    var supplierWood = new SupplierWood() { DisplayName = DisplayName, Adress = Adress, Typess = Typess, Form = Form };

                    DataProvider.Ins.DB.SupplierWood.Add(supplierWood);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Add(supplierWood);
                }
            });



            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.SupplierWood.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var supplierWood = DataProvider.Ins.DB.SupplierWood.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                supplierWood.DisplayName = DisplayName;
                supplierWood.Adress = Adress;
                supplierWood.Typess = Typess;
                supplierWood.Form = Form;

                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                List = DataServices.Instances.SupplierWoodList;

            });



            DeleteCommand = new RelayCommand<object>((p) => {
                return SelectedItem != null;
            }, (p) => {
                var supplierWood = DataProvider.Ins.DB.SupplierWood.Where(x => x.Id == SelectedItem.Id)
                    .SingleOrDefault();

                if (supplierWood!= null)
                {

                    var relatedItems = DataProvider.Ins.DB.Wood.Where(x => x.IdSupplierWood == SelectedItem.Id).ToList();

                    DataProvider.Ins.DB.Wood.RemoveRange(relatedItems);
                    DataProvider.Ins.DB.SupplierWood.Remove(supplierWood);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Remove(SelectedItem);
                    DataServices.Instances.SupplierWoodList.Remove(SelectedItem);
                }
            });
        }
    }
}