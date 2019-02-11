using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace MyLittleIRAF
{
    public class ComboBoxDataSource : NSComboBoxDataSource
    {
        private List<string> arrayList = new List<string>();

        public ComboBoxDataSource(List<string> arrayList)
        {
            this.arrayList = arrayList;

        }

        public override NSObject ObjectValueForItem(NSComboBox comboBox, nint index)
        {
            return NSObject.FromObject(arrayList[(int)index]);
        }

        public override string CompletedString(NSComboBox comboBox, string uncompletedString)
        {
            return arrayList.Find(n => n.StartsWith(uncompletedString, StringComparison.InvariantCultureIgnoreCase));
        }

        public override nint IndexOfItem(NSComboBox comboBox, string value)
        {
            return arrayList.FindIndex(n => n.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        public override nint ItemCount(NSComboBox comboBox)
        {
            return arrayList.Count;
        }


    }
}
