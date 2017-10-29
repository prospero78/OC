' пи -- "пространство имён"
' Ок -- "Оберон компилятор"
Namespace пиОк
   Public Module модКокон ' Consol Colorize

      Const txtObcomp As String = "|   Oberon-07 compiler   2017 BSD-2  |"
      Const txtBuild_ As String = "|   Build 0078     2017-10-29 20:40  |"
      Const txtAuthor As String = "|   KBK Technicks Ltd.       (c)     |"

      Public Sub Инфо(ByRef txt As String) ' нужен для вывода информационных сообщений белыми буквами на синем фоне
         Static FoneColor As System.ConsoleColor
         FoneColor = Console.BackgroundColor
         Console.BackgroundColor = System.ConsoleColor.Blue
         Console.WriteLine(txt)
         Console.BackgroundColor = FoneColor
      End Sub

      Public Sub Ошибка(ByRef txt As String) ' нужен для вывода сообщений об ошибках белыми буквами на красном фоне
         Static FoneColor As System.ConsoleColor
         Static SymColor As System.ConsoleColor
         FoneColor = Console.BackgroundColor
         SymColor = Console.ForegroundColor
         Console.BackgroundColor = System.ConsoleColor.Red
         Console.ForegroundColor = System.ConsoleColor.Yellow
         Console.WriteLine(txt)
         Console.BackgroundColor = FoneColor
         Console.ForegroundColor = SymColor
      End Sub

      Public Sub Сплэш_Печать() 'Первичная надпись при старте компилятора
         Console.WriteLine()
         Console.Write("                 ")
         Инфо("+------------------------------------+")
         Console.Write("                 ")
         Инфо(txtObcomp)
         Console.Write("                 ")
         Инфо(txtBuild_)
         Console.Write("                 ")
         Инфо(txtAuthor)
         Console.Write("                 ")
         Инфо("+------------------------------------+")
         Console.WriteLine()
      End Sub

   End Module

End Namespace
