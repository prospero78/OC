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
      Me.components = New System.ComponentModel.Container()
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(окнГлавное))
      Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
      Me.ФайлToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.ОткрытьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.СохраитьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.ЗакрытьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
      Me.ВыходToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.ПомощьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.СправкаToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
      Me.ОПрограммеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
      Me.длгФайлОткрыть = New System.Windows.Forms.OpenFileDialog()
      Me.TabControl1 = New System.Windows.Forms.TabControl()
      Me.TabPage2 = New System.Windows.Forms.TabPage()
      Me.TabPage1 = New System.Windows.Forms.TabPage()
      Me.TabPage3 = New System.Windows.Forms.TabPage()
      Me.текстРезультат = New System.Windows.Forms.RichTextBox()
      Me.текстИсходник = New System.Windows.Forms.RichTextBox()
      Me.FastColoredTextBox1 = New FastColoredTextBoxNS.FastColoredTextBox()
      Me.MenuStrip1.SuspendLayout()
      Me.TabControl1.SuspendLayout()
      Me.TabPage2.SuspendLayout()
      Me.TabPage1.SuspendLayout()
      Me.TabPage3.SuspendLayout()
      CType(Me.FastColoredTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
      Me.ОткрытьToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
      Me.ОткрытьToolStripMenuItem.Text = "&Открыть"
      '
      'СохраитьToolStripMenuItem
      '
      Me.СохраитьToolStripMenuItem.Name = "СохраитьToolStripMenuItem"
      Me.СохраитьToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
      Me.СохраитьToolStripMenuItem.Text = "&Сохранить"
      '
      'ЗакрытьToolStripMenuItem
      '
      Me.ЗакрытьToolStripMenuItem.Name = "ЗакрытьToolStripMenuItem"
      Me.ЗакрытьToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
      Me.ЗакрытьToolStripMenuItem.Text = "&Закрыть"
      '
      'ToolStripMenuItem1
      '
      Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
      Me.ToolStripMenuItem1.Size = New System.Drawing.Size(129, 6)
      '
      'ВыходToolStripMenuItem
      '
      Me.ВыходToolStripMenuItem.Name = "ВыходToolStripMenuItem"
      Me.ВыходToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
      Me.ВыходToolStripMenuItem.Text = "&Выход"
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
      Me.СправкаToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
      Me.СправкаToolStripMenuItem.Text = "Справка"
      '
      'ToolStripMenuItem2
      '
      Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
      Me.ToolStripMenuItem2.Size = New System.Drawing.Size(146, 6)
      '
      'ОПрограммеToolStripMenuItem
      '
      Me.ОПрограммеToolStripMenuItem.Name = "ОПрограммеToolStripMenuItem"
      Me.ОПрограммеToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
      Me.ОПрограммеToolStripMenuItem.Text = "О программе"
      Me.ОПрограммеToolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
      '
      'длгФайлОткрыть
      '
      Me.длгФайлОткрыть.DefaultExt = "Оберон-07;*.o7"
      Me.длгФайлОткрыть.Filter = "Оберон-07(*.o7)|*.o7"
      Me.длгФайлОткрыть.Title = "Открыть модуль Оберон-07"
      '
      'TabControl1
      '
      Me.TabControl1.Controls.Add(Me.TabPage2)
      Me.TabControl1.Controls.Add(Me.TabPage1)
      Me.TabControl1.Controls.Add(Me.TabPage3)
      Me.TabControl1.Location = New System.Drawing.Point(12, 27)
      Me.TabControl1.Name = "TabControl1"
      Me.TabControl1.SelectedIndex = 0
      Me.TabControl1.Size = New System.Drawing.Size(838, 452)
      Me.TabControl1.TabIndex = 5
      '
      'TabPage2
      '
      Me.TabPage2.Controls.Add(Me.текстИсходник)
      Me.TabPage2.Location = New System.Drawing.Point(4, 22)
      Me.TabPage2.Name = "TabPage2"
      Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
      Me.TabPage2.Size = New System.Drawing.Size(830, 426)
      Me.TabPage2.TabIndex = 1
      Me.TabPage2.Text = "TabPage2"
      Me.TabPage2.UseVisualStyleBackColor = True
      '
      'TabPage1
      '
      Me.TabPage1.Controls.Add(Me.текстРезультат)
      Me.TabPage1.Location = New System.Drawing.Point(4, 22)
      Me.TabPage1.Name = "TabPage1"
      Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
      Me.TabPage1.Size = New System.Drawing.Size(830, 426)
      Me.TabPage1.TabIndex = 2
      Me.TabPage1.Text = "TabPage1"
      Me.TabPage1.UseVisualStyleBackColor = True
      '
      'TabPage3
      '
      Me.TabPage3.Controls.Add(Me.FastColoredTextBox1)
      Me.TabPage3.Location = New System.Drawing.Point(4, 22)
      Me.TabPage3.Name = "TabPage3"
      Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
      Me.TabPage3.Size = New System.Drawing.Size(830, 426)
      Me.TabPage3.TabIndex = 3
      Me.TabPage3.Text = "TabPage3"
      Me.TabPage3.UseVisualStyleBackColor = True
      '
      'текстРезультат
      '
      Me.текстРезультат.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
      Me.текстРезультат.Location = New System.Drawing.Point(6, 6)
      Me.текстРезультат.Name = "текстРезультат"
      Me.текстРезультат.Size = New System.Drawing.Size(818, 414)
      Me.текстРезультат.TabIndex = 6
      Me.текстРезультат.Text = ""
      '
      'текстИсходник
      '
      Me.текстИсходник.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
      Me.текстИсходник.Location = New System.Drawing.Point(6, 6)
      Me.текстИсходник.Name = "текстИсходник"
      Me.текстИсходник.Size = New System.Drawing.Size(818, 414)
      Me.текстИсходник.TabIndex = 5
      Me.текстИсходник.Text = ""
      '
      'FastColoredTextBox1
      '
      Me.FastColoredTextBox1.AutoCompleteBracketsList = New Char() {Global.Microsoft.VisualBasic.ChrW(40), Global.Microsoft.VisualBasic.ChrW(41), Global.Microsoft.VisualBasic.ChrW(123), Global.Microsoft.VisualBasic.ChrW(125), Global.Microsoft.VisualBasic.ChrW(91), Global.Microsoft.VisualBasic.ChrW(93), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(34), Global.Microsoft.VisualBasic.ChrW(39), Global.Microsoft.VisualBasic.ChrW(39)}
      Me.FastColoredTextBox1.AutoIndent = Global.My.Settings.Default.UserTrue
      Me.FastColoredTextBox1.AutoScrollMinSize = New System.Drawing.Size(491, 816)
      Me.FastColoredTextBox1.BackBrush = Nothing
      Me.FastColoredTextBox1.CharHeight = 17
      Me.FastColoredTextBox1.CharWidth = 8
      Me.FastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
      Me.FastColoredTextBox1.DataBindings.Add(New System.Windows.Forms.Binding("AutoIndent", Global.My.Settings.Default, "UserTrue", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
      Me.FastColoredTextBox1.DataBindings.Add(New System.Windows.Forms.Binding("FoldingIndicatorColor", Global.My.Settings.Default, "UserFolding", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
      Me.FastColoredTextBox1.DataBindings.Add(New System.Windows.Forms.Binding("Language", Global.My.Settings.Default, "Oberon", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
      Me.FastColoredTextBox1.DataBindings.Add(New System.Windows.Forms.Binding("ShowLineNumbers", Global.My.Settings.Default, "UserTrue", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
      Me.FastColoredTextBox1.DataBindings.Add(New System.Windows.Forms.Binding("ToolTipDelay", Global.My.Settings.Default, "UserDelay", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
      Me.FastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
      Me.FastColoredTextBox1.FoldingIndicatorColor = Global.My.Settings.Default.UserFolding
      Me.FastColoredTextBox1.Font = New System.Drawing.Font("Consolas", 11.0!)
      Me.FastColoredTextBox1.IsReplaceMode = False
      Me.FastColoredTextBox1.Language = Global.My.Settings.Default.Oberon
      Me.FastColoredTextBox1.Location = New System.Drawing.Point(6, 6)
      Me.FastColoredTextBox1.Name = "FastColoredTextBox1"
      Me.FastColoredTextBox1.Paddings = New System.Windows.Forms.Padding(0)
      Me.FastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
      Me.FastColoredTextBox1.ServiceColors = CType(resources.GetObject("FastColoredTextBox1.ServiceColors"), FastColoredTextBoxNS.ServiceColors)
      Me.FastColoredTextBox1.ShowFoldingLines = True
      Me.FastColoredTextBox1.ShowLineNumbers = Global.My.Settings.Default.UserTrue
      Me.FastColoredTextBox1.Size = New System.Drawing.Size(818, 414)
      Me.FastColoredTextBox1.SourceTextBox = Me.FastColoredTextBox1
      Me.FastColoredTextBox1.TabIndex = 0
      Me.FastColoredTextBox1.TabLength = 3
      Me.FastColoredTextBox1.Text = resources.GetString("FastColoredTextBox1.Text")
      Me.FastColoredTextBox1.ToolTipDelay = Global.My.Settings.Default.UserDelay
      Me.FastColoredTextBox1.Zoom = 100
      '
      'окнГлавное
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(862, 507)
      Me.Controls.Add(Me.TabControl1)
      Me.Controls.Add(Me.MenuStrip1)
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.MainMenuStrip = Me.MenuStrip1
      Me.Name = "окнГлавное"
      Me.Text = "Компилятор Оберон-07"
      Me.MenuStrip1.ResumeLayout(False)
      Me.MenuStrip1.PerformLayout()
      Me.TabControl1.ResumeLayout(False)
      Me.TabPage2.ResumeLayout(False)
      Me.TabPage1.ResumeLayout(False)
      Me.TabPage3.ResumeLayout(False)
      CType(Me.FastColoredTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
   Friend WithEvents TabControl1 As TabControl
   Friend WithEvents TabPage2 As TabPage
   Friend WithEvents TabPage1 As TabPage
   Friend WithEvents TabPage3 As TabPage
   Friend WithEvents FastColoredTextBox1 As FastColoredTextBoxNS.FastColoredTextBox
   Friend WithEvents текстИсходник As RichTextBox
   Friend WithEvents текстРезультат As RichTextBox
End Class
