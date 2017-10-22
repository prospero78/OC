chcp 65001
cls

vbc /t:library /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ modConst.vb

vbc /t:library /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ modCocon.vb

vbc /t:library /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ /r:modCocon.dll modHelp.vb

vbc /t:library /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+  /r:modHelp.dll /r:modCocon.dll modArg.vb

vbc /t:library /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ /r:modArg.dll /r:modCocon.dll modFile.vb

vbc /t:library /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ /r:modArg.dll /r:modCocon.dll /r:modFile.dll /r:modConst.dll modScaner.vb

vbc /t:library /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ /r:modFile.dll modLexer.vb

vbc /t:library /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ /r:modLexer.dll /r:modCocon.dll modCompiler.vb

vbc /t:exe /debug+ /debug:pdbonly /optionexplicit+ /optioninfer+ /rootnamespace:nsOc /optionstrict+ /nologo /platform:x86 /utf8output+ /r:modCocon.dll /r:modArg.dll /r:modFile.dll /r:modScaner.dll /r:modCompiler.dll oc.vb