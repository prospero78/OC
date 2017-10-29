echo off
chcp 65001
cls

echo 1. Compile modConst.dll
vbc source\modConst.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /t:library /out:"bin\modConst.dll" /optimize+

echo 2. Compile modCocon.dll
vbc source\modCocon.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /t:library /out:"bin\modCocon.dll" /optimize+

echo 3. Compile modHelp.dll
vbc source\modHelp.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc  /platform:x86 /utf8output+ /r:"bin\modCocon.dll" /t:library /out:"bin\modHelp.dll" /optimize+

echo 4. Compile modArg.dll
vbc source\modArg.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modHelp.dll" /r:"bin\modCocon.dll" /t:library /out:"bin\modArg.dll" /optimize+

echo 5. Compile modFile.dll
vbc source\modFile.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modArg.dll" /r:"bin\modCocon.dll" /t:library /out:"bin\modFile.dll" /optimize+

echo 6. Compile modUtil.dll
vbc source\modUtil.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+  /t:library /out:"bin\modUtil.dll" /optimize+

echo 7. Compile modTagger.dll
vbc source\modTagger.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modCocon.dll" /r:"bin\modConst.dll" /r:"bin\modUtil.dll" /r:"bin\modFile.dll" /out:"bin\modTagger.dll" /t:library /optimize+

echo 8. Compile modScaner.dll
vbc source\modScaner.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modArg.dll" /r:"bin\modCocon.dll" /r:"bin\modFile.dll" /r:"bin\modConst.dll"  /t:library /out:"bin\modScaner.dll" /optimize+

echo 9. Compile modLexer.dll
vbc source\modLexer.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+  /r:"bin\modTagger.dll" /r:"bin\modUtil.dll" /r:"bin\modCocon.dll" /t:library /out:"bin\modLexer.dll" /optimize+

echo 10. Compile modCompiler.dll
vbc source\modCompiler.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+  /r:"bin\modTagger.dll" /r:"bin\modLexer.dll" /t:library /out:"bin\modCompiler.dll" /optimize+

echo +++ 11. Compile oc.exe +++
vbc source\oc.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+  /r:"bin\modCocon.dll" /r:"bin\modArg.dll" /r:"bin\modFile.dll" /r:"bin\modScaner.dll" /r:"bin\modCompiler.dll" /t:exe /out:"bin\oc.exe" /optimize+