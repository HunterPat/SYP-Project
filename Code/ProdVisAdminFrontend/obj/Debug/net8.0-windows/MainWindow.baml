   M S B A M L   `   `   `  ����O  KProdVisAdminFrontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ProdVisAdminFrontendProdVisAdminFrontend  5   	   Q-clr-namespace:ProdVisAdminFrontend.ViewModelsProdVisAdminFrontend.ViewModels  5   	   G(clr-namespace:ProdVisAdminFrontend.ViewsProdVisAdminFrontend.Views  5   	   %    ProdVisAdminFrontend.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:ProdVisAdminFrontend     5   	   D
viewmodels-clr-namespace:ProdVisAdminFrontend.ViewModels     5   	   :views(clr-namespace:ProdVisAdminFrontend.Views     5   	     � WindowStartupLocation$  CenterScreen=�5
   	   
������� � Title$ ProdVis Admin��5   	   $	��720��6   $
��1280��6,    � 	Resources 5       �� DataType7   1ProdVisAdminFrontend.ViewModels.SettingsViewModel-   'ProdVisAdminFrontend.Views.SettingsView � DataContext7  1ProdVisAdminFrontend.ViewModels.OverviewViewModel-   'ProdVisAdminFrontend.Views.OverviewView?  9ProdVisAdminFrontend.ViewModels.DetailedOverviewViewModel5   /ProdVisAdminFrontend.Views.DetailedOverviewView��%i  (��       5   5   *, +)(�� ^     5   5   *, +)(�� �     5   9   *, +)�� 5   
   settingsViewTemplate��"  65   .R�5         6!   �� 6;   5      �� 5   
   overviewViewTemplate��"  65   .R�5         6!   �� 6;   5      �� 5   
   detailedOverviewTemplate��"  69   .R�5         6)   �� 6C   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   6,   �� 5      $	��12* �6   6-   .+�5    
   � .+�5!      �� ���#EEEDED.+�5#      �� -   5        $��
Übersicht��5#   3   
�������$��16(�6Z   $��
10,10,10,5q�6h   $��Center=�6|   6�   �� 5$      -   5        $��Einstellungen��5$   3   
�������$��16(�6^   $��10q�6l   $��Center=�6x   6�   5%      5&      �� 5'   
   $��1��6   ��6)   �� 6?   5(      5)      