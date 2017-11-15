<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class окнГлавное
   Inherits System.Windows.Forms.Form

   'Форма переопределяет dispose для очистки списка компонентов.
   <System.Diagnostics.DebuggerNonUserCode()> _
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Является обязательной для конструктора форм Windows Forms
   Private components As System.ComponentModel.IContainer

   'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
   'Для ее изменения используйте конструктор форм Windows Form.  
   'Не изменяйте ее в редакторе исходного кода.
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(окнГлавное))
      Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
      Me.ФайлToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.ОткрытьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.СохраитьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.ЗакрытьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
      Me.ВыходToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.длгФайлОткрыть = New System.Windows.Forms.OpenFileDialog()
      Me.ПомощьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.СправкаToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
      Me.ОПрограммеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.MenuStrip1.SuspendLayout()
      Me.SuspendLayout()
      '
      'MenuStrip1
      '
      Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ФайлToolStripMenuItem, Me.ПомощьToolStripMenuItem})
      Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
      Me.MenuStrip1.Name = "MenuStrip1"
      Me.MenuStrip1.Size = New System.Drawing.Size(862, 24)
      Me.MenuStrip1.TabIndex = 0
      Me.MenuStrip1.Text = "MenuStrip1"
      '
      'ФайлToolStripMenuItem
      '
      Me.ФайлToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ОткрытьToolStripMenuItem, Me.СохраитьToolStripMenuItem, Me.ЗакрытьToolStripMenuItem, Me.ToolStripMenuItem1, Me.ВыходToolStripMenuItem})
      Me.ФайлToolStripMenuItem.Name = "ФайлToolStripMenuItem"
      Me.ФайлToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
      Me.ФайлToolStripMenuItem.Text = "&Файл"
      '
      'ОткрытьToolStripMenuItem
      '
      Me.ОткрытьToolStripMenuItem.Name = "ОткрытьToolStripMenuItem"
      Me.ОткрытьToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
      Me.ОткрытьToolStripMenuItem.Text = "&Открыть"
      '
      'СохраитьToolStripMenuItem
      '
      Me.СохраитьToolStripMenuItem.Name = "СохраитьToolStripMenuItem"
      Me.СохраитьToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
      Me.СохраитьToolStripMenuItem.Text = "&Сохранить"
      '
      'ЗакрытьToolStripMenuItem
      '
      Me.ЗакрытьToolStripMenuItem.Name = "ЗакрытьToolStripMenuItem"
      Me.ЗакрытьToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
      Me.ЗакрытьToolStripMenuItem.Text = "&Закрыть"
      '
      'ToolStripMenuItem1
      '
      Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
      Me.ToolStripMenuItem1.Size = New System.Drawing.Size(149, 6)
      '
      'ВыходToolStripMenuItem
      '
      Me.ВыходToolStripMenuItem.Name = "ВыходToolStripMenuItem"
      Me.ВыходToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
      Me.ВыходToolStripMenuItem.Text = "&Выход"
      '
      'длгФайлОткрыть
      '
      Me.длгФайлОткрыть.DefaultExt = "Оберон-07;*.o7"
      Me.длгФайлОткрыть.Filter = "Оберон-07(*.o7)|*.o7"
      Me.длгФайлОткрыть.Title = "Открыть модуль Оберон-07"
      '
      'ПомощьToolStripMenuItem
      '
      Me.ПомощьToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.СправкаToolStripMenuItem, Me.ToolStripMenuItem2, Me.ОПрограммеToolStripMenuItem})
      Me.ПомощьToolStripMenuItem.Name = "ПомощьToolStripMenuItem"
      Me.ПомощьToolStripMenuItem.Size = New System.Drawing.Size(68, 20)
      Me.ПомощьToolStripMenuItem.Text = "Помощь"
      '
      'СправкаToolStripMenuItem
      '
      Me.СправкаToolStripMenuItem.Name = "СправкаToolStripMenuItem"
      Me.СправкаToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
      Me.СправкаToolStripMenuItem.Text = "Справка"
      '
      'ToolStripMenuItem2
      '
      Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
      Me.ToolStripMenuItem2.Size = New System.Drawing.Size(149, 6)
      '
      'ОПрограммеToolStripMenuItem
      '
      Me.ОПрограммеToolStripMenuItem.Name = "ОПрограммеToolStripMenuItem"
      Me.ОПрограммеToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
      Me.ОПрограммеToolStripMenuItem.Text = "О программе"
      Me.ОПрограммеToolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
      '
      'окнГлавное
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(862, 507)
      Me.Controls.Add(Me.MenuStrip1)
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.MainMenuStrip = Me.MenuStrip1
      Me.Name = "окнГлавное"
      Me.Text = "Компилятор Оберон-07"
      Me.MenuStrip1.ResumeLayout(False)
      Me.MenuStrip1.PerformLayout()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub

   Friend WithEvents MenuStrip1 As MenuStrip
   Friend WithEvents ФайлToolStripMenuItem As ToolStripMenuItem
   Friend WithEvents ОткрытьToolStripMenuItem As ToolStripMenuItem
   Friend WithEvents СохраитьToolStripMenuItem As ToolStripMenuItem
   Friend WithEvents ЗакрытьToolStripMenuItem As ToolStripMenuItem
   Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
   Friend WithEvents ВыходToolStripMenuItem As ToolStripMenuItem
   Friend WithEvents длгФайлОткрыть As OpenFileDialog
   Friend WithEvents ПомощьToolStripMenuItem As ToolStripMenuItem
   Friend WithEvents СправкаToolStripMenuItem As ToolStripMenuItem
   Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
   Friend WithEvents ОПрограммеToolStripMenuItem As ToolStripMenuItem
End Class
