@echo off

:: open Unity Editor with the project in this path
IF EXIST "C:\Program Files (x86)\Unity\Editor\Unity.exe" (
	start "" "C:\Program Files (x86)\Unity\Editor\Unity.exe" -projectPath %~dp0
)
IF EXIST "D:\Development\Unity\Editor\Unity.exe" (
	start D:\Development\Unity\Editor\Unity.exe -projectPath %~dp0
)

exit