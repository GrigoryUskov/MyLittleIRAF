using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;

namespace MyLittleIRAF
{
    public class TableDelegate : NSTableViewDelegate
    {
        private const string CellIdentifier = "SpectraListTable";

        private TableDataSource DataSource;

        public NSTextField view;

        public TableDelegate(TableDataSource dataSource)
        {
            this.DataSource = dataSource;
        }

        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            view = (NSTextField)tableView.MakeView(CellIdentifier, this);

            if(view == null)
            {
                view = new NSTextField();
                view.Identifier = CellIdentifier;
                view.BackgroundColor = NSColor.Clear;
                view.Bordered = false;
                view.Selectable = false;
                view.Editable = false;
            }

            view.StringValue = DataSource.ListOfSpectra[(int)row].Name;
           



            return view;
        }
        

        public override bool ShouldSelectRow(NSTableView tableView, nint row)
        {
            return true;
        }





    }
}
