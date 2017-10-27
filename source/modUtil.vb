Namespace пиОк
   Public Module модУтиль
      Public Function ЕслиДробное(lit As String) As Boolean
         Dim bRes As Boolean = False
         Dim res As Double = 0
         Try
            res = Convert.ToDouble(lit)
            bRes = True
         Finally
            bRes = False
         End Try
         Return bRes
      End Function
      Public Function ЕслиЦелое(lit As String) As Boolean
         Dim bRes As Boolean = False
         Dim res As Single = 0
         Try
            res = Convert.ToInt64(lit)
            bRes = True
         Finally
            bRes = False
         End Try
         Return bRes
      End Function
      Public Function ЕслиЦифра(lit As String) As Boolean
         Dim bRes As Boolean = False
         If IsNumeric(lit) Then
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

