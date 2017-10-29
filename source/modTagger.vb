' Модуль разбивает исходник на лексемы

Namespace пиОк
   '''<summary>
   '''Класс хранит координаты позиции кода в исходном месте. Позволяет сканировать текст на новую строку.
   '''</summary>
   Public Class clsCoord
      '''<summary>
      '''Позиция тега в строке
      '''</summary>
      Dim pos As Integer = 0
      '''<summary>
      '''Номер строки в исходном тексте
      '''</summary>
      Dim str As Integer = 0
      '''<summary>
      '''Позиция тега в строке
      '''</summary>
      Public ReadOnly Property iPos() As Integer
         Get
            Return Me.pos
         End Get
      End Property
      '''<summary>
      '''Номер строки в исходном тексте, содержащий текущий тег
      '''</summary>
      Public ReadOnly Property iStr() As Integer
         Get
            Return Me.str
         End Get
      End Property
      Public Sub New(Optional _str As Integer = 0, Optional _pos As Integer = 0)
         If _pos < 0 Then
            модКокон.Ошибка("Позиция в строке не может быть отрицательной val=" + _pos.ToString())
            Environment.Exit(1)
         Else
            Me.pos = _pos
         End If
         If _str < 0 Then
            модКокон.Ошибка("Номер строки не может быть отрицательной val=" + _str.ToString())
            Environment.Exit(1)
         Else
            Me.str = _str
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
         ElseIf lit = vbLf Then
            Me.pos = 0
            Me.str += 1
            bRes = True
         Else
            Me.pos += 1
            bRes = False
         End If
         Return bRes
      End Function
   End Class
   Public Class clsTag
      ' Стили именования
      ' хранит в себе последовательно кусочек нераспознанного кода с координатами
      Public coord As clsCoord
      Public strTag As String = ""
      Public Sub New(_strTag As String, _coord As clsCoord)
         Me.coord = New clsCoord(_coord.iStr, _coord.iPos)
         Me.strTag = _strTag
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
         Dim tag As clsTag = New clsTag(lit, gCoord)
         If IsNothing(tags) Then
            ReDim Preserve tags(0)
         Else
            ReDim Preserve tags(tags.Length)
         End If
         tags(tags.Length - 1) = tag
         If gCoord.iStr > 0 And gCoord.iStr < 3 Then
            Console.WriteLine(tag.strTag + " " + gCoord.iStr.ToString() + "-" + gCoord.iPos.ToString())
         End If
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
         ElseIf modUtil.ЕслиЦифра(lit) Or modUtil.ЕслиБуква(lit) Or lit = "_" Then
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
