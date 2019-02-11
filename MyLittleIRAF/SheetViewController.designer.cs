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
	[Register ("SheetViewController")]
	partial class SheetViewController
	{
		[Outlet]
		AppKit.NSTextField NewNameTextField { get; set; }

		[Action ("CancelButton:")]
		partial void CancelButton (Foundation.NSObject sender);

		[Action ("RenameButton:")]
		partial void RenameButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (NewNameTextField != null) {
				NewNameTextField.Dispose ();
				NewNameTextField = null;
			}
		}
	}
}
