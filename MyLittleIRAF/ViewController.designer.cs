// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MyLittleIRAF
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTableColumn SpectraListColumn { get; set; }

		[Outlet]
		AppKit.NSTableView SpectraListTable { get; set; }

		[Outlet]
		AppKit.NSTextField TextFieldModel1 { get; set; }

		[Outlet]
		AppKit.NSTextField TextFieldModel2 { get; set; }

		[Outlet]
		AppKit.NSTextField TextFieldVelocity1 { get; set; }

		[Outlet]
		AppKit.NSTextField TextFieldVelocity2 { get; set; }

		[Action ("AddButton:")]
		partial void AddButton (Foundation.NSObject sender);

		[Action ("AddModelButton:")]
		partial void AddModelButton (Foundation.NSObject sender);

		[Action ("ContMakeButton:")]
		partial void ContMakeButton (Foundation.NSObject sender);

		[Action ("DelButton:")]
		partial void DelButton (Foundation.NSObject sender);

		[Action ("LoadModelButton1:")]
		partial void LoadModelButton1 (Foundation.NSObject sender);

		[Action ("LoadModelButton2:")]
		partial void LoadModelButton2 (Foundation.NSObject sender);

		[Action ("MathButton:")]
		partial void MathButton (Foundation.NSObject sender);

		[Action ("RadioButton:")]
		partial void RadioButton (AppKit.NSButton sender);

		[Action ("RenameButton:")]
		partial void RenameButton (Foundation.NSObject sender);

		[Action ("SaveButton:")]
		partial void SaveButton (Foundation.NSObject sender);

		[Action ("ShowButton:")]
		partial void ShowButton (Foundation.NSObject sender);

		[Action ("TrimButton:")]
		partial void TrimButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (SpectraListColumn != null) {
				SpectraListColumn.Dispose ();
				SpectraListColumn = null;
			}

			if (SpectraListTable != null) {
				SpectraListTable.Dispose ();
				SpectraListTable = null;
			}

			if (TextFieldModel1 != null) {
				TextFieldModel1.Dispose ();
				TextFieldModel1 = null;
			}

			if (TextFieldModel2 != null) {
				TextFieldModel2.Dispose ();
				TextFieldModel2 = null;
			}

			if (TextFieldVelocity1 != null) {
				TextFieldVelocity1.Dispose ();
				TextFieldVelocity1 = null;
			}

			if (TextFieldVelocity2 != null) {
				TextFieldVelocity2.Dispose ();
				TextFieldVelocity2 = null;
			}
		}
	}
}
