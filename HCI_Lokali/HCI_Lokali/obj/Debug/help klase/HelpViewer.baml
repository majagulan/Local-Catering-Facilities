   M S B A M L   `   `   `  ����  
HCI_Lokali    HCI_Lokali.HelpViewer   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  
HelpViewer��5   	   $	��300��6   $	��300��6)    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$# NavigationCommands.BrowseBack��5      6d     5   
   -   5        $&  NavigationCommands.BrowseForward��5      6j   .��5	      ]� .+�5
   
   c� $	��Top=�6   .@�5      �� $#��NavigationCommands.BrowseBack��6   .��6=   Back5      �� 5      $&�� NavigationCommands.BrowseForward��6   .��6@   	Forward5      5      (  "System.Windows.Controls.WebBrowser  5   
   -   5        $��wbHelp��5      6D   5      5      