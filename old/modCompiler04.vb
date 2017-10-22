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
            ВыводНов("Dim " + имя + " As Integer")
            Перем_Добав(имя)
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
         ВыводНов("Dim " + имя + " As Integer")
         Перем_Добав(имя)
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
      
      Sub Перем_Добав(имя As String)
         If цПерем<1000 Then
            перем(цПерем) = "Dim " + имя + " As Integer = 0"
            цПерем += 1
         Else
            модКокон.Ошибка("Слишком много переменных!")
         End If
      End Sub
      
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
         For i As Integer=0 To цПерем
            txtBeg += перем(i) + vbCrLf
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
End Namespace