   M S B A M L   `   `   `  ����G  CStocksParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:StocksParserStocksParser  5   	   ?$clr-namespace:StocksParser.ViewModelStocksParser.ViewModel  5	   	   A%clr-namespace:StocksParser.ConvertersStocksParser.Converters  5
   	       StocksParser.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:StocksParser     5   	   ;
viewModels$clr-namespace:StocksParser.ViewModel     5	   	   ;	converter%clr-namespace:StocksParser.Converters     5
   	     �Language$  ru-RU�5   )    � Title$ 
MainWindow��6	   $	��500��6   $	��750��6)    � 	Resources 5      )   #StocksParser.Converters.InverseBool   InverseBool��%   &	          5   
   6    5      .��5      � $��0,17,0,9q�6    � ColumnDefinitions 5   
   �� 5      $	��55* �6   �� 5      $
��282* �6   �� 5      $	��38* �6    � RowDefinitions 5   
   �� 5      $	��25* �6   �� 5      $	��38* �6   �� 5      $	��24* �6   �� 5      $	��38* �6   �� 5       $	��29* �6   �� 5!      $	��32* �6   �� 5"      $
��272* �6   .+�5%   
   � $��0��6   $��0��6    5&      �� 5'      $	��37* �6#   �� 5(      $	��20* �6#   5+      � 5-   
   $��0��6   $��1��6   .+�5.      �� $��Center=�6H   $
��10 0q�6e   ��6   �� *	DateToday+ �� StringFormat$ {0:d}��6e   5/      � 51   
   $��1��6   $��0��6   $��0,1,0,2q�6,   .+�52      �� $��
Api Token:��6   $��Center=�6+   $��Right=�6F   $��5q�6b   53      � 55   
   $��1��6   $��1��6   $��0,1,0,2q�6,    56      �� 57      $
��169* �6#   �� 58      $
��174* �6#   .+�5:      �� $��0,5,0,5q�60   $��2��6A   ��6   �� *
ApiToken+6A   5;      � 5=   
   $��1��6   $��2��6   $��0,1,0,2q�6,   .+�5>      �� $��Submit��6   $��5q�6Q   $��50��6\   ��6'   �� *SubmitApiTokenCommand+6\   5?      � 5A   
   $��5��6   $��1��6   $��0,1,0,0q�6,   .+�5B      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�5C      �� $��Create Database��6   $��5q�6�   $	��140��6�   $��11(�6�   $
��Left=�6�   ��63   �� *CreateDatabaseCommand+{�6]   �� *isConnectionDatabase+ �� 	Converter# [  6�   �� 5D      $	��Add��6   $��50��6'   $��5q�62   {�6=   �� *isConnectionDatabase+��6h   �� *OpenAddFormCommand+5F      5G      � 5I   
   $��2��6   $��0��6   $��0,5,0,5q�6,   .+�5J      �� $��5q�6   $��Center=�6$   $��Right=�6?   5K      � 5M   
   $��1��6   $��2��6   $��0,2,0,0q�6,   .+�5N      �� $	��Top=�63   $��Italic$�6K   ${�False��6^   ���	#FF807979��6   �� *
ApiToken+6p   5O      � 5Q   
   $��3��6   $��0��6   $��0,5,0,5q�6,   .+�5R      �� $��Connection string:��6   $��5q�62   $��Center=�6=   $��Right=�6X   5S      � 5U   
   $��1��6   $��3��6   $��0,2,0,0q�6,   .+�5V      �� $	��0 5q�68   $��Center=�6E   $
{�True��6a   ��6   �� *ConnectionString+6a   5W      � 5Y   
   $��1��6   $��4��6   $��0,2,0,0q�6,   .+�5Z      �� $	��Top=�6;   $��Italic$�6S   ${�False��6f   ���	#FF807979��6   �� *ConnectionString+6x   5[      � 5]   
   $��3��6   $��2��6   $��0,1,0,0q�6,   .+�5^      �� $��Submit��6   $��5q�6Y   $��50��6d   ��6'   �� *SubmitConnectionStringCommand+6d   5_      � 5d   
   $��1��6   $��6��6   $��0,5,0,5q�6,    5e      �� 5f      $	��64* �6    �� 5g      $	��67* �6    .+�5i      �� $��Center=�6=   $	��544��6Z   $��2��6�   ��6   �� *CompaniesInfo+{�6f   �� *TickerIsRefreshingNow+# [   �� View 5j      �� 5k      .P�5l      �� $��Ticker��6)   	 �� Width$	 46��69   
 �� DisplayMemberBinding
 6D   �� *ticker+�� 5m      $
��Name��6)   $	 70��67   
 6B   �� *Name+�� 5n      $��Records��6)   $	 53��6:   
 6E   �� *RecordsCount+�� 5o      $��Last Refreshed��6)   $		 100��6A   
 6M   �� *LastRefreshed+$ {0:d}���� 5p      $��Last user updated��6)   $		 100��6D   
 6P   �� *LastUserUpdate+$ {0:d}���� 5q       �� CellTemplate 5r      �� 5s   "   .R�5t   &   ��  ��Name$ 
EditButton��6-   $
��Edit��6A   $��25��6P   $��40��6\   ��6g   �� *!DataContext.OpenEditFormCommand+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" ����6�   ��  �� *Self+5u   #   5w      �� 5x       5y      �� 5z   "   .R�5{   &   �� $ DeleteButton��6-   $��Delete��6C   $��25��6T   $��40��6`   ��6k   �� *!DataContext.DeleteTickerCommand+ �� $ FindAncestor=�" ����6�   ��  �� *Self+5|   #   5~      �� 5       5�      �� 5�   "   .R�5�   &   �� $ RefreshButton��6-   $��Refresh��6D   $��25��6V   $��45��6b   ��6m   �� *" DataContext.RefreshTickerCommand+ �� $ FindAncestor=�" ����6�   ��  �� *Self+5�   #   5�      5�      5�      5�      5�      5�      