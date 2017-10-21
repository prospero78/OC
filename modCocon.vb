' пи -- "пространство имён"
' Ок -- "Оберон компилятор"
Namespace пиОк
   Public Module модКокон ' Consol Colorize
   
      Const txtObcomp as String = "|     Oberon-07 compiler 2017 BSD-2  |"
      Const txtBuild as String  = "|     Build 0004   2017-10-21 22:23  |"
      Const txtAuthor as String = "|     KBK Technicks Ltd.     (c)     |"
      
      Public Sub Инфо(ByRef txt As String)' нужен для вывода информационных сообщение белыми буквами на синем фоне
         Static FoneColor As System.ConsoleColor
         Console.BackgroundColor = System.ConsoleColor.Blue
         Console.WriteLine(txt)
         Console.BackgroundColor = FoneColor
      End Sub
      
      Public Sub Сплэш_Печать()'Первичная надпись при старте компилятора
         Console.WriteLine()
         Console.Write("                 ")
         Инфо("+------------------------------------+")
         Console.Write("                 ")
         Инфо(txtObcomp)
         Console.Write("                 ")
         Инфо(txtBuild)
         Console.Write("                 ")
         Инфо(txtAuthor)
         Console.Write("                 ")
         Инфо("+------------------------------------+")
         Console.WriteLine()
      End Sub
      
   End Module
   
End Namespace