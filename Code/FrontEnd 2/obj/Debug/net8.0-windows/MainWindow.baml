   M S B A M L   `   `   `  ����E  AFrontEnd 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:FrontEnd_2
FrontEnd_2  5   	   =#clr-namespace:FrontEnd_2.ViewModelsFrontEnd_2.ViewModels  5   	   3clr-namespace:FrontEnd_2.ViewsFrontEnd_2.Views  5   	       FrontEnd_2.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:FrontEnd_2     5   	   :
viewmodels#clr-namespace:FrontEnd_2.ViewModels     5   	   0viewsclr-namespace:FrontEnd_2.Views     5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��800��6)    � 	Resources 5       �� DataType-  'FrontEnd_2.ViewModels.SettingsViewModel#   FrontEnd_2.Views.SettingsView � DataContext-  'FrontEnd_2.ViewModels.OverviewViewModel#   FrontEnd_2.Views.OverviewView��%�   (��       5   5   *, +)(�� ^     5   1   *, +)�� 5   
   settingsViewTemplate��"  65   .R�5         6!   �� 6;   5      �� 5   
   overViewTemplate��"  61   .R�5         6!   �� 6;   5      5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5       6   .+�5%   
   ]� $��0��6   $��5��6$   
���iii�.+�5&      �� .+�5'      �� -   5        $��
Übersicht��5'      $��3q�6-   6X   �� 5(      -   5        $��Einstellungen��5(      $��3q�61   6]   5)      5*      �� 5+   
   $��0��6   $��1��6&   $��4��66   $��5��6J   ��6[   �� 6q   5,      5-      