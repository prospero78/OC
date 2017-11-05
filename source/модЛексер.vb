' Модуль разбивает исходник на лексемы

Namespace пиОк
   ''' <summary>
   ''' Хранит одну сторку исходника
   ''' </summary>
   Public Class туЗвСтрока
      Inherits туЗвено
      Public стрСтрока As String = ""
      Public Sub New(Optional пуПред As туЗвено = Nothing, Optional пуСлед As туЗвено = Nothing)
         Me.уПред_Уст(пуПред)
         Me.уСлед_Уст(пуСлед)
      End Sub
   End Class
   Public Class туИсход
      Dim стрИсхТекущ As String = "" ' одиночная текущая строка исходника
      Public мСтроки() As String ' массив строк исходного текста
      Public коорд As туКоорд
      Dim _лит As String = ""  ' операционная литера
      Public ReadOnly Property длина As Integer ' длина текущего текста
         Get
            Return Me.тхтИсхТекущ.Length
         End Get
      End Property
      Public ReadOnly Property лит As String ' возвращает очередную литеру
         Get
            If Me.длина > 0 Then
               Me._лит = Mid(Me.тхтИсхТекущ, 1, 1)
               Me.тхтИсхТекущ = Mid(Me.тхтИсхТекущ, 2)
               If Me._лит = vbLf Then
                  Me.коорд.цСтр_Уст(Me.коорд.цСтр + 1)
                  Me.коорд.цПоз_Уст(0)
                  If IsNothing(Me.мСтроки) Then
                     ReDim Me.мСтроки(0)
                  Else
                     ReDim Preserve Me.мСтроки(Me.мСтроки.Length)
                  End If
                  Me.мСтроки(Me.мСтроки.Length - 1) = Me.стрИсхТекущ
                  Console.WriteLine("туИсходник.стрИсхТекущ: " + Me.стрИсхТекущ)
                  Me.стрИсхТекущ = ""
               Else
                  Me.коорд.цПоз_Уст(Me.коорд.цПоз + 1)
                  Me.стрИсхТекущ += Me._лит
               End If
            End If
            Return Me._лит
         End Get
      End Property
      Public ReadOnly Property лит2 As String
         Get
            Return Mid(Me.тхтИсхТекущ, 1, 1)
         End Get
      End Property
      Public ReadOnly тхтИсхОригин As String ' исходный код оригинальный
      Public тхтИсхТекущ As String ' исходный код текущий
      Public Sub New(_тхт As String)
         Me.коорд = New туКоорд()
         Me.тхтИсхОригин = _тхт + vbCrLf + vbCrLf
         Me.тхтИсхТекущ = _тхт + vbCrLf + vbCrLf
         'Console.WriteLine("clsTxtSource.New() " + _txt)
      End Sub
   End Class
   '''<summary>
   '''Класс хранит координаты позиции кода в исходном месте. Позволяет сканировать текст на новую строку.
   '''</summary>
   Public Class туКоорд
      '''<summary>
      '''Позиция тега в строке
      '''</summary>
      Dim _цПоз As Integer = 0
      Public ReadOnly Property цПоз As Integer
         Get
            Return Me._цПоз
         End Get
      End Property
      Public Sub цПоз_Уст(value As Integer)
         Me._цПоз = value
      End Sub
      '''<summary>
      '''Номер строки в исходном тексте, содержащий текущий тег
      '''</summary>
      Dim _цСтр As Integer = 0
      Public ReadOnly Property цСтр As Integer
         Get
            Return Me._цСтр
         End Get
      End Property
      Public Sub цСтр_Уст(value As Integer)
         Me._цСтр = value
      End Sub

      Public Sub New(Optional пцСтр As Integer = 0, Optional пцПоз As Integer = 0)
         If пцПоз < 0 Then
            модКокон.Ошибка("Позиция в строке не может быть отрицательной val=" + пцПоз.ToString())
            Environment.Exit(1)
         Else
            Me._цПоз = пцПоз
         End If
         If пцСтр < 0 Then
            модКокон.Ошибка("Номер строки не может быть отрицательной val=" + пцСтр.ToString())
            Environment.Exit(1)
         Else
            Me._цСтр = пцСтр
         End If
      End Sub
      '''<summary>
      '''По литере определяет налчие новой строки. В любом случае, обновляет координаты
      '''Похоже, этот метод не нужен
      '''</summary>
      Sub Уст(str_ As Integer, pos_ As Integer)
         Me._цПоз = pos_
         Me._цСтр += str_
      End Sub
   End Class
   Public Class туЛекс
      Inherits туЗвено
      ' хранит в себе последовательно кусочек нераспознанного кода с координатами
      Public уКоорд As туКоорд
      Dim _стрЛекс As String
      Public ReadOnly Property стрЛекс As String
         Get
            Return Me._стрЛекс
         End Get
      End Property
      Public Sub New(Optional псЛекс As String = "", Optional пуКоорд As туКоорд = Nothing)
         Me.уКоорд = пуКоорд
         Me._стрЛекс = псЛекс
         Me.уСлед_Уст(Nothing)
         Me.уПред_Уст(Nothing)
      End Sub
   End Class
   Public Class тЦепьЛекс
      Dim _послед As туЛекс ' последняя лексема в цепи
      Dim _первая As туЛекс ' первая лексема в цепи
      Public ReadOnly Property уПервЛекс As туЛекс
         Get
            Return Me._первая
         End Get
      End Property
      Public Function ЕслиОтсев(lit As String) As Boolean
         Dim res As Boolean = False
         If lit <= " " Then
            res = True
         End If
         Return res
      End Function
      Public Sub Вставить(_lit As String, _coord As туКоорд) 'Создать новый тэг
         Dim луЛекс As туЛекс = New туЛекс(_lit, _coord)
         'Console.WriteLine("clsTagList.Add(): " + _lit + " " + Str(_str) + " " + Str(_pos))
         If IsNothing(Me._первая) Then
            Me._первая = луЛекс
            Me._послед = луЛекс
            Me._первая.уСлед_Уст(Me._послед)
            Me._послед.уПред_Уст(Me._первая)
         Else
            Me._послед.уСлед_Уст(луЛекс)
            луЛекс.уПред_Уст(Me._послед)
            Me._послед = луЛекс
         End If
      End Sub
      Public ReadOnly Property len As Integer
         Get
            If IsNothing(Me.tags) Then
               ReDim Me.tags(0)
            End If
            Return Me.tags.Length
         End Get
      End Property
      Public Sub New()

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
      Public Sub Tagging(_src As туИсход)
         Dim гсТег As String = ""
         Dim lit As String = ""
         Dim lit2 As String = ""
         Dim _tmp As String = "" ' перменная для правильного пропуска doubletag
         lit = txtSrc.лит
         Do While _src.длина > 1 ' общий цикл с правильными литерами
            If Me.ЕслиОтсев(lit) Then ' отбросим мусор
               ' Пропуск символов-мусора
               lit = txtSrc.лит
            End If
            If Me.ClassTag(lit) = modConst.singletag Then ' если тег-одиночка
               'Console.WriteLine("singletag-1")
               Me.Вставить(lit, txtSrc.коорд)
               lit = txtSrc.лит
            End If
            If Me.ClassTag(lit) = modConst.doubletag Then ' если сложный тег
               lit2 = txtSrc.лит2
               If InStr(":><", lit) > 0 And lit2 = "=" Then
                  'Console.WriteLine("doubletag-1")
                  Me.Вставить(lit + lit2, txtSrc.коорд)
                  _tmp = txtSrc.лит
               ElseIf lit = "(" And lit2 = "*" Then
                  'Console.WriteLine("doubletag-2")
                  Me.Вставить(lit + lit2, txtSrc.коорд)
                  _tmp = txtSrc.лит
               ElseIf lit = "*" And lit2 = ")" Then
                  'Console.WriteLine("doubletag-3")
                  Me.Вставить(lit + lit2, txtSrc.коорд)
                  _tmp = txtSrc.лит
               End If
               lit = txtSrc.лит
            End If
            If Me.ClassTag(lit) = modConst.multitag Then
               Do While Me.ClassTag(lit) = modConst.multitag ' если многосимвольный тэг
                  гсТег += lit
                  lit = txtSrc.лит
               Loop
               Me.Вставить(гсТег, txtSrc.коорд)
               гсТег = ""
            End If
         Loop
      End Sub
   End Class
   ''' <summary>
   ''' попытка сделать по уму
   ''' </summary>
   Public Module модЛексер
      Public уЦепьЛекс As тЦепьЛекс = New тЦепьЛекс()
      Public txtSrc As туИсход ' исходник разбитый построчно
      ' =================== Лексирование =======================
      Public Sub Лексемы_Разметить()
         tags = New тЦепьЛекс()
         txtSrc = New туИсход(модФайл.txtFileO7 + "  ") ' хвост нужен, чтобы гарантированно не обрезать тег
         tags.Tagging(txtSrc)
      End Sub
   End Module
End Namespace
