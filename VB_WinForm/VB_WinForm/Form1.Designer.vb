<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.CurrentFolder_GV = New System.Windows.Forms.DataGridView()
        CType(Me.CurrentFolder_GV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CurrentFolder_GV
        '
        Me.CurrentFolder_GV.AllowUserToAddRows = False
        Me.CurrentFolder_GV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.CurrentFolder_GV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.CurrentFolder_GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CurrentFolder_GV.Location = New System.Drawing.Point(33, 67)
        Me.CurrentFolder_GV.Name = "CurrentFolder_GV"
        Me.CurrentFolder_GV.RowTemplate.Height = 24
        Me.CurrentFolder_GV.Size = New System.Drawing.Size(368, 90)
        Me.CurrentFolder_GV.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(676, 395)
        Me.Controls.Add(Me.CurrentFolder_GV)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.CurrentFolder_GV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CurrentFolder_GV As DataGridView
End Class
