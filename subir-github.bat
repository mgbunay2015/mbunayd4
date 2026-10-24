@echo off
cd /d "%~dp0"

where git >nul 2>&1
if errorlevel 1 (
    echo No se encontro Git. Instala Git desde https://git-scm.com/download/win
    pause
    exit /b 1
)

where gh >nul 2>&1
if errorlevel 1 (
    echo No se encontro GitHub CLI. Instala gh desde https://cli.github.com
    pause
    exit /b 1
)

if not exist ".git" (
    git init
    git branch -M main
)

git add -A
git diff --cached --quiet
if errorlevel 1 git commit -m "Carga del proyecto mbunayd4"

git remote get-url origin >nul 2>&1
if not errorlevel 1 goto push

gh auth status >nul 2>&1
if not errorlevel 1 goto crear

echo.
echo La sesion de Visual Studio no autoriza este programa.
echo Se abrira el navegador: acepta el permiso de GitHub CLI.
echo.
gh auth login --hostname github.com --git-protocol https --web
if errorlevel 1 (
    echo No se completo el inicio de sesion de GitHub CLI.
    pause
    exit /b 1
)

:crear
for /f "delims=" %%i in ('gh api user -q .login') do set GHUSER=%%i
if "%GHUSER%"=="" (
    echo No se pudo leer el usuario de GitHub.
    pause
    exit /b 1
)

gh repo view %GHUSER%/mbunayd4 >nul 2>&1
if not errorlevel 1 (
    git remote remove origin >nul 2>&1
    git remote add origin https://github.com/%GHUSER%/mbunayd4.git
    goto push
)

gh repo create mbunayd4 --public --source=. --remote=origin --push
if not errorlevel 1 goto fin

echo No se pudo crear el repositorio.
pause
exit /b 1

:push
git branch -M main
git push -u origin main
if errorlevel 1 (
    echo No se pudo subir el proyecto.
    pause
    exit /b 1
)

:fin
echo.
echo Proyecto cargado: https://github.com/%GHUSER%/mbunayd4
echo.
pause
