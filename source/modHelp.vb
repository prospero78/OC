'Модуль справки для компилятора
Namespace пиОк
   Public Module модСправка
   Const txtHelp As String = vbCrLf & "Справка по использованию консольного компилятора:" & vbCrLf & _
                                      "                                                 " & vbCrLf & _
                                      "   /h    - показать эту справку                  " & vbCrLf & _
                                      "   /m:<module>.o7    - модуль для комиляции      " & vbCrLf & _
                                      "                                                 " & vbCrLf
                              

      Sub Справа_Показать()
         модКокон.Инфо(txtHelp)
      End Sub
   End Module

End Namespace