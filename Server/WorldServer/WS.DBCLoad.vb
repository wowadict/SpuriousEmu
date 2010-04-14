' 
' Copyright (C) 2008 Spurious <http://SpuriousEmu.com>
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

Imports System
Imports System.IO
Imports Spurious.Common
Imports Spurious.Common.BaseWriter
Imports Spurious.WorldServer
Imports Spurious.Common.DBC

Public Module WS_DBCLoad

#Region "MapDifficulty"
    Public Sub InitializeMapDifficulty()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\MapDifficulty.dbc")

            Dim KeyID As Integer
            Dim Map As Integer
            Dim i As Integer = 0

            For i = 0 To tmpDBC.Rows - 1
                KeyID = i
                Map = tmpDBC.Item(i, 1, DBCValueType.DBC_INTEGER)
                Dim MapD As New MapDifficulty
                MapD.MapID = Map
                MapD.Difficulty = tmpDBC.Item(i, 2, DBC.DBCValueType.DBC_INTEGER)
                MapD.ResetTime = tmpDBC.Item(i, 20, DBC.DBCValueType.DBC_INTEGER)
                MapD.MaxPlayers = tmpDBC.Item(i, 21, DBC.DBCValueType.DBC_INTEGER)

                MapDifficulties.Add(KeyID, MapD)
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} MapDifficulty initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : MapDifficulty missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "MapContinents"
    Public Sub InitializeWorldMapContinent()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\WorldMapContinent.dbc")
            Dim Map As Integer

            Dim i As Integer = 0
            For i = 0 To tmpDBC.Rows - 1
                Map = tmpDBC.Item(i, 1)
                Dim info As New WorldMapContinentDimension

                info.X_Minimum = tmpDBC.Item(i, 9, DBC.DBCValueType.DBC_FLOAT)
                info.Y_Minimum = tmpDBC.Item(i, 10, DBC.DBCValueType.DBC_FLOAT)
                info.X_Maximum = tmpDBC.Item(i, 11, DBC.DBCValueType.DBC_FLOAT)
                info.Y_Maximum = tmpDBC.Item(i, 12, DBC.DBCValueType.DBC_FLOAT)

                WorldMapContinent.Add(Map, info)
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} WorldMapContinent initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : WorldMapContinent missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
    Public Sub InitializeWorldMapTransforms()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\WorldMapTransforms.dbc")

            Dim i As Integer = 0
            For i = 0 To tmpDBC.Rows - 1
                Dim info As New WorldMapTransformsDimension

                info.Map = tmpDBC.Item(i, 1, DBC.DBCValueType.DBC_INTEGER)
                info.X_Minimum = tmpDBC.Item(i, 2, DBC.DBCValueType.DBC_FLOAT)
                info.Y_Minimum = tmpDBC.Item(i, 3, DBC.DBCValueType.DBC_FLOAT)
                info.X_Maximum = tmpDBC.Item(i, 4, DBC.DBCValueType.DBC_FLOAT)
                info.Y_Maximum = tmpDBC.Item(i, 5, DBC.DBCValueType.DBC_FLOAT)

                info.Dest_Map = tmpDBC.Item(i, 6, DBC.DBCValueType.DBC_INTEGER)
                info.Dest_X = tmpDBC.Item(i, 7, DBC.DBCValueType.DBC_FLOAT)
                info.Dest_Y = tmpDBC.Item(i, 8, DBC.DBCValueType.DBC_FLOAT)

                WorldMapTransforms.Add(info)
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} WorldMapTransforms initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : WorldMapTransforms missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub


