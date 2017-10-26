' Главный модуль компилятора
' TODO: Для анализатора -- надо проверять, чтобы имена не начинались  с цифер
Imports System.IO
Imports System.Diagnostics

Namespace пиОк
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
   Public Class clsLexem
      Inherits клсТег
      Public type_ As String = "" ' тип тега
      Public name_alias As String = "" ' алиас для имени модуля
      Public name_origin As String = "" ' настоящее имя модуля
   End Class
   Public Class clsImport ' содержит имена модулей для имопрта и их алиасы
      Public name As String = ""
      Public alias_ As String = ""
   End Class
   Public Class clsModule ' Описывает модуль целиком
      Inherits clsLexem
      Public tag_end As Integer = 0 'номер последнего значимого тега
      Public level As Integer = 0 ' 0 -- это главный
      Public loaded As Boolean
      Public import() As clsImport  ' Список модулей импорта
      Public proc As Integer
   End Class
   Public Module модКомпиль2
      ' ==================== ПРАВИЛА ============================
      Dim tagc As Integer = 0 ' текущий тег на анализе
      Dim sRes As String = "" ' результат анализа
      Dim lex() As clsLexem
      Dim txtLine() As String ' список строк исходника
      Dim prog As clsModule ' объект главного модуля есть программа
      Sub Структуры_Копировать()
         Dim i As Integer = 0
         Dim lex_ As clsLexem
         If Not IsNothing(модТеггер.теги) Then
            Do While (i < модТеггер.теги.Length)
               lex_ = New clsLexem With {
                   .цСтр = модТеггер.теги(i).цСтр,
                   .цПоз = модТеггер.теги(i).цПоз,
                   .стрТег = модТеггер.теги(i).стрТег
               }
               ReDim Preserve lex(i)
               lex(i) = lex_
               i += 1
            Loop
         End If
         ReDim модТеггер.теги(0)

         i = 0
         If Not IsNothing(модТеггер.txtLine) Then
            Do While i <= модТеггер.гцСтр
               If IsNothing(txtLine) Then
                  ReDim txtLine(0)
               Else
                  ReDim Preserve txtLine(i + 1)
               End If

               txtLine(i) = модТеггер.txtLine(i)
               i += 1
            Loop
         End If

         ReDim модТеггер.txtLine(0)

      End Sub
      Function Смещ(ind As Integer) As String
         Dim s As String = "^"
         For i As Integer = 1 To ind - 1
            s = " " + s
         Next
         Return s
      End Function
      Sub ОшибкаИмени(msg As String, t As Integer)
         модКокон.Ошибка(msg + ":" + Str(t) + " >" + Str(lex(t).цСтр) + "<")
         модКокон.Ошибка("Крд: " + Str(lex(t).цСтр) + " -" + Str(lex(t).цПоз))
         Console.WriteLine(txtLine(lex(t).цСтр))
         Console.WriteLine(Смещ(lex(t).цПоз))
         модКокон.Ошибка("Имя должно начинается с буквы или ""_""")
      End Sub
      Sub Пр_КОММЕНТАРИЙ()
         ' правило ищет комметарии и иключает их из кода
         Dim count As Integer = 0
         Dim bStrip As Boolean
         Dim tmpLex() As clsLexem = Nothing
         Dim i As Integer = 0
         If sRes = "comment" Then
            Do While count < lex.Length
               If lex(count).стрТег = "(*" Then ' начало коммента
                  bStrip = True
               End If
               If bStrip = True Then ' пропускаем комментарий
                  If lex(count).стрТег = "*)" Then
                     bStrip = False
                  End If
                  count += 1
               Else ' копирование остального
                  If Not IsNothing(lex(count)) Then
                     If IsNothing(txtLine) Then
                        ReDim tmpLex(0)
                     Else
                        ReDim Preserve tmpLex(i)
                     End If

                     tmpLex(i) = lex(count)
                     i += 1
                     count += 1
                  End If
               End If
            Loop

            lex = tmpLex
            tmpLex = Nothing

            If bStrip = True Then
               модКокон.Ошибка("Крд: " + Str(lex(lex.Length - 1).цСтр + 1) + " -" + Str(lex(lex.Length - 1).цПоз))
               Console.WriteLine(txtLine(lex(lex.Length - 1).цСтр + 1))
               Console.WriteLine(Смещ(lex(lex.Length - 1).цПоз))
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
            If lex(0).стрТег = "MODULE" Then
               ' открываем наш модуль
               prog.loaded = True
               sRes = "1.2"
            Else
               модКокон.Ошибка("Крд: " + Str(lex(0).цСтр) + " -" + Str(lex(0).цПоз))
               Console.WriteLine(txtLine(lex(0).цСтр))
               Console.WriteLine(Смещ(lex(0).цПоз))
               модКокон.Ошибка("Модуль должен начинаться с ""MODULE""")
               sRes = "err"
               Exit Sub
            End If
         End If
         '1.2 У модуля должно быть имя
         If sRes = "1.2" Then
            ' имя модуля -- № 1, разделитель -- № 2
            If lex(2).стрТег <> ";" Then ' пропущено имя модуля
               модКокон.Ошибка("Крд: " + Str(lex(1).цСтр) + " -" + Str(lex(1).цПоз))
               Console.WriteLine(txtLine(lex(1).цСтр))
               Console.WriteLine(Смещ(lex(1).цПоз))
               модКокон.Ошибка("Пропущено имя модуля или разделитель")
               sRes = "err"
               Exit Sub
            End If
            ' проверка на допустимое имя. Должно начинаться либо с "_"  либо с буквы
            If модУтиль.ЕслиНачИмени(Mid(lex(1).стрТег, 1, 1)) Then
               prog.стрТег = lex(1).стрТег
               sRes = "1.3"
            Else
               ОшибкаИмени("MODULE", 1)
               sRes = "err"
               Exit Sub
            End If
         End If
         ' 1.3 У Модуля должно быть окончание
         If sRes = "1.3" Then
            Dim bEnd As Boolean = False
            Dim i As Integer = 3 ' начинаем отсчёт сразу за определением модуля
            Do While i < lex.Count
               If lex(i).стрТег = "END" Then ' относится ли это к концу модуля?
                  If lex(i + 2).стрТег = "." Then ' конец ли это? i+2 -- через имя
                     bEnd = True
                     ' отбрасываем лишние тэги
                     ' ограничивать будем полем структуры программы
                     prog.tag_end = i + 2
                     sRes = "1.4"
                     Exit Do
                  End If
               End If
               i += 1
            Loop
            If Not bEnd Then 'а конца то нет!! работаем с последним тегом
               модКокон.Ошибка("Крд: " + Str(lex(lex.Count - 1).цСтр) + " -" + Str(lex(lex.Count - 1).цПоз))
               Console.WriteLine(txtLine(lex(lex.Count - 1).цСтр))
               Console.WriteLine(Смещ(lex(lex.Count - 1).цПоз))
               модКокон.Ошибка("Модуль должен иметь ""END <NameModule.>""")
               sRes = "err"
            End If
         End If
         '1.4 Модуль должен быть один
         If sRes = "1.4" Then
            ' Организуем цикл в поиске МОДУЛЬ с учётом, что это может быть строка
            ' Интересует только первая тотальная встреча
            Dim i As Integer = 1 ' нельзя брать 0, так как это и есть модуль
            Dim bKw As Boolean = True
            Do While i < prog.tag_end  ' последние тэги мы уже выяснили
               If lex(i).стрТег = "MODULE" Then 'надо выясить, может это часть выражения, или строка
                  If (lex(i - 1).стрТег = ".") Then
                     bKw = False ' часть сущности
                  ElseIf lex(i - 1).стрТег = """" And lex(i + 1).стрТег = """" Then
                     bKw = False ' строка
                  ElseIf lex(i - 1).стрТег = "'" And lex(i + 1).стрТег = "'" Then
                     bKw = False ' строка
                  Else ' да. Это не строка, и не часть сущности!!
                     bKw = True
                     модКокон.Ошибка("Крд: " + Str(lex(i).цСтр + 1) + " -" + Str(lex(i).цПоз))
                     Console.WriteLine(txtLine(lex(i).цСтр + 1))
                     Console.WriteLine(Смещ(lex(i).цПоз))
                     модКокон.Ошибка("MODULE должен быть один")
                     sRes = "err"
                     Exit Sub
                  End If
               End If
               i += 1
            Loop
            If i = prog.tag_end Then
               bKw = False
               sRes = "1.5"
            End If
         End If
         ' 1.5 Имя модуля и имя конца должны совпадать
         If sRes = "1.5" Then
            If prog.стрТег <> lex(prog.tag_end - 1).стрТег Then
               ' это залёт!
               Console.WriteLine(txtLine(lex(0).цСтр))
               Console.WriteLine(txtLine(lex(prog.tag_end - 1).цСтр))
               модКокон.Ошибка("Имя модуля несогласовано")
               sRes = "err"
               Exit Sub
            Else
               sRes = "import"
            End If
         End If
      End Sub
      Sub Импорт_Ошибка()
         модКокон.Ошибка("Крд: " + Str(lex(tagc).цСтр) + " -" + Str(lex(tagc).цПоз))
         Console.WriteLine(txtLine(lex(tagc).цСтр))
         Console.WriteLine(Смещ(lex(tagc).цПоз))
         модКокон.Ошибка("Нарушение порядка импорта>")
         sRes = "err"
      End Sub
      Sub Пр_ИМПОРТ()
         ' прочесали модуль, теперь проверить нет ли импорта
         If sRes = "import" Then ' 2.1 IMPORT может идти тегом № 3 -- проверяем
            If lex(3).стрТег = "IMPORT" Then
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
               If lex(tagc + 1).стрТег = "," Or lex(tagc + 1).стрТег = ";" Then ' Первая ветка -- прямой импорт
                  ' проверить имя модуля и алиас на допустимость
                  If Not модУтиль.ЕслиНачИмени(Mid(lex(tagc).стрТег, 1, 1)) Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc + 1)
                     sRes = "err"
                     Exit Sub
                  End If
                  If IsNothing(prog.import) Then
                     ReDim prog.import(0)
                  Else
                     ReDim Preserve prog.import(prog.import.Length + 1)
                  End If
                  Dim imp As clsImport = New clsImport With {
                  .name = lex(tagc).стрТег,
                  .alias_ = lex(tagc).стрТег
                  }
                  prog.import(prog.import.Length - 1) = imp
                  If lex(tagc + 1).стрТег = "," Then 'импорт может закончился?
                     tagc += 2
                     Continue Do
                  ElseIf lex(tagc + 1).стрТег = ";" Then ' импорт закончить
                     tagc += 2
                     sRes = "2.3"
                     Exit Do
                  Else ' а это уже ошибка!!
                     Импорт_Ошибка()
                     Exit Sub
                  End If
               ElseIf lex(tagc + 1).стрТег = ":=" Then ' вторая ветка -- импорт с алиасом
                  ' проверка имени
                  If Not модУтиль.ЕслиНачИмени(Mid(lex(tagc + 2).стрТег, 1, 1)) Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc + 2)
                     sRes = "err"
                     Exit Sub
                  End If
                  ' проверка алиаса
                  If Not модУтиль.ЕслиНачИмени(Mid(lex(tagc).стрТег, 1, 1)) Then
                     '  неправильное имени
                     ОшибкаИмени("IMPORT", tagc + 1)
                     sRes = "err"
                     Exit Sub
                  End If
                  ' значит добавить элемент импрота
                  If IsNothing(prog.import) Then
                     ReDim prog.import(0)
                  Else
                     ReDim Preserve prog.import(prog.import.Length + 1)
                  End If
                  Dim imp As clsImport = New clsImport With {
                  .name = lex(tagc + 2).стрТег,
                  .alias_ = lex(tagc).стрТег
                  }
                  prog.import(prog.import.Length - 1) = imp
                  ' проверка на продолжение
                  If lex(tagc + 3).стрТег = "," Then 'импорт может закончился?
                     tagc += 4
                     Continue Do
                  ElseIf lex(tagc + 3).стрТег = ";" Then ' импорт закончить
                     tagc += 4
                     sRes = "2.3"
                     Exit Do
                  Else ' а это уже ошибка!!
                     Импорт_Ошибка()
                     sRes = "err"
                     Exit Sub
                  End If
               Else
                  Импорт_Ошибка()
                  sRes = "err"
                  Exit Sub
               End If
            Loop
         End If
         ' 2.3 Проверка на ошибку досрочного окончания импорта
         If sRes = "2.3" Then
            If (lex(tagc).стрТег = "CONST" Or lex(tagc).стрТег = "TYPE" Or
                  lex(tagc).стрТег = "VAR" Or lex(tagc).стрТег = "PROCEDURE" Or
                  lex(tagc).стрТег = "BEGIN") Or
                  (lex(prog.tag_end - 2).стрТег = lex(tagc).стрТег And
                  lex(prog.tag_end).стрТег = ".") Then
               ' здесь вообще варианты. Но все дальше должны проверить
               sRes = "const"
            Else
               tagc -= 1
               Импорт_Ошибка()
               модКокон.Ошибка("Досрочное прекращение импорта")
               sRes = "err"
               Exit Sub
            End If
         End If
      End Sub
      Sub Пр_КОНСТ()
         ' Проверяет правильность объявления констант
         If sRes = "const" Then ' Правило объявления инструкции CONST
            If lex(tagc).стрТег <> "CONST" Then ' возможно просто нет такой секции
               sRes = "4.1"
               Exit Sub
            Else ' такая инструкция есть
               sRes = "3.2"
               tagc += 1
            End If
         End If
         If sRes = "3.2" Then ' начинаем разбор констант
            Do
               ' секция CONST может быть пустой
               If lex(tagc).стрТег = "TYPE" Or lex(tagc).стрТег = "VAR" Or
                     lex(tagc).стрТег = "PROCEDURE" Or lex(tagc).стрТег = "BEGIN" Or
                     (lex(tagc).стрТег = "END" And lex(tagc + 2).стрТег = ".") Then
                  tagc += 1
                  sRes = "4.1"
                  Exit Do
               End If
               If ЕслиВнутрТег(Mid(lex(tagc).стрТег, 1, 1)) <> модТеггер.multitag Then ' имя не может быть пустым
                  модКокон.Ошибка("Крд: " + Str(lex(tagc).цСтр) + " -" + Str(lex(tagc).цПоз))
                  Console.WriteLine(txtLine(lex(tagc).цСтр))
                  Console.WriteLine(Смещ(lex(tagc).цПоз))
                  модКокон.Ошибка("Пропущено имя константы")
                  sRes = "err"
                  Exit Do
               Else
                  lex(tagc).type_ = "const_name"
                  tagc += 1
                  If lex(tagc).стрТег <> "=" Then
                     модКокон.Ошибка("Крд: " + Str(lex(tagc).цСтр) + " -" + Str(lex(tagc).цПоз))
                     Console.WriteLine(txtLine(lex(tagc).цСтр))
                     Console.WriteLine(Смещ(lex(tagc).цПоз))
                     модКокон.Ошибка("Нарушение присовения константы")
                     sRes = "err"
                     Exit Do
                  Else
                     lex(tagc).type_ = "="
                     tagc += 1
                     If lex(tagc).стрТег = "" Then ' Константа не может быть пустой
                        модКокон.Ошибка("Крд: " + Str(lex(tagc).цСтр) + " -" + Str(lex(tagc).цПоз))
                        Console.WriteLine(txtLine(lex(tagc).цСтр))
                        Console.WriteLine(Смещ(lex(tagc).цПоз))
                        модКокон.Ошибка("Нет значения для присвоения константы")
                        sRes = "err"
                        Exit Do
                     Else
                        lex(tagc).type_ = "const_value"
                        tagc += 1
                        If lex(tagc).стрТег <> ";" Then ' Константа не может быть пустой
                           модКокон.Ошибка("Крд: " + Str(lex(tagc - 1).цСтр) + " -" + Str(lex(tagc - 1).цПоз))
                           Console.WriteLine(txtLine(lex(tagc - 1).цСтр))
                           Console.WriteLine(Смещ(lex(tagc - 1).цПоз))
                           модКокон.Ошибка("Нет ограничения присвоения константы")
                           sRes = "err"
                           Exit Do
                        Else
                           lex(tagc).type_ = ";"
                           tagc += 1
                        End If
                     End If
                  End If
               End If
            Loop
         End If
      End Sub
      Sub Правила()
         ' проверить правильность полученного исходного текста
         sRes = "comment"
         Пр_КОММЕНТАРИЙ()
         Пр_МОДУЛЬ()
         Пр_ИМПОРТ()
         Пр_КОНСТ()
      End Sub
      Public Sub Компилировать()
         ' нарезать колбасу из исхдника с присовением координат
         Debug.WriteLine("Разметка тегов")
         модТеггер.Тег_Разметить()
         Console.WriteLine("Копирование структур")
         Структуры_Копировать()
         ' создать объект програмы и упаковать его
         prog = New clsModule()
         Console.WriteLine("Отработать правила")
         Правила()
         Dim i As Integer = 0
         Do While i < lex.Length - 1 And i < 20
            Console.WriteLine(Str(i) + ": " + lex(i).стрТег)
            i += 1
         Loop
         Console.WriteLine("_end_")

      End Sub
   End Module
End Namespace