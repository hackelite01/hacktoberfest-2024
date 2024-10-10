@echo off

REM Displaying a personalized welcome message to the user
echo.
echo   Welcome to Wi-Fi Password Retrieval Tool!
echo   ========================================
echo   We're glad to have you here. Sit back, relax,
echo   and let's retrieve those Wi-Fi passwords!
echo.

setlocal enabledelayedexpansion

REM Prompt the user to choose the retrieval option
echo.
echo   Do you want to retrieve all saved Wi-Fi passwords or a specific one?
echo   1. All saved Wi-Fi passwords
echo   2. Retrieve a specific Wi-Fi password
set /p choice="Enter your choice (1 or 2): "

REM Check user input and proceed accordingly
if "%choice%"=="1" (
    REM Call the subroutine to retrieve all saved Wi-Fi passwords
    call :retrieveAllPasswords
) else if "%choice%"=="2" (
    REM Call the subroutine to retrieve a specific Wi-Fi password
    call :retrieveSpecificPassword
) else (
    REM Invalid input, display an error message and exit
    echo Invalid choice! Please enter 1 or 2.
    pause
    exit /b
)

REM Display footer for the table
echo.

REM Display your LinkedIn information
echo Thank you for using Wi-Fi Password Retrieval Tool.
echo If you have any questions or suggestions, feel free to contact:
echo - Zeeshan Mukhtar
echo - Connect with me on LinkedIn: https://www.linkedin.com/in/zeeshanmukhtar1/

REM Pause at the end of the script
pause

REM End of script
exit /b

:retrieveAllPasswords
REM Display message indicating the start of password retrieval process
echo Searching for saved Wi-Fi passwords. Please wait...

REM Display header for the table
echo Wi-Fi Name                  Detected Password
echo ---------------------------------------------

REM Loop through each saved Wi-Fi profile
for /f "tokens=2 delims=:" %%a in ('netsh wlan show profile') do (
    REM Remove quotation marks from the Wi-Fi profile name
    set "ssid=%%~a"
    REM Call the subroutine to retrieve the password
    call :getpwd "%%ssid:~1%%"
)

REM Display success message if passwords are found, else display error message
if %passwordsFound% equ 1 (
    echo.
    echo Successfully fetched all Wi-Fi passwords.
) else (
    echo.
    echo No Wi-Fi passwords found. Please connect to a Wi-Fi network first.
)

REM Return from the subroutine
exit /b

:retrieveSpecificPassword
REM Prompt the user to enter the Wi-Fi profile name
set /p profileName="Enter the name of the Wi-Fi profile: "

REM Display message indicating the start of password retrieval process
echo Searching for the Wi-Fi password for "%profileName%". Please wait...

REM Call the subroutine to retrieve the password for the specified Wi-Fi profile
call :getpwd "%profileName%"

REM Display success message if password is found, else display error message
if %passwordsFound% equ 1 (
    echo.
    echo Successfully fetched the Wi-Fi password for "%profileName%".
) else (
    echo.
    echo No Wi-Fi password found for "%profileName%". Please check the profile name and try again.
)

REM Return from the subroutine
exit /b

:getpwd
set "ssid=%*"

REM Retrieve password for the specified Wi-Fi profile
for /f "tokens=2 delims=:" %%i in ('netsh wlan show profile name^="%ssid%" key^=clear ^| findstr /C:"Key Content"') do (
    REM Print Wi-Fi name and password in customized format
    echo !ssid!                        %%i
    REM Set flag indicating that at least one password is found
    set "passwordsFound=1"
)

REM If no password is found, set flag indicating that no password is found
if not defined passwordsFound (
    set "passwordsFound=0"
)

REM Return from subroutine
exit /b