using QuanLyRung.Model;
using QuanLyRung.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyRung.ViewModel
{
    public class TreeViewModel : BaseViewModel
    {
        private ObservableCollection<Tree> _List;
        public ObservableCollection<Tree> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<SupplierTree> _SupplierTree;
        public ObservableCollection<SupplierTree> SupplierTree { get => _SupplierTree; set { _SupplierTree = value; OnPropertyChanged(); } }



        private ObservableCollection<Tree> _FilteredTreeList;
        public ObservableCollection<Tree> FilteredTreeList
        {
            get => _FilteredTreeList;
            set { _FilteredTreeList = value; OnPropertyChanged(); }
        }



        private Tree _SelectedItem;
        public Tree SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {

                    DisplayName = SelectedItem.DisplayName;
                    SelectedSupplierTree = SelectedItem.SupplierTree;
                    Count = SelectedItem.Count;



                }
            }
        }
        private SupplierTree _SelectedSupplierTree;
        public SupplierTree SelectedSupplierTree
        {
            get => _SelectedSupplierTree;
            set
            {
                _SelectedSupplierTree = value;
                OnPropertyChanged();

            }
        }




        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }
        private int? _Count;
        public int? Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }




        private string _SearchTerm;
        public string SearchTerm
        {
            get => _SearchTerm;
            set
            {
                _SearchTerm = value;
                OnPropertyChanged();
                FilterTree();


            }
        }


        private bool _showSearchResults;
        public bool ShowSearchResults
        {
            get => _showSearchResults;
            set
            {
                _showSearchResults = value;
                OnPropertyChanged();
            }
        }
   




        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }





        public TreeViewModel()
        {
            List = new ObservableCollection<Tree>(DataProvider.Ins.DB.Tree);
            SupplierTree = DataService.Instance.SupplierTreeList;

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSupplierTree == null)
                    return false;

                return true;

            }, (p) =>
            {
                {
                    var tree = new Tree() { DisplayName = DisplayName, IdSupplierTree = SelectedSupplierTree.Id, Count = Count };

                    DataProvider.Ins.DB.Tree.Add(tree);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Add(tree);
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null;
            }, (p) =>
            {
                var treeToDelete = DataProvider.Ins.DB.Tree.FirstOrDefault(u => u.Id == SelectedItem.Id);
                if (treeToDelete != null)
                {
                    DataProvider.Ins.DB.Tree.Remove(treeToDelete);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Remove(SelectedItem);
                    List = new ObservableCollection<Tree>(DataProvider.Ins.DB.Tree);
                    SupplierTree = DataService.Instance.SupplierTreeList;
                }
            });



            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedSupplierTree == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Tree.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var tree = DataProvider.Ins.DB.Tree.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                tree.DisplayName = DisplayName;
                tree.IdSupplierTree = SelectedSupplierTree.Id;
                tree.Count = Count;


                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
                List = new ObservableCollection<Tree>(DataProvider.Ins.DB.Tree);
                SupplierTree = DataService.Instance.SupplierTreeList;

            });

            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedSupplierTree == null)
                {
                    ShowSearchResults = false;
                    OnPropertyChanged(nameof(ShowSearchResults));
                    return;
                }
                FilterTree();
            });
        }

        private void FilterTree()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                FilteredTreeList = new ObservableCollection<Tree>(List.Where(t => t.IdSupplierTree == SelectedSupplierTree.Id));
                ShowSearchResults = true;
            }
            else
            {
                FilteredTreeList = new ObservableCollection<Tree>(
                    List.Where(t => t.DisplayName.Contains(SearchTerm))
                );
            }
            OnPropertyChanged(nameof(ShowSearchResults));
            OnPropertyChanged(nameof(FilteredTreeList));
        }
    }
}
