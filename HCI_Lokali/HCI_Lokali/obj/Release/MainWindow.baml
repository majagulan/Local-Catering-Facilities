   M S B A M L   `   `   `  ����   
HCI_Lokali    HCI_Lokali.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $	��win��  � Title$2  ,Evidencija o geografskoj distribuciji lokala��$	��696��$
��1016�� � 
ResizeMode$ CanMinimize=����	#FFA2D9FA J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$ ApplicationCommands.Help��.��� -    �Name$
 grid��.+�o� -   $
��meni��$��23��$
��Left=�$	��Top=�.@�m� $
��Novi��$��Times New Rolok)�$��20(�$
��Bold"����	#FF1D8FE0.@�m� -    m�Name$ Lokal��$��Lokal�����	#FF1D8FE0m� -   $	 Tip��$	��Tip�����	#FF1D8FE0m� -   $ Etiketa��$��Etiketa�����	#FF1D8FE0m� $��	Kolekcija��$��Times New Rolok)�$��20(�$
��Bold"����	#FF1D8FE0.@�m� -   $ Tipovi��$��Tipovi�����	#FF1D8FE0m� -	   $ Etikete��$��Etikete�����	#FF1D8FE0�� -
    ��Name$ canvas��$��487,30,30.6,79.4q� J� 	AllowDrop . &   System.Windows.Controls.DataGrid  -   $��tabela1��   AutoGenerateColumns .  $	��196��$
��Left=�$��	10,30,0,0q�	   
IsReadOnly	 . 
   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	
 25* $	��Top=�$	��432��   CanUserResizeColumns .    CanUserReorderColumns . ���	#FFF9B3D1$��14(����� *	lk_list+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ OZNAKA��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *oznaka+  $	 IME�� �� *ime+  $
 OPIS�� �� *opis+  $ IME TIPA�� �� *	tip.ime+ � ToolTip ��  �� Orientation$ 
Horizontal=�.+��� $��5q�$
��Wrap=�$	��150��usTabelarni prikaz svih lokala koje u datom trenutku postoje. Za prikaz na mapi selektovani lokal prevući iz tabele.�� -   $	��eti��$
��Novi��$��32��$
��Left=�$��352,245,0,0q�$	��Top=�$��88�����	#FFF4FF6F$��16(��� -   $��brisi��$��Izbriši��$��32��$
��Left=�$��
18,245,0,0q�$	��Top=�$��89�����	#FFF4FF6F$��16(��� -   $��izmeni��$��Izmeni��$��32��$
��Left=�$��189,245,0,0q�$	��Top=�$��89�����	#FFF4FF6F$��16(��� -   $��calkohol��$��Služenje alkohola��$��22��$
��Left=�$��
35,400,0,0q�$	��Top=�$	��154��$��14(��� -   $��	salkohola��$��22��$
��Left=�$��209,400,0,0q�$	��Top=�$	��191���� -   $��ccena��$��Kategorija cena��$��22��$
��Left=�$��
35,428,0,0q�$	��Top=�$	��125��$��14(��� -   $��
katCenacmb��$��22��$
��Left=�$��209,428,0,0q�$	��Top=�$	��190��
���   �
���   ��� -   $��	btnFilter��$��	Filtriraj��$��32��$
��Left=�$��
84,475,0,0q�$	��Top=�$��75�����	#FFF4FF6F$��16(� �� $ 
Horizontal=�.+��� $��5q�$
��Wrap=�$	��150��USOznačiti po kojim sve parametrima se radi filtriranje i izabrati njihovu vrednost.�� -   $��btnUndo��$��Poništi��$��32��$
��Left=�$��252,476,0,0q�$	��Top=�$��75�����	#FFF4FF6F$��16(� �� $ 
Horizontal=�.+��� $��5q�$
��Wrap=�$	��150��$"Poništavanje efekata filtriranja.�� -   $��txtTrazi��$��23��$
��Left=�$��208,334,0,0q�$	��Top=�$	��190��.�� �� $ 
Horizontal=�.+��� $��5q�$
��Wrap=�$	��150��!Unesite ime lokala za pretragu..���� -   $��cbTip��$	��Tip��$��23��$
��Left=�$��
35,370,0,0q�$	��Top=�$��14(�$��48���� -   $��tipCmb��$��23��$
��Left=� �� DisplayMemberPath$	 ime��$��209,370,0,0q�$	��Top=�$	��190�����	#FFBF1919���	#FFEA3232���� *
tip_list+�� -   $��button1��$��Pomoć��$��32��$
��Left=�$��170,516,0,0q�$	��Top=�$��75�����	#FFF4FF6F$��16(��� $��Ime lokala:��$
��Left=�$��
33,331,0,0q�$	��Top=�$��96��$��14(�$��39���� $
��Left=�$��46��$��
18,610,0,0q�$	��Top=�$	��141�����	#FFF4FF6F.���� $��logo-12.png��$
��Fill=�$��40��$	��255���� $��Kliknite za demo mod��$
��Left=�$��
17,569,0,0q�$	��Top=�$	��172��$��16(