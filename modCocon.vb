' пи -- "пространство имён"
' Ок -- "Оберон компилятор"
Namespace пиОк
   Public Module modCocon
   
      Const txtObcomp as String = "|     Oberon-07 compiler 2017 BSD-2  |"
      Const txtBuild as String  = "|     Build 0002   2017-10-21 21:06  |"
      Const txtAuthor as String = "|     KBK Technicks Ltd.     (c)     |"
      
      Public Sub Info(ByRef txt As String)' нужен для вывода информационных сообщение белыми буквами на синем фоне
         Static FoneColor As System.ConsoleColor
         Console.BackgroundColor = System.ConsoleColor.Blue
         Console.WriteLine(txt)
         Console.BackgroundColor = FoneColor
      End Sub
      
      Public Sub Splash()'Первичная надпись при старте компилятора
         Console.WriteLine()
         Console.Write("                 ")
         Info("+------------------------------------+")
         Console.Write("                 ")
         Info(txtObcomp)
         Console.Write("                 ")
         Info(txtBuild)
         Console.Write("                 ")
         Info(txtAuthor)
         Console.Write("                 ")
         Info("+------------------------------------+")
         Console.WriteLine()
      End Sub
      
   End Module
   
End Namespace