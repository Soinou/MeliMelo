#define MyAppName "MeliMelo.Animes"
#define MyProjectName "MeliMelo.Animes"
#define MyAppVersion GetFileVersion(MyProjectName)
#define MyAppPublisher "Abricot Soinou"
#define MyAppURL "https://github.com/Soinou/MeliMelo"
#define MyAppExeName "MeliMelo.Animes.exe"

[Setup]
AppId={{a5af7fba-5de2-44de-b7ae-d4421cd6dcef}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
CloseApplications=yes
DefaultDirName={userappdata}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=..\LICENSE.md
OutputDir=..\Releases\
OutputBaseFilename={#MyAppName}.Setup
Compression=lzma2/ultra64
SetupIconFile=..\..\{#MyProjectName}\Icon.ico
SolidCompression=yes
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\..\{#MyProjectName}\build\Release\x86\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\{#MyProjectName}\build\Release\x86\*.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commonstartup}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Flags: nowait runhidden

[UninstallRun]
Filename: "{sys}\taskkill.exe"; Parameters: "/f /im {#MyAppExeName}"; Flags: runhidden
