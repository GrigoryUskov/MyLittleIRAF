using System;
using System.IO;
using AppKit;
using Foundation;
using Interp;
using System.Collections;
using System.Collections.Generic;

namespace MyLittleIRAF
{
    public partial class ViewController : NSViewController
    {

        private List<Spectrum> ListOfSpectra = new List<Spectrum>();
        private TableDataSource DataSource;
        private int i = 0;

        private List<int> indexies = new List<int>();


        public string RbId
        {
            get;
            private set;
        }
        public string Vel1
        {
            get;
            private set;
        }
        public string Vel2
        {
            get;
            private set;
        }

        partial void AddButton(NSObject sender)
        {

            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowedFileTypes = new string[] { "dat", "txt" };
            if (dlg.RunModal() == 1)
            {


                var url = dlg.Urls[0];

              //  MainClass.listOfSpectra.Add(new Spectrum(url.Path));
                ListOfSpectra.Add(new Spectrum(url.Path));
                DataSource = new TableDataSource(ListOfSpectra);

                SpectraListTable.DataSource = DataSource;
                SpectraListTable.Delegate = new TableDelegate(DataSource);
            }


        }

        partial void DelButton(NSObject sender)
        {
            int k = (int)SpectraListTable.SelectedRow;
            if (SpectraListTable.IsRowSelected(SpectraListTable.SelectedRow))
            {
                ListOfSpectra.RemoveAt(k);
                SpectraListTable.RemoveRows(new NSIndexSet(k), NSTableViewAnimation.SlideLeft);
                if (indexies.Count > 0)
                {
                    for (int i = 0; i < indexies.Count; i++)
                    {
                        if (indexies[i] == k)
                        {
                            indexies.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < indexies.Count; i++)
                    {
                        if(indexies[i] > k)
                        {
                            indexies[i] -= 1;
                        }
                    }

                }


                return;
            }

        }

        partial void SaveButton(NSObject sender)
        {
            if (SpectraListTable.IsRowSelected(SpectraListTable.SelectedRow))
            {
                int k = (int)SpectraListTable.SelectedRow;
                var dlg = new NSSavePanel
                {
                    Title = "Save Spectrum",
                    AllowedFileTypes = new string[] { "dat", "txt" },
                    ShowsTagField = false,
                    CanCreateDirectories = true,
                    NameFieldStringValue = ListOfSpectra[k].Name + ".dat",
                    AccessibilityExpanded = true

                };

                if (dlg.RunModal() == 1)
                {

                    string path = dlg.Url.Path;
                    Console.WriteLine(path);


                    double[][] tmpArray = new double[ListOfSpectra[k].XX.Length][];

                    for (int i = 0; i < tmpArray.GetLength(0); i++)
                    {
                        tmpArray[i] = new double[2];
                        tmpArray[i][0] = ListOfSpectra[k].XX[i];
                        tmpArray[i][1] = ListOfSpectra[k].YY[i];
                    }

                    TableReader.SaveTable(path, tmpArray);

                }

            }
        }

        // It works with Segue Instance.
        public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            switch (segue.Identifier)
            {
                case "RenameSheetSegue":
                    var sheet = segue.DestinationController as SheetViewController;
                    int k;

                    if (SpectraListTable.IsRowSelected(SpectraListTable.SelectedRow))
                    {
                        k = (int)SpectraListTable.SelectedRow;
                        sheet.NewName = ListOfSpectra[k].Name;
                    }

                    sheet.SheetAccepted += (s, e) =>
                    {
                        if (SpectraListTable.IsRowSelected(SpectraListTable.SelectedRow))
                        {
                            k = (int)SpectraListTable.SelectedRow;
                            ListOfSpectra[k].Name = sheet.NewName;

                            DataSource = new TableDataSource(ListOfSpectra);

                            for (int i = 0; i <= ListOfSpectra.Count; i++)
                            {
                                SpectraListTable.DataSource = DataSource;
                                SpectraListTable.Delegate = new TableDelegate(DataSource);
                            }
                        }

                    };
                    sheet.Presentor = this;
                    break;
                case "TrimSegue":
                    var trimSheet = segue.DestinationController as TrimDialogController;
                    int l = (int)SpectraListTable.SelectedRow;

                    trimSheet.SheetAccepted += (s, e) =>
                    {
                        if (SpectraListTable.IsRowSelected(SpectraListTable.SelectedRow))
                        {
                            double l1 = Convert.ToDouble(trimSheet.Lamda1);
                            double l2 = Convert.ToDouble(trimSheet.Lamda2);
                            ListOfSpectra.Add(ListOfSpectra[l].Trimmed(l1, l2));

                            ReloadTable();

                        }

                    };

                    trimSheet.Presentor = this;
                    break;

                case "MathSegue":
                    var mathSheet = segue.DestinationController as CustomDialogController;
                    string[] mas = new string[ListOfSpectra.Count];
                    for (int i = 0; i < ListOfSpectra.Count; i++)
                    {
                        mas[i] = ListOfSpectra[i].Name;
                    }

                    // Fill List
                    mathSheet.BoxSpectraList = new List<string>(mas);
                    mathSheet.SheetAccepted += (s, e) =>
                     {
                         Spectrum sp1 = ListOfSpectra[mathSheet.Index1];
                         Spectrum sp2 = ListOfSpectra[mathSheet.Index2];
                         switch (mathSheet.RadioId)
                         {
                             case "PlusId":
     
                                 ListOfSpectra.Add(sp1 + sp2);
                                 break;
                             case "MinusId":
                                 ListOfSpectra.Add(sp1 - sp2);
                                 break;
                             case "MultiplyId":
                                 ListOfSpectra.Add(sp1 * sp2);
                                 break;
                             case "DivisionId":
                                 ListOfSpectra.Add(sp1 / sp2);
                                 break;
                         }
                         ReloadTable();
                     };
                    mathSheet.Presentor = this;
                    SpectraListTable.ReloadData();
                    break;
                case "PlotSegue":
                    var dlg = segue.DestinationController as PlotWindowController;
                   // Console.WriteLine(indexies.Count.ToString() + "======================");
                    if (SpectraListTable.IsRowSelected(SpectraListTable.SelectedRow))
                    {
                        k = (int)SpectraListTable.SelectedRow;
                        if (indexies.Count >0)
                        {
                            int c = 0;
                            for (int i = 0; i < indexies.Count; i++)
                            {
                                if (indexies[i] == k) c++;
                            }
                            if (c == 0)
                            {
                                indexies.Add(k);
                            }
                        }
                        else indexies.Add(k);




                        for (int j = 0; j < indexies.Count; j++)
                        {
                            Console.WriteLine("==="+indexies[j].ToString()+"===");
                            dlg.ListOfSpectra.Add(ListOfSpectra[indexies[j]]);
                        }
                        //dlg.ListOfSpectra.Add(ListOfSpectra[k]);
                    }

                        dlg.DialogAccepted += (s, e) =>
                    {
                        DismissController(dlg);
                    };
                    dlg.DialogCanceled += (s, e) =>
                    {
                        indexies.Clear();
                        DismissController(dlg);
                    };
                    dlg.Presentor = this;
                    break;

            }
        }

