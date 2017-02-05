﻿'   Copyright (C) 2015-2016 EXploringEA
'
' This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
' See the GNU General Public License for more details.
' You should have received a copy of the GNU General Public Licensealong with this program.  If not, see <http://www.gnu.org/licenses/>.
'

Imports Microsoft.Win32

Public Class frmInspector3

    Private Sub frmInspector2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            LinkLabel1.Links.Add(0, 15, "http://Exploringea.com")
            init_lv(lv2) ' set up the list view
            setWidths(lv2) ' set list view column widths
            poplv(lv2)
            ToolTip1.SetToolTip(btHelp, versionString)
            ' Check OS and set text
            If Environment.Is64BitOperatingSystem Then
                ' OSLabel.Text = "64-bit detected"
                tbOS.Text = "64-bit detected"
            Else
                ' OSLabel.Text = "32-bit  detected"
                tbOS.Text = "32-bit detected"
            End If
            tbLocation.Text = Registry.GetValue(EA, "Install Path", "")
            tbVersion.Text = Registry.GetValue(EA, "Version", "")

        Catch ex As Exception

        End Try
    End Sub



    Private Sub lv2_SizeChanged(sender As Object, e As EventArgs) Handles lv2.SizeChanged
        Try
            If TypeOf sender Is ListView Then
                Dim lv As ListView = sender


                'setWidths(sender.width)
                ' we want to add up width of cols 0 to 6 and set the DLL as rest
                Dim col05width As Integer = 0
                For i = 0 To 5
                    col05width += lv.Columns(i).Width
                Next
                lv.Columns(6).Width = lv.Width - col05width

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btRefresh_Click(sender As Object, e As EventArgs) Handles btRefresh.Click
        Try
            lv2.Items.Clear()
            poplv(lv2)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btHelp_Click(sender As Object, e As EventArgs) Handles btHelp.Click
        Dim myInfo As New frmHelp
        frmHelp.ShowDialog()
    End Sub

    Private Sub btCopy_Click(sender As Object, e As EventArgs) Handles btCopy.Click
        Try
            Dim gfx As Graphics = Me.CreateGraphics()
            Dim bmp As Bitmap = New Bitmap(Me.Width, Me.Height)
            Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))
            My.Computer.Clipboard.SetImage(bmp)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
        Catch ex As Exception

        End Try

    End Sub

    Private Sub lv2_DoubleClick(sender As Object, e As EventArgs) Handles lv2.DoubleClick
        Try
            Dim myS As Object = sender
            If TypeOf sender Is ListView Then
                Dim mySS As ListView = sender
                ' need to get information from entrt
                Dim mySS1 As ListViewItem = mySS.SelectedItems(0)
                Dim myListViewSubItems As ListViewItem.ListViewSubItemCollection = mySS1.SubItems
                ' we want to launch a frm
                Dim myEntryDetail As New AddInDetail
                myEntryDetail.AddInName = myListViewSubItems(0).Text
                myEntryDetail.SparxEntry = myListViewSubItems(1).Text
                myEntryDetail.ClassDefinition = myListViewSubItems(2).Text
                myEntryDetail.ClassSource = myListViewSubItems(3).Text
                myEntryDetail.CLSID = myListViewSubItems(4).Text
                myEntryDetail.CLSIDSource = myListViewSubItems(5).Text
                myEntryDetail.DLL = myListViewSubItems(6).Text
                Dim myDetail As New frmEntryDetail(myEntryDetail)
                myDetail.ShowDialog()
            End If

        Catch ex As Exception

        End Try
    End Sub


End Class