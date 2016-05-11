StyleCopPlus.MSBuild.4.7.49.5
StyleCop.4.7.49

★プロジェクトのテンプレート「NUnit2.zip」

■自動化テスト(http://codezine.jp/article/detail/6518/)
１．NUnit-2.6.4とNUnitTestAdapter2.0.0をインストールする。
PM> Set-ExecutionPolicy RemoteSigned -Scope CurrentUser -Confirm

PM>Install-Package NUnit -Version 2.6.4
PM>Install-Package NUnitTestAdapter -Version 2.0.0


２．テスト用に、新規クラスライブラリプロジェクトを「テスト対象プロジェクト.Tests」で作成する。
例）「SampleAppli」プロジェクトだった場合は「SampleAppli.Tests」とする。
３．テスト対象クラス用に「テスト対象クラスTest」クラスを作成し、
　　「using NUnit.Framework;」を付ける。
例）「MyClass」クラスだった場合は「MyClassTest」とする。
４．テストプロジェクトの参照に対象プロジェクトを追加する。
５．テストメソッドは「public void」で作成し、「[Test]または[TestCase(input, expected)]」属性を付ける。
　　案）名前は「テスト対象メソッド名＋<OK or NG>(※1) + <任意の文字列>」とする。
　　　　※正常系のテストはOK、異常系のテストはNGとする
６．テストコードは以下の内容を記載する。「AAAパターン(Arrange, Act, Assert)」
　　6-1.テストメソッドの実行に必要な準備
　　6-2.テスト対象メソッドの実行と結果の取得
　　6-3.テスト対象メソッドの結果を評価
７．アサーションはConstraint-Based Assert Model」を使用する。
８．実測値は「actual」という名前とし、期待値は「expected」という名前とし
　　Assert.That(actual, Is.EqualTo(expected));
　　Assert.That(actual == expected);

•TestFixtureSetUp属性
 この属性が付けられたメソッドは、そのテストクラスでのテストを実行する前に、1度だけ実行される。
•SetUp属性
 この属性が付けられたメソッドは、そのテストクラス中のテストメソッドを実行する前に、毎回実行される。
•TearDown属性
 この属性が付けられたメソッドは、そのテストクラス中のテストメソッドを実行しおわった後に、毎回実行される。
•TestFixtureTearDown属性
 この属性が付けられたメソッドは、そのテストクラスでのテストをすべて実行しおわった後に、1度だけ実行される。

■ブランチカバレッジ
ReportGenerator2.3.4、OpenCover4.6.166をインストール

rem NUnitのインストール先
set nunit_home=C:\Program Files (x86)\NUnit 2.6.4

rem プロジェクトフォルダ
set project_home=D:\git\VS\ClassLibraryTest

rem OpenCoverのインストール先
set opencover_home=%project_home%\packages\OpenCover.4.6.166\tools

rem ReportGeneratorのインストール先
set reportgen_dir=%project_home%\packages\ReportGenerator.2.3.4.0\tools

rem パスの設定
set path=%path%;%opencover_home%;%reportgen_dir%\

rem 実行するテストのアセンブリ
set target_test=ClassLibraryTest.dll

rem 実行するテストのアセンブリの格納先
set target_dir=%project_home%\ClassLibraryTest\bin\Debug

rem カバレッジ計測対象の指定
set filters=+[ClassLibrary1*]*


rem OpenCoverの実行
OpenCover.Console -register:user -target:"%nunit_home%\bin\nunit-console.exe" -targetargs:"/nologo %target_test%"  -targetdir:"%target_dir%" -output:result.xml -mergebyhash  -filter:"%filters%"

rem レポートの生成
ReportGenerator "result.xml" html
pause

■privateメソッドのテスト方法
http://nakaji.hatenablog.com/entry/2013/04/04/232625
http://www.pine4.net/Memo/Article/Archives/294
PrivateObjectを使う
１．インスタンスのprivateメソッド
　RemoteConnectManager manager = new RemoteConnectManager();
　var po = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(manager);
　
　RemoteConnection conn = new RemoteConnection("a", "b", "c", "d");
　po.Invoke("Write",conn, filePath);
２．クラスのprivateメソッド
　RemoteConnectManager manager = new RemoteConnectManager();
　var po = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(RemoteConnectManager));

　RemoteConnection conn = new RemoteConnection("a", "b", "c", "d");
　po.InvokeStatic("Write",conn, filePath);
　
■例外の確認
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                FileDeleteUtil.DeleteFile();
            });

            // ロックしているので削除できないため存在する
            Assert.IsTrue(File.Exists("test1.txt"));
            Assert.That(ex.Message, Is.EqualTo("test1.txtが削除できませんでした。"));

■テストを一時的に無効に
ignore
Explicit

■internalクラスのテスト
テスト対象クラスのProertiesにあるAssemblyInfo.csに追加する。
InternalsVisibleToAttribute
[assembly: InternalsVisibleTo("テストモジュール名(拡張子を除いたファイル名？")]
https://msdn.microsoft.com/library/system.runtime.compilerservices.internalsvisibletoattribute(v=vs.110).aspx

■Fake
https://msdn.microsoft.com/ja-jp/library/hh549175.aspx

■画面がある場合のテスト
メソッドに[Test, STAThread]を付ける