#End Region
#Region "Spells"
    Public Sub InitializeSpellRadius()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\SpellRadius.dbc")

            Dim radiusID As Integer
            Dim radiusValue As Single
            Dim radiusValue2 As Single

            Dim i As Integer
            For i = 0 To tmpDBC.Rows - 1
                radiusID = tmpDBC.Item(i, 0)
                radiusValue = tmpDBC.Item(i, 1, DBC.DBCValueType.DBC_FLOAT) ' Hostile?
                radiusValue2 = tmpDBC.Item(i, 3, DBC.DBCValueType.DBC_FLOAT) ' May be needed in the future - Friendly?

                SpellRadius(radiusID) = radiusValue
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} SpellRadius initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : SpellRadius missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
    Public Sub InitializeSpellCastTime()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\SpellCastTimes.dbc")

            Dim spellCastID As Integer
            Dim spellCastTimeS As Integer

            Dim i As Integer
            For i = 0 To tmpDBC.Rows - 1
                spellCastID = tmpDBC.Item(i, 0)
                spellCastTimeS = tmpDBC.Item(i, 1)

                SpellCastTime(spellCastID) = spellCastTimeS
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} SpellCastTimes initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : SpellCastTimes missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
    Public Sub InitializeSpellRange()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\SpellRange.dbc")

            Dim spellRangeIndex As Integer
            Dim spellRangeMin As Single
            Dim spellRangeMax As Single

            Dim i As Integer
            For i = 0 To tmpDBC.Rows - 1
                spellRangeIndex = tmpDBC.Item(i, 0)
                spellRangeMin = tmpDBC.Item(i, 1, DBC.DBCValueType.DBC_FLOAT) ' Added back may be needed in the future - Min Range Hostile
                'spellRangeMinFriendly = tmpDBC.Item(i, 2, DBCValueType.DBC_FLOAT) ' Min Range Friendly
                spellRangeMax = tmpDBC.Item(i, 3, DBC.DBCValueType.DBC_FLOAT) ' Max Range Hostile
                'spellRangeMaxFriendly = tmpDBC.Item(i, 4, DBC.DBCValueType.DBC_FLOAT) ' Max Range Friendly
                'Type = tmpDBC.Item(i, 5, DBCValueType.DBC_INTEGER)

                SpellRange(spellRangeIndex) = spellRangeMax
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} SpellRanges initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : SpellRanges missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
    Public Sub InitializeSpellShapeShift()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\SpellShapeshiftForm.dbc")

            Dim ID As Integer
            Dim Flags1 As Integer
            Dim CreatureType As Integer
            Dim AttackSpeed As Integer

            Dim i As Integer
            For i = 0 To tmpDBC.Rows - 1
                ID = tmpDBC.Item(i, 0)
                Flags1 = tmpDBC.Item(i, 19)
                CreatureType = tmpDBC.Item(i, 20)
                AttackSpeed = tmpDBC.Item(i, 22)

                SpellShapeShiftForm.Add(New TSpellShapeshiftForm(ID, Flags1, CreatureType, AttackSpeed))
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} SpellShapeshiftForms initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : SpellShapeshiftForms missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
    Public Sub InitializeSpellFocusObject()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\SpellFocusObject.dbc")

            Dim spellFocusIndex As Integer
            Dim spellFocusObjectName As String

            Dim i As Integer
            For i = 0 To tmpDBC.Rows - 1
                spellFocusIndex = tmpDBC.Item(i, 0)
                spellFocusObjectName = tmpDBC.Item(i, 1, DBC.DBCValueType.DBC_STRING)

                SpellFocusObject(spellFocusIndex) = spellFocusObjectName
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} SpellFocusObjects initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : SpellFocusObjects missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
    Public Sub InitializeSpellDuration()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\SpellDuration.dbc")

            Dim SpellDurationIndex As Integer
            Dim SpellDurationValue As Integer
            Dim SpellDurationValue2 As Integer
            Dim SpellDurationValue3 As Integer

            Dim i As Integer
            For i = 0 To tmpDBC.Rows - 1
                SpellDurationIndex = tmpDBC.Item(i, 0)
                SpellDurationValue = tmpDBC.Item(i, 1)
                SpellDurationValue2 = tmpDBC.Item(i, 2) ' May be needed in the future
                SpellDurationValue3 = tmpDBC.Item(i, 3) ' May be needed in the future

                SpellDuration(SpellDurationIndex) = SpellDurationValue
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} SpellDurations initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : SpellDurations missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

    Public Sub InitializeSpells()
        Try
            Dim SpellDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\Spell.dbc")
            'Console.WriteLine("[" & Format(TimeOfDay, "hh:mm:ss") & "] " & SpellDBC.GetFileInformation)
            Log.WriteLine(LogType.INFORMATION, "DBC: Initializing Spells - This may take a few moments....")

            '2.4.3: One new field in spell effect, at position 119 I would guess, or else it's somewhere between 112-119 for sure.

            Dim i As Long
            Dim ID As Integer
            For i = 0 To SpellDBC.Rows - 1
                Try
                    ID = SpellDBC.Item(i, 0)
                    SPELLs(ID) = New SpellInfo
                    SPELLs(ID).ID = ID
                    SPELLs(ID).Category = SpellDBC.Item(i, 1)
                    SPELLs(ID).DispellType = SpellDBC.Item(i, 2)
                    SPELLs(ID).Mechanic = SpellDBC.Item(i, 3)
                    SPELLs(ID).Attributes = SpellDBC.Item(i, 4)
                    SPELLs(ID).AttributesEx = SpellDBC.Item(i, 5)
                    SPELLs(ID).AttributesEx2 = SpellDBC.Item(i, 6)
                    SPELLs(ID).AttributesEx3 = SpellDBC.Item(i, 7)
                    SPELLs(ID).AttributesEx4 = SpellDBC.Item(i, 8)
                    'AttributesEx5 = 9
                    'AttributesEx6 = 10
                    'AttributesEx7 = 11   ' 3.2.0 (0x20 - totems, 0x4 - paladin auras, etc...)
                    SPELLs(ID).RequredCasterStance = SpellDBC.Item(i, 12) ' RequiredShapeShift
                    'unk_320_2 = 13  ' 3.2.0
                    SPELLs(ID).ShapeshiftExclude = SpellDBC.Item(i, 14)
                    'unk_320_3 = 15 ' 3.2.0
                    SPELLs(ID).Target = SpellDBC.Item(i, 16)
                    SPELLs(ID).TargetCreatureType = SpellDBC.Item(i, 17)
                    SPELLs(ID).FocusObjectIndex = SpellDBC.Item(i, 18)
                    SPELLs(ID).FacingCasterFlags = SpellDBC.Item(i, 19)
                    SPELLs(ID).CasterAuraState = SpellDBC.Item(i, 20)
                    SPELLs(ID).TargetAuraState = SpellDBC.Item(i, 21)
                    SPELLs(ID).ExcludeCasterAuraState = SpellDBC.Item(i, 22)
                    SPELLs(ID).ExcludeTargetAuraState = SpellDBC.Item(i, 23)
                    'CasterAuraSpell = 24
                    'TargetAuraSpell = 25
                    'ExcludeCasterAuraSpell = 26
                    'ExcludeTargetAuraSpell = 27
                    SPELLs(ID).SpellCastTimeIndex = SpellDBC.Item(i, 28)
                    SPELLs(ID).SpellCooldown = SpellDBC.Item(i, 29)
                    SPELLs(ID).CategoryCooldown = SpellDBC.Item(i, 30)
                    SPELLs(ID).interruptFlags = SpellDBC.Item(i, 31)
                    SPELLs(ID).auraInterruptFlags = SpellDBC.Item(i, 32)
                    SPELLs(ID).channelInterruptFlags = SpellDBC.Item(i, 33)
                    SPELLs(ID).procFlags = SpellDBC.Item(i, 34)
                    SPELLs(ID).procChance = SpellDBC.Item(i, 35)
                    SPELLs(ID).procCharges = SpellDBC.Item(i, 36)
                    SPELLs(ID).maxLevel = SpellDBC.Item(i, 37)
                    SPELLs(ID).baseLevel = SpellDBC.Item(i, 38)
                    SPELLs(ID).spellLevel = SpellDBC.Item(i, 39)
                    SPELLs(ID).DurationIndex = SpellDBC.Item(i, 40)
                    SPELLs(ID).powerType = SpellDBC.Item(i, 41)
                    SPELLs(ID).manaCost = SpellDBC.Item(i, 42)
                    SPELLs(ID).manaCostPerlevel = SpellDBC.Item(i, 43)
                    SPELLs(ID).manaPerSecond = SpellDBC.Item(i, 44)
                    SPELLs(ID).manaPerSecondPerLevel = SpellDBC.Item(i, 45)
                    SPELLs(ID).rangeIndex = SpellDBC.Item(i, 46)
                    SPELLs(ID).Speed = SpellDBC.Item(i, 47, DBC.DBCValueType.DBC_FLOAT)
                    SPELLs(ID).modalNextSpell = SpellDBC.Item(i, 48)
                    SPELLs(ID).maxStack = SpellDBC.Item(i, 49)
                    SPELLs(ID).Totem(0) = SpellDBC.Item(i, 50)
                    SPELLs(ID).Totem(1) = SpellDBC.Item(i, 51)

                    SPELLs(ID).Reagents(0) = SpellDBC.Item(i, 52)
                    SPELLs(ID).Reagents(1) = SpellDBC.Item(i, 53)
                    SPELLs(ID).Reagents(2) = SpellDBC.Item(i, 54)
                    SPELLs(ID).Reagents(3) = SpellDBC.Item(i, 55)
                    SPELLs(ID).Reagents(4) = SpellDBC.Item(i, 56)
                    SPELLs(ID).Reagents(5) = SpellDBC.Item(i, 57)
                    SPELLs(ID).Reagents(6) = SpellDBC.Item(i, 58)
                    SPELLs(ID).Reagents(7) = SpellDBC.Item(i, 59)

                    SPELLs(ID).ReagentsCount(0) = SpellDBC.Item(i, 60)
                    SPELLs(ID).ReagentsCount(1) = SpellDBC.Item(i, 61)
                    SPELLs(ID).ReagentsCount(2) = SpellDBC.Item(i, 62)
                    SPELLs(ID).ReagentsCount(3) = SpellDBC.Item(i, 63)
                    SPELLs(ID).ReagentsCount(4) = SpellDBC.Item(i, 64)
                    SPELLs(ID).ReagentsCount(5) = SpellDBC.Item(i, 65)
                    SPELLs(ID).ReagentsCount(6) = SpellDBC.Item(i, 66)
                    SPELLs(ID).ReagentsCount(7) = SpellDBC.Item(i, 67)

                    SPELLs(ID).EquippedItemClass = SpellDBC.Item(i, 68)
                    SPELLs(ID).EquippedItemSubClass = SpellDBC.Item(i, 69)
                    SPELLs(ID).EquippedItemInventoryType = SpellDBC.Item(i, 70)

                    If Int(SpellDBC.Item(i, 71)) <> 0 Then
                        SPELLs(ID).SpellEffects(0) = New SpellEffect

                        SPELLs(ID).SpellEffects(0).ID = SpellDBC.Item(i, 71)
                        SPELLs(ID).SpellEffects(0).diceSides = SpellDBC.Item(i, 74)
                        'SPELLs(ID).SpellEffects(0).diceBase = SpellDBC.Item(i, 77)     ' GONE???
                        'SPELLs(ID).SpellEffects(0).dicePerLevel = SpellDBC.Item(i, 80) ' GONE???
                        SPELLs(ID).SpellEffects(0).valuePerLevel = SpellDBC.Item(i, 77, DBC.DBCValueType.DBC_FLOAT)
                        'SPELLs(ID).SpellEffects(0).valueDie = SpellDBC.Item(i, 74) ' GONE???
                        SPELLs(ID).SpellEffects(0).valueBase = SpellDBC.Item(i, 80)
                        SPELLs(ID).SpellEffects(0).Mechanic = SpellDBC.Item(i, 83)
                        SPELLs(ID).SpellEffects(0).implicitTargetA = SpellDBC.Item(i, 86)
                        SPELLs(ID).SpellEffects(0).implicitTargetB = SpellDBC.Item(i, 89)
                        SPELLs(ID).SpellEffects(0).RadiusIndex = SpellDBC.Item(i, 92) ' spellradius.dbc
                        SPELLs(ID).SpellEffects(0).ApplyAuraIndex = SpellDBC.Item(i, 95)
                        SPELLs(ID).SpellEffects(0).Amplitude = SpellDBC.Item(i, 98)
                        'MultipleValue = 101
                        SPELLs(ID).SpellEffects(0).ChainTarget = SpellDBC.Item(i, 104)
                        SPELLs(ID).SpellEffects(0).ItemType = SpellDBC.Item(i, 107)
                        SPELLs(ID).SpellEffects(0).MiscValue = SpellDBC.Item(i, 110)
                        SPELLs(ID).SpellEffects(0).MiscValueB = SpellDBC.Item(i, 113)
                        SPELLs(ID).SpellEffects(0).TriggerSpell = SpellDBC.Item(i, 116)
                        SPELLs(ID).SpellEffects(0).valuePerComboPoint = SpellDBC.Item(i, 119)
                    Else
                        SPELLs(ID).SpellEffects(0) = Nothing
                    End If

                    If Int(SpellDBC.Item(i, 72)) <> 0 Then
                        SPELLs(ID).SpellEffects(1) = New SpellEffect

                        SPELLs(ID).SpellEffects(1).ID = SpellDBC.Item(i, 72)
                        SPELLs(ID).SpellEffects(1).diceSides = SpellDBC.Item(i, 75)
                        'SPELLs(ID).SpellEffects(1).diceBase = SpellDBC.Item(i, 78)     ' GONE???
                        'SPELLs(ID).SpellEffects(1).dicePerLevel = SpellDBC.Item(i, 81) ' GONE???
                        SPELLs(ID).SpellEffects(1).valuePerLevel = SpellDBC.Item(i, 78, DBC.DBCValueType.DBC_FLOAT)
                        'SPELLs(ID).SpellEffects(1).valueDie = SpellDBC.Item(i, 75) ' GONE???
                        SPELLs(ID).SpellEffects(1).valueBase = SpellDBC.Item(i, 81)
                        SPELLs(ID).SpellEffects(1).Mechanic = SpellDBC.Item(i, 84)
                        SPELLs(ID).SpellEffects(1).implicitTargetA = SpellDBC.Item(i, 87)
                        SPELLs(ID).SpellEffects(1).implicitTargetB = SpellDBC.Item(i, 90)
                        SPELLs(ID).SpellEffects(1).RadiusIndex = SpellDBC.Item(i, 93) ' spellradius.dbc
                        SPELLs(ID).SpellEffects(1).ApplyAuraIndex = SpellDBC.Item(i, 96)
                        SPELLs(ID).SpellEffects(1).Amplitude = SpellDBC.Item(i, 99)
                        'MultipleValue = 102
                        SPELLs(ID).SpellEffects(1).ChainTarget = SpellDBC.Item(i, 105)
                        SPELLs(ID).SpellEffects(1).ItemType = SpellDBC.Item(i, 108)
                        SPELLs(ID).SpellEffects(1).MiscValue = SpellDBC.Item(i, 111)
                        SPELLs(ID).SpellEffects(1).MiscValueB = SpellDBC.Item(i, 114)
                        SPELLs(ID).SpellEffects(1).TriggerSpell = SpellDBC.Item(i, 117)
                        SPELLs(ID).SpellEffects(1).valuePerComboPoint = SpellDBC.Item(i, 120)
                    Else
                        SPELLs(ID).SpellEffects(1) = Nothing
                    End If

                    If Int(SpellDBC.Item(i, 73)) <> 0 Then
                        SPELLs(ID).SpellEffects(2) = New SpellEffect

                        SPELLs(ID).SpellEffects(2).ID = SpellDBC.Item(i, 73)
                        SPELLs(ID).SpellEffects(2).diceSides = SpellDBC.Item(i, 76)
                        'SPELLs(ID).SpellEffects(2).diceBase = SpellDBC.Item(i, 79)     ' GONE???
                        'SPELLs(ID).SpellEffects(2).dicePerLevel = SpellDBC.Item(i, 82) ' GONE???
                        SPELLs(ID).SpellEffects(2).valuePerLevel = SpellDBC.Item(i, 79, DBC.DBCValueType.DBC_FLOAT)
                        'SPELLs(ID).SpellEffects(2).valueDie = SpellDBC.Item(i, 76) ' GONE???
                        SPELLs(ID).SpellEffects(2).valueBase = SpellDBC.Item(i, 82)
                        SPELLs(ID).SpellEffects(2).Mechanic = SpellDBC.Item(i, 85)
                        SPELLs(ID).SpellEffects(2).implicitTargetA = SpellDBC.Item(i, 88)
                        SPELLs(ID).SpellEffects(2).implicitTargetB = SpellDBC.Item(i, 91)
                        SPELLs(ID).SpellEffects(2).RadiusIndex = SpellDBC.Item(i, 94) ' spellradius.dbc
                        SPELLs(ID).SpellEffects(2).ApplyAuraIndex = SpellDBC.Item(i, 97)
                        SPELLs(ID).SpellEffects(2).Amplitude = SpellDBC.Item(i, 100)
                        'MultipleValue = 103
                        SPELLs(ID).SpellEffects(2).ChainTarget = SpellDBC.Item(i, 106)
                        SPELLs(ID).SpellEffects(2).ItemType = SpellDBC.Item(i, 109)
                        SPELLs(ID).SpellEffects(2).MiscValue = SpellDBC.Item(i, 112)
                        SPELLs(ID).SpellEffects(2).MiscValueB = SpellDBC.Item(i, 115)
                        SPELLs(ID).SpellEffects(2).TriggerSpell = SpellDBC.Item(i, 118)
                        SPELLs(ID).SpellEffects(2).valuePerComboPoint = SpellDBC.Item(i, 121)
                    Else
                        SPELLs(ID).SpellEffects(2) = Nothing
                    End If

                    'EffectSpellClassMaskA1 = 122
                    'EffectSpellClassMaskA2 = 123
                    'EffectSpellClassMaskA3 = 124
                    'EffectSpellClassMaskB1 = 125
                    'EffectSpellClassMaskB2 = 126
                    'EffectSpellClassMaskB3 = 127
                    'EffectSpellClassMaskC1 = 128
                    'EffectSpellClassMaskC2 = 129
                    'EffectSpellClassMaskC3 = 130
                    SPELLs(ID).SpellVisual = SpellDBC.Item(i, 131)
                    'SpellVisual2 = 132
                    'SpellIconID = 133
                    'ActiveIconID = 134
                    'SpellPriority = 135
                    'SpellName (string*16) = 136-151
                    'SpellNameFlag = 152
                    'Rank (string*16) = 153-168
                    'RankFlags = 169
                    'Description (string*16) = 170-185
                    'DescriptionFlags = 186
                    'Tooltip (string*16) = 187-202
                    'TooltipFlags = 203
                    SPELLs(ID).manaCostPercent = SpellDBC.Item(i, 204)
                    'StartRecoveryCategory = 205
                    'StartRecoveryTime = 206
                    'MaxTargetLevel = 207
                    'SpellFamilyName = 208
                    'SpellFamilyFlags0 = 209 \
                    'SpellFamilyFlags1 = 210 |-12 bytes flag mask
                    'SpellFamilyFlags2 = 211 /
                    SPELLs(ID).MaxTargets = SpellDBC.Item(i, 212)
                    SPELLs(ID).DamageType = SpellDBC.Item(i, 213)
                    'PreventionType = 214
                    'StanceBarOrder = 215
                    SPELLs(ID).DamageMultiplier = SpellDBC.Item(i, 216, DBCValueType.DBC_FLOAT)
                    'DmgMultiplier2 = 217
                    'DmgMultiplier3 = 218
                    'MinFactionId = 219
                    'MinReputation = 220
                    'RequiredAuraVision = 221
                    SPELLs(ID).TotemCategory(0) = SpellDBC.Item(i, 222)
                    SPELLs(ID).TotemCategory(1) = SpellDBC.Item(i, 223)
                    SPELLs(ID).RequiredAreaID = SpellDBC.Item(i, 224)
                    SPELLs(ID).School = SpellDBC.Item(i, 225)
                    'runeCostID = 226
                    'spellMissileID = 227
                    'PowerDisplayId = 228               ' Integer - PowerDisplay.dbc, new in 3.1
                    'unk_320_4[3] = 229-231             ' Float - 3.2.0
                    'spellDescriptionVariableID = 232   ' Integer - 3.2.0
                    'SpellDifficultyId = 233            ' Integer - 3.3.0

                Catch e As Exception
                    Log.WriteLine(LogType.FAILED, "Line {0} caused error: {1}", i, e.ToString)
                End Try

            Next i

            SpellDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} Spells initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : Spells missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "Taxi"
    Public Sub InitializeTaxiNodes()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\TaxiNodes.dbc")

            Dim taxiPosX As Single
            Dim taxiPosY As Single
            Dim taxiPosZ As Single
            Dim taxiMapID As Integer
            Dim taxiNode As Integer
            Dim taxiMountType_Horde As Integer
            Dim taxiMountType_Alliance As Integer

            Dim i As Integer = 0
            For i = 0 To tmpDBC.Rows - 1
                taxiNode = tmpDBC.Item(i, 0)
                taxiMapID = tmpDBC.Item(i, 1)
                taxiPosX = tmpDBC.Item(i, 2, DBC.DBCValueType.DBC_FLOAT)
                taxiPosY = tmpDBC.Item(i, 3, DBC.DBCValueType.DBC_FLOAT)
                taxiPosZ = tmpDBC.Item(i, 4, DBC.DBCValueType.DBC_FLOAT)
                taxiMountType_Horde = tmpDBC.Item(i, 22)
                taxiMountType_Alliance = tmpDBC.Item(i, 23)

                If Config.Maps.Contains(taxiMapID.ToString) Then
                    TaxiNodes.Add(taxiNode, New TTaxiNode(taxiPosX, taxiPosY, taxiPosZ, taxiMapID, taxiMountType_Horde, taxiMountType_Alliance))
                End If
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} TaxiNodes initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : TaxiNodes missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

    Public Sub InitializeTaxiPaths()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\TaxiPath.dbc")

            Dim taxiNode As Integer
            Dim taxiFrom As Integer
            Dim taxiTo As Integer
            Dim taxiPrice As Integer
            Dim i As Integer = 0

            For i = 0 To tmpDBC.Rows - 1
                taxiNode = tmpDBC.Item(i, 0)
                taxiFrom = tmpDBC.Item(i, 1)
                taxiTo = tmpDBC.Item(i, 2)
                taxiPrice = tmpDBC.Item(i, 3)

                TaxiPaths.Add(taxiNode, New TTaxiPath(taxiFrom, taxiTo, taxiPrice))

            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} TaxiPaths initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : TaxiPath missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

    Public Sub InitializeTaxiPathNodes()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\TaxiPathNode.dbc")

            Dim taxiNode As Integer
            Dim taxiPath As Integer
            Dim taxiSeq As Integer
            Dim taxiMapID As Integer
            Dim taxiPosX As Single
            Dim taxiPosY As Single
            Dim taxiPosZ As Single
            Dim taxiUnk1 As Integer
            Dim taxiWaitTime As Integer
            Dim taxiUnk2 As Integer
            Dim taxiUnk3 As Integer
            Dim i As Integer = 0

            For i = 0 To tmpDBC.Rows - 1
                taxiNode = tmpDBC.Item(i, 0)
                taxiPath = tmpDBC.Item(i, 1)
                taxiSeq = tmpDBC.Item(i, 2)
                taxiMapID = tmpDBC.Item(i, 3)
                taxiPosX = tmpDBC.Item(i, 4, DBC.DBCValueType.DBC_FLOAT)
                taxiPosY = tmpDBC.Item(i, 5, DBC.DBCValueType.DBC_FLOAT)
                taxiPosZ = tmpDBC.Item(i, 6, DBC.DBCValueType.DBC_FLOAT)
                taxiUnk1 = tmpDBC.Item(i, 7)     ' actionFlag
                taxiWaitTime = tmpDBC.Item(i, 8) ' Delay
                taxiUnk2 = tmpDBC.Item(i, 9)     ' arrivalEventID
                taxiUnk3 = tmpDBC.Item(i, 10)    ' departureEventID

                If Config.Maps.Contains(taxiMapID.ToString) Then
                    TaxiPathNodes.Add(taxiNode, New TTaxiPathNode(taxiPosX, taxiPosY, taxiPosZ, taxiMapID, taxiPath, taxiSeq, taxiWaitTime))
                End If
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} TaxiPathNodes initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : TaxiPathNode missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "GraveYards"
    Public Sub InitializeGraveyards()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\WorldSafeLocs.dbc")

            Dim locationPosX As Single
            Dim locationPosY As Single
            Dim locationPosZ As Single
            Dim locationMapID As Integer
            Dim locationIndex As Integer

            Dim i As Integer = 0
            For i = 0 To tmpDBC.Rows - 1
                locationIndex = tmpDBC.Item(i, 0)
                locationMapID = tmpDBC.Item(i, 1)
                locationPosX = tmpDBC.Item(i, 2, DBC.DBCValueType.DBC_FLOAT)
                locationPosY = tmpDBC.Item(i, 3, DBC.DBCValueType.DBC_FLOAT)
                locationPosZ = tmpDBC.Item(i, 4, DBC.DBCValueType.DBC_FLOAT)

                If Config.Maps.Contains(locationMapID.ToString) Then
                    Graveyards.Add(New TGraveyard(locationPosX, locationPosY, locationPosZ, locationMapID))
                End If
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} Graveyards initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : WorldSafeLocs missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "Skills"
    Public Sub InitializeSkillLines()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\SkillLine.dbc")

            Dim skillID As Integer
            Dim skillLine As Integer
            Dim skillUnk1 As Integer
            Dim skillName As String

            Dim i As Integer = 0
            For i = 0 To tmpDBC.Rows - 1
                skillID = tmpDBC.Item(i, 0)
                skillLine = tmpDBC.Item(i, 1) ' Type? - Category?
                skillUnk1 = tmpDBC.Item(i, 2) ' May be needed in the future - skillCostID
                skillName = tmpDBC.Item(i, 3) ' May be needed in the future
                '19 string flags
                'description[16]               ' 20-35
                '36 string flags
                'spellIconID = 37
                'alternateVerb[16]             ' 38-53
                '54 string flags
                'canLink = 55                  ' (prof. with recipes
                SkillLines(skillID) = skillLine
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} SkillLines initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : SkillLines missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "Locks"
    Public Sub InitializeLocks()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\Lock.dbc")

            Dim lockID As Integer
            Dim keyType(4) As Byte
            Dim key(4) As Integer
            Dim reqMining As Integer
            Dim reqLockSkill As Integer

            Dim i As Integer = 0
            For i = 0 To tmpDBC.Rows - 1
                lockID = tmpDBC.Item(i, 0)
                keyType(0) = CByte(tmpDBC.Item(i, 1))
                keyType(1) = CByte(tmpDBC.Item(i, 2))
                keyType(2) = CByte(tmpDBC.Item(i, 3))
                keyType(3) = CByte(tmpDBC.Item(i, 4))
                keyType(4) = CByte(tmpDBC.Item(i, 5))
                ' 1-8 = Type
                key(0) = tmpDBC.Item(i, 9)
                key(1) = tmpDBC.Item(i, 10)
                key(2) = tmpDBC.Item(i, 11)
                key(3) = tmpDBC.Item(i, 12)
                key(4) = tmpDBC.Item(i, 13)
                ' 9-16 Index?
                ' 17-24 Skill?
                ' 25-32 Action?
                ' TODO: Check into the two values below???
                reqMining = tmpDBC.Item(i, 17)
                reqLockSkill = tmpDBC.Item(i, 18)

                Locks(lockID) = New TLock(keyType, key, reqMining, reqLockSkill)
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} Locks initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : Locks missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "AreaTable"
    Public Sub InitializeAreaTable()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\AreaTable.dbc")

            Dim areaID As Integer
            Dim areaMapID As Integer
            Dim areaExploreFlag As Integer
            Dim areaLevel As Integer
            Dim areaZone As Integer
            Dim areaZoneType As Integer
            Dim areaEXP As Integer
            Dim areaTeam As Integer
            Dim areaName As String

            Dim i As Integer = 0
            For i = 0 To tmpDBC.Rows - 1
                areaID = tmpDBC.Item(i, 0)
                areaMapID = tmpDBC.Item(i, 1) ' May be needed in the future
                areaZone = tmpDBC.Item(i, 2) ' if 0 then it's zone, else it's zone id of this area
                areaExploreFlag = tmpDBC.Item(i, 3) ' main index
                areaZoneType = tmpDBC.Item(i, 4) ' flags - unknown value but 312 for all cities
                areaEXP = tmpDBC.Item(i, 8) ' May be needed in the future - 5-9 unused?
                areaLevel = tmpDBC.Item(i, 10)
                areaName = tmpDBC.Item(i, 11) ' May be needed in the future - 11-26
                ' 27 - string flags, unused
                areaTeam = tmpDBC.Item(i, 28) ' May be category?

                If areaLevel > 255 Then areaLevel = 255
                If areaLevel < 0 Then areaLevel = 0

                AreaTable(areaExploreFlag) = New TArea
                AreaTable(areaExploreFlag).ID = areaID
                AreaTable(areaExploreFlag).Level = areaLevel
                AreaTable(areaExploreFlag).Name = areaName
                AreaTable(areaExploreFlag).Zone = areaZone
                AreaTable(areaExploreFlag).ZoneType = areaZoneType
                AreaTable(areaExploreFlag).Team = areaTeam
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} Areas initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : AreaTable missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "Emotes"
    Public Sub InitializeEmotesText()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\EmotesText.dbc")
            Dim textEmoteID As Integer
            Dim EmoteID As Integer
            Dim EmoteID2 As Integer
            Dim EmoteID3 As Integer
            Dim EmoteID4 As Integer
            Dim EmoteID5 As Integer = 0
            Dim EmoteID6 As Integer = 0

            Dim i As Integer = 0
            For i = 0 To tmpDBC.Rows - 1
                textEmoteID = tmpDBC.Item(i, 0)
                ' Name = 1 - internal name
                EmoteID = tmpDBC.Item(i, 2)  ' AnimationID - ref to animationData
                EmoteID2 = tmpDBC.Item(i, 3) ' May be needed in the future - 3 - Flags, bitmask, may be unit_flags
                EmoteID3 = tmpDBC.Item(i, 4) ' May be needed in the future - 4 - EmoteType, Can be 0, 1 or 2 (determine how emote are shown)
                EmoteID4 = tmpDBC.Item(i, 5) ' May be needed in the future - 5 - UnitStandState, uncomfirmed, may be enum UnitStandStateType
                'SoundId = 6    ' ref to soundEntries
                'EmoteID5 = tmpDBC.Item(i, 7) ' May be needed in the future
                'EmoteID6 = tmpDBC.Item(i, 8) ' May be needed in the future

                If EmoteID <> 0 Then EmotesText(textEmoteID) = EmoteID
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} EmotesText initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : EmotesText missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "Factions"
    Public Sub InitializeFactions()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\Faction.dbc")

            Dim factionID As Integer
            Dim factionFlag As Integer
            Dim Flags(3) As Integer
            Dim ReputationStats(3) As Integer
            Dim ReputationFlags(3) As Integer
            Dim factionTeam As Integer
            Dim factionName As String

            Dim i As Integer
            For i = 0 To tmpDBC.Rows - 1
                factionID = tmpDBC.Item(i, 0)
                factionFlag = tmpDBC.Item(i, 1) ' reputationListID - reputationIndex
                Flags(0) = tmpDBC.Item(i, 2)    ' 2-5 BaseRepRaceMask[4]
                Flags(1) = tmpDBC.Item(i, 3)
                Flags(2) = tmpDBC.Item(i, 4)
                Flags(3) = tmpDBC.Item(i, 5)
                ' BaseRepClassMask[4]   ' 6-9      m_reputationClassMask
                ReputationStats(0) = tmpDBC.Item(i, 10) ' 10-13  BaseRepValue[4]
                ReputationStats(1) = tmpDBC.Item(i, 11)
                ReputationStats(2) = tmpDBC.Item(i, 12)
                ReputationStats(3) = tmpDBC.Item(i, 13)
                ReputationFlags(0) = tmpDBC.Item(i, 14) ' 14-17 ReputationFlags[4]
                ReputationFlags(1) = tmpDBC.Item(i, 15)
                ReputationFlags(2) = tmpDBC.Item(i, 16)
                ReputationFlags(3) = tmpDBC.Item(i, 17)
                factionTeam = tmpDBC.Item(i, 18) ' parentFactionID
                ' Unk1 = 19  ' Float
                ' Unk2 = 20  ' Float
                ' Unk3 = 21  ' Integer
                ' Unk4 = 22  ' Integer
                factionName = tmpDBC.Item(i, 23) ' May be needed in the future - 23-38
                ' 39 string flags
                ' description[16]   ' 40-55
                ' 56 string flags

                FactionInfo(factionID) = New WS_DBCDatabase.TFaction(factionID, factionFlag, _
                   Flags(0), Flags(1), Flags(2), Flags(3), _
                   ReputationStats(0), ReputationStats(1), ReputationStats(2), ReputationStats(3), _
                   ReputationFlags(0), ReputationFlags(1), ReputationFlags(2), ReputationFlags(3))
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} Factions initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : Factions missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

    Public Sub InitializeFactionTemplates()
        Try
            Dim i As Integer

            'Loading from DBC
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\FactionTemplate.dbc")

            Dim templateID As Integer

            For i = 0 To tmpDBC.Rows - 1
                templateID = tmpDBC.Item(i, 0)
                FactionTemplatesInfo.Add(templateID, New TFactionTemplate)
                FactionTemplatesInfo(templateID).FactionID = tmpDBC.Item(i, 1)
                ' factionFlags = 2
                FactionTemplatesInfo(templateID).ourMask = tmpDBC.Item(i, 3)
                FactionTemplatesInfo(templateID).friendMask = tmpDBC.Item(i, 4)
                FactionTemplatesInfo(templateID).enemyMask = tmpDBC.Item(i, 5)
                FactionTemplatesInfo(templateID).enemyFaction1 = tmpDBC.Item(i, 6)
                FactionTemplatesInfo(templateID).enemyFaction2 = tmpDBC.Item(i, 7)
                FactionTemplatesInfo(templateID).enemyFaction3 = tmpDBC.Item(i, 8)
                FactionTemplatesInfo(templateID).enemyFaction4 = tmpDBC.Item(i, 9)
                FactionTemplatesInfo(templateID).friendFaction1 = tmpDBC.Item(i, 10)
                FactionTemplatesInfo(templateID).friendFaction2 = tmpDBC.Item(i, 11)
                FactionTemplatesInfo(templateID).friendFaction3 = tmpDBC.Item(i, 12)
                FactionTemplatesInfo(templateID).friendFaction4 = tmpDBC.Item(i, 13)
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} FactionTemplates initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : FactionsTemplates missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

    Public Sub InitializeCharRaces()
        Try
            Dim i As Integer

            'Loading from DBC
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\ChrRaces.dbc")

            Dim raceID As Integer
            Dim factionID As Integer
            Dim modelM As Integer
            Dim modelF As Integer
            Dim teamID As Integer '1 = Horde / 7 = Alliance
            Dim cinematicID As Integer
            Dim expansion As Integer

            For i = 0 To tmpDBC.Rows - 1
                raceID = tmpDBC.Item(i, 0)
                factionID = tmpDBC.Item(i, 2)
                modelM = tmpDBC.Item(i, 4)
                modelF = tmpDBC.Item(i, 5)
                teamID = tmpDBC.Item(i, 7)
                cinematicID = tmpDBC.Item(i, 12)
                ' name[16]  ' 14-29 used for DBC language detection/selection
                expansion = tmpDBC.Item(i, 68)

                CharRaces(CByte(raceID)) = New TCharRace(CShort(factionID), modelM, modelF, CByte(teamID), cinematicID, CByte(expansion))
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} CharRaces initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : CharRaces missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

    Public Sub InitializeCharClasses()
        Try
            Dim i As Integer

            'Loading from DBC
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\ChrClasses.dbc")

            Dim classID As Integer
            Dim powerType As Integer
            Dim cinematicID As Integer
            Dim expansion As Integer

            For i = 0 To tmpDBC.Rows - 1
                classID = tmpDBC.Item(i, 0)
                powerType = tmpDBC.Item(i, 2, DBC.DBCValueType.DBC_INTEGER)
                cinematicID = tmpDBC.Item(i, 58)
                expansion = tmpDBC.Item(i, 59)

                CharClasses(CByte(classID)) = New TCharClass(powerType, cinematicID, CByte(expansion))
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} CharClasses initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : CharRaces missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "DurabilityCosts"
    Public Sub InitializeDurabilityCosts()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\DurabilityCosts.dbc")

            Dim itemBroken As Integer
            Dim itemType As Integer
            Dim itemPrice As Integer

            Dim i As Integer
            For i = 0 To tmpDBC.Rows - 1
                itemBroken = tmpDBC.Item(i, 0)

                For itemType = 1 To tmpDBC.Columns - 1
                    itemPrice = tmpDBC.Item(i, itemType)
                    DurabilityCosts(itemBroken, itemType - 1) = itemPrice
                Next itemType

            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} DurabilityCosts initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : DurabilityCosts missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "BankBagSlots"
    Public Sub InitializeBankBagSlotPrices()
        Try
            Dim tmpDBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\BankBagSlotPrices.dbc")

            Dim slot As Integer
            Dim price As Integer

            Dim i As Integer = 0
            For i = 0 To tmpDBC.Rows - 1
                slot = tmpDBC.Item(i, 0)
                price = tmpDBC.Item(i, 1)

                dbcBankBagSlotPrices(slot - 1) = price
            Next i

            tmpDBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} BankBagSlotPrices initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : BankBagSlotPrices missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

