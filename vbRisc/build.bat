echo off
chcp 65001
cls

vbc /nologo /t:library ORS.vb

vbc /nologo /t:library /r:ORS.dll ORB.vb

vbc /nologo ORP.vb
