Imports System.IO

Public Class ExportListview

    Public Shared Sub SaveAsCSV(ByVal lv As ListView)
        Dim sfd As New SaveFileDialog
        sfd.Filter = "CSV Files|*.csv"

        If sfd.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub

        Dim strFilename As String = sfd.FileName
        Dim writer As New StreamWriter(strFilename)

        Dim strHeaders As String = ""

        For Each colTitle As ColumnHeader In lv.Columns
            strHeaders += Chr(34) & colTitle.Text & Chr(34) & ","
        Next

        If strHeaders.EndsWith(",") Then strHeaders = strHeaders.Remove(strHeaders.Length - 1)
        writer.WriteLine(strHeaders)


        For Each row As ListViewItem In lv.Items
            Dim strRowText As String = ""
            For Each itm As ListViewItem.ListViewSubItem In row.SubItems
                strRowText = strRowText & Chr(34) & itm.Text & Chr(34) & ","
            Next
            If strRowText.EndsWith(",") Then strRowText = strRowText.Remove(strRowText.Length - 1)
            writer.WriteLine(strRowText)
        Next

        writer.Close()
        MsgBox("See file:  " & strFilename, MsgBoxStyle.Information)
    End Sub

End Class
