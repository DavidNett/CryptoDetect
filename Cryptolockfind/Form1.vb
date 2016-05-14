Imports System
Imports System.IO
Imports System.Text
Imports System.IO.FileSystemInfo
Imports System.Security.Principal
Imports System.Security.AccessControl

<System.Runtime.InteropServices.ComVisible(False)> Public Class Form1
    Const MaxSignatures = 200
    Const MaxSigBytes = 200
    Structure Signature
        Dim Offset() As Integer
        Dim Value() As Integer
        Dim Name As String
        Dim Ext As String
        Dim Bytes As Integer
    End Structure
    Structure Fileattrib
        Dim FileSize As Long
        Dim FileDate As Date
        Dim ntfsDate As Date
        Dim FileOwner As String

    End Structure
    Structure Resultslevelstruct
        Dim ALL As Boolean
        Dim VirusDetect As Boolean
        Dim BadFileFormat As Boolean
        Dim AvoidTempFile As Boolean
        Dim AvoidShortFiles As Boolean
        Dim AvoidAccessErrors As Boolean
    End Structure
    Structure FILETIME
        Dim dwLowDateTime As Integer
        Dim dwHighDateTime As Integer
    End Structure
    Structure lpFileInformation
        Dim dwFileAttributes As Integer
        Dim ftCreationTime As FILETIME
        Dim ftLastAccesstime As FILETIME
        Dim ftLastWriteTime As FILETIME
        Dim dwVolumeSerialNumber As Integer
        Dim nFileSizeHigh As Integer
        Dim nfileSizeLow As Integer
        Dim nNumverOfLinks As Integer
        Dim nFileIndexHigh As Integer
        Dim nFileIndexLow As Integer
    End Structure
    Declare Auto Function GetFileInformationByHandle Lib "kernel32" (ByVal Filehandle As IntPtr, ByRef BY_HANDLE_FILE_INFORMATION As lpFileInformation) As Integer


    Dim RecordLevel As Resultslevelstruct
    Dim SignatureBad(MaxSignatures) As Signature
    Dim SignatureGood(MaxSignatures) As Signature
    Dim NumSignatures As Integer
    Dim goodSignatures As Integer
    Dim FileCount As Long
    Dim BadFileCount As Long
    Dim Stoprunning As Boolean
    Dim starttime As Date
    Dim stoptime As Date


    Function SHA1Compare(ByVal Hash As Byte(), ByVal S As Byte(), ByRef Out As Byte()) As Boolean
        Dim asc As New System.Text.UTF8Encoding
        Dim enc As New System.Security.Cryptography.SHA1CryptoServiceProvider
        Dim bytes() As Byte

        Dim Pos As Integer


        bytes = enc.ComputeHash(S)
        Out = bytes


        SHA1Compare = False
        If Hash.Length = bytes.Length Then
            SHA1Compare = True
            For Pos = 0 To bytes.Length - 1
                If bytes(Pos) <> Hash(Pos) Then SHA1Compare = False
            Next
        End If

    End Function
    Sub Load_Signatures()
        Dim ConfigFilename As String
        Dim Line As String
        Dim command As String
        Dim str As String
        Dim loop1 As Integer
        Dim Sigbad As Integer
        Dim SigGood As Integer
        Dim sigtype As String
        Dim sigbytecount As Integer
        Dim initialoffset As Integer
        Dim offset As Integer
        Dim byteval As Integer

        ConfigFilename = "CryptoSignatures.txt"
        If Not File.Exists(ConfigFilename) Then
            Exit Sub
        End If


        Sigbad = 0
        SigGood = 0

        Dim ConfigFile As New System.IO.StreamReader(ConfigFilename)
        While ConfigFile.Peek <> -1
            Line = ConfigFile.ReadLine
            If InStr(Line, "'") = 1 Then Line = ""
            If InStr(Line, "'") > 1 Then
                Line = Mid(Line, 1, InStr(Line, "'") - 1)
            End If

            If InStr(Line, ":") > 0 Then
                command = Mid(Line, 1, InStr(Line, ":") - 1)
                str = Mid(Line, InStr(Line, ":") + 1, 999)
                command = LCase(command)
                Select Case command
                    Case "bad"
                        sigtype = "Bad"
                        Sigbad = Sigbad + 1
                        sigbytecount = 0
                        NumSignatures = NumSignatures + 1
                        SignatureBad(Sigbad).Name = str
                        ReDim SignatureBad(Sigbad).Offset(MaxSigBytes)
                        ReDim SignatureBad(Sigbad).Value(MaxSigBytes)
                        SignatureBad(Sigbad).Bytes = 0
                    Case "good"
                        sigtype = "Good"
                        SigGood = SigGood + 1
                        sigbytecount = 0
                        SignatureGood(SigGood).Name = str
                        goodSignatures = goodSignatures + 1
                        ReDim SignatureGood(SigGood).Offset(MaxSigBytes)
                        ReDim SignatureGood(SigGood).Value(MaxSigBytes)
                        SignatureGood(SigGood).Bytes = 0
                    Case "offset"
                        initialoffset = Val(Replace(str, " ", ""))
                    Case "bytes"
                        str = Replace(str, " ", "")
                        For loop1 = 1 To (Len(str) / 2)
                            byteval = Val("&H" & Mid(str, (loop1 - 1) * 2 + 1, 2))
                            offset = loop1 - 1 + initialoffset
                            If sigtype = "Bad" Then
                                SignatureBad(Sigbad).Offset(sigbytecount) = offset
                                SignatureBad(Sigbad).Value(sigbytecount) = byteval 'Val("&H" & Mid(str, offset, 2))
                                SignatureBad(Sigbad).Bytes = SignatureBad(Sigbad).Bytes + 1
                            End If
                            If sigtype = "Good" Then

                                SignatureGood(SigGood).Offset(sigbytecount) = offset
                                SignatureGood(SigGood).Value(sigbytecount) = byteval ' al("&H" & Mid(str, offset))
                                SignatureGood(SigGood).Bytes = SignatureGood(SigGood).Bytes + 1
                            End If
                            sigbytecount = sigbytecount + 1
                        Next
                    Case "ext"
                        SignatureGood(SigGood).Ext = Replace(LCase((Replace(str, " ", ""))), ".", "")

                End Select
            End If
        End While


        ConfigFile.Close()
    End Sub


    Function ScanFile(ByVal TestFile As String, ByRef Feedback As String, ByRef FileFormat As String, ByRef FileInfo As Fileattrib) As Boolean

        Dim locA As String
        Dim HashBytes(19) As Byte
        Dim TestBytes(259) As Byte
        Dim TestHash() As Byte
        Dim Result As Boolean
        Dim fpos As Integer
        Dim Ext As String
        Dim Loop1 As Integer
        Dim Sigresult As Boolean
        Dim fsSource As FileStream
        Dim FInfo As FileSecurity
        FileFormat = ""
        locA = "0" ' Using for Debugging

        Dim Pos As Integer

        On Error Resume Next
        locA = "0.3" & TestFile
        Err.Clear()
        fsSource = File.OpenRead(TestFile)
        If Err.Number > 0 Then
            '5 = Access is denied
            locA = Err.ToString
            '   locA = Err.Number
            '   locA = Err.Source
            '   locA = Err.Description
            Feedback = "Error:" & Err.Number & " " & Err.Description
        End If
        If Err.Number = 0 Then
            locA = "0.05" & TestFile
            If fsSource.CanRead Then
                If Err.Number > 0 Then
                    locA = Err.Number
                    locA = Err.ToString
                    locA = Err.Source
                    locA = Err.Description
                    Feedback = "Error:access error reading file"
                End If

                FileInfo.FileDate = File.GetLastWriteTime(TestFile)
                FInfo = File.GetAccessControl(TestFile) ', Security.AccessControl.AccessControlSections.Owner)
                FileInfo.FileOwner = FInfo.GetOwner(GetType(NTAccount)).ToString

                FileInfo.FileSize = fsSource.Length

                locA = "1"
                If fsSource.Length > 260 Then
                    Pos = fsSource.Read(HashBytes, 0, 20)
                    Pos = fsSource.Read(TestBytes, 4, 256)


                    Result = SHA1Compare(HashBytes, TestBytes, TestHash)  ' Test for Cryptolocker that has the hash in the first 20 bytes, of the next 256 bytes.
                    If Result Then Feedback = "Cryptolocker V1:True"

                    ' this was a theory to catch copycats, but need to rethink.

                    'fpos = 0
                    'If Result = False Then
                    '    While Result = False And fpos < 100

                    '        fsSource.Position = 0 'fpos

                    '        Pos = fsSource.Read(HashBytes, 0, 20)
                    '        Pos = fsSource.Read(TestBytes, 6, Pos)
                    '        Pos = fsSource.Read(TestBytes, 4, 256)
                    '        locA = "4"
                    '        Result = SHA1Compare(HashBytes, TestBytes, TestHash)
                    '        If Result = True Then
                    '            Feedback = "Cryptolocker P" & Pos & ":True "
                    '        End If
                    '        fpos = fpos + 1
                    '    End While
                    'End If
                    'fsSource.Position = 0
                    'Pos = fsSource.Read(TestBytes, 0, 256)
                    'locA = "8"
                    If Result = False Then 'Check For other Variants
                        'Check for File Signatures

                        Dim Sig(MaxSignatures) As Signature

                        locA = "14"
                        Sig = SignatureBad

                        For Pos = 1 To NumSignatures
                            Loop1 = 0
                            Sigresult = True
                            locA = "20"
                            While Sigresult = True And Loop1 < Sig(Pos).Bytes
                                If TestBytes(Sig(Pos).Offset(Loop1)) <> Sig(Pos).Value(Loop1) Then Sigresult = False
                                Loop1 = Loop1 + 1
                            End While
                            If Loop1 > 1 And Sigresult = True Then
                                Result = True
                                Feedback = "Signature " & Pos & " Found (" & Sig(Pos).Name & ")"
                            End If
                        Next
                        locA = "30"
                    End If
                    If Result = False Then Feedback = ""
                Else 'File Too Short
                    Feedback = "File too short"
                End If
                locA = "35"
                Ext = fsSource.Name
                If InStr(Ext, ".") > 0 Then

                    While (Len(Ext) + 1 > InStr(Ext, ".")) And (InStr(Ext, ".") > 0)
                        Ext = LCase(Mid(Ext, InStr(Ext, ".") + 1, 999))
                    End While
                    Loop1 = 1
                    locA = "45"
                    While (Ext <> SignatureGood(Loop1).Ext) And (Loop1 <= goodSignatures)
                        Loop1 = Loop1 + 1
                    End While
                    locA = "50"
                    If Ext = SignatureGood(Loop1).Ext Then ' found at least one
                        ' Check if valid for that Ext
                        For Pos = 1 To goodSignatures
                            If Ext = SignatureGood(Pos).Ext Then
                                Sigresult = True
                                Loop1 = 0
                                While (Sigresult = True) And (Loop1 < SignatureGood(Pos).Bytes)
                                    If TestBytes(SignatureGood(Pos).Offset(Loop1)) <> SignatureGood(Pos).Value(Loop1) Then Sigresult = False
                                    Loop1 = Loop1 + 1
                                End While
                                If (Loop1 > 1) And (Sigresult = True) Then
                                    FileFormat = "True"
                                Else
                                    If FileFormat <> "True" Then FileFormat = "False"
                                End If
                            End If
                        Next

                    End If
                End If
                ScanFile = Result


                fsSource.Close()
            Else
                Feedback = "File not ready to read/ in use"
            End If
        End If
        ' Catch ex As Exception
        'Label2.Text = Mid(ex.Message, 1, 100) & "LOC:" & locA ' & ex.GetType

        ' Feedback = "Error with File" & ex.Message
        ' End Try

    End Function

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = ""
        Label2.Text = ""
        Label4.Text = ""
        BtScan.Enabled = False
        Stoprunning = False

        ClbOptions.Items.Add("ALL")
        ClbOptions.Items.Add("Virus infected File")
        ClbOptions.Items.Add("Bad File Signature")
        ClbOptions.Items.Add("Avoid Short Files")
        ClbOptions.Items.Add("Avoid file Access Errors")
        ClbOptions.Items.Add("Avoid Temp '\$*' Files")
        ClbOptions.SetItemChecked(1, True)
        ClbOptions.SetItemChecked(2, True)
        ClbOptions.SetItemChecked(3, True)
        ClbOptions.SetItemChecked(5, True)
        Label5.Text = ""
        Label6.Text = ""
        RecordLevel.ALL = ClbOptions.CheckedItems.Contains("ALL")
        RecordLevel.VirusDetect = ClbOptions.CheckedItems.Contains("Virus infected File")
        RecordLevel.BadFileFormat = ClbOptions.CheckedItems.Contains("Bad File Signature")
        RecordLevel.AvoidShortFiles = ClbOptions.CheckedItems.Contains("Avoid Short Files")
        RecordLevel.AvoidTempFile = ClbOptions.CheckedItems.Contains("Avoid Temp '\$*' Files")
        RecordLevel.AvoidAccessErrors = ClbOptions.CheckedItems.Contains("Avoid file Access Errors")

    End Sub

    Private Sub BtFoldertoScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtFoldertoScan.Click
        Label1.Text = ""
        Label4.Text = ""
        Label5.Text = ""

        Load_Signatures()
        DgvOutput.ClearSelection()

        BtFoldertoScan.Enabled = False
        BtScan.Enabled = False
        If Not CbFileOnly.Checked Then
            FolderBrowserDialog1.ShowDialog()
            If FolderBrowserDialog1.SelectedPath <> "" Then
                Label2.Text = FolderBrowserDialog1.SelectedPath
                BtScan.Enabled = True
            End If
        Else
            OpenFileDialog1.Title = "Select a File to Scan"
            OpenFileDialog1.ShowDialog()
            If OpenFileDialog1.FileName <> "" Then
                Label2.Text = OpenFileDialog1.FileName
                BtScan.Enabled = True
            End If

        End If


        BtFoldertoScan.Enabled = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbFileOnly.CheckedChanged
        If CbFileOnly.Checked Then
            BtFoldertoScan.Text = "File to Scan"
            CbSubDirectories.Visible = False
        End If
        If Not CbFileOnly.Checked Then

            CbSubDirectories.Visible = True
            BtFoldertoScan.Text = "Folder to Scan"
        End If
        BtScan.Enabled = False
    End Sub

    Sub ScanFolder(ByVal Filename As String)
        Dim Result As Boolean
        Dim Feedback As String
        Dim ValidFormat As String
        Dim Record As Boolean
        Dim fileInfo As Fileattrib
        fileInfo.FileOwner = ""

        Result = ScanFile(Filename, Feedback, ValidFormat, fileInfo)
        If Result = True Then BadFileCount = BadFileCount + 1
        FileCount = FileCount + 1
        If Result = False And Feedback <> "" Then

        End If

        Record = False
        If RecordLevel.ALL Then Record = True
        If RecordLevel.VirusDetect And Result Then Record = True
        If RecordLevel.BadFileFormat And Result Or (ValidFormat = "False") Then Record = True
        If RecordLevel.AvoidShortFiles And Feedback = "File too short" Then Record = False
        If RecordLevel.AvoidTempFile And InStr(Filename, "\$") > 0 Then Record = False
        If RecordLevel.AvoidTempFile And InStr(Filename, "\~$") > 0 Then Record = False
        If RecordLevel.AvoidAccessErrors And Feedback = "Error Reading File" Then Record = False
        If RecordLevel.AvoidAccessErrors And Mid(Feedback, 1, 6) = "Error:" Then Record = False

        If Record Then DgvOutput.Rows.Add(Filename, Result, Feedback, ValidFormat, fileInfo.FileDate, fileInfo.FileSize, fileInfo.FileOwner)


        If FileCount Mod 100 = 0 Then
            Dim timerunning As TimeSpan
            timerunning = Now - starttime
            Label4.Text = "Files:" & FileCount & " / Bad:" & BadFileCount
            Label1.Text = Filename

            Label5.Text = "running:" & timerunning.Days & " Days " & timerunning.Hours & ":" & timerunning.Minutes & ":" & timerunning.Seconds

        End If
    End Sub

    Sub ScanFolders(ByVal FolderName As String, ByVal Recursive As Boolean, ByVal First As Boolean)
        Dim Record As Boolean
        If First Then
            FileCount = 0
            BadFileCount = 0
            Stoprunning = False
            DgvOutput.Rows.Clear()
            starttime = Now
            BtStop.Visible = True
            Label6.Text = "Started at:" & Now
            ' Future Feature 
        End If
        Dim D As String
        Dim F As String
        Dim DirName As String

        On Error Resume Next

        If First Then
            ScanFolder(FolderName)
        End If

        If Recursive Then
            Err.Clear()
            For Each D In Directory.GetDirectories(FolderName)
                If Err.Number <> 0 Then
                    Label1.Text = Err.Description & Err.Number
                    Record = False
                    If RecordLevel.ALL Then Record = True
                    If Not RecordLevel.AvoidAccessErrors Then Record = True
                    DirName = D
                    If D = Nothing Then
                        DirName = FolderName
                    End If
                    If Record Then
                        DgvOutput.Rows.Add(DirName, False, "Access Denied to Directory", "")

                    End If
                Else
                    If Len(D) > 0 Then
                        For Each F In Directory.GetFiles(D, "*.*")
                            If Len(F) > 0 Then
                                ScanFolder(F)
                            End If
                            If Stoprunning Then Exit Sub
                            My.Application.DoEvents()
                        Next
                        If Len(D) > 0 Then
                            ScanFolders(D, True, False)
                        End If
                    End If
                End If
                Err.Clear()
            Next
        Else
            For Each F In Directory.GetFiles(FolderName, "*.*")
                ScanFolder(F)
                If Stoprunning Then Exit Sub
            Next
        End If
        'Catch ex As Exception
        'Label1.Text = ex.Message
        'End Try

        If First Then BtStop.Visible = False

    End Sub


    Private Sub BtScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtScan.Click
        Dim Infected As Boolean
        Dim Feedback As String
        Dim ValidFormat As String
        '  Dim filesize As Long
        '  Dim filedate As Date
        '  Dim fileowner As String
        Dim FileInfo As Fileattrib

        If CbFileOnly.Checked Then

            Infected = ScanFile(OpenFileDialog1.FileName, Feedback, ValidFormat, FileInfo) 'filesize, filedate, fileowner)
            Label4.Text = Feedback
            If Feedback = "" And Not Infected Then Label4.Text = "No Encryption identified"
        Else  ' Folders
            ScanFolders(FolderBrowserDialog1.SelectedPath, CbSubDirectories.Enabled, True)
            Label4.Text = "Files:" & FileCount & " / Bad:" & BadFileCount

            stoptime = Now

        End If
    End Sub

    Private Sub BtStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtStop.Click
        Stoprunning = True
    End Sub

    Private Sub BtExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtExport.Click
        Dim Delim As String
        Dim SDelim As String
        Dim EDelim As String

        SaveFileDialog1.DefaultExt = ".csv"
        SaveFileDialog1.Filter = "Comma Seperated|*.CSV|Quote and Comma Seperated|*.CSV|Tab Seperated|*.csv"
        SaveFileDialog1.FilterIndex = 2
        SaveFileDialog1.Title = "Export CSV File"
        Dim pos As Integer
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName <> "" Then
            SDelim = ""
            EDelim = ""
            Select Case SaveFileDialog1.FilterIndex
                Case 1 : Delim = ","
                Case 2
                    Delim = ""","""
                    SDelim = """"
                    EDelim = """"
                Case 3 : Delim = vbTab
            End Select
            On Error Resume Next
            Dim w As StreamWriter = File.CreateText(SaveFileDialog1.FileName)
            If Err.Number = 0 Then
                w.WriteLine(SDelim & "Filename" & Delim & "Infected" & Delim & "output" & Delim & "ValidFileHeader" & Delim & "FileDate" & Delim & "FileSize" & Delim & "FileOwner" & EDelim)
                For pos = 0 To DgvOutput.Rows.Count - 1
                    If (CbExport.Checked = False) Or (DgvOutput.Item("Infected", pos).Value = True) Then
                        w.Write(SDelim & DgvOutput.Item("FileName", pos).Value.ToString & Delim)
                        w.Write(DgvOutput.Item("Infected", pos).Value.ToString & Delim)
                        w.Write(DgvOutput.Item("Output", pos).Value.ToString & Delim)

                        w.Write(DgvOutput.Item("ValidFileFormat", pos).Value.ToString & Delim)
                        w.Write(DgvOutput.Item("FileDate", pos).Value.ToString & Delim)
                        w.Write(DgvOutput.Item("FileSize", pos).Value.ToString & Delim)
                        w.Write(DgvOutput.Item("FileOwner", pos).Value.ToString)
                        w.WriteLine(EDelim)
                    End If
                Next
                w.Flush()
                w.Close()
            Else

                Label1.Text = "error saving file."
                'error opening file
            End If
        End If
    End Sub


    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvOutput.CellContentClick
        If e.ColumnIndex = 0 Then
            Dim filename As String

            filename = DgvOutput.Item(0, e.RowIndex).Value.ToString

            Process.Start("explorer.exe", "/select," & filename)
        End If
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ClbOptions_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClbOptions.SelectedIndexChanged
        RecordLevel.ALL = ClbOptions.CheckedItems.Contains("ALL")
        RecordLevel.VirusDetect = ClbOptions.CheckedItems.Contains("Virus infected File")
        RecordLevel.BadFileFormat = ClbOptions.CheckedItems.Contains("Bad File Signature")
        RecordLevel.AvoidShortFiles = ClbOptions.CheckedItems.Contains("Avoid Short Files")
        RecordLevel.AvoidTempFile = ClbOptions.CheckedItems.Contains("Avoid Temp '\$*' Files")
        RecordLevel.AvoidAccessErrors = ClbOptions.CheckedItems.Contains("Avoid file Access Errors")

    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

        DgvOutput.Width = Me.Width - 50

        DgvOutput.Height = Me.Height - 187 - 50


    End Sub



    Private Sub BtAbout_Click(sender As Object, e As EventArgs) Handles BtAbout.Click

        Dim oform As AboutBox1
        oform = New AboutBox1()
        oform.Show()
        oform = Nothing
    End Sub


End Class
