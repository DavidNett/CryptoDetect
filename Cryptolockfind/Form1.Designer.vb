<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtScan = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BtFoldertoScan = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BtExport = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog2 = New System.Windows.Forms.FolderBrowserDialog()
        Me.CbFileOnly = New System.Windows.Forms.CheckBox()
        Me.CbSubDirectories = New System.Windows.Forms.CheckBox()
        Me.DgvOutput = New System.Windows.Forms.DataGridView()
        Me.FileName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Infected = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Output = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValidFileFormat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FileDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FileSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FileOwner = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtStop = New System.Windows.Forms.Button()
        Me.CbExport = New System.Windows.Forms.CheckBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ClbOptions = New System.Windows.Forms.CheckedListBox()
        Me.BtAbout = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.DgvOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtScan
        '
        Me.BtScan.Enabled = False
        Me.BtScan.Location = New System.Drawing.Point(160, 80)
        Me.BtScan.Name = "BtScan"
        Me.BtScan.Size = New System.Drawing.Size(120, 48)
        Me.BtScan.TabIndex = 0
        Me.BtScan.Text = "Scan"
        Me.BtScan.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(111, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "LbScanningFile"
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Folder to Scan"
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.AddExtension = False
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Title = "File to test"
        '
        'BtFoldertoScan
        '
        Me.BtFoldertoScan.Location = New System.Drawing.Point(1, 72)
        Me.BtFoldertoScan.Name = "BtFoldertoScan"
        Me.BtFoldertoScan.Size = New System.Drawing.Size(87, 30)
        Me.BtFoldertoScan.TabIndex = 2
        Me.BtFoldertoScan.Text = "Folder to Scan"
        Me.BtFoldertoScan.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(89, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "LbLocation"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(211, 137)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "LbFilestatus"
        '
        'BtExport
        '
        Me.BtExport.Location = New System.Drawing.Point(12, 137)
        Me.BtExport.Name = "BtExport"
        Me.BtExport.Size = New System.Drawing.Size(75, 23)
        Me.BtExport.TabIndex = 8
        Me.BtExport.Text = "Export"
        Me.BtExport.UseVisualStyleBackColor = True
        '
        'CbFileOnly
        '
        Me.CbFileOnly.AutoSize = True
        Me.CbFileOnly.BackColor = System.Drawing.SystemColors.ControlLight
        Me.CbFileOnly.Location = New System.Drawing.Point(94, 97)
        Me.CbFileOnly.Name = "CbFileOnly"
        Me.CbFileOnly.Size = New System.Drawing.Size(66, 17)
        Me.CbFileOnly.TabIndex = 10
        Me.CbFileOnly.Text = "File Only"
        Me.CbFileOnly.UseVisualStyleBackColor = False
        '
        'CbSubDirectories
        '
        Me.CbSubDirectories.AutoSize = True
        Me.CbSubDirectories.Checked = True
        Me.CbSubDirectories.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CbSubDirectories.Location = New System.Drawing.Point(12, 112)
        Me.CbSubDirectories.Name = "CbSubDirectories"
        Me.CbSubDirectories.Size = New System.Drawing.Size(95, 17)
        Me.CbSubDirectories.TabIndex = 11
        Me.CbSubDirectories.Text = "SubDirectories"
        Me.CbSubDirectories.UseVisualStyleBackColor = True
        '
        'DgvOutput
        '
        Me.DgvOutput.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.DgvOutput.AllowUserToAddRows = False
        Me.DgvOutput.AllowUserToDeleteRows = False
        Me.DgvOutput.AllowUserToOrderColumns = True
        Me.DgvOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DgvOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvOutput.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FileName, Me.Infected, Me.Output, Me.ValidFileFormat, Me.FileDate, Me.FileSize, Me.FileOwner})
        Me.DgvOutput.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DgvOutput.Location = New System.Drawing.Point(10, 187)
        Me.DgvOutput.Name = "DgvOutput"
        Me.DgvOutput.ReadOnly = True
        Me.DgvOutput.Size = New System.Drawing.Size(890, 184)
        Me.DgvOutput.TabIndex = 13
        '
        'FileName
        '
        Me.FileName.HeaderText = "FileName"
        Me.FileName.Name = "FileName"
        Me.FileName.ReadOnly = True
        '
        'Infected
        '
        Me.Infected.HeaderText = "Infected"
        Me.Infected.Name = "Infected"
        Me.Infected.ReadOnly = True
        '
        'Output
        '
        Me.Output.HeaderText = "Output"
        Me.Output.Name = "Output"
        Me.Output.ReadOnly = True
        '
        'ValidFileFormat
        '
        Me.ValidFileFormat.HeaderText = "ValidFileFormat"
        Me.ValidFileFormat.Name = "ValidFileFormat"
        Me.ValidFileFormat.ReadOnly = True
        '
        'FileDate
        '
        DataGridViewCellStyle4.Format = "G"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.FileDate.DefaultCellStyle = DataGridViewCellStyle4
        Me.FileDate.HeaderText = "FileDate"
        Me.FileDate.Name = "FileDate"
        Me.FileDate.ReadOnly = True
        '
        'FileSize
        '
        Me.FileSize.HeaderText = "FileSize"
        Me.FileSize.Name = "FileSize"
        Me.FileSize.ReadOnly = True
        '
        'FileOwner
        '
        Me.FileOwner.HeaderText = "FileOwner"
        Me.FileOwner.Name = "FileOwner"
        Me.FileOwner.ReadOnly = True
        '
        'BtStop
        '
        Me.BtStop.Location = New System.Drawing.Point(408, 126)
        Me.BtStop.Name = "BtStop"
        Me.BtStop.Size = New System.Drawing.Size(75, 23)
        Me.BtStop.TabIndex = 14
        Me.BtStop.Text = "Stop"
        Me.BtStop.UseVisualStyleBackColor = True
        Me.BtStop.Visible = False
        '
        'CbExport
        '
        Me.CbExport.AutoSize = True
        Me.CbExport.Checked = True
        Me.CbExport.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CbExport.Location = New System.Drawing.Point(12, 164)
        Me.CbExport.Name = "CbExport"
        Me.CbExport.Size = New System.Drawing.Size(120, 17)
        Me.CbExport.TabIndex = 15
        Me.CbExport.Text = "Export only Infected"
        Me.CbExport.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(214, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "LbRunningStatus"
        '
        'ClbOptions
        '
        Me.ClbOptions.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.ClbOptions.CheckOnClick = True
        Me.ClbOptions.FormattingEnabled = True
        Me.ClbOptions.Location = New System.Drawing.Point(504, 87)
        Me.ClbOptions.Name = "ClbOptions"
        Me.ClbOptions.Size = New System.Drawing.Size(161, 94)
        Me.ClbOptions.TabIndex = 20
        '
        'BtAbout
        '
        Me.BtAbout.Location = New System.Drawing.Point(1, 0)
        Me.BtAbout.Name = "BtAbout"
        Me.BtAbout.Size = New System.Drawing.Size(35, 21)
        Me.BtAbout.TabIndex = 21
        Me.BtAbout.Text = "?"
        Me.BtAbout.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(313, 89)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "LbStartedTime"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(937, 398)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BtAbout)
        Me.Controls.Add(Me.ClbOptions)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CbExport)
        Me.Controls.Add(Me.BtStop)
        Me.Controls.Add(Me.DgvOutput)
        Me.Controls.Add(Me.CbSubDirectories)
        Me.Controls.Add(Me.CbFileOnly)
        Me.Controls.Add(Me.BtExport)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtFoldertoScan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtScan)
        Me.MinimumSize = New System.Drawing.Size(500, 400)
        Me.Name = "Form1"
        Me.Text = "CryptoDetect"
        CType(Me.DgvOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtScan As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BtFoldertoScan As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BtExport As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog2 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents CbFileOnly As System.Windows.Forms.CheckBox
    Friend WithEvents CbSubDirectories As System.Windows.Forms.CheckBox
    Friend WithEvents DgvOutput As System.Windows.Forms.DataGridView
    Friend WithEvents BtStop As System.Windows.Forms.Button
    Friend WithEvents CbExport As System.Windows.Forms.CheckBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ClbOptions As System.Windows.Forms.CheckedListBox
    Friend WithEvents BtAbout As System.Windows.Forms.Button
    Friend WithEvents FileName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Infected As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Output As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValidFileFormat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FileDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FileSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FileOwner As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
