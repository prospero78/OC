echo off
chcp 65001
cls

echo 1. Компиляция модКонстанты.dll
vbc source\модКонстанты.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /t:library /out:"bin\модКонстанты.dll" /optimize+

echo 2. Компиляция modCocon.dll
vbc source\modCocon.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /t:library /out:"bin\modCocon.dll" /optimize+

echo 3. Компиляция modHelp.dll
vbc source\modHelp.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc  /platform:x86 /utf8output+ /r:"bin\modCocon.dll" /t:library /out:"bin\modHelp.dll" /optimize+

echo 4. Компиляция modArg.dll
vbc source\modArg.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modHelp.dll" /r:"bin\modCocon.dll" /t:library /out:"bin\modArg.dll" /optimize+

echo 5. Компиляция modFile.dll
vbc source\modFile.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modArg.dll" /r:"bin\modCocon.dll" /t:library /out:"bin\modFile.dll" /optimize+

echo 6. Компиляция модУтиль.dll
vbc source\модУтиль.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+  /r:"bin\modCocon.dll" /t:library /out:"bin\модУтиль.dll" /optimize+

echo 7. Компиляция modTagger.dll
vbc source\modTagger.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modCocon.dll" /r:"bin\модКонстанты.dll" /r:"bin\модУтиль.dll" /r:"bin\modFile.dll" /out:"bin\modTagger.dll" /t:library /optimize+

echo 8. Компиляция modScaner.dll
vbc source\modScaner.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modArg.dll" /r:"bin\modCocon.dll" /r:"bin\modFile.dll" /r:"bin\модКонстанты.dll"  /t:library /out:"bin\modScaner.dll" /optimize+

echo 9. Компиляция modType.dll
vbc source\modType.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modTagger.dll" /r:"bin\модУтиль.dll" /r:"bin\modCocon.dll" /r:"bin\modFile.dll" /r:"bin\модКонстанты.dll" /t:library /out:"bin\modType.dll" /optimize+

echo 10. Компиляция modLexer.dll
vbc source\modLexer.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+ /r:"bin\modTagger.dll" /r:"bin\modCocon.dll" /r:"bin\modUtil.dll" /r:"bin\modType.dll" /t:library /out:"bin\modLexer.dll" /optimize+

echo 11. Компиляция modCompiler.dll
vbc source\modCompiler.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+  /r:"bin\modTagger.dll" /r:"bin\modLexer.dll" /t:library /out:"bin\modCompiler.dll" /optimize+

echo +++ 12. Компиляция oc.exe +++
vbc source\oc.vb /nologo /debug+ /optionexplicit+ /optioninfer+ /optionstrict+ /rootnamespace:nsOc /platform:x86 /utf8output+  /r:"bin\modCocon.dll" /r:"bin\modArg.dll" /r:"bin\modFile.dll" /r:"bin\modScaner.dll" /r:"bin\modCompiler.dll" /t:exe /out:"bin\oc.exe" /optimize+