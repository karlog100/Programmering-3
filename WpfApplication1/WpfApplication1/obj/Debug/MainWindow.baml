   M S B A M L   `   `   `  ����  WpfApplication1    MailClient.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��
mainWindow��5   )   $��313.582��5   @   $��904.702��6Q     � Title$  Karlogs mail client��6a    � 	Resources 5         mailOverView��%2   &	        �� 5   
   .R�5      � 5      5      .��5	      � $��Stretch=�6   $��Stretch=�6)   $��0,0,0.4,-0.2q�6E    � RowDefinitions 5
   
   �� 5      $��25 �6   �� 5      $
��296* �6    � ColumnDefinitions 5   
   �� 5      $��20��6   $	��450��6-   �� 5      $��* �6   �� 5      $��30��6   $	��500��6-   .+�5   
   o� -   5        $��	menuPanel��5      $��26��6    
��� � �$��Stretch=�6?   $��Stretch=�6[   $��3��6y   $��0,0,-0.4,258q�6�   $��2��6�   .@�5      m� -   5         m�Name$ NewMailAccount��5      $��Add mail account��6/   $	��112��6I   6U   m� 5       -   5        $ menuSend��5       $
��Send��6)   $��52��67   6B   m� 5!      -   5        $ 
menuRecive��5!      $��Recive��6+   $��Stretch=�6;   $��64��6Y   $��23��6d   6p   m� 5"      -   5        $ menuSendAndRecive��5"      $��Send and recive all��62   $	��127��6O   m� 5#      -   5        $ 	menuAbout��5#      $��About��6*   69   5$      R� 5&   
   $��Stretch=�6   $��0��61   $��Stretch=�6A   
�������$��	0,0,0.2,0q�6t   $��2��6�   �� 5(   
   -   5        $��listMailOverView��5(      $��1��6.   $��2��6;   $��Stretch=�6K   $��Stretch=�6i   5*      (  "System.Windows.Controls.WebBrowser  5,   
   -	   5        $��browserMailRead��5,      $��Stretch=�6,   $��5,0,0,0q�6J   $��1��6[   $��2��6k   $��Stretch=�6x   �� 50   
   $
��Left=�6   $��5��62    �� ShowsPreview . $��1��6P   $��2��6`   $��0,0,5,0q�6m   �� 51   
   $
��Left=�6   $��5��62    . $��2��6P   $��2��6`   $��0,0,5,0q�6m   
�������6~   52      55      