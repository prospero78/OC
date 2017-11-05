Namespace пиОк
   ''' <summary>
   '''  Тип предоставляет одно звено в цепи звеньев
   ''' </summary>
   Public Class туЗвено
      Public звПред As туЗвено ' предыдущее звено
      Public звСлед As туЗвено ' следующее звено
      Public Sub New(Optional пЗвПред As туЗвено = Nothing, Optional пЗвСлед As туЗвено = Nothing)
         Me.звПред = пЗвПред
         Me.звСлед = пЗвСлед
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
   ''' <summary>
   ''' Модуль, скорей всего тут не нужен.
   ''' Интерес представляет из себя только класс.
   ''' </summary>
   Module модЦепь

   End Module
End Namespace

