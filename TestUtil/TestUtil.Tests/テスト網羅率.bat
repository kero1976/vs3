@echo off
rem #################################################################################################
rem コードカバレッジ確認用バッチ
rem
rem 前提条件
rem ・Visual Studioでテストを実行していること(本バッチではコンパイルは行っていないため)
rem ・NUnit 2.6.4を別途インストールしていること(NuGetではdllで、バッチからの実行にはNUnitのEXEが必要)
rem   http://www.nunit.org/index.php?p=download
rem ・OpenCoverを使用するため、.Net Framework3.5(.NET 2.0および3.0を含む)をインストールしていること
rem   (Windows10環境では初期状態でインストールされていないため別途インストールが必要)
rem
rem 使い方
rem ・テストプロジェクトの値を変更し、バッチを実行する
rem #################################################################################################

rem テストプロジェクト名
set project_name=TestUtil.Tests

rem NUnitのインストール先
rem set nunit_home=C:\Program Files\NUnit 2.6.4
set nunit_home=D:\NUnit-2.6.4

rem ソリューションフォルダ
set solution_dir=..\

rem OpenCoverのインストール先
set opencover_home=%solution_dir%\packages\OpenCover.4.6.519\tools

rem ReportGeneratorのインストール先
set reportgen_dir=%solution_dir%\packages\ReportGenerator.2.4.4.0\tools

rem パスの設定
set path=%path%;%opencover_home%;%reportgen_dir%\

rem 実行するテストのアセンブリ
set target_test=%project_name%.dll

rem 実行するテストのアセンブリの格納先
set target_dir=%solution_dir%\%project_name%\bin\Debug

rem カバレッジ計測対象の指定
set filters=+[*]*

rem OpenCoverの実行
OpenCover.Console -register:user -target:"%nunit_home%\bin\nunit-console.exe" -targetargs:"/nologo %target_test%"  -targetdir:"%target_dir%" -output:result.xml -mergebyhash  -filter:"%filters%"

rem レポートの生成
ReportGenerator "result.xml" html
pause