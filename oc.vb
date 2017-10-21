Namespace nsOc
public class clsWinMain
end class
module modOc
   Dim FoneColor as System.ConsoleColor
   Sub Color_Write(ByRef txt As String, Optional color as integer = 15)
      Console.BackgroundColor = System.ConsoleColor.DarkBlue
      Console.WriteLine(txt)
      Console.BackgroundColor = FoneColor
   End Sub
   Sub main()
      Console.Clear()
      FoneColor = Console.BackgroundColor
       
      'Console.ForegroundColor = 
      Console.Title = "Oberon-07 Compiler"
      'Console.writeline("Oberon-07 compiler", System.ConsoleColor.Gray)
      Color_Write("Oberon-07 compiler")
      Console.BackgroundColor = System.ConsoleColor.Black
      Console.ResetColor()
      Console.WriteLine()
      Console.Write(">")
      Console.Read()
   end sub
end module
end namespace
