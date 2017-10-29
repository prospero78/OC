' Главный модуль компилятора
' TODO: Для анализатора -- надо проверять, чтобы имена не начинались  с цифер

Imports System.IO
Imports System.Diagnostics

Namespace пиОк
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
            If res = "" Then
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
   End Class
   Public Class clsModule ' Описывает модуль целиком
      Public tag_end As Integer = 0 'номер последнего значимого тега
      Public level As Integer = 0 ' 0 -- это главный
      Public loaded As Boolean
      Public import() As clsImport  ' Список модулей импорта
      Public const_() As clsConst 'список констант
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
   Public Module модКомпиль
      Dim литАнализ As String = "" ' Look литера для анализа
      Dim txtBeg As String = "" ' Начало текст Visual Basic
      Dim txtOut As String = "" ' Конец текст Visual Basic
      Dim перем(1000) As String ' Массив добавляемых переменных
      Dim цПерем As Integer = 0 'Свободный элемент массива
      Sub Вых_Записать()
         Dim sw As StreamWriter
         sw = File.CreateText("out.vb")
         Using sw
            sw.Write(txtOut)
         End Using
      End Sub

      Sub Транслировать()
         Console.WriteLine("Начало трансляции")
         Dim pr As New Process()
         pr.StartInfo.FileName = "vbc"
         pr.StartInfo.Arguments = "/debug- /t:exe /platform:x86 /nologo /utf8output /optionexplicit+ /optioninfer+ /rootnamespace:Oberon07 /out:out.exe out.vb"
         pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
         pr.Start()
         pr.WaitForExit()

         'Process.Start("vbc /debug- /t:exe /platform:x86 /nologo /utf8output /optionexplicit+ /optioninfer+ /rootnamespace:Oberon07 /out:out.exe out.vb")
      End Sub

      Sub Заголовок()
         txtBeg = "' Автогенерация текста Visual Basic" + vbCrLf
         txtBeg += "' Данные для компиляции" + vbCrLf
         txtBeg += "Namespace Oberon07" + vbCrLf
         txtBeg += "Public Module modOut" + vbCrLf + vbCrLf
         txtBeg += "Dim рег0 As Integer = 0" + vbCrLf
         txtBeg += "Dim рег1 As Integer = 0" + vbCrLf
         txtBeg += "Dim head As Integer = 0" + vbCrLf
         txtBeg += "Dim стек(1000) As Integer' программный стек" + vbCrLf
         txtBeg += "Dim sp As Integer = 0'указатель стека" + vbCrLf + vbCrLf
         txtBeg += "Sub push(arg As Integer)" + vbCrLf
         txtBeg += "   If (sp+1)<1000 Then" + vbCrLf
         txtBeg += "      sp +=1" + vbCrLf
         txtBeg += "   Else" + vbCrLf
         txtBeg += "      Console.WriteLine(""ВНИМАНИЕ! Стек переполнен!!!"")" + vbCrLf
         txtBeg += "   End If" + vbCrLf
         txtBeg += "   стек(sp) = arg" + vbCrLf
         txtBeg += "End Sub" + vbCrLf + vbCrLf

         txtBeg += "Sub pop(ByRef arg As Integer)" + vbCrLf
         txtBeg += "   arg = стек(sp)" + vbCrLf
         txtBeg += "   If (sp-1)>=0 Then" + vbCrLf
         txtBeg += "      sp -=1" + vbCrLf
         txtBeg += "   Else" + vbCrLf
         txtBeg += "      Console.WriteLine(""ВНИМАНИЕ! Стек пустой!!!"")" + vbCrLf
         txtBeg += "   End If" + vbCrLf
         txtBeg += "End Sub" + vbCrLf + vbCrLf

         txtOut += "Sub Main()" + vbCrLf
      End Sub

      Sub Подвал()
         For i As Integer = 0 To цПерем - 1
            txtBeg += "Dim " + перем(i) + " As Integer = 0" + vbCrLf
         Next
         txtOut += "Console.WriteLine(""Result: "" + Str(рег0))" + vbCrLf
         txtOut += "End Sub" + vbCrLf
         txtOut += "End Module" + vbCrLf
         txtOut += "End Namespace"
         txtOut = txtBeg + txtOut
         Вых_Записать()
         Транслировать()
      End Sub

      Public Sub Компилировать()
         Заголовок()
         Подвал()
      End Sub
   End Module
   Public Module модКомпиль2
      ' ==================== ПРАВИЛА ============================
      Dim tagc As Integer = 0 ' текущий тег на анализе
      Dim sRes As String = "" ' результат анализа
      Dim mLex() As clsLex
      Dim txtLine() As String ' список строк исходника
      Dim prog As clsModule ' объект главного модуля есть программ
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
      Function Смещ(ind As Integer) As String
         Dim s As String = "^"
         For i As Integer = 1 To ind - 1
            s = " " + s
         Next
         Return s
      End Function
      Sub ОшибкаИмени(msg As String, t As Integer)
         модКокон.Ошибка(msg + ":" + t.ToString() + " >" + mLex(t).strTag + "<")
         модКокон.Ошибка("Крд: " + Str(mLex(t).coord.iStr) + " -" + Str(mLex(t).coord.iPos))
         Console.WriteLine(txtLine(mLex(t).coord.iStr))
         Console.WriteLine(Смещ(mLex(t).coord.iPos))
         модКокон.Ошибка("Имя должно начинаться с буквы или ""_""")
         Environment.Exit(1)
      End Sub
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
               If mLex(i).strTag = "END" And modUtil.ЕслиИмя(mLex(i + 1).strTag) = "" And
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
                  If modUtil.ЕслиИмя(mLex(tagc).strTag) <> "" Then
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
                  If modUtil.ЕслиИмя(mLex(tagc + 2).strTag) <> "" Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc + 2)
                  End If
                  ' проверка алиаса
                  If modUtil.ЕслиИмя(mLex(tagc).strTag) <> "" Then
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
      Sub Пр_КОНСТ()
         ' Проверяет правильность объявления констант
         If sRes = "const" Then ' Правило объявления инструкции CONST
            If mLex(tagc).strTag <> "CONST" Then ' возможно просто нет такой секции
               tagc += 1
               sRes = "4.1"
               Exit Sub
            Else ' такая инструкция есть, сразу должно идти имя. Остальное запрещено
               tagc += 1
               If modUtil.ЕслиИмя(mLex(tagc).strTag) = "" Then
                  sRes = "3.2"
               Else
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
               If modUtil.ЕслиИмя(mLex(tagc).strTag) <> "" Then
                  ОшибкаИмени("Константы:", tagc)
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
      Sub Правила()
         ' проверить правильность полученного исходного текста
         sRes = "comment"
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
         Пр_МОДУЛЬ()
         Console.WriteLine("Len(mLex)=" + mLex.Length.ToString)
         Пр_ИМПОРТ()
         Пр_КОНСТ()
      End Sub
      Public Sub Компилировать()
         ' нарезать колбасу из исхдника с присовением координат
         Console.WriteLine("Разметка тегов")
         modTagger.Тег_Разметить()
         Console.WriteLine("All tags:" + Str(modTagger.tags.Length))
         Dim i As Integer = 0
         Do While i < modTagger.tags.Length - 12
            Console.Write(Str(i) + ")" + modTagger.tags(i).strTag + vbTab)
            i += 1
            Console.Write(Str(i) + ")" + modTagger.tags(i).strTag + vbTab)
            i += 1
            Console.WriteLine(Str(i) + ")" + modTagger.tags(i).strTag)
            i += 1
         Loop
         Console.WriteLine("Копирование структур")
         Структуры_Копировать()
         ' создать объект програмы и упаковать его
         prog = New clsModule()
         Console.WriteLine("Отработать правила")
         Правила()
         i = 0
         Do While i < mLex.Length - 1 And i < 20
            Console.WriteLine(Str(i) + ": " + mLex(i).strTag)
            i += 1
         Loop
         Console.Read()
         Console.WriteLine("_end_")

      End Sub
   End Module
End Namespace