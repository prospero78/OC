Public Class окнГлавное
   Private Sub СохраитьToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles СохраитьToolStripMenuItem.Click

   End Sub

   Private Sub ОткрытьToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ОткрытьToolStripMenuItem.Click
      Dim имя_файла As String
      длгФайлОткрыть.ShowDialog()
      имя_файла = длгФайлОткрыть.FileName
      Dim Txt As String() = IO.File.ReadAllLines(имя_файла, System.Text.Encoding.UTF8)
      Dim txtFileO7 As String

      Using sr As System.IO.StreamReader = System.IO.File.OpenText(имя_файла)
         txtFileO7 = sr.ReadToEnd()
      End Using
      текстИсходник.Text = txtFileO7
      'Dim i As Integer = 0
      'Do While i < Txt.Length
      'текстИсходник.Text += Txt(i) + vbCrLf
      'i += 1
      'Loop
   End Sub

   Private Sub ВыходToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ВыходToolStripMenuItem.Click
      Environment.Exit(0)
   End Sub

   Private Sub ОПрограммеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ОПрограммеToolStripMenuItem.Click
      окнИнфо.Show()
   End Sub

   Private Sub окнГлавное_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Me.Text = "Компилятор Оберон-07 сборка " + My.Application.Info.Version.Build.ToString()

   End Sub

   Private Sub FastColoredTextBox1_Load(sender As Object, e As EventArgs) Handles FastColoredTextBox1.Load

   End Sub
End Class