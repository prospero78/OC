Namespace пиОк
   ''' <summary>
   ''' Содержит распознанный тэг -- лексему.
   '''наследуется от clsTag 
   ''' </summary>
   Public Class clsLex
      Inherits clsTag
      Public type_ As String = "" ' тип тега
      ''' <summary>
      ''' Строковое предствление тега для лексемы.
      ''' </summary>
      Public litName As clsName ' настоящее имя модуля
      Public Sub New(_strTag As String, _coord As clsCoord)
         MyBase.New(_strTag, _coord)
         Me.litName = New clsName(_strTag)
      End Sub
   End Class
   ''' <summary>
   ''' Содержит имя объекта и методы обработки имени
   ''' </summary>
   Public Class clsName
      Dim _name As String = ""
      ''' <summary>
      ''' Строкове обозначение имени объекта.
      ''' </summary>
      ''' <param name="_str"></param>
      ''' <returns></returns>
      Public Property strVal() As String
         Get
            Return Me._name
         End Get
         Set(value As String)
            Dim res As String
            res = modUtil.ЕслиИмя(value)
            If res = "_name_" Then
               Me._name = value
            Else
               Throw New Exception(Me._name + ":" + res + " val=" + value)
            End If
         End Set
      End Property
      Public Sub New(_Name As String)
         Me._name = _Name
      End Sub
   End Class
   ''' <summary>
   '''  Класс описывает еденицу типа
   ''' </summary>
   Public Class clsType
      Inherits clsLex
      Public base As String = "_none_" ' базовый класс
      Public Sub New(_strTag As String, _coord As clsCoord)
         MyBase.New(_strTag, _coord)
      End Sub
      ''' <summary>
      ''' Ошибка в разделителе между именем типа и описанием типа
      ''' </summary>
      ''' <param name="txtLine">Строка с описанием типа</param>
      ''' <param name="_mLex">Лекссема с ошибкой</param>
      Public Sub ErrorTerminal(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + " -" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("Ошибочный разделитель между именем типа и его описателем")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Сообщение, при неверном определении типа
      ''' </summary>
      ''' <param name="txtLine">Строка с неверным определением</param>
      ''' <param name="_mLex">Ошибочная лексема</param>
      Public Sub ErrorKeywordType(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + " -" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("Ошибочный описатель типа")
         Environment.Exit(1)
      End Sub
   End Class
   Public Class clsModule ' Описывает модуль целиком
      Public tag_end As Integer = 0 'номер последнего значимого тега
      Public level As Integer = 0 ' 0 -- это главный
      Public loaded As Boolean
      Public import() As clsImport  ' Список модулей импорта
      Public const_() As clsConst 'список констант
      Public type_() As clsType ' список объявленных типов в модуле
      Public proc As Integer
      Public name As String = ""
      ''' <summary>
      ''' Сообщение об отсутствии кейворда MODULE
      ''' </summary>
      ''' <param name="txtLine">Строка, где MODULE не встречено </param>
      ''' <param name="_mLex">Лексема, которая должна была содржать MODULE</param>
      Public Sub ErrorOpen(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + " -" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("Модуль должен начинаться с ""MODULE""")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      ''' Сообщение о том, что не найден разделитель имени модуля
      ''' </summary>
      ''' <param name="txtLine">Строка с именем модуля</param>
      ''' <param name="_mLex">ошибочная лексема</param>
      Public Sub ErrorEndName(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + " -" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("Нет разделителя имени модуля")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      ''' Сообщение при отсутствии маркера конца модуля
      ''' </summary>
      ''' <param name="txtLine">Последняя строка</param>
      ''' <param name="_mLex">Последняя лексема</param>
      Public Sub BadEndNModule(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + "-" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("Модуль должен иметь ""END <NameModule.>""")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      ''' Сообщение о ошибке, что MODULE не один
      ''' </summary>
      ''' <param name="txtLine">Строка в которой встречен второй MODULE</param>
      ''' <param name="_mLex">лексема запрещённый второй MODULE</param>
      Public Sub ErrorDoubleNModule(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + " -" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("MODULE должен быть один")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      ''' Сообщение о том, что имя модуля в начале, и имя модуля в конце
      ''' не совпадают.
      ''' </summary>
      ''' <param name="txtLine">Строка содержащая имя модуля</param>
      ''' <param name="_mLex">Лексема содержащая имя модуля</param>
      Public Sub MissMathName(txtLine1 As String, txtLine2 As String)
         Console.WriteLine(txtLine1)
         Console.WriteLine(txtLine2)
         модКокон.Ошибка("Имя модуля несогласовано")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      ''' Сообщение об ошибке, что неправильно записана инструкция
      ''' за разделителем имени модуля.
      ''' </summary>
      ''' <param name="txtLine">Строка в которой встречена запрещённая инструкция</param>
      ''' <param name="_mLex">Лексема запрещённая инструкция</param>
      Public Sub ErrorNextStatement(txtLine As String, _mLex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_mLex.coord.iStr) + " -" + Str(_mLex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_mLex.coord.iPos))
         модКокон.Ошибка("Запрещённая лексема в этом месте")
         Environment.Exit(1)
      End Sub
   End Class
   ''' <summary>
   ''' Содержит список модулей для импорта
   ''' </summary>
   Public Class clsImport ' содержит имена модулей для имопрта и их алиасы
      Public name As clsName
      Public alias_ As clsName
      Public Sub New(Optional _name As String = "", Optional _alias As String = "")
         Me.name = New clsName(_name)
         Me.alias_ = New clsName(_alias)
      End Sub
      Public Sub Import_Error(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + " -" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("Нарушение порядка импорта>")
         Environment.Exit(1)
      End Sub
   End Class
   Public Class clsConst ' класс содержащий константу
      Public lex As clsLex ' содержит строкове представление и координаты
      Public exp() As clsLex ' тут может быть целое выражение!!
      Public type_ As String ' тип константы
      ''' <summary>
      ''' Выводится при отсутствии имени константы
      ''' </summary>
      ''' <param name="txtLine">Строка, в которой ошибка</param>
      ''' <param name="_lex">Неверная лексема</param>
      Public Sub ErrorMissingName(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + " -" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("Пропущено имя константы")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      ''' Сообщение об запрете пустого имени константы
      ''' </summary>
      ''' <param name="txtLine">Строка с ошибочным именем</param>
      ''' <param name="_lex">Ошибочная лексема</param>
      Public Sub ErrorEmptyName(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + " -" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("Пустое имя константы")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Выводится сообщение при отсутствии присвоения константе
      ''' </summary>
      ''' <param name="txtLine">Строка с ошибочным присвоение</param>
      ''' <param name="_lex">Ошибочная лексема на месте присвоения</param>
      Public Sub ErrorAsign(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + "-" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("Нарушение присвоения константы")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Вызывается, если достигнут конец кода, а окончания выражения константы нет
      ''' </summary>
      ''' <param name="txtLine">Строка константы с выражением</param>
      ''' <param name="_lex">Последняя просмотренная лексема</param>
      Public Sub ErrorEndSource(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + "-" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("Нет ограничителя константы")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Проверяет, если вместо имени константы встречено ключевое слово
      ''' </summary>
      ''' <param name="strTag">Строка с ключевым словом вместо имени</param>
      Public Function NextKeyword(strTag As String) As Boolean
         Dim bRes As Boolean = False
         Dim ts(9) As String
         ts(0) = "TYPE"
         ts(1) = "VAR"
         ts(2) = "PROCEDURE"
         ts(3) = "BEGIN"
         ts(5) = "END"
         ts(6) = "^"
         ts(7) = ":"
         ts(8) = ","
         Dim sr() As String
         sr = Filter(ts, strTag, True, CompareMethod.Binary)
         If sr.Length <> 0 Then
            bRes = True
         End If
         Return bRes
      End Function
      ''' <summary>
      '''  Выводит сообщение, если в этом месте выражения константы ключевое слово запрещено
      ''' </summary>
      ''' <param name="txtLine">Строка с запрещённым ключевым словом</param>
      ''' <param name="_lex">Само ключевое слово</param>
      Public Sub ErrorKeyword(txtLine As String, _lex As clsLex)
         модКокон.Ошибка("Крд: " + Str(_lex.coord.iStr) + "-" + Str(_lex.coord.iPos))
         Console.WriteLine(txtLine)
         Console.WriteLine(Смещ(_lex.coord.iPos))
         модКокон.Ошибка("В этом месте ключевое слово запрещено")
         Environment.Exit(1)
      End Sub
   End Class
   ''' <summary>
   '''  Модуль описывает клас лексера для проверки правильности построения модуля
   ''' </summary>
   Public Module modLexer
      Dim sRes As String = "" ' результат анализа
      Dim mLex() As clsLex
      Dim prog As clsModule ' объект главного модуля есть программ
      Dim tagc As Integer = 0 ' текущий тег на анализе
      Dim txtLine() As String ' список строк исходника
      Function Смещ(ind As Integer) As String
         Dim s As String = "^"
         For i As Integer = 1 To ind - 1
            s = " " + s
         Next
         Return s
      End Function
      Sub Структуры_Копировать()
         Dim i As Integer = 0
         Dim lex_ As clsLex
         If Not IsNothing(modTagger.tags) Then
            Do While (i < modTagger.tags.Length)
               lex_ = New clsLex(modTagger.tags(i).strTag,
                                      modTagger.tags(i).coord)
               If IsNothing(mLex) Then
                  ReDim mLex(0)
               Else
                  ReDim Preserve mLex(mLex.Length)
               End If

               mLex(mLex.Length - 1) = lex_
               i += 1
            Loop
         End If
         modTagger.tags = Nothing

         i = 0
         If Not IsNothing(modTagger.txtLine) Then
            Do While i < modTagger.gCoord.iStr
               If IsNothing(txtLine) Then
                  ReDim txtLine(0)
               Else
                  ReDim Preserve txtLine(txtLine.Length)
               End If
               txtLine(txtLine.Length - 1) = modTagger.txtLine(i)
               i += 1
            Loop
         End If
         modTagger.txtLine = Nothing
      End Sub
      Sub ОшибкаИмени(msg As String, t As Integer)
         модКокон.Ошибка(msg + ":" + t.ToString() + " >" + mLex(t).strTag + "<")
         модКокон.Ошибка("Крд: " + Str(mLex(t).coord.iStr) + " -" + Str(mLex(t).coord.iPos))
         Console.WriteLine(txtLine(mLex(t).coord.iStr))
         Console.WriteLine(Смещ(mLex(t).coord.iPos))
         модКокон.Ошибка("Имя должно начинаться с буквы или ""_""")
         Environment.Exit(1)
      End Sub
      ''' <summary>
      '''  Правило выкидывает комментарии.
      '''  Входящий ключ "comment"
      ''' </summary>
      Sub Пр_КОММЕНТАРИЙ()
         ' правило ищет комметарии и иключает их из кода
         Dim count As Integer = 0
         Dim bStrip As Boolean
         Dim tmpLex() As clsLex = Nothing
         Dim tag As String
         If sRes = "comment" Then
            Do While count < mLex.Length
               tag = mLex(count).strTag
               If tag = "(*" Then ' начало коммента
                  bStrip = True
                  count += 1
                  Continue Do
               End If
               If bStrip = True Then ' пропускаем комментарий
                  If tag = "*)" Then
                     bStrip = False
                  End If
                  count += 1
                  Continue Do
               End If
               ' копирование остального
               If IsNothing(tmpLex) Then
                  ReDim tmpLex(0)
               Else
                  ReDim Preserve tmpLex(tmpLex.Length)
               End If

               tmpLex(tmpLex.Length - 1) = mLex(count)
               count += 1
            Loop

            mLex = tmpLex
            tmpLex = Nothing

            If bStrip = True Then
               модКокон.Ошибка("Крд: " + Str(mLex(mLex.Length - 1).coord.iStr + 1) + " -" _
                               + Str(mLex(mLex.Length - 1).coord.iPos))
               Console.WriteLine(txtLine(mLex(mLex.Length - 1).coord.iStr + 1))
               Console.WriteLine(Смещ(mLex(mLex.Length - 1).coord.iPos))
               модКокон.Ошибка("Блок комментария не закрыт")
               sRes = "err"
               Exit Sub
            Else
               sRes = "module"
            End If
         End If
      End Sub
      ''' <summary>
      '''  Правило контролирует начало и коец модуля
      ''' </summary>
      Sub Пр_МОДУЛЬ()
         ' 1.1 МОДУЛЬ должен быть первым
         If sRes = "module" Then
            If mLex(0).strTag = "MODULE" Then
               ' открываем наш модуль
               sRes = "1.2"
            Else ' если тег не оказался MODULE
               prog.ErrorOpen(txtLine(mLex(0).coord.iStr), mLex(0))
            End If
         End If
         '1.2 У модуля должно быть имя
         If sRes = "1.2" Then
            ' проверка на допустимое имя. Должно начинаться либо с "_"  либо с буквы
            If modUtil.ЕслиНачИмени(mLex(1).strTag) Then
               prog.name = mLex(1).strTag
            Else
               ОшибкаИмени("MODULE", 1)
               Environment.Exit(1)
            End If

            ' имя модуля -- № 1, разделитель -- № 2
            If mLex(2).strTag <> ";" Then ' пропущен разделитель
               prog.ErrorEndName(txtLine(mLex(1).coord.iStr), mLex(1))
            Else
               sRes = "1.3"
            End If
         End If
         ' 1.3 У Модуля должно быть окончание
         If sRes = "1.3" Then
            Dim bEnd As Boolean = False
            Dim i As Integer = 3 ' начинаем отсчёт сразу за определением модуля
            Do While i < mLex.Length - 2 ' учитываем ссылку вперёд на точку
               ' конец ли это? i+2 -- через имя
               If mLex(i).strTag = "END" And modUtil.ЕслиИмя(mLex(i + 1).strTag) = "_name_" And
                   mLex(i + 2).strTag = "." Then
                  bEnd = True
                  ' ограничивать будем полем структуры программы
                  prog.tag_end = i + 2
                  sRes = "1.4"
                  Exit Do
               End If
               i += 1
            Loop
            If Not bEnd Then 'а конца то нет!! работаем с последним тегом
               prog.BadEndNModule(txtLine(mLex(mLex.Length - 1).coord.iStr - 1), mLex(mLex.Length - 1))
            End If
         End If
         '1.4 Модуль должен быть один
         If sRes = "1.4" Then
            ' Организуем цикл в поиске МОДУЛЬ с учётом, что это может быть строка
            ' Интересует только первая тотальная встреча
            Dim i As Integer = 1 ' нельзя брать 0, так как это и есть модуль
            Do While i < prog.tag_end  ' последние тэги мы уже выяснили
               If mLex(i).strTag = "MODULE" Then 'надо выясить, может это часть выражения, или строка
                  If (mLex(i - 1).strTag = ".") Then
                     i += 1
                     Continue Do
                  ElseIf mLex(i - 1).strTag = """" And mLex(i + 1).strTag = """" Then
                     i += 1
                     Continue Do
                  ElseIf mLex(i - 1).strTag = "'" And mLex(i + 1).strTag = "'" Then
                     i += 1
                     Continue Do
                  Else ' да. Это не строка, и не часть сущности!!
                     prog.ErrorDoubleNModule(txtLine(mLex(i).coord.iStr), mLex(i))
                  End If
               End If
               i += 1
            Loop
            sRes = "1.5"
         End If
         ' 1.5 Имя модуля и имя конца должны совпадать
         If sRes = "1.5" Then
            If prog.name <> mLex(prog.tag_end - 1).strTag Then
               ' это залёт!
               prog.MissMathName(txtLine(mLex(0).coord.iStr), txtLine(mLex(prog.tag_end - 1).coord.iStr))
            Else
               sRes = "1.6"
            End If
         End If
         ' дальше должно идти на выбор: IMPORT CONST TYPE VAR PROCEDURE BEGIN
         If sRes = "1.6" Then
            tagc = 3 ' первая инструкция после разделителя имени модуля
            If mLex(tagc).strTag = "IMPORT" Or mLex(tagc).strTag = "CONST" Or
                  mLex(tagc).strTag = "TYPE" Or mLex(tagc).strTag = "VAR" Or
                  mLex(tagc).strTag = "PROCEDURE" Or mLex(tagc).strTag = "BEGIN" Then
               sRes = "import"
            Else
               ' запрещённый символ
               Console.WriteLine(">" + mLex(tagc).strTag + "<")
               prog.ErrorNextStatement(txtLine(mLex(tagc).coord.iStr), mLex(tagc))
            End If
         End If
      End Sub
      ''' <summary>
      '''  Правило контролирует импорт в модуле
      ''' </summary>
      Sub Пр_ИМПОРТ()
         ' прочесали модуль, теперь проверить нет ли импорта
         If sRes = "import" Then ' 2.1 IMPORT может идти тегом № 3 -- проверяем
            If mLex(3).strTag = "IMPORT" Then
               sRes = "2.2"
            End If
         End If
         ' 2.2 Проверяем весь доступный импорт
         If sRes = "2.2" Then
            ' Может быть прямой импорт, а может быть и с алиасами.
            ' Если импорт прямой, то tagc+2 будет ";", а сли алиас -- то ":="
            ' После ИМПОРТ имя файла или алиас по счёту -- 4 тег в файле
            tagc = 4 ' за именем -- либо разделитель, либо присвоение алиаса
            Do While True
               ' Импортов может быть 
               ' прямой, c алиасом, с запятой (продолжение), с ";" -- конец импорта
               If mLex(tagc + 1).strTag = "," Or mLex(tagc + 1).strTag = ";" Then ' Первая ветка -- прямой импорт
                  ' проверить имя модуля и алиас на допустимость
                  If modUtil.ЕслиИмя(mLex(tagc).strTag) <> "_name_" Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc + 1)
                  End If
                  If IsNothing(prog.import) Then
                     ReDim prog.import(0)
                  Else
                     ReDim Preserve prog.import(prog.import.Length)
                  End If
                  Dim imp As clsImport = New clsImport(mLex(tagc).strTag)
                  prog.import(prog.import.Length - 1) = imp
                  tagc += 1 ' ищем разделитель импорта
                  If mLex(tagc).strTag = "," Then 'импорт может закончился?
                     tagc += 1
                     Continue Do
                  ElseIf mLex(tagc).strTag = ";" Then ' импорт закончить
                     tagc += 1
                     sRes = "2.3"
                     Exit Do
                  Else ' а это уже ошибка!!
                     prog.import(prog.import.Length - 1).Import_Error(txtLine(mLex(tagc + 1).coord.iStr), mLex(tagc + 1))
                  End If
               ElseIf mLex(tagc + 1).strTag = ":=" Then ' вторая ветка -- импорт с алиасом
                  ' проверка имени
                  If modUtil.ЕслиИмя(mLex(tagc + 2).strTag) <> "_name_" Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc + 2)
                  End If
                  ' проверка алиаса
                  If modUtil.ЕслиИмя(mLex(tagc).strTag) <> "_name_" Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc)
                  End If
                  ' значит добавить элемент импрота
                  If IsNothing(prog.import) Then
                     ReDim prog.import(0)
                  Else
                     ReDim Preserve prog.import(prog.import.Length + 1)
                  End If
                  Dim imp As clsImport = New clsImport(mLex(tagc + 2).strTag, mLex(tagc).strTag)
                  prog.import(prog.import.Length - 1) = imp
                  ' проверка на продолжение
                  If mLex(tagc + 3).strTag = "," Then 'импорт может закончился?
                     tagc += 4
                     Continue Do
                  ElseIf mLex(tagc + 3).strTag = ";" Then ' импорт закончить
                     tagc += 4
                     sRes = "const"
                     Exit Do
                  Else ' а это уже ошибка!!
                     prog.import(prog.import.Length - 1).Import_Error(txtLine(mLex(tagc + 3).coord.iStr), mLex(tagc + 3))
                  End If
               Else
                  prog.import(prog.import.Length - 1).Import_Error(txtLine(mLex(tagc + 1).coord.iStr), mLex(tagc + 1))
               End If
            Loop
         End If
      End Sub
      ''' <summary>
      '''  Правило контролирует секцию констант.
      '''  TODO: сделать разбор выражений
      ''' </summary>
      Sub Пр_КОНСТ()
         ' Проверяет правильность объявления констант
         If sRes = "const" Then ' Правило объявления инструкции CONST
            If mLex(tagc).strTag <> "CONST" Then ' возможно просто нет такой секции
               tagc += 1
               sRes = "type"
               Exit Sub
               ' такая инструкция есть, но сразу вместо имени может идти ключевое слово.
               ' Остальное запрещено
            Else
               tagc += 1
               ' может имя, а может ключевое слово
               If modUtil.ЕслиИмя(mLex(tagc).strTag) = "_name_" Then
                  Dim cst As clsConst = New clsConst()
                  ' если имя оказалось ключевым словом -- идти к следующему блоку
                  If cst.NextKeyword(mLex(tagc).strTag) Then
                     tagc += 1
                     sRes = "type"
                     Exit Sub
                  End If
                  sRes = "3.2"
               Else ' если имя пустое
                  If IsNothing(prog.const_) Then
                     ReDim prog.const_(0)
                  End If
                  prog.const_(0).ErrorMissingName(txtLine(mLex(tagc).coord.iStr), mLex(tagc))
               End If
            End If
         End If
         If sRes = "3.2" Then ' начинаем разбор выражения константы
            ' номер тега уже содержит имя константы
            Do
               ' может быть что угодно в выражении, но не ";". Пока не разбираем что именно
               ' Добавляем тег в секцию констант
               ' сначала добавить имя константы, проверим на правильность имени
               If modUtil.ЕслиИмя(mLex(tagc).strTag) <> "_name_" Then
                  ОшибкаИмени("Константы:", tagc)
               End If
               ' проверить на следующее ключевое слово
               If IsNothing(prog.const_) Then
                  ReDim prog.const_(0)
                  prog.const_(0) = New clsConst()
               End If
               ' В это месте ключевое слово разрешено, и если что -- выход
               If prog.const_(0).NextKeyword(mLex(tagc).strTag) Then
                  sRes = "type"
                  ' tagc += 1 Указатель итак на ключевое слово
                  Exit Sub
               End If
               ' теперь создать лексему-имя в константу
               Dim _lex As clsLex = mLex(tagc)
               ' теперь создать саму константу с её выражением для присвоения
               Dim _const As clsConst = New clsConst With {.lex = _lex} ' заполнение константы
               ' в константе может быть выражение для присвоения
               ' перейти к следующему символу для проверки
               tagc += 1
               ' если нет "равно" -- сообщить об ошибке, иначе продолжать
               If mLex(tagc).strTag <> "=" Then
                  prog.const_(0).ErrorAsign(txtLine(mLex(tagc).coord.iStr), mLex(tagc))
               Else
                  tagc += 1
               End If
               ' создать массив лексем для выражения
               Dim _exp() As clsLex = Nothing
               tagc += 1
               ' перебирать лексемы, пока не закончится выражение, либо не закончится код
               Do While tagc < prog.tag_end
                  ' если встречен ";"
                  If mLex(tagc).strTag = ";" Then
                     ' здесь надо прервать внутренний цикл с выходом на внешний
                     Exit Do
                  End If
                  ' TODO: двоеточие в выражениях запрещена (и для SET запятую надо переделать!!!)
                  If _const.NextKeyword(mLex(tagc).strTag) Then
                     ' здесь надо вообще покончить с разбором (пока)
                     prog.const_(0).ErrorKeyword(txtLine(mLex(tagc).coord.iStr), mLex(tagc))
                  End If
                  ' заполняем выражение
                  If IsNothing(_exp) Then
                     ReDim _exp(0)
                  Else
                     ReDim Preserve _exp(_exp.Length)
                  End If
                  _exp(_exp.Length - 1) = mLex(tagc)
                  tagc += 1
               Loop
               ' если достигнут конец, а ограничителя всё нет -- закончить
               If tagc = prog.tag_end Then ' это ошибка!!
                  _const.ErrorEndSource(txtLine(prog.tag_end), mLex(tagc))
               End If
               ' создать новую константу в массиве констант модуля
               If IsNothing(prog.const_) Then
                  ReDim prog.const_(0)
               Else
                  ReDim Preserve prog.const_(prog.const_.Length)
               End If
               ' добавить выражение константы
               _const.exp = _exp
               ' сохранить новую константу
               prog.const_(prog.const_.Length - 1) = _const
               ' перейти к следующей литере
               tagc += 1
            Loop
         End If
      End Sub
      ''' <summary>
      '''  Правило контролирует секцию TYPE
      ''' </summary>
      Sub Пр_ТИПЫ()
         ' проверка на ключевое слово "TYPE"
         ' такой секции может просто не быть
         If sRes = "type" Then
            If mLex(tagc).strTag = "TYPE" Then
               Console.WriteLine("Обнаружена секция типы")
               ' продолжаем работу над сбором типов
               tagc += 1
               sRes = "4.2"
            End If
         End If
         ' обнаружена и подтверждена секция типов
         If sRes = "4.2" Then
            ' секция типов может быть пустая
            If mLex(tagc).strTag = "VAR" Or mLex(tagc).strTag = "PROCEDURE" Or
                  mLex(tagc).strTag = "BEGIN" Or
                  (mLex(tagc).strTag = "END" And mLex(tagc + 1).strTag = prog.name And
                  mLex(tagc + 2).strTag = ".") Then
               tagc += 1
               sRes = "var"
               Exit Sub
            End If
            ' это не следующая секция, это имя типа. Начать перебор
            Do
               If modUtil.ЕслиИмя(mLex(tagc).strTag) = "_name_" Then
                  ' добавить новый тип
                  Dim _type As clsType = New clsType(mLex(tagc).strTag, mLex(tagc).coord)
                  tagc += 1
                  ' проверка на разделитель
                  If mLex(tagc).strTag = "=" Then
                     Console.WriteLine("Обнаружен правильный разделитель типа")
                     tagc += 1
                  Else
                     _type.ErrorTerminal(txtLine(mLex(tagc).coord.iStr), mLex(tagc))
                  End If
                  ' проверка на ключевое слово RECORD
                  If mLex(tagc).strTag = "RECORD" Then
                     _type.type_ = "record"
                     tagc += 1
                     ' проверка на POINTER TO RECORD
                  ElseIf mLex(tagc).strTag = "POINTER" And mLex(tagc + 1).strTag = "TO" _
                         And mLex(tagc + 2).strTag = "RECORD" Then
                     _type.type_ = "pointer to record"
                     tagc += 3
                  Else ' в противом случае -- непонятная запись
                     _type.ErrorKeywordType(txtLine(mLex(tagc).coord.iStr), mLex(tagc))
                  End If
               End If
            Loop
         End If
      End Sub
      ''' <summary>
      '''  Список всех правил по порядку
      ''' </summary>
      Sub Правила()
         ' проверить правильность полученного исходного текста
         sRes = "comment"
         Console.WriteLine("Пр_КОММЕНТАРИЙ")
         Пр_КОММЕНТАРИЙ()
         Dim i As Integer = 0
         Do While i < mLex.Length - 2
            Console.Write(Str(i) + ")" + mLex(i).strTag + vbTab)
            i += 1
            Console.Write(Str(i) + ")" + mLex(i).strTag + vbTab)
            i += 1
            Console.WriteLine(Str(i) + ")" + mLex(i).strTag)
            i += 1
         Loop
         Console.WriteLine("Пр_МОДУЛЬ")
         Пр_МОДУЛЬ()
         Console.WriteLine("Len(mLex)=" + mLex.Length.ToString)
         Console.WriteLine("Пр_ИМПОРТ")
         Пр_ИМПОРТ()
         Console.WriteLine("Пр_КОНСТ")
         Пр_КОНСТ()
         Console.WriteLine("Пр_ТИПЫ")
         Пр_ТИПЫ()
      End Sub
      Sub Lexer_Run()
         Console.WriteLine("Копирование структур")
         Структуры_Копировать()
         ' создать объект програмы и упаковать его
         prog = New clsModule()
         Console.WriteLine("Отработать правила")
         Правила()
      End Sub
   End Module
End Namespace