#End Region
#Region "Talents"
    Public Sub LoadTalentDBC()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\Talent.dbc")

            Dim tmpInfo As TalentInfo

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                tmpInfo = New TalentInfo

                tmpInfo.TalentID = DBC.Item(i, 0)
                tmpInfo.TalentTab = DBC.Item(i, 1)
                tmpInfo.Row = DBC.Item(i, 2)
                tmpInfo.Col = DBC.Item(i, 3)
                tmpInfo.RankID(0) = DBC.Item(i, 4)
                tmpInfo.RankID(1) = DBC.Item(i, 5)
                tmpInfo.RankID(2) = DBC.Item(i, 6)
                tmpInfo.RankID(3) = DBC.Item(i, 7)
                tmpInfo.RankID(4) = DBC.Item(i, 8)

                tmpInfo.RequiredTalent(0) = DBC.Item(i, 13) ' dependson
                tmpInfo.RequiredTalent(1) = DBC.Item(i, 14) ' ???
                tmpInfo.RequiredTalent(2) = DBC.Item(i, 15) ' ???
                tmpInfo.RequiredPoints(0) = DBC.Item(i, 16) ' dependsonrank
                tmpInfo.RequiredPoints(1) = DBC.Item(i, 17) ' ???
                tmpInfo.RequiredPoints(2) = DBC.Item(i, 18) ' ???

                Talents.Add(tmpInfo.TalentID, tmpInfo)
            Next i

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} Talents initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : Talents missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

    Public Sub LoadTalentTabDBC()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\TalentTab.dbc")

            Dim TalentTab As Integer
            Dim TalentMask As Integer
            Dim petTalentMask As Integer
            Dim TalentTabPage As Integer

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                TalentTab = DBC.Item(i, 0)
                TalentMask = DBC.Item(i, 20) ' Was 12
                petTalentMask = DBC.Item(i, 21)
                TalentTabPage = DBC.Item(i, 22) ' May be needed in the future

                TalentsTab.Add(TalentTab, TalentMask)
            Next i

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} Talent tabs initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : TalentTab missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "AuctionHouse"
    Public Sub LoadAuctionHouseDBC()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\AuctionHouse.dbc")

            Dim AHId As Integer
            Dim unk As Integer
            Dim fee As Integer
            Dim tax As Integer

            'What the hell is this doing? o_O

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                AHId = DBC.Item(i, 0)
                unk = DBC.Item(i, 1) ' Faction - id of faction.dbc for player factions associated with city
                fee = DBC.Item(i, 2) ' Deposit Percent
                tax = DBC.Item(i, 3) ' Cut Percent
                ' name[16]  ' 4-19
                ' 20 string flag, unused

                AuctionID = AHId
            Next i

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} AuctionHouses initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : AuctionHouse missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

