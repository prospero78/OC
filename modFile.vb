'Модуль выполняет необходимые операции с файлами
Imports System.IO' для File

Namespace пиОк
   Public Module модФайл
      Dim argMod As clsArgs
      Public txtFileO7 as String = "" ' содержимое исходного файла Оберон-07
      Public Sub Init()
         argMod = модАрг.argMod
      End Sub
      
      Public Sub о7_Загрузить()
         If Not File.Exists(argMod.val) Then
            модКокон.Ошибка("Файл " + argMod.val + " не существует или ошибка в имени")
         Else
            ' Open the stream and read it back. 
            Using sr As StreamReader = File.OpenText(argMod.val)
               txtFileO7 = sr.ReadToEnd()
               sr.Close()
            End Using
         End If
      End Sub
   End Module
End Namespace