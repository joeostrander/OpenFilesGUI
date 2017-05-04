'TO DO: 
'clear results button


Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.UnmanagedType
Imports System.Runtime.InteropServices.Marshal
Imports System.Runtime.InteropServices.StructLayoutAttribute
Imports System.DirectoryServices.AccountManagement
Imports System.Text
Imports System
Imports System.Threading
Imports System.ComponentModel
Imports Microsoft.Win32

Public Class Form1


    Inherits System.Windows.Forms.Form


    Const MAX_PREFERRED_LENGTH As Integer = -1 ' originally 0

#Region "WINAPI"



    Public Enum PERMISSIONS
        PERM_FILE_NONE = 0
        PERM_FILE_READ = 1
        PERM_FILE_WRITE = 2
        PERM_FILE_CREATE = 4
        PERM_FILE_EXECUTE = 8
    End Enum

    <StructLayout(LayoutKind.Sequential)>
    Structure FILE_INFO_3
        Public fi3_Id As Integer
        Public fi3_Permissions As PermFile
        Public fi3_NumLocks As Integer
        <MarshalAs(UnmanagedType.LPWStr)>
        Public fi3_PathName As String
        <MarshalAs(UnmanagedType.LPWStr)>
        Public fi3_UserName As String
    End Structure

    <Flags()>
    Public Enum PermFile
        ' file network permissions 
        Read = &H1
        ' user has read access 
        Write = &H2
        ' user has write access 
        Create = &H4
        ' user has create access 
        Perm08 = &H8
        ' ? 
        Perm10 = &H10
        ' ? 
        Perm20 = &H20
        ' ? 
    End Enum

    Public Enum NERR
        ''' <summary>
        ''' Operation was a success.
        ''' </summary>
        NERR_Success = 0
        ''' <summary>
        ''' More data available to read. dderror getting all data.
        ''' </summary>
        ERROR_MORE_DATA = 234
        ''' <summary>
        ''' Network browsers not available.
        ''' </summary>
        ERROR_NO_BROWSER_SERVERS_FOUND = 6118
        ''' <summary>
        ''' LEVEL specified is not valid for this call.
        ''' </summary>
        ERROR_INVALID_LEVEL = 124
        ''' <summary>
        ''' Security context does not have permission to make this call.
        ''' </summary>
        ERROR_ACCESS_DENIED = 5
        ''' <summary>
        ''' Parameter was incorrect.
        ''' </summary>
        ERROR_INVALID_PARAMETER = 87
        ''' <summary>
        ''' Out of memory.
        ''' </summary>
        ERROR_NOT_ENOUGH_MEMORY = 8
        ''' <summary>
        ''' Unable to contact resource. Connection timed out.
        ''' </summary>
        ERROR_NETWORK_BUSY = 54
        ''' <summary>
        ''' Network Path not found.
        ''' </summary>
        ERROR_BAD_NETPATH = 53
        ''' <summary>
        ''' No available network connection to make call.
        ''' </summary>
        ERROR_NO_NETWORK = 1222
        ''' <summary>
        ''' Pointer is not valid.
        ''' </summary>
        ERROR_INVALID_HANDLE_STATE = 1609
        ''' <summary>
        ''' Extended Error.
        ''' </summary>
        ERROR_EXTENDED_ERROR = 1208
        ''' <summary>
        ''' Base.
        ''' </summary>
        NERR_BASE = 2100
        ''' <summary>
        ''' Unknown Directory.
        ''' </summary>
        NERR_UnknownDevDir = (NERR_BASE + 16)
        ''' <summary>
        ''' Duplicate Share already exists on server.
        ''' </summary>
        NERR_DuplicateShare = (NERR_BASE + 18)
        ''' <summary>
        ''' Memory allocation was to small.
        ''' </summary>
        NERR_BufTooSmall = (NERR_BASE + 23)
    End Enum


    '<DllImport("netapi32.dll", ExactSpelling:=True, SetLastError:=False)>
    'Private Shared Function NetApiBufferFree(ByVal bufptr As IntPtr) As Integer
    'End Function


    <DllImport("netapi32.dll", CharSet:=Runtime.InteropServices.CharSet.Unicode, ExactSpelling:=True, SetLastError:=False)>
    Private Shared Function NetFileClose(ByVal server As String, ByVal id As Integer) As Integer
    End Function

    Declare Function SetLastError Lib "kernel32.dll" (ByVal dwErrCode As Integer) As Integer

    Declare Function NetFileEnum Lib "netapi32.dll" (
    ByVal servername As IntPtr,
    ByVal basepath As String,
    ByVal username As String,
    ByVal level As Integer,
    ByRef bufptr As IntPtr,
    ByVal prefmaxlen As Integer,
    ByRef entriesread As Long,
    ByRef totalentries As Long,
    <Out> ByRef resume_handle As IntPtr) As Integer

    Private Declare Function NetApiBufferFree Lib "netapi32" _
    (ByVal Buffer As Long) As Long

    Declare Unicode Function NetServerEnum Lib "Netapi32.dll" _
    (ByVal Servername As Integer, ByVal level As Integer,
    ByRef buffer As Integer, ByVal PrefMaxLen As Integer,
    ByRef EntriesRead As Integer, ByRef TotalEntries As Integer,
    ByVal ServerType As Integer, ByVal DomainName As String,
    ByRef ResumeHandle As Integer) As Integer


