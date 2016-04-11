#define MyAppName "MeliMelo.MangaReader"
#define MyProjectName "MeliMelo.MangaReader"
#define MyAppVersion GetFileVersion(MyProjectName)
#define MyAppPublisher "Abricot Soinou"
#define MyAppURL "https://github.com/Soinou/MeliMelo"
#define MyAppExeName "MeliMelo.MangaReader.exe"

[Setup]
AppId={{05874d14-6fee-4418-8d32-544647d3c9f6}
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
