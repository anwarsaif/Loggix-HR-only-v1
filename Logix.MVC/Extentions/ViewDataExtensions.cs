using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Logix.Application.Common;

namespace Logix.MVC.Extentions
{
    public static class ViewDataExtensions
    {
        private static readonly string noConflict = "neverPossibleConflict";
        public static SelectList GetDDL<TValue>(this ViewDataDictionary viewData, string listName, bool hasDefault = true, string defaultText = "إختر", TValue defaultValue = default, TValue selectedValue = default)
        {
            if (viewData.ContainsKey(listName + noConflict))
            {
                var first = new DDLItem<TValue> { Value = defaultValue, Name = defaultText };
                var selectList = new List<DDLItem<TValue>>();
                if (hasDefault)
                {
                    selectList.Add(first);
                }

                try
                {
                    var list = viewData[listName + noConflict] as List<DDLItem<TValue>>;
                    if (list != null && list.Any())
                    {
                        selectList.AddRange(list);
                    }
                    else
                    {
                        if (hasDefault)
                            selectList[0].Name = "لا توجد بيانات";
                    }
                }
                catch (Exception)
                {
                    if (hasDefault)
                        selectList[0].Name = "خطأ! لا توجد بيانات";
                }

                return new SelectList(selectList, "Value", "Name", selectedValue);
            }
            else
            {
                throw new ArgumentException($"DDL ({listName}) not found, be sure to add it in the correct action.");
            }
        }

        public static bool AddDDL(this ViewDataDictionary viewData, SelectList selectList, string listName)
        {
            try
            {
                viewData[listName + noConflict] = selectList;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool AddDDL<TValue>(this ViewDataDictionary viewData, List<DDLItem<TValue>> list, string listName)
        {
            try
            {
                viewData[listName + noConflict] = list;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