#End Region

    Private boolRunning As Boolean = False
    Private strServer As String
    Private intTotalEntriesFound As Integer
    Private strLastError As String
    Private strFilter As String = ""

    Private listViewItems As List(Of ListViewItem)

    'Delegate Sub AddListViewItemCallback(ByVal lvi As ListViewItem)
    'Delegate Sub AddListViewItemsCallback(ByVal items As List(Of ListViewItem))
    Delegate Sub AddListViewItemsCallback()
    Delegate Sub UpdateStatusTextCallback(ByVal [text] As String)



    Public Sub FileEnum(Optional ByVal sServer As String = "")
        Dim EntriesRead As Integer = 0
        Dim TotalRead As Integer = 0
        Dim ResumeHandle As Integer = 0
        Dim bufptr As IntPtr = IntPtr.Zero
        Dim ret As Integer
        Dim i As Integer, ptr As IntPtr

        Dim handle As GCHandle = GCHandle.Alloc(sServer, GCHandleType.Pinned)
        Dim strPtr As IntPtr = handle.AddrOfPinnedObject

        Dim totalEntries As Integer = 0
        strLastError = ""

        Dim idx As Integer = totalEntries
        intTotalEntriesFound = 0
        listViewItems = New List(Of ListViewItem)

        Do
            ret = NetFileEnum(strPtr, Nothing, Nothing, 3, bufptr, MAX_PREFERRED_LENGTH, EntriesRead, TotalRead, ResumeHandle)
            SetLastError(ret)
            Dim errorMessage As String = New Win32Exception(Marshal.GetLastWin32Error()).Message
            'Console.WriteLine("errorMessage={0}", errorMessage)
            'Console.WriteLine("ret={0}", ret)
            If ret <> NERR.NERR_Success And ret <> NERR.ERROR_MORE_DATA Then
                'Exit Do
                strLastError = "Error:  " & errorMessage
                BackgroundWorker1.CancelAsync()
                Exit Sub
            End If


            For i = 0 To EntriesRead - 1
                If BackgroundWorker1.CancellationPending Then Exit Sub
                Dim entry As New FILE_INFO_3
                ptr = bufptr + (Marshal.SizeOf(entry) * i)
                entry = Marshal.PtrToStructure(ptr, entry.GetType)
                idx = totalEntries + i

                intTotalEntriesFound += 1
                'Console.WriteLine(intTotalEntriesFound)

                Dim boolAdd As Boolean = False
                If strFilter = "" Then
                    boolAdd = True
                Else
                    If entry.fi3_PathName.ToLower().Contains(strFilter.ToLower()) Or entry.fi3_UserName.ToLower().Contains(strFilter.ToLower()) Then
                        boolAdd = True
                    End If
                End If

                If boolAdd Then
                    Dim lvi As ListViewItem = New ListViewItem(entry.fi3_PathName)
                    lvi.SubItems.Add(entry.fi3_UserName)
                    lvi.SubItems.Add(entry.fi3_NumLocks)
                    lvi.SubItems.Add(GetPermissions(entry.fi3_Permissions))
                    lvi.SubItems.Add(entry.fi3_Id)
                    listViewItems.Add(lvi)
                End If



            Next
            totalEntries += EntriesRead

            Dim boolMore As Boolean = False
            If ret = NERR.ERROR_MORE_DATA Then boolMore = True


            'End If
        Loop While ret = NERR.ERROR_MORE_DATA

        SetStatusText_ThreadSafe("Scan complete, processing list...")

        Dim e As New AddListViewItemsCallback(AddressOf ListViewAdd)
        Me.Invoke(e, New Object() {})


        If bufptr <> IntPtr.Zero Then
            NetApiBufferFree(bufptr)
        End If

        handle.Free()

        BackgroundWorker1.ReportProgress(100)


    End Sub

    Private Sub SetStatusText_ThreadSafe(strNewText As String)
        Dim f As New UpdateStatusTextCallback(AddressOf UpdateStatusText)
        Dim NewText As String = strNewText
        Me.Invoke(f, New Object() {NewText})
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ToolStripStatusLabel1.Text = ""
        Me.Text = Application.ProductName
        LoadSettings()

        AddHandler BackgroundWorker1.DoWork, AddressOf bw_DoWork
        AddHandler BackgroundWorker1.ProgressChanged, AddressOf bw_ProgressChanged
        AddHandler BackgroundWorker1.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted


    End Sub


    'Private Sub ListBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.TextChanged
    Private Sub ButtonScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonScan.Click
        If boolRunning Then
            BackgroundWorker1.CancelAsync()
            Exit Sub
        End If

        strServer = ComboBoxServer.Text
        If strServer = "" Then
            MsgBox("Enter a server name!", MessageBoxIcon.Exclamation, Application.ProductName)
            ComboBoxServer.Focus()
            Exit Sub
        End If

        'If the server wasn't in the list, add it permanently
        If Not ComboBoxServer.Items.Contains(strServer.ToLower()) Then
            ComboBoxServer.Items.Add(strServer.ToLower())
        End If

        SaveSettings()

        ListView1.Items.Clear()
        ToolStripStatusLabel1.Text = ""
        ToolStripProgressBar1.Value = 0


        boolRunning = True
        EnableControls(False)

        strFilter = TextBoxFilter.Text

        'DO BACKGROUND WORK
        BackgroundWorker1.RunWorkerAsync()


    End Sub

    Private Function GetPermissions(ByVal intPerm As Integer) As String
        Select Case intPerm
            Case PERMISSIONS.PERM_FILE_WRITE
                GetPermissions = "Write"
            Case PERMISSIONS.PERM_FILE_EXECUTE
                GetPermissions = "Execute"
            Case PERMISSIONS.PERM_FILE_READ
                GetPermissions = "Read"
            Case PERMISSIONS.PERM_FILE_READ + PERMISSIONS.PERM_FILE_WRITE
                GetPermissions = "Read+Write"
            Case Else
                GetPermissions = intPerm
        End Select
    End Function

    Private Sub EnableControls(ByVal boolEnable As Boolean)
        ButtonScan.Text = If(boolEnable, "&Start", "&Stop")
        TextBoxFilter.Enabled = boolEnable
        ComboBoxServer.Enabled = boolEnable
        ListView1.Enabled = boolEnable

        ToolStripProgressBar1.Style = If(boolEnable, ProgressBarStyle.Blocks, ProgressBarStyle.Marquee)
    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        boolRunning = False
        EnableControls(True)

        If e.Cancelled = True Then
            ToolStripProgressBar1.Value = 0
            If strLastError <> "" Then
                ToolStripStatusLabel1.Text = strLastError
            Else
                ToolStripStatusLabel1.Text = "Scanning cancelled."
            End If

        ElseIf e.Error IsNot Nothing Then
            ToolStripProgressBar1.Value = 0
            Dim strError As String = ""
            Try
                'strError = e.Error.Message
                strError = e.Error.ToString
            Catch ex As Exception
                'strError = ex.Message
            End Try
            'e.Error.Source
            ToolStripStatusLabel1.Text = "Error:  " & strError
        Else
            'ToolStripProgressBar1.Value = 100
            ToolStripStatusLabel1.Text = "Scanning complete.  Total entries:  " & intTotalEntriesFound
        End If

        ListViewColumnSorter.SortMyListView(ListView1, 0,, True)
        ColorLines(ListView1, False)
        TextBoxFilter.Focus()
    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)

        Thread.CurrentThread.Priority = ThreadPriority.Lowest

        FileEnum(strServer)

        If BackgroundWorker1.CancellationPending Then e.Cancel = True

    End Sub


    'Private Sub ListViewAdd(ByVal lvi As ListViewItem)
    '    ListView1.Items.Add(lvi)
    '    'ListView1.Refresh()
    '    ListView1.Items(ListView1.Items.Count - 1).EnsureVisible()
    'End Sub

    Private Sub ListViewAdd(ByVal items As List(Of ListViewItem))
        ListView1.Items.AddRange(items.ToArray)
    End Sub

    Private Sub ListViewAdd()
        ListView1.Items.AddRange(listViewItems.ToArray)
    End Sub


    Private Sub ColorLines(ByVal ListView As ListView, ByVal ReNumber As Boolean)
        Dim count As Integer = 0
        For Each line As ListViewItem In ListView1.Items
            count += 1

            If ReNumber = True Then line.SubItems(0).Text = count

            If count Mod 2 = 0 Then
                line.BackColor = Color.OldLace
            Else
                line.BackColor = Color.White
            End If

            line.ForeColor = Color.Black

        Next
    End Sub

    Private Sub ListView1_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListView1.ColumnClick
        ListViewColumnSorter.SortMyListView(sender, e.Column,, True)
        ColorLines(sender, False)
    End Sub

    Private Sub TextBoxFilter_GotFocus(sender As Object, e As EventArgs) Handles TextBoxFilter.GotFocus
        TextBoxFilter.SelectAll()
    End Sub


    Private Sub TextBoxFilter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxFilter.TextChanged
        If TextBoxFilter.Text.Length > 2 Then
            FilterResults()
        End If
    End Sub
    Private Sub FilterResults()
        If listViewItems Is Nothing Then Exit Sub

        If listViewItems.Count = 0 Then Exit Sub

        Dim newList As New List(Of ListViewItem)
        For Each item As ListViewItem In listViewItems
            'For Each subitem As ListViewItem.ListViewSubItem In item.SubItems
            '    If subitem.Text.ToLower().Contains(TextBoxFilter.Text.ToLower()) Then
            '        newList.Add(item)
            '        Exit For
            '    End If
            'Next
            'instead of searching every column... just do path & user...
            If item.SubItems(0).Text.ToLower().Contains(TextBoxFilter.Text.ToLower()) Or item.SubItems(1).Text.ToLower().Contains(TextBoxFilter.Text.ToLower()) Then
                newList.Add(item)
            End If
        Next

        ListView1.Items.Clear()
        ListViewAdd(newList)

        ColorLines(ListView1, False)
    End Sub

    Private Sub CloseFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseFileToolStripMenuItem.Click
        If ListView1.SelectedItems.Count = 0 Then Exit Sub

        Dim intClosedCount As Integer = 0
        Dim intFailedCount As Integer = 0
        Dim output As String = ""

        For Each lvi As ListViewItem In ListView1.SelectedItems

            Dim strFilePath As String = lvi.SubItems(0).Text
            Dim strID As String = lvi.SubItems(4).Text

            Dim boolExists As Boolean = FileOrFolderExists(strFilePath)


            If boolExists Then
                Dim ret As Integer = NetFileClose(strServer, CInt(strID))
                If ret = 0 Or ret = 2314 Then
                    intClosedCount += 1
                    ListView1.Items.Remove(lvi)
                Else
                    SetLastError(ret)
                    Dim errorMessage As String = "(" & ret & ") " & New Win32Exception(Marshal.GetLastWin32Error()).Message
                    intFailedCount += 1
                    output = output & "Failed:  " & strFilePath & vbCrLf & errorMessage & vbCrLf & vbCrLf
                End If

                'ToolStripStatusLabel1.Text = errorMessage
                'ElseIf IO.File.Exists(strFilePath) Then
                '    ExploreFolder(strFilePath)
                '    'Shell("cmd /c " & Chr(34) & strPath & Chr(34), AppWinStyle.MinimizedNoFocus)
            Else
                intFailedCount += 1
                output = output & "Not found:  " & strFilePath
            End If
        Next

        ColorLines(ListView1, False)

        If intClosedCount > 0 Then
            MsgBox("Closed " & intClosedCount & " file(s)", MessageBoxIcon.Information, Application.ProductName)
        End If

        If intFailedCount > 0 Then
            MsgBox("Failed to close " & intFailedCount & " file(s):" & vbCrLf & output, MessageBoxIcon.Exclamation, Application.ProductName)
        End If


    End Sub

    Private Function FileOrFolderExists(ByRef strFilePath As String) As Boolean
        Dim boolExists As Boolean = False

        'Try a few options
        'replace : with $
        Dim strTest As String = "\\" & strServer & "\" & strFilePath.Replace(":", "$")
        'replace C:, D:, etc. with nothing
        Dim strTest2 As String = "\\" & strServer & Mid(strFilePath, InStr(strFilePath, ":") + 1)


        If IO.Directory.Exists(strTest) Or IO.File.Exists(strTest) Then
            boolExists = True
            strFilePath = strTest
        ElseIf IO.Directory.Exists(strTest2) Or IO.File.Exists(strTest2) Then
            boolExists = True
            strFilePath = strTest2
        End If

        Return boolExists

    End Function

    Private Function GetSelectedPath() As String
        If ListView1.SelectedItems.Count = 0 Then
            Return ""
        End If

        Dim strFilePath As String

        strFilePath = ListView1.SelectedItems(0).SubItems(0).Text


        Return strFilePath
    End Function



    Private Sub UpdateStatusText(ByVal strNewText As String)
        Console.WriteLine("update status text: {0}", strNewText)
        ToolStripStatusLabel1.Text = strNewText
        StatusStrip1.Refresh()
    End Sub

    Private Sub ExploreFolder(ByVal strPath As String)

        Try
            'If SearchMode = "File" Then
            Shell("explorer.exe /e,/select," & Chr(34) & strPath & Chr(34), AppWinStyle.NormalFocus)
            'Else
            'Shell("explorer.exe /e," & Chr(34) & strFolder & Chr(34), AppWinStyle.NormalFocus)
            'End If


        Catch ex As Exception
            'Do Nothing
            MsgBox(ex.Message, MessageBoxIcon.Exclamation, Application.ProductName)
        End Try

    End Sub




    Public Sub SaveSettings()

        Dim strKeyPath As String = "Software\" & Application.ProductName

        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey(strKeyPath, True)

        If IsNothing(regKey) Then
            'Create the key
            Registry.CurrentUser.CreateSubKey(strKeyPath)
            regKey = Registry.CurrentUser.OpenSubKey(strKeyPath, True)
            If IsNothing(regKey) Then
                regKey.Close()
                Exit Sub
            End If
        End If

        'Delete all existing
        For Each valName In regKey.GetValueNames()
            regKey.DeleteValue(valName)
        Next

        For Each item In ComboBoxServer.Items
            Console.WriteLine(item.GetType().ToString())
            'Dim boolFound As Boolean
            If Not regKey.GetValueNames().Contains(item.ToString().ToLower()) Then
                regKey.SetValue(item.ToString().ToLower(), "")
            End If

        Next

    End Sub



    Public Sub LoadSettings()

        Dim strKeyPath As String = "Software\" & Application.ProductName

        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey(strKeyPath, True)

        If IsNothing(regKey) Then Exit Sub

        For Each valName In regKey.GetValueNames()
            ComboBoxServer.Items.Add(valName.ToLower())
        Next

    End Sub

    Private Sub TextBoxFilter_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBoxFilter.KeyUp
        If e.KeyCode = Keys.Enter Then
            FilterResults()
        End If
    End Sub

    Private Sub ExportListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportListToolStripMenuItem.Click
        ExportListview.SaveAsCSV(ListView1)
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        If ComboBoxServer.Text <> "" Then
            ComboBoxServer.Items.Remove(ComboBoxServer.SelectedItem)
            SaveSettings()
        End If
    End Sub

    Private Sub ContextMenuStripServer_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStripServer.Opening
        If ComboBoxServer.Text = "" Then e.Cancel = True
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub ContextMenuStripListView_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStripListView.Opening
        If ListView1.Items.Count = 0 Then e.Cancel = True
    End Sub

    Private Sub ShowInExplorerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowInExplorerToolStripMenuItem.Click
        If ListView1.SelectedItems.Count = 0 Then Exit Sub

        If ListView1.SelectedItems.Count > 1 Then
            MsgBox("Please select only 1 item.", MessageBoxIcon.Information, Application.ProductName)
            Exit Sub
        End If

        Dim strFilePath As String = ListView1.SelectedItems(0).SubItems(0).Text

        Dim boolExists As Boolean = FileOrFolderExists(strFilePath)

        Console.WriteLine(strFilePath)
        If boolExists Then
            ExploreFolder(strFilePath)
        Else
            MsgBox("Not found:  " & strFilePath, MessageBoxIcon.Exclamation, Application.ProductName)
        End If

    End Sub

    Private Sub ClearResultsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearResultsToolStripMenuItem.Click
        listViewItems.Clear()
        ListView1.Items.Clear()
    End Sub
End Class

