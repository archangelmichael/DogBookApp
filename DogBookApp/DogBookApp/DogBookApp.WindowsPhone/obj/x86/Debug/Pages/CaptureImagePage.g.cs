﻿

#pragma checksum "C:\Users\Radi\Documents\GitHub\DogBookApp\DogBookApp\DogBookApp\DogBookApp.Shared\Pages\CaptureImagePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4AB46BE173D9966BA2F99558BD94C1B3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DogBookApp.Pages
{
    partial class CaptureImagePage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 24 "..\..\..\Pages\CaptureImagePage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.BackButton_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 45 "..\..\..\Pages\CaptureImagePage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.BtnCapturePhoto_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