#End Region
#Region "Regen"
    Public Const GT_MAX_LEVEL As Integer = 100
    Public Sub LoadOCTLifeRegenDBC()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\gtOCTRegenHP.dbc")

            Dim Ratio As Single

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                Ratio = DBC.Item(i, 0, Common.DBC.DBCValueType.DBC_FLOAT)
                gtOCTRegenHP.Add(Ratio)
            Next i

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} OCTLifeRegen initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : gtOCTRegenHP missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
    Public Sub LoadOCTManaRegenDBC()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\gtOCTRegenMP.dbc")

            Dim Ratio As Single

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                Ratio = DBC.Item(i, 0, Common.DBC.DBCValueType.DBC_FLOAT)
                gtOCTRegenMP.Add(Ratio)
            Next i

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} OCTManaRegen initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : gtOCTRegenMP missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
    Public Sub LoadRegenLifePerSpiritDBC()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\gtRegenHPPerSpt.dbc")

            Dim Ratio As Single

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                Ratio = DBC.Item(i, 0, Common.DBC.DBCValueType.DBC_FLOAT)
                gtRegenHPPerSpt.Add(Ratio)
            Next i

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} RegenLifePerSpirit initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : gtRegenHPPerSpt missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
    Public Sub LoadRegenManaPerSpiritDBC()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\gtRegenMPPerSpt.dbc")

            Dim Ratio As Single

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                Ratio = DBC.Item(i, 0, Common.DBC.DBCValueType.DBC_FLOAT)
                gtRegenMPPerSpt.Add(Ratio)
            Next i

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} RegenManaPerSpirit initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : gtRegenMPPerSpt missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "Items"
    Public Sub LoadItemExtendedCost()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\ItemExtendedCost.dbc")

            Dim ID As Integer
            Dim HonorPoints As Integer
            Dim ArenaPoints As Integer
            Dim ItemRequired(4) As Integer
            Dim ItemCount(4) As Integer
            Dim Unknown As Integer

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                ID = DBC.Item(i, 0)
                HonorPoints = DBC.Item(i, 1)
                ArenaPoints = DBC.Item(i, 2)
                'ArenaSlot = DBC.Item(i,3)
                For j As Byte = 0 To 4
                    ItemRequired(0) = DBC.Item(i, 4 + j)
                    ItemCount(0) = DBC.Item(i, 9 + j)
                Next
                Unknown = DBC.Item(i, 14) ' required personal arena rating

                ItemExtendedCosts.Add(ID, New TItemExtendedCost(HonorPoints, ArenaPoints, ItemRequired(0), ItemRequired(1), ItemRequired(2), ItemRequired(3), ItemRequired(4), ItemCount(0), ItemCount(1), ItemCount(2), ItemCount(3), ItemCount(4)))
            Next

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} ItemExtendedCosts initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : ItemExtendedCost missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

    Public Sub LoadGemProperties()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\GemProperties.dbc")

            Dim ID As Integer
            Dim SpellItemEnchantment As Integer
            Dim Color As Byte

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                ID = DBC.Item(i, 0)
                SpellItemEnchantment = DBC.Item(i, 1)
                Color = CByte(DBC.Item(i, 4))

                GemProperties.Add(ID, New TGemProperties(SpellItemEnchantment, Color))
            Next

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} GemProperties initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : GemProperties missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub

    Public Sub LoadGlyphProperties()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\GlyphProperties.dbc")

            Dim entry As Integer
            Dim type As Integer
            Dim GlyphEnchantment As Integer
            Dim unk As Integer

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                entry = DBC.Item(i, 0) 'Base entryID
                GlyphEnchantment = DBC.Item(i, 1) 'SpellID
                type = DBC.Item(i, 2) '0:Major 1:Minor
                unk = DBC.Item(i, 3) 'Unk flag? - GlyphIconId (SpellIcon.dbc)

                GlyphProperties.Add(entry, New TGlyphProperties(GlyphEnchantment, type))
            Next

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} GlyphProperties initialized.", i)
        Catch e As Exception
            Log.WriteLine(LogType.FAILED, e.Message)
        End Try
    End Sub

    Public Sub LoadSpellItemEnchantments()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\SpellItemEnchantment.dbc")

            Dim ID As Integer
            Dim Type(2) As Integer
            Dim Amount(2) As Integer
            Dim SpellID(2) As Integer
            Dim AuraID As Integer
            Dim Slot As Integer
            Dim GemID As Integer
            Dim EnchantmentConditions As Integer

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                ID = DBC.Item(i, 0)
                'Charges = 1
                Type(0) = DBC.Item(i, 2)
                Type(1) = DBC.Item(i, 3)
                Type(2) = DBC.Item(i, 4)
                Amount(0) = DBC.Item(i, 5)
                Amount(1) = DBC.Item(i, 6)
                Amount(2) = DBC.Item(i, 7)
                'Amount2 = 8-10
                SpellID(0) = DBC.Item(i, 11)
                SpellID(1) = DBC.Item(i, 12)
                SpellID(2) = DBC.Item(i, 13)
                'description[16] ' 14-29
                'descriptionFlags ' 30 name flags
                AuraID = DBC.Item(i, 31)
                Slot = DBC.Item(i, 32)
                GemID = DBC.Item(i, 33)
                EnchantmentConditions = DBC.Item(i, 34)
                'requiredSkill = 35
                'requiredSkillValue = 36
                'requiredLevel = 37

                SpellItemEnchantments.Add(ID, New TSpellItemEnchantment(Type, Amount, SpellID, AuraID, Slot, GemID, EnchantmentConditions))
            Next

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} SpellItemEnchantments initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : SpellItemEnchantments missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "Player Interaction"
    Public Sub LoadBarberShopStyles()
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\BarberShopStyle.dbc")

            Dim ID As Integer
            Dim Type As Integer
            Dim Name As Integer
            Dim Race As Integer
            Dim Gender As Integer
            Dim HairID As Integer

            Dim i As Integer = 0
            For i = 0 To DBC.Rows - 1
                ID = DBC.Item(i, 0)
                Type = DBC.Item(i, 1) ' value 0 -> hair, value 2 -> facialhair
                Name = DBC.Item(i, 2) ' 2-17 name of hair style
                'name_flags = 18
                'unk_name[16] ' 19-34, all empty
                'unk_flags = 35
                ' CostMultiplier = 36 ' Float - values 1 and 0.75
                Race = DBC.Item(i, 37)
                Gender = DBC.Item(i, 38) ' 0 = Male, 1 = Female
                HairID = DBC.Item(i, 39) ' real ID to hair/facial hair

                BarberShopStyles.Add(ID, New TBarberShopStyles(Name, Type, Race, Gender, HairID))
            Next

            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} BarberShopStyles initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : BarberShopStyles missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region
