' Модуль разбивает исходник на лексемы
#Disable Warning IDE1006
Namespace пиОк
   ' Стили именования
   Public Class clsCoord
      ' Стили именования
      Dim _pos As Integer = 0
      Dim _str As Integer = 0
      Public ReadOnly Property iPos() As Integer
         Get
            Return Me._pos
         End Get
      End Property
      Public ReadOnly Property iStr() As Integer
         Get
            Return Me._str
         End Get
      End Property
      Public Sub New(Optional pos_ As Int64 = 0, Optional str_ As Int64 = 0)
         If pos_ < 0 Then
            Throw New ApplicationException("Позиция не может быть отрицательной val=" + Str(_pos))
         Else
            Me._pos = _pos
         End If
         If str_ < 0 Then
            Throw New ApplicationException("Cтрока не может быть отрицательной val=" + Str(_str))
         Else
            Me._str = _str
         End If
      End Sub
   End Class
   ' Стили именования
   Public Class clsTag
      ' Стили именования
      ' хранит в себе последовательно кусочек нераспознанного кода с координатами
      Public ReadOnly coord As clsCoord
      Private ReadOnly _strTag As String = ""
      Public ReadOnly Property strTag() As String
         Get
            Return Me._strTag
         End Get
      End Property
      Public Sub New(_strTag As String, _iStr As Int64, _iPoz As Int64)
         Me.coord = New clsCoord(_iPoz, _iStr)
         Me._strTag = _strTag
      End Sub
   End Class
   Public Module модТеггер
      ' попытка сделать по уму
      Public Const multitag = 0
      Const doubletag = 2
      Const singletag = 1
      Public tags() As clsTag
      Dim txtSrc As String = "" ' текст исходника
      Public txtLine() As String ' исходник разбитый построчно
      Public гцСтр As Integer = 0
      Dim гцПоз As Integer = 0
      ' =================== ТЕГИРОВАНИЕ =======================
      Sub Поз_Получ(lit As String)
         Static srcLine As String = "" ' очередная строка исходного кода
         If lit = vbLf Then

            гцПоз = 0
            ' Добавить строку в массив исходников
            If IsNothing(txtLine) Then
               ReDim txtLine(0)
            Else
               ReDim Preserve txtLine(гцСтр + 1)
            End If
            txtLine(гцСтр) = srcLine
            гцСтр += 1
            srcLine = ""
         Else
            гцПоз += 1
            srcLine += lit
         End If
      End Sub
      Function ЕслиОтсев(lit As String) As Boolean
         Dim res As Boolean = False
         If lit <= " " Then
            res = True
         End If
         Return res
      End Function
      Sub Тег_Добавить(lit As String)
         'Создать новый тэг
         Dim tag As clsTag = New clsTag(lit, гцСтр, гцПоз)
         If IsNothing(tags) Then
            ReDim Preserve tags(0)
         Else
            ReDim Preserve tags(tags.Length)
         End If
         tags(tags.Length - 1) = tag
      End Sub
      Public Function ЕслиВнутрТег(lit As String) As Integer
         Dim res As Integer = -1
         If InStr(",;-+.[""]'=", lit) > 0 Then
            res = singletag
         ElseIf InStr(":<>()*", lit) > 0 Then
            res = doubletag
         ElseIf модУтиль.ЕслиЦифра(lit) Or модУтиль.ЕслиБуква(lit) Or lit = "_" Then
            res = multitag
         End If
         Return res
      End Function
      Public Sub Тег_Разметить()
         Dim lit, lit2 As String
         Dim гсТег As String = "" ' глобальный текущий тэг
         txtSrc = модФайл.txtFileO7 + "  " ' хвост нужен, чтобы гарантированно не обрезать тег
         lit = Mid(txtSrc, 1, 1)
         Поз_Получ(lit)
         Do While Len(txtSrc) > 1 ' общий цикл с правильными литерами
            If ЕслиОтсев(lit) Then ' отбросим мусор
               ' Пропуск символов-мусора
               txtSrc = Mid(txtSrc, 2)
               lit = Mid(txtSrc, 1, 1)
               Поз_Получ(lit)
            End If
            If ЕслиВнутрТег(lit) = singletag Then ' если тег-одиночка
               Тег_Добавить(lit)

               txtSrc = Mid(txtSrc, 2)
               lit = Mid(txtSrc, 1, 1)
               Поз_Получ(lit)
            End If
            If ЕслиВнутрТег(lit) = doubletag Then ' если сложный тег
               lit2 = Mid(txtSrc, 2, 1)
               If InStr(":><", lit) > 0 And lit2 = "=" Then
                  txtSrc = Mid(txtSrc, 2)
                  Поз_Получ(lit2)
                  Тег_Добавить(lit + lit2)
               ElseIf lit = "(" And lit2 = "*" Then
                  txtSrc = Mid(txtSrc, 2)
                  Поз_Получ(lit2)
                  Тег_Добавить(lit + lit2)
               ElseIf lit = "*" And lit2 = ")" Then
                  txtSrc = Mid(txtSrc, 2)
                  Поз_Получ(lit2)
                  Тег_Добавить(lit + lit2)
               Else
                  Тег_Добавить(lit)
               End If
               txtSrc = Mid(txtSrc, 2)
               lit = Mid(txtSrc, 1, 1)
               Поз_Получ(lit)
            End If
            If ЕслиВнутрТег(lit) = multitag Then

               Do While ЕслиВнутрТег(lit) = multitag ' если многосимвольный тэг
                  гсТег += lit
                  txtSrc = Mid(txtSrc, 2)
                  lit = Mid(txtSrc, 1, 1)
                  Поз_Получ(lit)
               Loop
               Тег_Добавить(гсТег)
               гсТег = ""
            End If
         Loop

         'If Not IsNothing(теги) Then
         '   Console.WriteLine("Вывод лексера:")
         '   Console.WriteLine("Len(теги)" + Str(теги.Count))
         '   Dim i As Integer = 0
         '   Do While i < теги.Count
         '      Console.WriteLine(Str(i) + ":" + теги(i).стрТег)
         '      i += 1
         '   Loop
         'End If
      End Sub
   End Module
End Namespace
