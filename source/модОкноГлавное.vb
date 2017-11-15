Public Class окнГлавное
   Private Sub СохраитьToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles СохраитьToolStripMenuItem.Click

   End Sub

   Private Sub ОткрытьToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ОткрытьToolStripMenuItem.Click
      Me.длгФайлОткрыть.ShowDialog()
   End Sub

   Private Sub ВыходToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ВыходToolStripMenuItem.Click
      Environment.Exit(0)
   End Sub
End Class