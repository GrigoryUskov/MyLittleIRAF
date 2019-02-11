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
	[Register ("TrimDialogController")]
	partial class TrimDialogController
	{
		[Outlet]
		AppKit.NSTextField Lambda1TextField { get; set; }

		[Outlet]
		AppKit.NSTextField Lambda2TextField { get; set; }

		[Action ("CancelButton:")]
		partial void CancelButton (Foundation.NSObject sender);

		[Action ("CutButton:")]
		partial void CutButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Lambda1TextField != null) {
				Lambda1TextField.Dispose ();
				Lambda1TextField = null;
			}

			if (Lambda2TextField != null) {
				Lambda2TextField.Dispose ();
				Lambda2TextField = null;
			}
		}
	}
}