#Region "Achievements"
    Public Sub LoadAchievement()
        'uint32    ID;                                           // 0
        'uint32    factionFlag;                                  // 1 -1=all, 0=horde, 1=alliance
        'uint32    mapID;                                        // 2 -1=none
        'uint32    categoryId;                                   // 38
        'uint32    points;                                       // 39 reward points
        'uint32    flags;                                        // 41
        Try
            Dim DBC As DBC.BufferedDBC = New DBC.BufferedDBC("dbc\Achievement.dbc")
            Dim ID As Integer
            Dim factionFlag As Integer
            Dim mapID As Integer
            Dim categoryID As Integer
            Dim points As Integer
            Dim flags As Integer
            Dim i As Integer = 0

            For i = 0 To DBC.Rows - 1
                ID = DBC.Item(i, 0)
                factionFlag = DBC.Item(i, 1) ' -1 = all, 0 = horde, 1 = alliance
                mapID = DBC.Item(i, 2)       ' -1 = none
                'parentAchievement = 3  ' its Achievement parent (can`t start while parent uncomplete, use its Criteria if don`t have own, use its progress on begin)
                '*name[16]  ' 4-19
                'name_flags = 20
                '*description[16] ' 21-36
                'desc_flags = 37
                categoryID = DBC.Item(i, 38)
                points = DBC.Item(i, 39)
                'OrderInCategory = 40
                flags = DBC.Item(i, 41)
                'icon = 42 ' icon (from SpellIcon.dbc)
                '*titleReward[16] ' 43-58
                'titleReward_flags = 59
                'count = 60 ' need this count of completed criterias (own or referenced achievement criterias)
                'refAchievement = 61 ' referenced achievement (counting of all completed criterias)
                Achievement.Add(ID, New TAchievement(factionFlag, mapID, categoryID, points, flags))
            Next
            DBC.Dispose()
            Log.WriteLine(LogType.INFORMATION, "DBC: {0} Achievement initialized.", i)
        Catch e As System.IO.DirectoryNotFoundException
            Console.ForegroundColor = System.ConsoleColor.DarkRed
            Console.WriteLine("DBC File : Achievement missing.")
            Console.ForegroundColor = System.ConsoleColor.Gray
        End Try
    End Sub
#End Region

End Module
