using System;
using System.Collections;
using System.Collections.Generic;
using AppKit;


namespace MyLittleIRAF
{
    public class TableDataSource : NSTableViewDataSource
    {

        public List<Spectrum> ListOfSpectra;

        public TableDataSource(List<Spectrum> ListOfSpectra) {
            this.ListOfSpectra = ListOfSpectra;
        }


        public override nint GetRowCount(NSTableView tableView)
        {
            return ListOfSpectra.Count;
            //return ListOfSpectra.Count;
        }





    }
}
