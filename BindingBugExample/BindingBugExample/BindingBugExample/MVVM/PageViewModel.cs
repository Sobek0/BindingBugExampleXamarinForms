using System.Collections.Generic;
using BindingBugExample.ApiViewModels;
using BindingBugExample.ViewModels;

namespace BindingBugExample.MVVM
{
    public class PageViewModel : BaseViewModel
    {
        public PageViewModel()
        {
            CategoriesList = new Dictionary<int, Dictionary<string, UniversalSwitchViewModel>>();
        }

        public void AddCategoriesOption(CategoriesListApiViewModel categories, int regionId)
        {
            CategoriesList.TryGetValue(regionId, out Dictionary<string, UniversalSwitchViewModel> dictionaryForRegion);

            bool IsExist = dictionaryForRegion != null;

            if (!IsExist)
            {
                dictionaryForRegion = new Dictionary<string, UniversalSwitchViewModel>();
            }

            foreach (var category in categories.Category)
            {
                dictionaryForRegion.Add(category.Value, new UniversalSwitchViewModel { Id = category.Key, Value = false });
            }

            if (IsExist) CategoriesList[regionId] = dictionaryForRegion;
            else CategoriesList.Add(regionId, dictionaryForRegion);
        }
        
        public void SetCategoriesOption(List<string> categories, int regionId)
        {
            foreach (var item in categories)
            {
                CategoriesList[regionId][item].Value = true;
            }

            CategoriesList = CategoriesList;
            //this.OnPropertyChanged(nameof(CategoriesList));
        }

        #region Categories

        private Dictionary<int, Dictionary<string, UniversalSwitchViewModel>> _categoriesList;

        public Dictionary<int, Dictionary<string, UniversalSwitchViewModel>> CategoriesList
        {
            get
            {
                return _categoriesList;
            }
            set
            {
                SetProperty(ref _categoriesList, value);
            }
        }

        #endregion
    }
}
