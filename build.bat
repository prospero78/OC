echo off
chcp 65001
cls

echo 1. Compile modConst.dll
vbc source\modConst.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /t:library /out:"modConst.dll" /optimize+

echo 2. Compile modCocon.dll
vbc source\modCocon.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /t:library /out:"modCocon.dll" /optimize+

echo 3. Compile modHelp.dll
vbc source\modHelp.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc  /platform:x86 /utf8output+ /r:"modCocon.dll" /t:library /out:"modHelp.dll" /optimize+

echo 4. Compile modArg.dll
vbc source\modArg.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"modHelp.dll" /r:"modCocon.dll" /t:library /out:"modArg.dll" /optimize+

echo 5. Compile modFile.dll
vbc source\modFile.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"modArg.dll" /r:"modCocon.dll" /t:library /out:"modFile.dll" /optimize+

echo 6. Compile modScaner.dll
vbc source\modScaner.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"modArg.dll" /r:"modCocon.dll" /r:"modFile.dll" /r:"modConst.dll" /t:library /out:"modScaner.dll" /optimize+

echo 7. Compile modLexer.dll
vbc source\modLexer.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"modFile.dll" /out:"modLexer.dll" /t:library /optimize+

echo 8. Compile modCompiler.dll
vbc source\modCompiler.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"modLexer.dll" /r:"modCocon.dll" /t:library /out:"modCompiler.dll" /optimize+

echo +++ 9. Compile oc.exe +++
vbc source\oc.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"modCocon.dll" /r:"modArg.dll" /r:"modFile.dll" /r:"modScaner.dll" /r:"modCompiler.dll" /t:exe /out:oc.exe /optimize+