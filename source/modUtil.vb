Namespace пиОк
   Public Module modUtil
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
         For i As Integer = 1 To lit.Length
            If InStr("0123456789", Mid(lit, i, 1)) = 0 Then
               Return False
            End If
         Next
         Return True
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
         lit = Mid(lit, 1, 1)
         If (lit = "_" Or ЕслиБуква(lit)) And (Not ЕслиЦифра(lit)) Then
            bRes = True
         End If
         Return bRes
      End Function
      Public Function ЕслиИмя(lit As String) As String
         'проверить, что не пусто
         If lit = "" Then
            Return "Имя не может быть пустой строкой"
         ElseIf Not ЕслиНачИмени(Mid(lit, 1, 1)) Then
            Return "Неправильное начало имени"
         Else
            Return "_name_"
         End If
      End Function
      Function Смещ(ind As Integer) As String
         Dim s As String = "^"
         For i As Integer = 1 To ind - 1
            s = " " + s
         Next
         Return s
      End Function
      Public Sub ASSERT(_test As Boolean, _msg As String)
         If _test = False Then
            модКокон.Ошибка(_msg)
            Environment.Exit(1)
         End If
      End Sub
   End Module
End Namespace

