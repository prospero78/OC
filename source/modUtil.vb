Namespace пиОк
   Public Module модУтиль
      Public Function ЕслиЦифра(lit As String) As Boolean
         Dim bRes As Boolean = False
         If InStr("0123456789", lit) > 0 Then
            bRes = True
         End If
         Return bRes
      End Function
      Public Function ЕслиБуква(lit As String) As Boolean
         Dim bRes As Boolean = False
         If InStr("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw", lit) > 0 Or
              InStr("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя", lit) > 0 Then
            bRes = True
         End If
         Return bRes
      End Function
      Public Function ЕслиНачИмени(lit As String) As Boolean
         ' имя должно начинаться с "_" или буквы
         Dim bRes As Boolean = False
         If (lit = "_" Or ЕслиБуква(lit)) And (Not ЕслиЦифра(lit)) Then
            bRes = True
         End If
         Return bRes
      End Function
   End Module
End Namespace

