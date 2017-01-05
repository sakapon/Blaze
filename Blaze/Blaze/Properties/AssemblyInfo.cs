using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("Blaze")]
[assembly: AssemblyDescription("The library for basic mathematics.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Keiho Sakapon")]
[assembly: AssemblyProduct("Blaze")]
[assembly: AssemblyCopyright("© 2016 Keiho Sakapon")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyMetadata("ProjectUrl", "https://github.com/sakapon/Blaze")]
[assembly: AssemblyMetadata("LicenseUrl", "https://github.com/sakapon/Blaze/blob/master/LICENSE")]
[assembly: AssemblyMetadata("Tags", "math mathematics random proposition")]
[assembly: AssemblyMetadata("ReleaseNotes", "Add features for propositional logic.")]

// ComVisible を false に設定すると、その型はこのアセンブリ内で COM コンポーネントから 
// 参照不可能になります。COM からこのアセンブリ内の型にアクセスする場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります
[assembly: Guid("e44e9675-bf4f-4018-a53a-9af336dc9a8a")]

// アセンブリのバージョン情報は次の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      Revision
//
// すべての値を指定するか、下のように '*' を使ってビルドおよびリビジョン番号を 
// 既定値にすることができます:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.9.0")]
[assembly: AssemblyFileVersion("1.1.9")]

[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("UnitTest")]
