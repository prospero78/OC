echo off
chcp 65001
cls

echo 1. Compile ORS.vb
vbc /nologo /t:library ORS.vb

echo 2. Compile ORB.vb
vbc /nologo /t:library /r:ORS.dll  ORB.vb

echo 3. Compile ORG.vb
vbc /nologo /t:library /r:ORS.dll  /r:ORB.dll ORG.vb

echo 4. Compile ORP.vb
vbc /nologo /r:ORB.dll /r:ORG.dll  ORP.vb
