' Главный модуль компилятора
Imports System.IO
Imports System.Diagnostics

Namespace пиОк
   Public Module модКомпиль
      Dim литАнализ As String = "" ' Look литера для анализа
      Dim txtBeg As String = "" ' Начало текст Visual Basic
      Dim txtOut As String = "" ' Конец текст Visual Basic
      Dim перем(1000) As String ' Массив добавляемых переменных
      Dim цПерем As Integer=0 'Свободный элемент массива
      
      Sub Лит_Получ() 'GetChar получение символа из входного потока
         литАнализ = Chr(Console.Read())
         End Sub
      
      Sub Ошибка(ByRef msg As String) ' Error вывод сообщения об ошибке
         Console.WriteLine()
         модКокон.Ошибка("   Ошибка: " + msg)
         End Sub
      
      Sub Прервать(ByRef msg As String) '  Abort Прерывание компиляции
         Ошибка(msg)
         Exit Sub 
         End Sub
      
      Sub Ожидалось(ByRef msg As String) ' Expected
         Прервать("Ожидалось " + msg)
         End Sub
      
      Sub Совпадение(lit As String) '  Math
         If литАнализ = lit Then
            Лит_Получ()
         Else
            Ожидалось("'" + lit + "'")
         End If
         End Sub
      
      Function ЕслиБуква(lit As String) As Boolean '  IsAlpha
         Dim res As Boolean = False
         lit = UCase(lit)
         If InStr("ABCDEFGHIJKLMNOPQRSTUVWXYZ", lit) <> 0 Then
            res = True
         End If
         If InStr("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ", lit)<>0 Then
            res = True
         End If
         Return res
         End Function
      
      Function ЕслиЦифра(lit As String) As Boolean '  IsDigit
         Dim res As Boolean = False
         If InStr("0123456789", lit)<>0 Then
            res = True
         End If
         Return res
         End Function
      
      Function ЕслиИмя(lit As String) As Boolean '  IsAlNum
         Return ЕслиЦифра(lit) Or ЕслиБуква(lit)
         End Function
      
      Function Имя_Получ() As String ' GetName
         If Not ЕслиБуква(литАнализ) Then
            Ожидалось("Имя")
         End If
         Dim lit As String = UCase(литАнализ)
         Лит_Получ()
         Return lit
         End Function
      
      Function Цифра_Получ() As String ' GetNum
         If Not ЕслиЦифра(литАнализ) Then
            Ожидалось("Целое")
         End If
         Dim lit As String = литАнализ
         Лит_Получ()
         Return lit
         End Function
      
      Sub Вывод(ByVal txt As String) ' Emit
         Console.Write(vbTab + txt)
         End Sub
      
      Sub ВыводНов(ByVal txt As String) ' EmitLn
         Вывод(txt)
         Console.Write(vbCrLf)
         End Sub
      
      Sub Сущность() ' Ident
         Dim имя As String = ""
         имя = Имя_Получ()
         If литАнализ = "(" Then ' анализ скобок
            Совпадение("(")
            Совпадение(")")
            ВыводНов(имя+"()")
            txtOut += имя+"()" + "'здесь косяк?2" + vbCrLf
         Else                               ' анализ констант
            If Not Перем_Добав(имя) Then
               ВыводНов("Dim " + имя + " As Integer = 0")
            End If
            ВыводНов("рег0 = " + имя)
            txtOut += "рег0 = " + имя + vbCrLf
         End If
         End Sub
      
      Sub Множитель() ' Factor
         If литАнализ = "(" Then ' анализ скобок
            Совпадение("(")
            Выражение()
            Совпадение(")")
         Else If ЕслиБуква(литАнализ) Then ' анализ имён
            Сущность()
         Else                               ' анализ констант
            Dim lit As String = Цифра_Получ()
            ВыводНов("рег0 = " + lit)
            txtOut += "рег0 = " + lit+vbCrLf
         End If
         End Sub
      
      Sub Умножить() ' Multiply
         Совпадение("*")
         Множитель()
         ВыводНов("pop(head)"+vbCrLf + _
                  vbTab + "рег0 *= head")
         txtOut += "pop(head)"+vbCrLf + _
                  "рег0 *= head"+vbCrLf
         End Sub
      
      Sub Разделить() ' Divide
         Совпадение("/")
         Множитель()
         ВыводНов("pop(head)"+vbCrLf + _
                  vbTab + "рег0 = head / рег0")
         txtOut += "pop(head)"+vbCrLf + _
                  "рег0 = head / рег0"+vbCrLf
         End Sub
      
      Sub Терминал() ' Term
         Множитель()
         Do While InStr("*/", литАнализ)>0
            ВыводНов("push(рег0)")
            txtOut += "push(рег0)" + vbCrLf
            Select Case литАнализ
               Case "*"
                  Умножить()
               Case "/"
                  Разделить()
               Case Else
                  Ожидалось("операция * или /")
               End Select
         Loop
         End Sub
      
      Sub Сложить() ' Add
         Совпадение("+")
         Терминал()
         ВыводНов("pop(head)" + vbCrLf + _
                  vbTab + "рег0 += head")
         txtOut += "pop(head)" + vbCrLf + _
                  "рег0 += head" + vbCrLf
         End Sub
      
      Sub Вычесть()' Substract
         Совпадение("-")
         Терминал()
         ВыводНов("pop(head)" + vbCrLf + _
                  vbTab +"рег0 = head - рег0")
         txtOut += "pop(head)" + vbCrLf + _
                  "рег0 = head - рег0" + vbCrLf
         End Sub
      
      Function ЕслиПлюсМинус(lit As String) As Boolean ' IsAddop
         Dim res As Boolean = False
         If InStr("+-", lit)>0 Then
            res = True
         End If
         Return res
         End Function
      
      Sub Выражение() ' Expression
         If ЕслиПлюсМинус(литАнализ) Then
            ВыводНов("рег0 *= 0")
            txtOut += "рег0 *= 0" + vbCrLf
         Else
            Терминал()
         End If
         Do While ЕслиПлюсМинус(литАнализ)
            ВыводНов("push(рег0)")
            ' для выходного файла
            txtOut += "push(рег0)" + vbCrLf
            Select Case литАнализ
               Case "+" 
                  Сложить()
               Case "-" 
                  Вычесть()
               Case Else
                  Ожидалось("операция +/-")
            End Select
         Loop
         End Sub
      
      Sub Присвоение() ' Assigment
         Dim имя As String = Имя_Получ()
         Совпадение("=")
         Выражение()
         ' Здесь надо придумать втыкание перед Sub Main()
         ' операторов вида:
         ' Dim <Name> As String
         If Not Перем_Добав(имя) Then
            ВыводНов("Dim " + имя + " As Integer = 0")
         End If
         ВыводНов(имя+" = рег0")
         ВыводНов("***")
         txtOut += имя+" = рег0"+vbCrlf
         End Sub
      
      Sub Настр() ' Init
         Лит_Получ()
         End Sub
      
      Sub Вых_Записать()
         Using sw As StreamWriter = File.CreateText("out.vb")
               sw.Write(txtOut)
               sw.Close()
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
      
      Function Перем_Добав(имя As String) As Boolean
         Dim bnew As Boolean = False
         If цПерем<1000 Then
            имя = UCase(имя)
            For i As Integer = 0 To цПерем
               If имя = перем(i) Then
                  bnew = True
               End If
               Console.WriteLine(Str(i) + ": """ + перем(i) + """")
            Next
            If Not bnew Then
               перем(цПерем) = имя
               цПерем += 1
            End If
         Else
            модКокон.Ошибка("Слишком много переменных!")
         End If
         return bnew
         End Function
      
      Sub Заголовок()
         Настр()
         txtBeg = "' Автогенерация текста Visual Basic" + vbCrLf
         txtBeg += "' Данные для компиляции" + vbCrLf
         txtBeg +="Namespace Oberon07"+vbCrLf
         txtBeg +="Public Module modOut"+vbCrLf+vbCrLf
         txtBeg +="Dim рег0 As Integer = 0"+vbCrLf
         txtBeg +="Dim рег1 As Integer = 0"+vbCrLf
         txtBeg +="Dim head As Integer = 0"+vbCrLf
         txtBeg +="Dim стек(1000) As Integer' программный стек"+vbCrlf
         txtBeg +="Dim sp As Integer = 0'указатель стека"+vbCrLf+vbCrLf
         txtBeg +="Sub push(arg As Integer)"+vbCrlf
         txtBeg +="   If (sp+1)<1000 Then"+vbCrLf
         txtBeg +="      sp +=1"+vbCrLf
         txtBeg +="   Else"+vbCrLf
         txtBeg +="      Console.WriteLine(""ВНИМАНИЕ! Стек переполнен!!!"")"+vbCrLf
         txtBeg +="   End If"+vbCrLf
         txtBeg +="   стек(sp) = arg"+vbCrLf
         txtBeg +="End Sub" + vbCrLf+vbCrLf
         
         txtBeg +="Sub pop(ByRef arg As Integer)"+vbCrlf
         txtBeg +="   arg = стек(sp)"+vbCrLf
         txtBeg +="   If (sp-1)>=0 Then"+vbCrLf
         txtBeg +="      sp -=1"+vbCrLf
         txtBeg +="   Else"+vbCrLf
         txtBeg +="      Console.WriteLine(""ВНИМАНИЕ! Стек пустой!!!"")"+vbCrLf
         txtBeg +="   End If"+vbCrLf
         txtBeg +="End Sub" + vbCrLf+vbCrLf
         
         txtOut +="Sub Main()"+vbCrLf
         End Sub
      
      Sub Подвал()
         For i As Integer=0 To цПерем - 1
            txtBeg += "Dim " + перем(i) + " As Integer = 0"+ vbCrLf
         Next
         txtOut += "Console.WriteLine(""Result: "" + Str(рег0))" + vbCrLf
         txtOut += "End Sub" + vbCrLf
         txtout += "End Module" + vbCrLf
         txtOut += "End Namespace"
         txtOut = txtBeg + txtOut
         Вых_Записать()
         Транслировать()
         End Sub
         
      Public Sub Компилировать()
         Заголовок()
         Присвоение()
         Подвал()
         End Sub
      End Module
   Public Module модКомпиль2
      ' попытка сделать по уму
      Class клсТэг
         ' хранит в себе последовательно кусочек нераспознанного кода с координатами
         Public цСтр As Integer = 0
         Public цПоз As Integer = 0
         Public стрТег As String = ""
         Public type_ As String = "" ' тип тега
         Public name_alias As String = "" ' алиас для имени модуля
         Public name_origin As String = "" ' настоящее имя модуля
         End class
      Dim теги() As клсТэг
      Dim цТегСчёт As Integer = 0 ' солько всего тэгов
      Dim txtSrc As String ="" ' текст исходника
      Dim txtLine() As String ' исходник разбитый построчно
      Dim countLine As Integer = 0 ' количество строк в массиве
      Dim гцСтр As Integer = 1 ' нумерация строк с 1
      Dim гцПоз As Integer = 0
      Dim гсТег As String = "" ' глобальный текущий тэг
      ' =================== ТЕГИРОВАНИЕ =======================
      Sub Поз_Получ(lit As String)
         Static srcLine As String = "" ' очередная строка исходного кода
         if lit = vbLf Then
            гцСтр += 1
            гцПоз = 0
            ' Добавить строку в массив исходников
            ReDim Preserve txtLine(countLine+1)
            txtLine(countLine) = srcLine
            countLine += 1
            srcLine = ""
         Else
            гцПоз += 1
            srcLine += lit
         End If
         End Sub
      Function ЕслиОтсев(lit As String) As Boolean
         Dim res As Boolean = False
         If lit<=" " Then
            res = True
         End If
         Return res
         End Function
      Sub Тег_Добавить(lit As string)
         'Создать новй тэг
         Dim tag As клсТэг = New клсТэг()
         tag.стрТег = lit
         tag.цСтр = гцСтр
         tag.цПоз = гцПоз
         ReDim Preserve теги(цТегСчёт+1)
         теги(цТегСчёт)=tag
         цТегСчёт += 1
         End Sub
      Function ЕслиВнутрТег(lit As String) As Integer
         Dim res As Integer = 0
         If InStr(",(;)*-+.[""]'", lit) >0 Then
            res = 1
         Else If InStr(":<>", lit)>0 Then
            Console.WriteLine("Сложный тег: "+lit+"?")
            res=2
         End If
         Return res
         End Function
      Sub ТегСложн_Добавить(lit As String)
         Dim lit2 As String = Mid(txtSrc,1,1)
         If InStr("=", lit2)>0 Then
            lit += lit2
            Поз_Получ(lit2)
            txtSrc = Mid(txtSrc,2)
         End If
         Тег_Добавить(lit)
         End Sub
      Sub ТегВнутр_Получ(lit As String)
         ' уже есть тег-литера и его надо распознать
         If ЕслиВнутрТег(lit)=1 Then
            Тег_Добавить(lit)
         Else If ЕслиВнутрТег(lit)=2 Then
               ТегСложн_Добавить(lit)
         End If
         'Позиция тут учтена раньше, можно просто вырезать
         txtSrc = Mid(txtSrc,2)
         End Sub
      Sub Тег_Получ()
         Dim lit As String
         Dim bBreak As Boolean = False
         гсТег=""
         lit = Mid(txtSrc, 1,1)
         Do While (lit>" " And (Len(txtSrc)>1)) ' общий цикл с правильными литерами
            If ЕслиВнутрТег(lit)=0 Then
               гсТег += lit
               txtSrc = Mid(txtSrc, 2)
               lit = Mid(txtSrc, 1,1)
               Поз_Получ(lit)
            Else
               bBreak = True
               Exit Do
            End If
         Loop
         If (Not bBreak) Then
            ' Здесь гцСтр ещё не перешёл на новую строку.
            ' теперь в тэге надо выделить внутренние разделители: ":= ( ) [ ]" . и т. д.
            Тег_Добавить(гсТег)
            гсТег=""
         Else If Len(гсТег)>0 And bBreak Then
            Тег_Добавить(гсТег)
            ТегВнутр_Получ(lit)
         Else If bBreak Then
            ТегВнутр_Получ(lit)
         End If
         End Sub
      Sub Разметить()
         txtSrc = модФайл.txtFileO7 + "  "' хвост нужен, чтобы гарантированно не обрезать тег
         Dim lit As String = ""
         ' сканируем файл вдоль, считаем координаты
         ' по необходимости в массив добавлем тэги
         Do While txtSrc<>""
            lit = Mid(txtSrc,1,1)
            Поз_Получ(lit)' учитываем строку и позицию
            ' фильтруем дурные символы
            If ЕслиОтсев(lit)' отбросим мусор
               txtSrc = Mid(txtSrc, 2)
            Else
               Тег_Получ()
            End If
            Loop
      End Sub
      ' ==================== ПРАВИЛА ============================
      Dim tagc As Integer = 0 ' текущий тег на анализе
      Dim sRes As String = "" ' результат анализа
      Function Смещ(ind As Integer) As String
         Dim s As String ="^"
         For i As Integer = 1 To ind-1
            s = " " + s
         Next
         Return s
         End Function
      Sub Пр_МОДУЛЬ()
         If sRes="1.1" Then ' 1.1 МОДУЛЬ должен быть первым
            If теги(tagc).стрТег<>"MODULE" Then
               модКокон.Ошибка("Ошибка: стр " + Str(теги(tagc).цСтр) + " поз " + Str(теги(tagc).цПоз))
               Console.WriteLine(txtLine(теги(tagc).цСтр-1))
               Console.WriteLine(Смещ(теги(tagc).цПоз))
               модКокон.Ошибка("Модуль должен начинаться с ""MODULE""")
               sRes="err"
            Else
               теги(tagc).type_="MODULE"
               tagc +=1
               sRes = "1.2"
            End If
            End If
         If sRes="1.2" Then ' 1.2 У модуля должно быть имя
            If теги(tagc).стрТег=";" Then ' пропущено имя модуля
               модКокон.Ошибка("Ошибка: стр " + Str(теги(tagc).цСтр) + " поз " + Str(теги(tagc).цПоз))
               Console.WriteLine(txtLine(теги(tagc).цСтр-1))
               Console.WriteLine(Смещ(теги(tagc).цПоз))
               модКокон.Ошибка("Модуль должен иметь имя")
               sRes="err"
            Else
               теги(tagc).type_="module_name"
               теги(tagc+1).type_=";"
               tagc += 2 'Пропускаем ";"
               sRes = "1.3"
            End If
            End If
         If sRes="1.3" Then ' 1.3 У Модуля должно быть окончание
            Dim bEnd As Boolean=False
            Dim i As Integer
            For i = 0 To цТегСчёт-1
               If теги(i).стрТег="END" Then ' относится ли это к концу модуля?
                  If теги(i+2).стрТег="." Then ' конец ли это? i+2 -- через имя
                     теги(i).type_="module_end"
                     теги(i+1).type_="module_name"
                     теги(i+2).type_="module_dot"
                     bEnd=True
                     tagc=i
                     Exit For
                  End If
               End If
               Next
            If bEnd = False Then'а конца то нет!! работаем с последним тегом
               модКокон.Ошибка("Ошибка: стр " + Str(теги(цТегСчёт-1).цСтр) + " поз " + Str(теги(цТегСчёт-1).цПоз))
               Console.WriteLine(txtLine(теги(цТегСчёт-1).цСтр-1))
               Console.WriteLine(Смещ(теги(цТегСчёт-1).цПоз))
               модКокон.Ошибка("Модуль должен иметь ""END <NameModule.>""")
               sRes="err"
            Else
               ' отбрасываем лишние тэги
               ' ограничивать будем счётчиком
               цТегСчёт = tagc+1
               tagc = 4' 1-модуль; 2-имя модуля; 3-";"
               sRes = "1.4"
            End If
            End If
         If sRes="1.4" Then ' 1.4 Модуль должен быть один
            ' Организуем цикл в поиске МОДУЛЬ с учётом, что это может быть строка
            ' Интересует только первая тотальная встреча
            Dim i As Integer = 0 
            Dim bKw As Boolean = True
            For i = 4 To цТегСчёт-2 ' последние тэги мы уже выяснили
               If теги(i).стрТег="MODULE" Then 'надо выясить, может это часть выражения, или строка
                  If (теги(i-1).стрТег=".") Then
                     bKw=False
                  Else If теги(i-1).стрТег="""" And теги(i+1).стрТег="""" Then
                     bKw=False
                  Else If теги(i-1).стрТег="'" And теги(i+1).стрТег="'" Then
                     bKw = False ' это не ключевое слово
                  Else ' да. Это не строка ,и не часть сущности!!
                     Exit For
                  End IF
               End If
            Next
            If i = цТегСчёт-1 Then
               bKw = False
            End If
            If bKw = True Then
               модКокон.Ошибка("Ошибка: стр " + Str(теги(i).цСтр) + " поз " + Str(теги(i).цПоз))
               Console.WriteLine(txtLine(теги(i).цСтр-1))
               Console.WriteLine(Смещ(теги(i).цПоз) + Str(i) + Str(цТегСчёт))
               модКокон.Ошибка("MODULE в модуле должен быть один")
               sRes = "Err"
            ELSE
               sRes = "2.1"
            End If
            End If
         End Sub
      Function Импорт_Конец() As Boolean
         ' Проверяет правильн оли закончился импорт
         Dim bRes As Boolean
         bRes = теги(tagc).стрТег="TYPE" Or теги(tagc).стрТег="CONST" Or теги(tagc).стрТег="VAR"
         bRes = bRes Or теги(tagc).стрТег="PROCEDURE" Or теги(tagc).стрТег="BEGIN"
         bRes = bRes Or теги(tagc).type_="module_end"
         Return bRes
         End Function
      Sub Пр_ИМПОРТ()
         ' прочесали модуль, теперь проверить нет ли импорта
         If sRes="2.1" Then ' 2.1 IMPORT может идти тегом № 3 -- проверяем
            If теги(3).стрТег="IMPORT" Then
               теги(3).type_="import"
               tagc = 3
               sRes = "2.2"
            End If
            End If
         If sRes="2.2" Then ' 2.2 Проверяем весь доступный импорт
            ' Может быть прямой импорт, а может быть и с алиасами.
            ' Если импорт прямой, то tagc+2 будет ";", а сли алиас -- то ":="
            tagc = 4 ' После ИМПОРТ имя файла или алиас по счёту -- 4 тег в файле
            Do While True
               Console.WriteLine("+0 "+теги(tagc).стрТег)
               Console.WriteLine("+1 "+теги(tagc+1).стрТег)
               Console.WriteLine("+2 "+теги(tagc+2).стрТег)
               ' Импортов может быть 
               ' прямой, c алиасом, с запятой (продолжение), с ";" -- конец импорта
               If теги(tagc+1).стрТег="," Or теги(tagc+1).стрТег=";" Then' Первая ветка -- прямой импорт
                  теги(tagc).type_="module"
                  теги(tagc).name_origin = теги(tagc).стрТег
                  tagc += 2 ' утановить счётчик на следующий тег (возможно импорта)
                  If теги(tagc+1).стрТег=";" Then ' импорт закончить
                     Exit Do
                  End If
               Else If теги(tagc+1).стрТег=":=" Then ' вторая ветка -- импорт с алиасом
                  теги(tagc).type_="module_alias"
                  теги(tagc).name_origin = теги(tagc+2).стрТег
                  теги(tagc+2).type_ = "module"
                  теги(tagc+2).name_origin = теги(tagc+2).стрТег
                  tagc += 3 ' утановить счётчик на следующий тег (возможно импорта)
                  If теги(tagc+3).стрТег=";" Then ' импорт закончить
                     Exit Do
                  End If
               End If
            Loop
            If  Импорт_Конец() Then ' Если не "," и не ";" и не ":=" -- возможно нарушение инструкции импорта
               sRes = "2.3"
            Else ' Ошибка импорта
               модКокон.Ошибка("Ошибка: стр " + Str(теги(tagc+2).цСтр) + " поз " + Str(теги(tagc+2).цПоз))
               Console.WriteLine(txtLine(теги(tagc).цСтр-1))
               Console.WriteLine(Смещ(теги(tagc+2).цПоз))
               модКокон.Ошибка("Дожно быть имя модуля для импорта в виде <mName := modFullName;>")
               sRes="err"
            End If
         End If
         End Sub
      Sub Правила()
         sRes="1.1"
         Пр_МОДУЛЬ()
         Пр_ИМПОРТ()
         End Sub
      Public Sub Компилировать()
         ' нарезать колбасу из исхдника с присовением координат
         Разметить()
         For i As Integer = 0 To цТегСчёт-1
            Console.WriteLine(Str(i) +" "+ теги(i).стрТег + " " + теги(i).type_)
         Next
         Exit Sub
         ' проверить правильность полученного исходного текста
         Правила()
         End Sub
      End Module
End Namespace