Public Class Form1


    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown




        '"現在作業中の対象"Columnを作成
        Dim ColImage As New DataGridViewImageColumn
        ColImage.Name = "CurrentProgress"
        'Set Header text
        ColImage.HeaderText = "現在作業中の対象"
        'Set column width
        ColImage.Width = 100
        'Add column to CurrentFolder_GV
        CurrentFolder_GV.Columns.Add(ColImage)





        '"本日の精度監視対象"Columnを作成
        Dim ColFolder As New DataGridViewTextBoxColumn
        ColFolder.HeaderText = "本日の精度監視対象"
        ColFolder.Name = "TargetFolder"
        ColFolder.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        ColFolder.SortMode = DataGridViewColumnSortMode.NotSortable
        'Add column to CurrentFolder_GV
        CurrentFolder_GV.Columns.Add(ColFolder)




        'Fill up the CurrentFolder_GV with the content of TargetFolderList
        Dim TargetFolderList As New List(Of String)({"C:\YourFolder", "C:\YourFolder2", "C:\YourFolder3", "C:\YourFolder4"})
        For i As Integer = 0 To TargetFolderList.Count - 1

            'Create a new row 
            Dim newRow As New DataGridViewRow
            CurrentFolder_GV.Rows.Add(newRow)

            'Set value for "現在作業中の対象"
            'We use a white image as the default value
            Dim arrowCell As New DataGridViewImageCell
            arrowCell.Value = New Bitmap(1, 1)
            'Add to the new row
            CurrentFolder_GV.Rows(i).Cells(0) = arrowCell

            'Set value for 本日の精度監視対象"
            Dim folderCell As New DataGridViewTextBoxCell
            folderCell.Value = TargetFolderList(i)
            'Add to the new row
            CurrentFolder_GV.Rows(i).Cells(1) = folderCell
        Next



        'Premise: a flag file is created for 精度監視終了フォルダー
        '精度監視が実施されていないフォルダーを現在作業中の対象として表示する
        Dim nextTargetFolderIndex = 1
        scrollToCurrentProgress(nextTargetFolderIndex)






    End Sub

    '精度監視が実施されていないフォルダーを現在作業中の対象として表示する
    Private Sub scrollToCurrentProgress(rowNo)
        CurrentFolder_GV.Rows(rowNo).Cells(0).Value = Image.FromFile("./arrow.png")

        CurrentFolder_GV.CurrentCell = CurrentFolder_GV.Rows(rowNo).Cells(0)
        CurrentFolder_GV.Rows(rowNo).DefaultCellStyle.BackColor = System.Drawing.Color.Orange


    End Sub

    'Disable editing of this gridview
    Private Sub CurrentFolder_GV_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles CurrentFolder_GV.SelectionChanged
        CurrentFolder_GV.ClearSelection()

    End Sub

    'Hide the leftmost auto-generated row header 
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CurrentFolder_GV.RowHeadersVisible = False
    End Sub
End Class
