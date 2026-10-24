@echo off
setlocal
cd /d "%~dp0"

where git >nul 2>&1
if errorlevel 1 (
    echo No se encontro Git. Instala Git desde https://git-scm.com/download/win
    pause
    exit /b 1
)

git config --global user.name >nul 2>&1
if errorlevel 1 (
    echo Configura tu nombre y correo de Git antes de continuar:
    echo   git config --global user.name "Tu Nombre"
    echo   git config --global user.email "tu-correo@ejemplo.com"
    pause
    exit /b 1
)

if not exist ".git" (
    git init
    git branch -M main
)

git add -A
git diff --cached --quiet
if errorlevel 1 (
    git commit -m "Carga del proyecto mbunayd4"
    if errorlevel 1 (
        echo No se pudo crear el commit.
        pause
        exit /b 1
    )
)

git remote get-url origin >nul 2>&1
if not errorlevel 1 goto push

where gh >nul 2>&1
if errorlevel 1 goto manual

gh auth status >nul 2>&1
if errorlevel 1 (
    echo Inicia sesion en GitHub.
    gh auth login
    if errorlevel 1 (
        echo No se pudo iniciar sesion.
        pause
        exit /b 1
    )
)

set REPO=mbunayd4
set /p REPO=Nombre del repositorio en GitHub [%REPO%]:
if "%REPO%"=="" set REPO=mbunayd4

set VIS=publico
set /p VIS=Repositorio publico o privado [%VIS%]:
if /i "%VIS%"=="privado" (
    gh repo create "%REPO%" --private --source=. --remote=origin --push
) else (
    gh repo create "%REPO%" --public --source=. --remote=origin --push
)
if errorlevel 1 (
    echo No se pudo crear el repositorio. Si ya existe, pega su URL.
    goto manual
)
goto fin

:manual
set /p URL=Pega la URL del repositorio vacio de GitHub:
if "%URL%"=="" (
    echo No se indico una URL.
    pause
    exit /b 1
)
git remote remove origin >nul 2>&1
git remote add origin "%URL%"

:push
git branch -M main
git push -u origin main
if errorlevel 1 (
    echo No se pudo subir el proyecto.
    pause
    exit /b 1
)

:fin
echo Proyecto cargado en GitHub.
pause
endlocal
