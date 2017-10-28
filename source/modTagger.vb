' Модуль разбивает исходник на лексемы

Namespace пиОк
   '''<summary>
   '''Класс хранит координаты позиции кода в исходном месте. Позволяет сканировать текст на новую строку.
   '''</summary>
   Public Class clsCoord
      '''<summary>
      '''Позиция тега в строке
      '''</summary>
      Dim _pos As Integer = 0
      '''<summary>
      '''Номер строки в исходном тексте
      '''</summary>
      Dim _str As Integer = 0
      '''<summary>
      '''Позиция тега в строке
      '''</summary>
      Public ReadOnly Property iPos() As Integer
         Get
            Return Me._pos
         End Get
      End Property
      '''<summary>
      '''Номер строки в исходном тексте, содержащий текущий тег
      '''</summary>
      Public ReadOnly Property iStr() As Integer
         Get
            Return Me._str
         End Get
      End Property
      Public Sub New(Optional pos_ As Integer = 0, Optional str_ As Integer = 0)
         If pos_ < 0 Then
            модКокон.Ошибка("Позиция не может быть отрицательной val=" + Str(_pos))
            Environment.Exit(1)
         Else
            Me._pos = _pos
         End If
         If str_ < 0 Then
            модКокон.Ошибка("Cтрока не может быть отрицательной val=" + Str(_str))
            Environment.Exit(1)
         Else
            Me._str = _str
         End If
      End Sub
      '''<summary>
      '''По литере определяет налчие новой строки. В любом случае, обновляет координаты
      '''</summary>
      Public Function Coord_Update(lit As String) As Boolean
         Dim bRes As Boolean
         If lit = "" Then
            модКокон.Ошибка("Литера не может быть пустой для установки её позиции. lit=" + lit)
            Environment.Exit(1)
         ElseIf lit = vbCr Then
            Me._pos = 0
            Me._str += 1
            bRes = True
         Else
            Me._pos += 1
            bRes = False
         End If
         Return bRes
      End Function
   End Class
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
      Public Sub New(_strTag As String, _iStr As Integer, _iPoz As Integer)
         Me.coord = New clsCoord(_iPoz, _iStr)
         Me._strTag = _strTag
      End Sub
   End Class
   Public Module modTagger
      ' попытка сделать по уму
      Public tags() As clsTag
      Dim txtSrc As String = "" ' текст исходника
      Public txtLine() As String ' исходник разбитый построчно
      Public gCoord As clsCoord ' хранит глобальные координаты
      ' =================== ТЕГИРОВАНИЕ =======================
      Sub Pos_Get(lit As String)
         Static srcLine As String = "" ' очередная строка исходного кода

         If gCoord.Coord_Update(lit) Then ' если новая строка
            ' Добавить строку в массив исходников
            If IsNothing(txtLine) Then
               ReDim txtLine(0)
            Else
               ReDim Preserve txtLine(txtLine.Length)
            End If
            txtLine(txtLine.Length - 1) = srcLine
            srcLine = ""
         Else
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
         Dim tag As clsTag = New clsTag(lit, gCoord.iStr, gCoord.iPos)
         If IsNothing(tags) Then
            ReDim Preserve tags(0)
         Else
            ReDim Preserve tags(tags.Length)
         End If
         tags(tags.Length - 1) = tag
      End Sub
      Public Function ClassTag(lit As String) As Integer
         ' описывает типы тегов в зависимости от длины:
         '   синглетег -- одиночная литера
         '   дублетег -- двойная литера
         '   мультитег -- мультилитера
         Dim res As Integer = -1

         If InStr(",;-+.[""]'=", lit) > 0 Then
            res = modConst.singletag
         ElseIf InStr(":<>()*", lit) > 0 Then
            res = modConst.doubletag
         ElseIf модУтиль.ЕслиЦифра(lit) Or модУтиль.ЕслиБуква(lit) Or lit = "_" Then
            res = modConst.multitag
         End If
         Return res
      End Function
      Public Function New_Lit() As String
         txtSrc = Mid(txtSrc, 2)
         Dim sRes As String = Mid(txtSrc, 1, 1)
         Pos_Get(sRes)
         Return sRes
      End Function

      Public Sub Тег_Разметить()
         Dim lit, lit2 As String
         Dim гсТег As String = "" ' глобальный текущий тэгg
         gCoord = New clsCoord()
         txtSrc = модФайл.txtFileO7 + "  " ' хвост нужен, чтобы гарантированно не обрезать тег
         lit = Mid(txtSrc, 1, 1)
         Pos_Get(lit)
         Do While txtSrc.Length > 1 ' общий цикл с правильными литерами
            If ЕслиОтсев(lit) Then ' отбросим мусор
               ' Пропуск символов-мусора
               lit = New_Lit()
            End If
            If ClassTag(lit) = modConst.singletag Then ' если тег-одиночка
               Тег_Добавить(lit)
               lit = New_Lit()
            End If
            If ClassTag(lit) = modConst.doubletag Then ' если сложный тег
               lit2 = Mid(txtSrc, 2, 1)
               If InStr(":><", lit) > 0 And lit2 = "=" Then
                  txtSrc = Mid(txtSrc, 2)
                  Pos_Get(lit2)
                  Тег_Добавить(lit + lit2)
               ElseIf lit = "(" And lit2 = "*" Then
                  txtSrc = Mid(txtSrc, 2)
                  Pos_Get(lit2)
                  Тег_Добавить(lit + lit2)
               ElseIf lit = "*" And lit2 = ")" Then
                  txtSrc = Mid(txtSrc, 2)
                  Pos_Get(lit2)
                  Тег_Добавить(lit + lit2)
               Else
                  Тег_Добавить(lit)
               End If
               lit = New_Lit()
            End If
            If ClassTag(lit) = modConst.multitag Then
               Do While ClassTag(lit) = modConst.multitag ' если многосимвольный тэг
                  гсТег += lit
                  lit = New_Lit()
               Loop
               Тег_Добавить(гсТег)
               гсТег = ""
            End If
         Loop
      End Sub
   End Module
End Namespace
