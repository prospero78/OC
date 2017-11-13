Namespace пиОк
   ''' <summary>
   '''  Тип предоставляет одно звено в цепи звеньев
   ''' </summary>
   Public Class туЗвено
      Dim _уПред As туЗвено
      ''' <summary>
      '''  Предыдущий указатель в звене
      ''' </summary>
      Public ReadOnly Property уПред As туЗвено ' предыдущее звено
         Get
            Return Me._уПред
         End Get
      End Property
      Public Sub уПред_Уст(пуПред As туЗвено)
         Me._уПред = пуПред
      End Sub

      Dim _уСлед As туЗвено
      ''' <summary>
      ''' Возвращает указатель на следующий элемент
      ''' </summary>
      ''' <returns></returns>
      Public ReadOnly Property уСлед As туЗвено ' следующее звено
         Get
            Return Me._уСлед
         End Get
      End Property
      Public Sub уСлед_Уст(пуСлед As туЗвено)
         Me._уСлед = пуСлед
      End Sub

      Shared _цВсего As Integer = 0
      Public Shared ReadOnly Property цВсего As Integer
         Get
            Return _цВсего
         End Get
      End Property

      Public Sub New(Optional пуПред As туЗвено = Nothing, Optional пуСлед As туЗвено = Nothing)
         Me._уПред = пуПред
         Me._уСлед = пуСлед
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

