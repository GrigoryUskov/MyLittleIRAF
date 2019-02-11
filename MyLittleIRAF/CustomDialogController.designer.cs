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
	[Register ("CustomDialogController")]
	partial class CustomDialogController
	{
		[Outlet]
		AppKit.NSComboBox ComboBox1 { get; set; }

		[Outlet]
		AppKit.NSComboBox ComboBox2 { get; set; }

		[Action ("AcceptButton:")]
		partial void AcceptButton (Foundation.NSObject sender);

		[Action ("CancelButton:")]
		partial void CancelButton (Foundation.NSObject sender);

		[Action ("ChooseOperationButton:")]
		partial void ChooseOperationButton (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ComboBox1 != null) {
				ComboBox1.Dispose ();
				ComboBox1 = null;
			}

			if (ComboBox2 != null) {
				ComboBox2.Dispose ();
				ComboBox2 = null;
			}
		}
	}
}
