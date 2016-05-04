@echo off
rem #################################################################################################
rem �R�[�h�J�o���b�W�m�F�p�o�b�`
rem
rem �O�����
rem �EVisual Studio�Ńe�X�g�����s���Ă��邱��(�{�o�b�`�ł̓R���p�C���͍s���Ă��Ȃ�����)
rem �ENUnit 2.6.4��ʓr�C���X�g�[�����Ă��邱��(NuGet�ł�dll�ŁA�o�b�`����̎��s�ɂ�NUnit��EXE���K�v)
rem   http://www.nunit.org/index.php?p=download
rem �EOpenCover���g�p���邽�߁A.Net Framework3.5(.NET 2.0�����3.0���܂�)���C���X�g�[�����Ă��邱��
rem   (Windows10���ł͏�����ԂŃC���X�g�[������Ă��Ȃ����ߕʓr�C���X�g�[�����K�v)
rem
rem �g����
rem �E�e�X�g�v���W�F�N�g�̒l��ύX���A�o�b�`�����s����
rem #################################################################################################

rem �e�X�g�v���W�F�N�g��
set project_name=TestUtil.Tests

rem NUnit�̃C���X�g�[����
rem set nunit_home=C:\Program Files\NUnit 2.6.4
set nunit_home=D:\NUnit-2.6.4

rem �\�����[�V�����t�H���_
set solution_dir=..\

rem OpenCover�̃C���X�g�[����
set opencover_home=%solution_dir%\packages\OpenCover.4.6.519\tools

rem ReportGenerator�̃C���X�g�[����
set reportgen_dir=%solution_dir%\packages\ReportGenerator.2.4.4.0\tools

rem �p�X�̐ݒ�
set path=%path%;%opencover_home%;%reportgen_dir%\

rem ���s����e�X�g�̃A�Z���u��
set target_test=%project_name%.dll

rem ���s����e�X�g�̃A�Z���u���̊i�[��
set target_dir=%solution_dir%\%project_name%\bin\Debug

rem �J�o���b�W�v���Ώۂ̎w��
set filters=+[*]*

rem OpenCover�̎��s
OpenCover.Console -register:user -target:"%nunit_home%\bin\nunit-console.exe" -targetargs:"/nologo %target_test%"  -targetdir:"%target_dir%" -output:result.xml -mergebyhash  -filter:"%filters%"

rem ���|�[�g�̐���
ReportGenerator "result.xml" html
pause