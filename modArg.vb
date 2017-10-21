'Модуль парсит параметры командной строки
Namespace пиОк
   Public Module модАрг
      Public arg As String = System.Environment.CommandLine
      Public args() As String = Split(arg, " ")
      Public Sub Парам_Получ()
         Static NumArg As Integer = 0
         Console.WriteLine("Командная строка:  <" + arg + ">")
         Console.WriteLine("Число аргументов: <" + Str(args.Length) + ">")
         If args.Length = 1 Then
            Console.WriteLine("Нет параметров для работы")
            модСправка.Справа_Показать()
         Else
            NumArg = args.Length
            For i as Integer = 0 to NumArg-1
               Console.WriteLine(str(i)+": <"+args(i) + "> len="+Str(Len(args(i))))
            Next
         End If
      End Sub
   End Module
End Namespace