        partial void LoadModelButton1(NSObject sender)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowedFileTypes = new string[] { "dat", "txt" };

            if(dlg.RunModal() == 1)
            {
                string path = dlg.Url.Path;
                TextFieldModel1.StringValue = path;
            }
        }

        partial void LoadModelButton2(NSObject sender)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowedFileTypes = new string[] { "dat", "txt" };

            if (dlg.RunModal() == 1)
            {
                string path = dlg.Url.Path;

                TextFieldModel2.StringValue = path;
            }
        }

        partial void AddModelButton(NSObject sender)
        {
            switch (RbId)
            {
                case "RadioOneId":
                    if(TextFieldModel1.StringValue != "")
                    {
                        try {
                             SpectrumMod spectrumMod = new SpectrumMod(TextFieldModel1.StringValue);
                             Vel1 = TextFieldVelocity1.StringValue;

                             double dVel1 = Convert.ToDouble(Vel1);
                             spectrumMod = spectrumMod.Shift(dVel1);
                           
                             ListOfSpectra.Add(spectrumMod.Spec);
                             ListOfSpectra[ListOfSpectra.Count-1].Name = "Model_" + i.ToString();
                             i++;
                             ReloadTable();

                        }
                        catch (FormatException) {
                            var alert = new NSAlert()
                            {
                                AlertStyle = NSAlertStyle.Critical,
                                InformativeText = "Check velocity field for correct format...",
                                MessageText = "Wrong value in the velocity field",
                            };
                            alert.RunModal();
                        }

                    }
                    else
                    {
                        var alert = new NSAlert()
                        {
                            AlertStyle = NSAlertStyle.Critical,
                            InformativeText = "At first load spectrum.",
                            MessageText = "Model spectrum is not selected!",
                        };
                        alert.RunModal();
                    }
                    break;
                case "RadioTwoId":
                    if (TextFieldModel1.StringValue != "" & TextFieldModel2.StringValue != "")
                    {
                        try
                        {
                            SpectrumMod spectrumMod1 = new SpectrumMod(TextFieldModel1.StringValue);
                            SpectrumMod spectrumMod2 = new SpectrumMod(TextFieldModel2.StringValue);

                            Vel1 = TextFieldVelocity1.StringValue;
                            Vel2 = TextFieldVelocity2.StringValue;

                            double dVel1 = Convert.ToDouble(Vel1);
                            double dVel2 = Convert.ToDouble(Vel2);

                            spectrumMod1 = spectrumMod1.Shift(dVel1);
                            spectrumMod2 = spectrumMod2.Shift(dVel2);

                            spectrumMod1 += spectrumMod2;

                            ListOfSpectra.Add(spectrumMod1);
                            ListOfSpectra[ListOfSpectra.Count - 1].Name = "Double_Model_"+i.ToString();
                            i++;
                            ReloadTable();

                        }
                        catch (FormatException)
                        {
                            var alert = new NSAlert()
                            {
                                AlertStyle = NSAlertStyle.Critical,
                                InformativeText = "Check velocity field for correct format...",
                                MessageText = "Wrong value in the velocity field",
                            };
                            alert.RunModal();
                        }
                    }
                    else
                    {
                        var alert = new NSAlert()
                        {
                            AlertStyle = NSAlertStyle.Critical,
                            InformativeText = "At first load spectrum.",
                            MessageText = "Model spectrum is not selected!",
                        };
                        alert.RunModal();
                    }
                    break;
            }


        }



        public void ReloadTable()
        {
            DataSource = new TableDataSource(ListOfSpectra);
            for (int i = 0; i <= ListOfSpectra.Count; i++)
            {
                SpectraListTable.DataSource = DataSource;
                SpectraListTable.Delegate = new TableDelegate(DataSource);
            }
        }

        partial void RadioButton(NSButton sender)
        {
            RbId = sender.Identifier;

        }

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            RbId = "RadioOneId";
            Vel1 = "0";
            Vel2 = "0";
            TextFieldVelocity1.StringValue = Vel1;
            TextFieldVelocity2.StringValue = Vel2;


            // Do any additional setup after loading the view.
        }



        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
