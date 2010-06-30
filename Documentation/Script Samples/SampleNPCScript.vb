Imports System
Imports Microsoft.VisualBasic
Imports Spurious.WorldServer

' 
' Copyright (C) 2008-2009 Spurious <http://SpuriousEmu.com>
'
' This program is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
'
Namespace Scripts
    Public Class TalkScript
        Inherits TBaseTalk

        Public Overrides Sub OnGossipHello(ByRef c As CharacterObject, ByVal cGUID As Long)
            Console.WriteLine("[{0}] TestNPC: GossipHello()", Format(TimeOfDay, "hh:mm:ss"))

            Dim npcText As New NPCText
            npcText.Count = 1
            npcText.TextID = 999999991
            npcText.TextLine1(0) = "Sample script driven NPCText."
            SendNPCText(c.Client, npcText)


            Dim npcMenu As New GossipMenu
            npcMenu.AddMenu("Sample menu item [Close action]", 1, 0)
            c.SendGossip(cGUID, 999999991, npcMenu)
        End Sub

        Public Overrides Sub OnGossipSelect(ByRef c As CharacterObject, ByVal cGUID As Long, ByVal Selected As Integer)
            Console.WriteLine("[{0}] TestNPC: GossipSelect()", Format(TimeOfDay, "hh:mm:ss"))
            If Selected = 0 Then
                c.SendGossipComplete()
            End If
        End Sub

        Public Overrides Function OnQuestStatus(ByRef c As CharacterObject, ByVal cGUID As Long) As Integer
            Console.WriteLine("[{0}] TestNPC: OnQuestStatus()", Format(TimeOfDay, "hh:mm:ss"))
        End Function

        Public Overrides Function OnQuestHello(ByRef c As CharacterObject, ByVal cGUID As Long) As Boolean
            Console.WriteLine("[{0}] TestNPC: OnQuestHello()", Format(TimeOfDay, "hh:mm:ss"))
        End Function

    End Class
End Namespace
