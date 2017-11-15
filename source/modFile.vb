'Модуль выполняет необходимые операции с файлами
Imports System.IO ' для File

Namespace My
   Public Module модФайл
      Dim argMod As clsArgs
      Public txtFileO7 As String = "" ' содержимое исходного файла Оберон-07
      Public sRes As String = ""
      Public Sub Init()
         argMod = модАрг.argMod
      End Sub

      Public Sub Исх_Загрузить()
         If Not File.Exists(argMod.val) Then
            модКокон.Ошибка("Файл " + argMod.val + " не существует или ошибка в имени")
            sRes = "err"
            Exit Sub
         Else
            ' Open the stream and read it back. 
            Using sr As StreamReader = File.OpenText(argMod.val)
               txtFileO7 = sr.ReadToEnd()
            End Using
         End If
      End Sub
   End Module
End Namespace