using AppKit;
using Foundation;

namespace MyLittleIRAF
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {


        public AppDelegate()
        {
        }


        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }

        public override bool ApplicationShouldHandleReopen(NSApplication sender, bool hasVisibleWindows)
        {
            if(hasVisibleWindows == false)
            {
                var mainWindow = NSStoryboard.FromName("Main", null).InstantiateControllerWithIdentifier("Main") as NSWindowController;
                mainWindow.ShowWindow(this);
            }
            return true;
        }


    }
}
