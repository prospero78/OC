Namespace пиОк
   ''' <summary>
   ''' Модуль, скорей всего тут не нужен.
   ''' Интерес представляет из себя только класс.
   ''' </summary>
   Module модЦепь
      ''' <summary>
      '''  Тип предоставляет одно звено в цепи звеньев
      ''' </summary>
      Public Class туЗвено
         Public ReadOnly звПред As туЗвено ' предыдущее звено
         Public ReadOnly звСлед As туЗвено ' следующее звено
         Public Sub New()
            Me.звПред = Nothing
            Me.звСлед = Nothing
         End Sub
      End Class
      ''' <summary>
      '''  Тип предоставляет возможность манипуляции с цепями.
      ''' </summary>
      Public Class туЦепь
         Public ReadOnly звПервое As туЗвено
         Public Sub New()
            Me.звПервое = Nothing
         End Sub
      End Class
   End Module
End Namespace

